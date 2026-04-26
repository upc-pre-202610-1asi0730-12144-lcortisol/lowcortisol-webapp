using LowCortisol.Application.Contexts.Reporting.DTOs;
using LowCortisol.Application.Contexts.Reporting.Queries;
using LowCortisol.Application.Contexts.Reporting.Services;

namespace LowCortisol.Application.Contexts.Reporting.Handlers;

public class GetConsumptionReportsByOwnerAndPeriodHandler
{
    private readonly IReportingService _reportingService;

    public GetConsumptionReportsByOwnerAndPeriodHandler(IReportingService reportingService)
    {
        _reportingService = reportingService;
    }

    public Task<IReadOnlyCollection<ConsumptionReportDto>> HandleAsync(
        GetConsumptionReportsByOwnerAndPeriodQuery query,
        CancellationToken cancellationToken = default)
    {
        return _reportingService.GetByOwnerAndPeriodAsync(
            query.OwnerUserId,
            query.Period,
            cancellationToken);
    }
}