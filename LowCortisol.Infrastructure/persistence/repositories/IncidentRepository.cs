using LowCortisol.Domain.Contexts.IncidentManagement.Aggregates;
using LowCortisol.Domain.Contexts.IncidentManagement.Repositories;
using LowCortisol.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace LowCortisol.Infrastructure.Persistence.Repositories;

public class IncidentRepository : IIncidentRepository
{
    private readonly AppDbContext _dbContext;

    public IncidentRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Incident?> FindByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Incidents
            .Include(x => x.History)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyCollection<Incident>> FindByDeviceIdAsync(Guid deviceId, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Incidents
            .Include(x => x.History)
            .Where(x => x.DeviceId == deviceId)
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyCollection<Incident>> FindByAssignedUserIdAsync(Guid assignedUserId, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Incidents
            .Include(x => x.History)
            .Where(x => x.AssignedUserId == assignedUserId)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Incident incident, CancellationToken cancellationToken = default)
    {
        await _dbContext.Incidents.AddAsync(incident, cancellationToken);
    }

    public Task UpdateAsync(Incident incident, CancellationToken cancellationToken = default)
    {
        _dbContext.Incidents.Update(incident);
        return Task.CompletedTask;
    }
}