using LowCortisol.Domain.Contexts.Reporting.Aggregates;
using LowCortisol.Domain.Contexts.Reporting.ValueObjects;

namespace LowCortisol.Domain.Contexts.Reporting.Repositories;

public interface IConsumptionReportRepository
{
    Task<ConsumptionReport?> FindByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyCollection<ConsumptionReport>> FindByOwnerUserIdAsync(Guid ownerUserId, CancellationToken cancellationToken = default);
    Task<IReadOnlyCollection<ConsumptionReport>> FindByOwnerUserIdAndPeriodAsync(
        Guid ownerUserId,
        ReportPeriod period,
        CancellationToken cancellationToken = default);
    Task AddAsync(ConsumptionReport consumptionReport, CancellationToken cancellationToken = default);
    Task UpdateAsync(ConsumptionReport consumptionReport, CancellationToken cancellationToken = default);
}