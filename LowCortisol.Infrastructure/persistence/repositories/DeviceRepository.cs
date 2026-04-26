using LowCortisol.Domain.Contexts.DeviceManagement.Aggregates;
using LowCortisol.Domain.Contexts.DeviceManagement.Repositories;
using LowCortisol.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace LowCortisol.Infrastructure.Persistence.Repositories;

public class DeviceRepository : IDeviceRepository
{
    private readonly AppDbContext _dbContext;

    public DeviceRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Device?> FindByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Devices
            .Include(x => x.Sensors)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyCollection<Device>> FindByOwnerUserIdAsync(Guid ownerUserId, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Devices
            .Include(x => x.Sensors)
            .Where(x => x.OwnerUserId == ownerUserId)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Device device, CancellationToken cancellationToken = default)
    {
        await _dbContext.Devices.AddAsync(device, cancellationToken);
    }

    public Task UpdateAsync(Device device, CancellationToken cancellationToken = default)
    {
        _dbContext.Devices.Update(device);
        return Task.CompletedTask;
    }
}