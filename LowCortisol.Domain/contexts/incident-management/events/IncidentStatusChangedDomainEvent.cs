namespace LowCortisol.Domain.Contexts.IncidentManagement.Events;

public class IncidentStatusChangedDomainEvent
{
    public Guid IncidentId { get; }
    public string NewStatus { get; }
    public DateTime OccurredOnUtc { get; }

    public IncidentStatusChangedDomainEvent(Guid incidentId, string newStatus)
    {
        IncidentId = incidentId;
        NewStatus = newStatus;
        OccurredOnUtc = DateTime.UtcNow;
    }
}