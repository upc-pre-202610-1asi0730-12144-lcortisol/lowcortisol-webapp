using LowCortisol.Domain.Contexts.NotificationManagement.Aggregates;

namespace LowCortisol.Domain.Contexts.NotificationManagement.Services;

public interface INotificationPolicy
{
    bool CanBeSent(Notification notification);
}