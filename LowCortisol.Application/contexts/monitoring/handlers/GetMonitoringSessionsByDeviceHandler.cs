using LowCortisol.Application.Contexts.Monitoring.DTOs;
using LowCortisol.Application.Contexts.Monitoring.Queries;
using LowCortisol.Application.Contexts.Monitoring.Services;

namespace LowCortisol.Application.Contexts.Monitoring.Handlers;

public class GetMonitoringSessionsByDeviceHandler
{
    private readonly IMonitoringService _monitoringService;

    public GetMonitoringSessionsByDeviceHandler(IMonitoringService monitoringService)
    {
        _monitoringService = monitoringService;
    }

    public Task<IReadOnlyCollection<MonitoringSessionDto>> HandleAsync(
        GetMonitoringSessionsByDeviceQuery query,
        CancellationToken cancellationToken = default)
    {
        return _monitoringService.GetByDeviceAsync(query.DeviceId, cancellationToken);
    }
}