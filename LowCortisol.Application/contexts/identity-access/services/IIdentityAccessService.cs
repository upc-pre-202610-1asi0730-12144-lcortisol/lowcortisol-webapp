using LowCortisol.Application.Contexts.IdentityAccess.Commands;
using LowCortisol.Application.Contexts.IdentityAccess.DTOs;

namespace LowCortisol.Application.Contexts.IdentityAccess.Services;

public interface IIdentityAccessService
{
    Task<(bool Success, string Error)> RegisterAsync(
        RegisterUserCommand command,
        CancellationToken cancellationToken = default);

    Task<(bool Success, string Error, UserAccountDto? User)> LoginAsync(
        LoginUserCommand command,
        CancellationToken cancellationToken = default);

    Task<UserAccountDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
}