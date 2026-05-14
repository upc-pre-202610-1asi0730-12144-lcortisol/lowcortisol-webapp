using LowCortisol.Application.Common.Interfaces;
using LowCortisol.Infrastructure.Persistence.Context;

namespace LowCortisol.Infrastructure.Persistence;

public class AppUnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _dbContext;

    public AppUnitOfWork(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return _dbContext.SaveChangesAsync(cancellationToken);
    }
}