using LowCortisol.Application.Contexts.IdentityAccess.Commands;
using LowCortisol.Application.Contexts.IdentityAccess.Services;

namespace LowCortisol.Application.Contexts.IdentityAccess.Handlers;

public class RegisterUserHandler
{
    private readonly IIdentityAccessService _identityAccessService;

    public RegisterUserHandler(IIdentityAccessService identityAccessService)
    {
        _identityAccessService = identityAccessService;
    }

    public Task<(bool Success, string Error)> HandleAsync(
        RegisterUserCommand command,
        CancellationToken cancellationToken = default)
    {
        return _identityAccessService.RegisterAsync(command, cancellationToken);
    }
}