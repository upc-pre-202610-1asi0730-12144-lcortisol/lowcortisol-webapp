namespace LowCortisol.Domain.Contexts.NotificationManagement.Events;

public class NotificationSentDomainEvent
{
    public Guid NotificationId { get; }
    public DateTime OccurredOnUtc { get; }

    public NotificationSentDomainEvent(Guid notificationId)
    {
        NotificationId = notificationId;
        OccurredOnUtc = DateTime.UtcNow;
    }
}