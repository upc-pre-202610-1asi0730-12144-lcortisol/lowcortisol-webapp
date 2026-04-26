using LowCortisol.Domain.Contexts.Monitoring.Aggregates;

namespace LowCortisol.Domain.Contexts.Monitoring.Repositories;

public interface IMonitoringRepository
{
    Task<MonitoringSession?> FindByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyCollection<MonitoringSession>> FindByDeviceIdAsync(Guid deviceId, CancellationToken cancellationToken = default);
    Task AddAsync(MonitoringSession monitoringSession, CancellationToken cancellationToken = default);
    Task UpdateAsync(MonitoringSession monitoringSession, CancellationToken cancellationToken = default);
}