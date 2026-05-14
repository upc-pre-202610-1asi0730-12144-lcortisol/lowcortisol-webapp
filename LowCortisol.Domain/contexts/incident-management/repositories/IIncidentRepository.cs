using LowCortisol.Domain.Contexts.IncidentManagement.Aggregates;

namespace LowCortisol.Domain.Contexts.IncidentManagement.Repositories;

public interface IIncidentRepository
{
    Task<Incident?> FindByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyCollection<Incident>> FindByDeviceIdAsync(Guid deviceId, CancellationToken cancellationToken = default);
    Task<IReadOnlyCollection<Incident>> FindByAssignedUserIdAsync(Guid assignedUserId, CancellationToken cancellationToken = default);
    Task AddAsync(Incident incident, CancellationToken cancellationToken = default);
    Task UpdateAsync(Incident incident, CancellationToken cancellationToken = default);
}