namespace LowCortisol.Domain.Contexts.IncidentManagement.Events;

public class IncidentAssignedDomainEvent
{
    public Guid IncidentId { get; }
    public Guid AssignedUserId { get; }
    public DateTime OccurredOnUtc { get; }

    public IncidentAssignedDomainEvent(Guid incidentId, Guid assignedUserId)
    {
        IncidentId = incidentId;
        AssignedUserId = assignedUserId;
        OccurredOnUtc = DateTime.UtcNow;
    }
}