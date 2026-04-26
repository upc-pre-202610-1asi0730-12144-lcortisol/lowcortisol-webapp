namespace LowCortisol.Infrastructure.Security;

public class CurrentUserSession
{
    public Guid? UserId { get; private set; }

    public bool IsAuthenticated => UserId.HasValue;

    public void SignIn(Guid userId)
    {
        if (userId == Guid.Empty)
            throw new ArgumentException("User id cannot be empty.", nameof(userId));

        UserId = userId;
    }

    public void SignOut()
    {
        UserId = null;
    }
}