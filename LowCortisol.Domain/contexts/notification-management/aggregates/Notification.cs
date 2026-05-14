using LowCortisol.Domain.Contexts.NotificationManagement.Entities;
using LowCortisol.Domain.Contexts.NotificationManagement.Events;
using LowCortisol.Domain.Contexts.NotificationManagement.ValueObjects;

namespace LowCortisol.Domain.Contexts.NotificationManagement.Aggregates;

public class Notification
{
    private readonly List<object> _domainEvents = new();
    private readonly List<NotificationAttempt> _attempts = new();

    public Guid Id { get; private set; }
    public Guid RecipientUserId { get; private set; }
    public NotificationChannel Channel { get; private set; }
    public NotificationMessage Message { get; private set; }
    public NotificationStatus Status { get; private set; }
    public DateTime CreatedAtUtc { get; private set; }

    public IReadOnlyCollection<NotificationAttempt> Attempts => _attempts.AsReadOnly();
    public IReadOnlyCollection<object> DomainEvents => _domainEvents.AsReadOnly();

    private Notification()
    {
        Id = Guid.Empty;
        Channel = null!;
        Message = null!;
        Status = null!;
    }

    public Notification(Guid recipientUserId, NotificationChannel channel, NotificationMessage message)
    {
        if (recipientUserId == Guid.Empty)
            throw new ArgumentException("Recipient user id cannot be empty.", nameof(recipientUserId));

        Id = Guid.NewGuid();
        RecipientUserId = recipientUserId;
        Channel = channel;
        Message = message;
        Status = NotificationStatus.Pending;
        CreatedAtUtc = DateTime.UtcNow;

        AddDomainEvent(new NotificationCreatedDomainEvent(Id));
    }

    public void MarkAsSent()
    {
        Status = NotificationStatus.Sent;
        _attempts.Add(new NotificationAttempt(NotificationStatus.Sent));
        AddDomainEvent(new NotificationSentDomainEvent(Id));
    }

    public void MarkAsFailed(string reason)
    {
        Status = NotificationStatus.Failed;
        _attempts.Add(new NotificationAttempt(NotificationStatus.Failed, reason));
        AddDomainEvent(new NotificationFailedDomainEvent(Id, reason));
    }

    public void ClearDomainEvents() => _domainEvents.Clear();

    private void AddDomainEvent(object domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }
}