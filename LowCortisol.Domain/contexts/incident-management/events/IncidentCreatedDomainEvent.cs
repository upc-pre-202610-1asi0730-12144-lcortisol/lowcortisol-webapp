namespace LowCortisol.Domain.Contexts.IncidentManagement.Events;

public class IncidentCreatedDomainEvent
{
    public Guid IncidentId { get; }
    public DateTime OccurredOnUtc { get; }

    public IncidentCreatedDomainEvent(Guid incidentId)
    {
        IncidentId = incidentId;
        OccurredOnUtc = DateTime.UtcNow;
    }
}