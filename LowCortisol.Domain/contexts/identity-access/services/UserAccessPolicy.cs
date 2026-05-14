using LowCortisol.Domain.Contexts.IdentityAccess.Aggregates;
using LowCortisol.Domain.Contexts.IdentityAccess.Services;

namespace LowCortisol.Domain.Contexts.IdentityAccess.Services;

public class UserAccessPolicy : IUserAccessPolicy
{
    public bool CanAccessApplication(UserAccount userAccount)
    {
        return userAccount.IsActive;
    }

    public bool CanManageUsers(UserAccount userAccount)
    {
        return userAccount.IsActive && userAccount.Role.Value == "Admin";
    }

    public bool CanManageDevices(UserAccount userAccount)
    {
        return userAccount.IsActive &&
               (userAccount.Role.Value == "Admin" ||
                userAccount.Role.Value == "Technician" ||
                userAccount.Role.Value == "Homeowner");
    }

    public bool CanReviewIncidents(UserAccount userAccount)
    {
        return userAccount.IsActive &&
               (userAccount.Role.Value == "Admin" ||
                userAccount.Role.Value == "Technician");
    }
}