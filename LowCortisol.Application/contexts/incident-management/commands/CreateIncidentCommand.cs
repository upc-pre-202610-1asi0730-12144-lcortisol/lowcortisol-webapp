namespace LowCortisol.Application.Contexts.IncidentManagement.Commands;

public class CreateIncidentCommand
{
    public Guid DeviceId { get; set; }
    public Guid ReportedByUserId { get; set; }
    public string Severity { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}