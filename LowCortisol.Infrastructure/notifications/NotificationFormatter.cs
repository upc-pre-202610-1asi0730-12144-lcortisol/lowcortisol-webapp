using LowCortisol.Domain.Contexts.NotificationManagement.Aggregates;

namespace LowCortisol.Infrastructure.Notifications;

public class NotificationFormatter
{
    public string Format(Notification notification)
    {
        return
            $"[{notification.Channel.Value}] " +
            $"{notification.Message.Title} - " +
            $"{notification.Message.Body}";
    }
}