namespace LowCortisol.Domain.Contexts.NotificationManagement.Events;

public class NotificationCreatedDomainEvent
{
    public Guid NotificationId { get; }
    public DateTime OccurredOnUtc { get; }

    public NotificationCreatedDomainEvent(Guid notificationId)
    {
        NotificationId = notificationId;
        OccurredOnUtc = DateTime.UtcNow;
    }
}