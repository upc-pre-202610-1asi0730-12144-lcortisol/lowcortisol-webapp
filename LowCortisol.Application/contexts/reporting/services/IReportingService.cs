using LowCortisol.Application.Contexts.Reporting.Commands;
using LowCortisol.Application.Contexts.Reporting.DTOs;

namespace LowCortisol.Application.Contexts.Reporting.Services;

public interface IReportingService
{
    Task<(bool Success, string Error)> GenerateAsync(
        GenerateConsumptionReportCommand command,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyCollection<ConsumptionReportDto>> GetByOwnerAsync(
        Guid ownerUserId,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyCollection<ConsumptionReportDto>> GetByOwnerAndPeriodAsync(
        Guid ownerUserId,
        string period,
        CancellationToken cancellationToken = default);
}