namespace LowCortisol.Infrastructure.Security;

public interface ICurrentUserAccessor
{
    Guid? GetCurrentUserId();
    bool IsAuthenticated();
}