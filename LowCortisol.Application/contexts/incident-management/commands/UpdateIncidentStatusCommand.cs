namespace LowCortisol.Application.Contexts.IncidentManagement.Commands;

public class UpdateIncidentStatusCommand
{
    public Guid IncidentId { get; set; }
    public string Status { get; set; } = string.Empty;
    public string Note { get; set; } = string.Empty;
}