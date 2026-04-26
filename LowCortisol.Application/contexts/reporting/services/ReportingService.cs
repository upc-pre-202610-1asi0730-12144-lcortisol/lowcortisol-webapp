using LowCortisol.Application.Common.Interfaces;
using LowCortisol.Application.Contexts.Reporting.Commands;
using LowCortisol.Application.Contexts.Reporting.DTOs;
using LowCortisol.Domain.Contexts.Reporting.Aggregates;
using LowCortisol.Domain.Contexts.Reporting.Entities;
using LowCortisol.Domain.Contexts.Reporting.Repositories;
using LowCortisol.Domain.Contexts.Reporting.ValueObjects;

namespace LowCortisol.Application.Contexts.Reporting.Services;

public class ReportingService : IReportingService
{
    private readonly IConsumptionReportRepository _consumptionReportRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ReportingService(
        IConsumptionReportRepository consumptionReportRepository,
        IUnitOfWork unitOfWork)
    {
        _consumptionReportRepository = consumptionReportRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<(bool Success, string Error)> GenerateAsync(
        GenerateConsumptionReportCommand command,
        CancellationToken cancellationToken = default)
    {
        try
        {
            if (command.OwnerUserId == Guid.Empty)
                return (false, "Owner user id is required.");

            var report = new ConsumptionReport(
                command.OwnerUserId,
                new ReportPeriod(command.Period),
                new ConsumptionValue(command.TotalWaterConsumption, command.TotalWaterUnit),
                new ConsumptionValue(command.TotalGasConsumption, command.TotalGasUnit)
            );

            await _consumptionReportRepository.AddAsync(report, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return (true, string.Empty);
        }
        catch (Exception ex)
        {
            return (false, ex.Message);
        }
    }

    public async Task<IReadOnlyCollection<ConsumptionReportDto>> GetByOwnerAsync(
        Guid ownerUserId,
        CancellationToken cancellationToken = default)
    {
        var reports = await _consumptionReportRepository.FindByOwnerUserIdAsync(ownerUserId, cancellationToken);
        return reports.Select(MapToDto).ToList();
    }

    public async Task<IReadOnlyCollection<ConsumptionReportDto>> GetByOwnerAndPeriodAsync(
        Guid ownerUserId,
        string period,
        CancellationToken cancellationToken = default)
    {
        var reports = await _consumptionReportRepository.FindByOwnerUserIdAndPeriodAsync(
            ownerUserId,
            new ReportPeriod(period),
            cancellationToken);

        return reports.Select(MapToDto).ToList();
    }

    private static ConsumptionReportDto MapToDto(ConsumptionReport report)
    {
        return new ConsumptionReportDto
        {
            Id = report.Id,
            OwnerUserId = report.OwnerUserId,
            Period = report.Period.Value,
            TotalWaterConsumption = report.TotalWaterConsumption.Value,
            TotalWaterUnit = report.TotalWaterConsumption.Unit,
            TotalGasConsumption = report.TotalGasConsumption.Value,
            TotalGasUnit = report.TotalGasConsumption.Unit,
            GeneratedAtUtc = report.GeneratedAtUtc,
            Lines = report.Lines.Select(MapLineToDto).ToList()
        };
    }

    private static ReportLineDto MapLineToDto(ReportLine line)
    {
        return new ReportLineDto
        {
            Id = line.Id,
            Label = line.Label,
            WaterConsumption = line.WaterConsumption.Value,
            WaterUnit = line.WaterConsumption.Unit,
            GasConsumption = line.GasConsumption.Value,
            GasUnit = line.GasConsumption.Unit
        };
    }
}