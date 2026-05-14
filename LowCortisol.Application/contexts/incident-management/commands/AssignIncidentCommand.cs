namespace LowCortisol.Application.Contexts.IncidentManagement.Commands;

public class AssignIncidentCommand
{
    public Guid IncidentId { get; set; }
    public Guid AssignedUserId { get; set; }
}