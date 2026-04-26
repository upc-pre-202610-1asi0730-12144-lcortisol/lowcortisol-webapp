namespace LowCortisol.Domain.Contexts.IdentityAccess.Events;

public class UserAccountRegisteredDomainEvent
{
    public Guid UserAccountId { get; }
    public DateTime OccurredOnUtc { get; }

    public UserAccountRegisteredDomainEvent(Guid userAccountId)
    {
        UserAccountId = userAccountId;
        OccurredOnUtc = DateTime.UtcNow;
    }
}