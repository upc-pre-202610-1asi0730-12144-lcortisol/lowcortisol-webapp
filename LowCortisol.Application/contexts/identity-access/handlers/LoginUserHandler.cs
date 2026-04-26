using LowCortisol.Application.Contexts.IdentityAccess.Commands;
using LowCortisol.Application.Contexts.IdentityAccess.DTOs;
using LowCortisol.Application.Contexts.IdentityAccess.Services;

namespace LowCortisol.Application.Contexts.IdentityAccess.Handlers;

public class LoginUserHandler
{
    private readonly IIdentityAccessService _identityAccessService;

    public LoginUserHandler(IIdentityAccessService identityAccessService)
    {
        _identityAccessService = identityAccessService;
    }

    public Task<(bool Success, string Error, UserAccountDto? User)> HandleAsync(
        LoginUserCommand command,
        CancellationToken cancellationToken = default)
    {
        return _identityAccessService.LoginAsync(command, cancellationToken);
    }
}