using LowCortisol.Application.Contexts.IncidentManagement.DTOs;
using LowCortisol.Application.Contexts.IncidentManagement.Queries;
using LowCortisol.Application.Contexts.IncidentManagement.Services;

namespace LowCortisol.Application.Contexts.IncidentManagement.Handlers;

public class GetIncidentsByAssignedUserHandler
{
    private readonly IIncidentManagementService _incidentManagementService;

    public GetIncidentsByAssignedUserHandler(IIncidentManagementService incidentManagementService)
    {
        _incidentManagementService = incidentManagementService;
    }

    public Task<IReadOnlyCollection<IncidentDto>> HandleAsync(
        GetIncidentsByAssignedUserQuery query,
        CancellationToken cancellationToken = default)
    {
        return _incidentManagementService.GetByAssignedUserAsync(query.AssignedUserId, cancellationToken);
    }
}