namespace LowCortisol.Domain.Contexts.IdentityAccess.Events;

public class UserAccountActivatedDomainEvent
{
    public Guid UserAccountId { get; }
    public DateTime OccurredOnUtc { get; }

    public UserAccountActivatedDomainEvent(Guid userAccountId)
    {
        UserAccountId = userAccountId;
        OccurredOnUtc = DateTime.UtcNow;
    }
}