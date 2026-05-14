namespace LowCortisol.Domain.Contexts.IdentityAccess.Events;

public class UserAccountDeactivatedDomainEvent
{
    public Guid UserAccountId { get; }
    public DateTime OccurredOnUtc { get; }

    public UserAccountDeactivatedDomainEvent(Guid userAccountId)
    {
        UserAccountId = userAccountId;
        OccurredOnUtc = DateTime.UtcNow;
    }
}