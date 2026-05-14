using LowCortisol.Application.Contexts.IncidentManagement.Commands;
using LowCortisol.Application.Contexts.IncidentManagement.DTOs;

namespace LowCortisol.Application.Contexts.IncidentManagement.Services;

public interface IIncidentManagementService
{
    Task<(bool Success, string Error)> CreateAsync(
        CreateIncidentCommand command,
        CancellationToken cancellationToken = default);

    Task<(bool Success, string Error)> AssignAsync(
        AssignIncidentCommand command,
        CancellationToken cancellationToken = default);

    Task<(bool Success, string Error)> UpdateStatusAsync(
        UpdateIncidentStatusCommand command,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyCollection<IncidentDto>> GetByDeviceAsync(
        Guid deviceId,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyCollection<IncidentDto>> GetByAssignedUserAsync(
        Guid assignedUserId,
        CancellationToken cancellationToken = default);
}