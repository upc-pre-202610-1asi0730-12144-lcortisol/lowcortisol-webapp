using LowCortisol.Domain.Contexts.IdentityAccess.Aggregates;

namespace LowCortisol.Domain.Contexts.IdentityAccess.Services;

public interface IUserAccessPolicy
{
    bool CanAccessApplication(UserAccount userAccount);
    bool CanManageUsers(UserAccount userAccount);
    bool CanManageDevices(UserAccount userAccount);
    bool CanReviewIncidents(UserAccount userAccount);
}