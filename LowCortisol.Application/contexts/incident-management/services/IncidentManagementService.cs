using LowCortisol.Application.Common.Interfaces;
using LowCortisol.Application.Contexts.IncidentManagement.Commands;
using LowCortisol.Application.Contexts.IncidentManagement.DTOs;
using LowCortisol.Domain.Contexts.IncidentManagement.Aggregates;
using LowCortisol.Domain.Contexts.IncidentManagement.Repositories;
using LowCortisol.Domain.Contexts.IncidentManagement.ValueObjects;

namespace LowCortisol.Application.Contexts.IncidentManagement.Services;

public class IncidentManagementService : IIncidentManagementService
{
    private readonly IIncidentRepository _incidentRepository;
    private readonly IUnitOfWork _unitOfWork;

    public IncidentManagementService(
        IIncidentRepository incidentRepository,
        IUnitOfWork unitOfWork)
    {
        _incidentRepository = incidentRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<(bool Success, string Error)> CreateAsync(
        CreateIncidentCommand command,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var incident = new Incident(
                command.DeviceId,
                command.ReportedByUserId,
                new IncidentSeverity(command.Severity),
                new IncidentDescription(command.Description)
            );

            await _incidentRepository.AddAsync(incident, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return (true, string.Empty);
        }
        catch (Exception ex)
        {
            return (false, ex.Message);
        }
    }

    public async Task<(bool Success, string Error)> AssignAsync(
        AssignIncidentCommand command,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var incident = await _incidentRepository.FindByIdAsync(command.IncidentId, cancellationToken);
            if (incident is null)
                return (false, "Incident not found.");

            incident.AssignTo(command.AssignedUserId);

            await _incidentRepository.UpdateAsync(incident, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return (true, string.Empty);
        }
        catch (Exception ex)
        {
            return (false, ex.Message);
        }
    }

    public async Task<(bool Success, string Error)> UpdateStatusAsync(
        UpdateIncidentStatusCommand command,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var incident = await _incidentRepository.FindByIdAsync(command.IncidentId, cancellationToken);
            if (incident is null)
                return (false, "Incident not found.");

            switch (command.Status.Trim())
            {
                case "InProgress":
                    incident.MarkInProgress();
                    break;
                case "Resolved":
                    incident.Resolve(command.Note);
                    break;
                case "Closed":
                    incident.Close(command.Note);
                    break;
                default:
                    return (false, "Invalid incident status.");
            }

            await _incidentRepository.UpdateAsync(incident, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return (true, string.Empty);
        }
        catch (Exception ex)
        {
            return (false, ex.Message);
        }
    }

    public async Task<IReadOnlyCollection<IncidentDto>> GetByDeviceAsync(
        Guid deviceId,
        CancellationToken cancellationToken = default)
    {
        var incidents = await _incidentRepository.FindByDeviceIdAsync(deviceId, cancellationToken);
        return incidents.Select(MapToDto).ToList();
    }

    public async Task<IReadOnlyCollection<IncidentDto>> GetByAssignedUserAsync(
        Guid assignedUserId,
        CancellationToken cancellationToken = default)
    {
        var incidents = await _incidentRepository.FindByAssignedUserIdAsync(assignedUserId, cancellationToken);
        return incidents.Select(MapToDto).ToList();
    }

    private static IncidentDto MapToDto(Incident incident)
    {
        return new IncidentDto
        {
            Id = incident.Id,
            DeviceId = incident.DeviceId,
            ReportedByUserId = incident.ReportedByUserId,
            AssignedUserId = incident.AssignedUserId,
            Severity = incident.Severity.Value,
            Status = incident.Status.Value,
            Description = incident.Description.Value,
            CreatedAtUtc = incident.CreatedAtUtc,
            History = incident.History
                .Select(x => $"{x.OccurredOnUtc:O} - {x.Action}: {x.Description}")
                .ToList()
        };
    }
}