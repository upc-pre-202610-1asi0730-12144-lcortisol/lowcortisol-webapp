using LowCortisol.Application.Contexts.IdentityAccess.DTOs;
using LowCortisol.Application.Contexts.IdentityAccess.Queries;
using LowCortisol.Application.Contexts.IdentityAccess.Services;

namespace LowCortisol.Application.Contexts.IdentityAccess.Handlers;

public class GetUserAccountByIdHandler
{
    private readonly IIdentityAccessService _identityAccessService;

    public GetUserAccountByIdHandler(IIdentityAccessService identityAccessService)
    {
        _identityAccessService = identityAccessService;
    }

    public Task<UserAccountDto?> HandleAsync(
        GetUserAccountByIdQuery query,
        CancellationToken cancellationToken = default)
    {
        return _identityAccessService.GetByIdAsync(query.UserId, cancellationToken);
    }
}