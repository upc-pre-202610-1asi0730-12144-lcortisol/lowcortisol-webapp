using LowCortisol.Application.Contexts.IncidentManagement.Commands;
using LowCortisol.Application.Contexts.IncidentManagement.Services;

namespace LowCortisol.Application.Contexts.IncidentManagement.Handlers;

public class CreateIncidentHandler
{
    private readonly IIncidentManagementService _incidentManagementService;

    public CreateIncidentHandler(IIncidentManagementService incidentManagementService)
    {
        _incidentManagementService = incidentManagementService;
    }

    public Task<(bool Success, string Error)> HandleAsync(
        CreateIncidentCommand command,
        CancellationToken cancellationToken = default)
    {
        return _incidentManagementService.CreateAsync(command, cancellationToken);
    }
}