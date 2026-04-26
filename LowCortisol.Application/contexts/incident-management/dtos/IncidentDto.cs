namespace LowCortisol.Application.Contexts.IncidentManagement.DTOs;

public class IncidentDto
{
    public Guid Id { get; set; }
    public Guid DeviceId { get; set; }
    public Guid ReportedByUserId { get; set; }
    public Guid? AssignedUserId { get; set; }
    public string Severity { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime CreatedAtUtc { get; set; }
    public List<string> History { get; set; } = new();
}