using LowCortisol.Domain.Contexts.NotificationManagement.Aggregates;

namespace LowCortisol.Application.Contexts.NotificationManagement.Contracts;

public interface INotificationSender
{
    Task<NotificationDispatchResult> SendAsync(
        Notification notification,
        CancellationToken cancellationToken = default);
}