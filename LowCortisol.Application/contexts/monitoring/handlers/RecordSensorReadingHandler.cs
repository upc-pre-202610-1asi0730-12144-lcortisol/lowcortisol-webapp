using LowCortisol.Application.Contexts.Monitoring.Commands;
using LowCortisol.Application.Contexts.Monitoring.Services;

namespace LowCortisol.Application.Contexts.Monitoring.Handlers;

public class RecordSensorReadingHandler
{
    private readonly IMonitoringService _monitoringService;

    public RecordSensorReadingHandler(IMonitoringService monitoringService)
    {
        _monitoringService = monitoringService;
    }

    public Task<(bool Success, string Error)> HandleAsync(
        RecordSensorReadingCommand command,
        CancellationToken cancellationToken = default)
    {
        return _monitoringService.RecordReadingAsync(command, cancellationToken);
    }
}