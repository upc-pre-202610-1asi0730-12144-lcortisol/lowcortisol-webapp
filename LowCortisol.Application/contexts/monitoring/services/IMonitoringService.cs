using LowCortisol.Application.Contexts.Monitoring.Commands;
using LowCortisol.Application.Contexts.Monitoring.DTOs;

namespace LowCortisol.Application.Contexts.Monitoring.Services;

public interface IMonitoringService
{
    Task<(bool Success, string Error)> RecordReadingAsync(
        RecordSensorReadingCommand command,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyCollection<MonitoringSessionDto>> GetByDeviceAsync(
        Guid deviceId,
        CancellationToken cancellationToken = default);
}