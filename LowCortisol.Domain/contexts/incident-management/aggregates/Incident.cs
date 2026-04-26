using LowCortisol.Domain.Contexts.IncidentManagement.Entities;
using LowCortisol.Domain.Contexts.IncidentManagement.Events;
using LowCortisol.Domain.Contexts.IncidentManagement.ValueObjects;

namespace LowCortisol.Domain.Contexts.IncidentManagement.Aggregates;

public class Incident
{
    private readonly List<object> _domainEvents = new();
    private readonly List<IncidentHistoryEntry> _history = new();

    public Guid Id { get; private set; }
    public Guid DeviceId { get; private set; }
    public Guid ReportedByUserId { get; private set; }
    public Guid? AssignedUserId { get; private set; }
    public IncidentSeverity Severity { get; private set; }
    public IncidentStatus Status { get; private set; }
    public IncidentDescription Description { get; private set; }
    public DateTime CreatedAtUtc { get; private set; }

    public IReadOnlyCollection<IncidentHistoryEntry> History => _history.AsReadOnly();
    public IReadOnlyCollection<object> DomainEvents => _domainEvents.AsReadOnly();

    private Incident()
    {
        Id = Guid.Empty;
        Severity = null!;
        Status = null!;
        Description = null!;
    }

    public Incident(
        Guid deviceId,
        Guid reportedByUserId,
        IncidentSeverity severity,
        IncidentDescription description)
    {
        if (deviceId == Guid.Empty)
            throw new ArgumentException("Device id cannot be empty.", nameof(deviceId));

        if (reportedByUserId == Guid.Empty)
            throw new ArgumentException("Reported by user id cannot be empty.", nameof(reportedByUserId));

        Id = Guid.NewGuid();
        DeviceId = deviceId;
        ReportedByUserId = reportedByUserId;
        Severity = severity;
        Status = IncidentStatus.Open;
        Description = description;
        CreatedAtUtc = DateTime.UtcNow;

        AddHistory("Created", description.Value);
        AddDomainEvent(new IncidentCreatedDomainEvent(Id));
    }

    public void AssignTo(Guid assignedUserId)
    {
        if (assignedUserId == Guid.Empty)
            throw new ArgumentException("Assigned user id cannot be empty.", nameof(assignedUserId));

        AssignedUserId = assignedUserId;
        Status = IncidentStatus.Assigned;

        AddHistory("Assigned", $"Incident assigned to user {assignedUserId}");
        AddDomainEvent(new IncidentAssignedDomainEvent(Id, assignedUserId));
        AddDomainEvent(new IncidentStatusChangedDomainEvent(Id, Status.Value));
    }

    public void MarkInProgress()
    {
        Status = IncidentStatus.InProgress;
        AddHistory("InProgress", "Incident work started.");
        AddDomainEvent(new IncidentStatusChangedDomainEvent(Id, Status.Value));
    }

    public void Resolve(string resolutionNote)
    {
        Status = IncidentStatus.Resolved;
        AddHistory("Resolved", resolutionNote);
        AddDomainEvent(new IncidentStatusChangedDomainEvent(Id, Status.Value));
    }

    public void Close(string closingNote)
    {
        Status = IncidentStatus.Closed;
        AddHistory("Closed", closingNote);
        AddDomainEvent(new IncidentStatusChangedDomainEvent(Id, Status.Value));
    }

    public void ClearDomainEvents() => _domainEvents.Clear();

    private void AddHistory(string action, string description)
    {
        _history.Add(new IncidentHistoryEntry(action, description));
    }

    private void AddDomainEvent(object domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }
}