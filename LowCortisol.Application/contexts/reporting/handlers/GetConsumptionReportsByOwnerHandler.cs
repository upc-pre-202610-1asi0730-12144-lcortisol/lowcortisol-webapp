using LowCortisol.Application.Contexts.Reporting.DTOs;
using LowCortisol.Application.Contexts.Reporting.Queries;
using LowCortisol.Application.Contexts.Reporting.Services;

namespace LowCortisol.Application.Contexts.Reporting.Handlers;

public class GetConsumptionReportsByOwnerHandler
{
    private readonly IReportingService _reportingService;

    public GetConsumptionReportsByOwnerHandler(IReportingService reportingService)
    {
        _reportingService = reportingService;
    }

    public Task<IReadOnlyCollection<ConsumptionReportDto>> HandleAsync(
        GetConsumptionReportsByOwnerQuery query,
        CancellationToken cancellationToken = default)
    {
        return _reportingService.GetByOwnerAsync(query.OwnerUserId, cancellationToken);
    }
}