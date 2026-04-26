using LowCortisol.Application.Contexts.IncidentManagement.Commands;
using LowCortisol.Application.Contexts.IncidentManagement.Services;

namespace LowCortisol.Application.Contexts.IncidentManagement.Handlers;

public class AssignIncidentHandler
{
    private readonly IIncidentManagementService _incidentManagementService;

    public AssignIncidentHandler(IIncidentManagementService incidentManagementService)
    {
        _incidentManagementService = incidentManagementService;
    }

    public Task<(bool Success, string Error)> HandleAsync(
        AssignIncidentCommand command,
        CancellationToken cancellationToken = default)
    {
        return _incidentManagementService.AssignAsync(command, cancellationToken);
    }
}