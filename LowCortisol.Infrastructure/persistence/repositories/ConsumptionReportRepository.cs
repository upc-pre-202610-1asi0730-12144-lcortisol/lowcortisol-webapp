using LowCortisol.Domain.Contexts.Reporting.Aggregates;
using LowCortisol.Domain.Contexts.Reporting.Repositories;
using LowCortisol.Domain.Contexts.Reporting.ValueObjects;
using LowCortisol.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace LowCortisol.Infrastructure.Persistence.Repositories;

public class ConsumptionReportRepository : IConsumptionReportRepository
{
    private readonly AppDbContext _dbContext;

    public ConsumptionReportRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ConsumptionReport?> FindByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.ConsumptionReports
            .Include(x => x.Lines)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyCollection<ConsumptionReport>> FindByOwnerUserIdAsync(Guid ownerUserId, CancellationToken cancellationToken = default)
    {
        return await _dbContext.ConsumptionReports
            .Include(x => x.Lines)
            .Where(x => x.OwnerUserId == ownerUserId)
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyCollection<ConsumptionReport>> FindByOwnerUserIdAndPeriodAsync(
        Guid ownerUserId,
        ReportPeriod period,
        CancellationToken cancellationToken = default)
    {
        return await _dbContext.ConsumptionReports
            .Include(x => x.Lines)
            .Where(x => x.OwnerUserId == ownerUserId && x.Period.Value == period.Value)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(ConsumptionReport consumptionReport, CancellationToken cancellationToken = default)
    {
        await _dbContext.ConsumptionReports.AddAsync(consumptionReport, cancellationToken);
    }

    public Task UpdateAsync(ConsumptionReport consumptionReport, CancellationToken cancellationToken = default)
    {
        _dbContext.ConsumptionReports.Update(consumptionReport);
        return Task.CompletedTask;
    }
}