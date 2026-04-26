using LowCortisol.Domain.Contexts.IdentityAccess.Aggregates;
using LowCortisol.Domain.Contexts.IdentityAccess.Repositories;
using LowCortisol.Domain.Contexts.IdentityAccess.ValueObjects;
using LowCortisol.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace LowCortisol.Infrastructure.Persistence.Repositories;

public class UserAccountRepository : IUserAccountRepository
{
    private readonly AppDbContext _dbContext;

    public UserAccountRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<UserAccount?> FindByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.UserAccounts
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<UserAccount?> FindByEmailAsync(EmailAddress email, CancellationToken cancellationToken = default)
    {
        return await _dbContext.UserAccounts
            .FirstOrDefaultAsync(x => x.Email.Value == email.Value, cancellationToken);
    }

    public async Task<bool> ExistsByEmailAsync(EmailAddress email, CancellationToken cancellationToken = default)
    {
        return await _dbContext.UserAccounts
            .AnyAsync(x => x.Email.Value == email.Value, cancellationToken);
    }

    public async Task AddAsync(UserAccount userAccount, CancellationToken cancellationToken = default)
    {
        await _dbContext.UserAccounts.AddAsync(userAccount, cancellationToken);
    }

    public Task UpdateAsync(UserAccount userAccount, CancellationToken cancellationToken = default)
    {
        _dbContext.UserAccounts.Update(userAccount);
        return Task.CompletedTask;
    }
}