using LowCortisol.Domain.Contexts.Monitoring.Aggregates;
using LowCortisol.Domain.Contexts.Monitoring.Repositories;
using LowCortisol.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace LowCortisol.Infrastructure.Persistence.Repositories;

public class MonitoringRepository : IMonitoringRepository
{
    private readonly AppDbContext _dbContext;

    public MonitoringRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<MonitoringSession?> FindByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.MonitoringSessions
            .Include(x => x.Readings)
            .Include(x => x.Events)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyCollection<MonitoringSession>> FindByDeviceIdAsync(Guid deviceId, CancellationToken cancellationToken = default)
    {
        return await _dbContext.MonitoringSessions
            .Include(x => x.Readings)
            .Include(x => x.Events)
            .Where(x => x.DeviceId == deviceId)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(MonitoringSession monitoringSession, CancellationToken cancellationToken = default)
    {
        await _dbContext.MonitoringSessions.AddAsync(monitoringSession, cancellationToken);
    }

    public Task UpdateAsync(MonitoringSession monitoringSession, CancellationToken cancellationToken = default)
    {
        _dbContext.MonitoringSessions.Update(monitoringSession);
        return Task.CompletedTask;
    }
}