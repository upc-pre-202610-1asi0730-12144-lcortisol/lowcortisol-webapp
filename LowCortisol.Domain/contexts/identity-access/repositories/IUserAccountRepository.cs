using LowCortisol.Domain.Contexts.IdentityAccess.Aggregates;
using LowCortisol.Domain.Contexts.IdentityAccess.ValueObjects;

namespace LowCortisol.Domain.Contexts.IdentityAccess.Repositories;

public interface IUserAccountRepository
{
    Task<UserAccount?> FindByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<UserAccount?> FindByEmailAsync(EmailAddress email, CancellationToken cancellationToken = default);
    Task<bool> ExistsByEmailAsync(EmailAddress email, CancellationToken cancellationToken = default);
    Task AddAsync(UserAccount userAccount, CancellationToken cancellationToken = default);
    Task UpdateAsync(UserAccount userAccount, CancellationToken cancellationToken = default);
}