using LowCortisol.Application.Contexts.Reporting.Commands;
using LowCortisol.Application.Contexts.Reporting.Services;

namespace LowCortisol.Application.Contexts.Reporting.Handlers;

public class GenerateConsumptionReportHandler
{
    private readonly IReportingService _reportingService;

    public GenerateConsumptionReportHandler(IReportingService reportingService)
    {
        _reportingService = reportingService;
    }

    public Task<(bool Success, string Error)> HandleAsync(
        GenerateConsumptionReportCommand command,
        CancellationToken cancellationToken = default)
    {
        return _reportingService.GenerateAsync(command, cancellationToken);
    }
}