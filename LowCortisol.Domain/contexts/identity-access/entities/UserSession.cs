namespace LowCortisol.Domain.Contexts.IdentityAccess.Entities;

public class UserSession
{
    public Guid Id { get; private set; }
    public Guid UserAccountId { get; private set; }
    public DateTime StartedAtUtc { get; private set; }
    public DateTime? EndedAtUtc { get; private set; }
    public bool IsActive => EndedAtUtc is null;

    private UserSession()
    {
    }

    public UserSession(Guid userAccountId)
    {
        if (userAccountId == Guid.Empty)
            throw new ArgumentException("User account id cannot be empty.", nameof(userAccountId));

        Id = Guid.NewGuid();
        UserAccountId = userAccountId;
        StartedAtUtc = DateTime.UtcNow;
    }

    public void Close()
    {
        if (EndedAtUtc is not null)
            return;

        EndedAtUtc = DateTime.UtcNow;
    }
}