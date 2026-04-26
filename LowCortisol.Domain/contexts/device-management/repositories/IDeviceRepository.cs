using LowCortisol.Domain.Contexts.DeviceManagement.Aggregates;

namespace LowCortisol.Domain.Contexts.DeviceManagement.Repositories;

public interface IDeviceRepository
{
    Task<Device?> FindByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyCollection<Device>> FindByOwnerUserIdAsync(Guid ownerUserId, CancellationToken cancellationToken = default);
    Task AddAsync(Device device, CancellationToken cancellationToken = default);
    Task UpdateAsync(Device device, CancellationToken cancellationToken = default);
}