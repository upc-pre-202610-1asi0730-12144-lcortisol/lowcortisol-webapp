using LowCortisol.Application.Contexts.IncidentManagement.Commands;
using LowCortisol.Application.Contexts.IncidentManagement.Services;

namespace LowCortisol.Application.Contexts.IncidentManagement.Handlers;

public class UpdateIncidentStatusHandler
{
    private readonly IIncidentManagementService _incidentManagementService;

    public UpdateIncidentStatusHandler(IIncidentManagementService incidentManagementService)
    {
        _incidentManagementService = incidentManagementService;
    }

    public Task<(bool Success, string Error)> HandleAsync(
        UpdateIncidentStatusCommand command,
        CancellationToken cancellationToken = default)
    {
        return _incidentManagementService.UpdateStatusAsync(command, cancellationToken);
    }
}