using LowCortisol.Application.Contexts.IdentityAccess.DTOs;

namespace LowCortisol.Presentation.Services;

public class UiSessionService
{
    public UserAccountDto? CurrentUser { get; private set; }

    public bool IsAuthenticated => CurrentUser is not null;

    public event Action? OnChange;

    public void SignIn(UserAccountDto user)
    {
        CurrentUser = user;
        OnChange?.Invoke();
    }

    public void SignOut()
    {
        CurrentUser = null;
        OnChange?.Invoke();
    }
}