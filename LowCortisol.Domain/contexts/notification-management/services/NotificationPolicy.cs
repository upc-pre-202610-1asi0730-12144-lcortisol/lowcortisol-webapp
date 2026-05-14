using LowCortisol.Domain.Contexts.NotificationManagement.Aggregates;

namespace LowCortisol.Domain.Contexts.NotificationManagement.Services;

public class NotificationPolicy : INotificationPolicy
{
    public bool CanBeSent(Notification notification)
    {
        return notification.Status.Value == "Pending";
    }
}