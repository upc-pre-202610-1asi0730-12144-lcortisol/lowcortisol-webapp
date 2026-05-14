using LowCortisol.Application.Contexts.IncidentManagement.DTOs;
using LowCortisol.Application.Contexts.IncidentManagement.Queries;
using LowCortisol.Application.Contexts.IncidentManagement.Services;

namespace LowCortisol.Application.Contexts.IncidentManagement.Handlers;

public class GetIncidentsByDeviceHandler
{
    private readonly IIncidentManagementService _incidentManagementService;

    public GetIncidentsByDeviceHandler(IIncidentManagementService incidentManagementService)
    {
        _incidentManagementService = incidentManagementService;
    }

    public Task<IReadOnlyCollection<IncidentDto>> HandleAsync(
        GetIncidentsByDeviceQuery query,
        CancellationToken cancellationToken = default)
    {
        return _incidentManagementService.GetByDeviceAsync(query.DeviceId, cancellationToken);
    }
}