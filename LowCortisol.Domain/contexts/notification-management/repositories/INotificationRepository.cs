using LowCortisol.Domain.Contexts.NotificationManagement.Aggregates;

namespace LowCortisol.Domain.Contexts.NotificationManagement.Repositories;

public interface INotificationRepository
{
    Task<Notification?> FindByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyCollection<Notification>> FindByRecipientUserIdAsync(Guid recipientUserId, CancellationToken cancellationToken = default);
    Task AddAsync(Notification notification, CancellationToken cancellationToken = default);
    Task UpdateAsync(Notification notification, CancellationToken cancellationToken = default);
}