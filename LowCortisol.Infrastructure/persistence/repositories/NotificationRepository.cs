using LowCortisol.Domain.Contexts.NotificationManagement.Aggregates;
using LowCortisol.Domain.Contexts.NotificationManagement.Repositories;
using LowCortisol.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace LowCortisol.Infrastructure.Persistence.Repositories;

public class NotificationRepository : INotificationRepository
{
    private readonly AppDbContext _dbContext;

    public NotificationRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Notification?> FindByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Notifications
            .Include(x => x.Attempts)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyCollection<Notification>> FindByRecipientUserIdAsync(Guid recipientUserId, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Notifications
            .Include(x => x.Attempts)
            .Where(x => x.RecipientUserId == recipientUserId)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Notification notification, CancellationToken cancellationToken = default)
    {
        await _dbContext.Notifications.AddAsync(notification, cancellationToken);
    }

    public Task UpdateAsync(Notification notification, CancellationToken cancellationToken = default)
    {
        _dbContext.Notifications.Update(notification);
        return Task.CompletedTask;
    }
}