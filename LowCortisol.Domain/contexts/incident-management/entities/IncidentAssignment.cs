namespace LowCortisol.Domain.Contexts.IncidentManagement.Entities;

public class IncidentAssignment
{
    public Guid Id { get; private set; }
    public Guid IncidentId { get; private set; }
    public Guid AssignedUserId { get; private set; }
    public DateTime AssignedAtUtc { get; private set; }

    private IncidentAssignment()
    {
        Id = Guid.Empty;
    }

    public IncidentAssignment(Guid incidentId, Guid assignedUserId)
    {
        if (incidentId == Guid.Empty)
            throw new ArgumentException("Incident id cannot be empty.", nameof(incidentId));

        if (assignedUserId == Guid.Empty)
            throw new ArgumentException("Assigned user id cannot be empty.", nameof(assignedUserId));

        Id = Guid.NewGuid();
        IncidentId = incidentId;
        AssignedUserId = assignedUserId;
        AssignedAtUtc = DateTime.UtcNow;
    }
}