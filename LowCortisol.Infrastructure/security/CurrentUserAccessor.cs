namespace LowCortisol.Infrastructure.Security;

public class CurrentUserAccessor : ICurrentUserAccessor
{
    private readonly CurrentUserSession _session;

    public CurrentUserAccessor(CurrentUserSession session)
    {
        _session = session;
    }

    public Guid? GetCurrentUserId()
    {
        return _session.UserId;
    }

    public bool IsAuthenticated()
    {
        return _session.IsAuthenticated;
    }
}