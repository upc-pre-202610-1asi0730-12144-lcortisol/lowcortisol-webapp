namespace LowCortisol.Domain.Contexts.NotificationManagement.Events;

public class NotificationFailedDomainEvent
{
    public Guid NotificationId { get; }
    public string Reason { get; }
    public DateTime OccurredOnUtc { get; }

    public NotificationFailedDomainEvent(Guid notificationId, string reason)
    {
        NotificationId = notificationId;
        Reason = reason;
        OccurredOnUtc = DateTime.UtcNow;
    }
}