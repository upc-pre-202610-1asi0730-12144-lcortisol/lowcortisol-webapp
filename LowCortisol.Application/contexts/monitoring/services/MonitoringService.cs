using LowCortisol.Application.Common.Interfaces;
using LowCortisol.Application.Contexts.Monitoring.Commands;
using LowCortisol.Application.Contexts.Monitoring.DTOs;
using LowCortisol.Domain.Contexts.Monitoring.Entities;
using LowCortisol.Domain.Contexts.Monitoring.Repositories;
using LowCortisol.Domain.Contexts.Monitoring.ValueObjects;
using LowCortisol.Application.Contexts.Monitoring.Contracts;

namespace LowCortisol.Application.Contexts.Monitoring.Services;

public class MonitoringService : IMonitoringService
{
    private readonly IMonitoringRepository _monitoringRepository;
    private readonly IThresholdEvaluator _thresholdEvaluator;
    private readonly IMonitoringAlertFactory _monitoringAlertFactory;
    private readonly IUnitOfWork _unitOfWork;

    public MonitoringService(
        IMonitoringRepository monitoringRepository,
        IThresholdEvaluator thresholdEvaluator,
        IMonitoringAlertFactory monitoringAlertFactory,
        IUnitOfWork unitOfWork)
    {
        _monitoringRepository = monitoringRepository;
        _thresholdEvaluator = thresholdEvaluator;
        _monitoringAlertFactory = monitoringAlertFactory;
        _unitOfWork = unitOfWork;
    }

    public async Task<(bool Success, string Error)> RecordReadingAsync(
        RecordSensorReadingCommand command,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var monitoringSession = await _monitoringRepository.FindByIdAsync(command.MonitoringSessionId, cancellationToken);
            if (monitoringSession is null)
                return (false, "Monitoring session not found.");

            var reading = new SensorReading(
                command.DeviceId,
                command.SensorId,
                new SensorType(command.SensorType),
                new ReadingValue(command.Value),
                new MeasurementUnit(command.Unit)
            );

            monitoringSession.RecordReading(reading);

            var evaluation = _thresholdEvaluator.Evaluate(reading, 100m);
            if (evaluation.IsExceeded)
            {
                var alertMessage = _monitoringAlertFactory.CreateAlertMessage(reading);
                monitoringSession.RegisterAnomaly(alertMessage);
            }

            await _monitoringRepository.UpdateAsync(monitoringSession, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return (true, string.Empty);
        }
        catch (Exception ex)
        {
            return (false, ex.Message);
        }
    }

    public async Task<IReadOnlyCollection<MonitoringSessionDto>> GetByDeviceAsync(
        Guid deviceId,
        CancellationToken cancellationToken = default)
    {
        var sessions = await _monitoringRepository.FindByDeviceIdAsync(deviceId, cancellationToken);

        return sessions.Select(MapToDto).ToList();
    }

    private static MonitoringSessionDto MapToDto(Domain.Contexts.Monitoring.Aggregates.MonitoringSession session)
    {
        return new MonitoringSessionDto
        {
            Id = session.Id,
            DeviceId = session.DeviceId,
            StartedAtUtc = session.StartedAtUtc,
            Readings = session.Readings.Select(r => new SensorReadingDto
            {
                Id = r.Id,
                DeviceId = r.DeviceId,
                SensorId = r.SensorId,
                SensorType = r.SensorType.Value,
                Value = r.Value.Value,
                Unit = r.Unit.Value,
                RecordedAtUtc = r.RecordedAtUtc
            }).ToList(),
            Events = session.Events.Select(e => $"{e.EventType}: {e.Description}").ToList()
        };
    }
}