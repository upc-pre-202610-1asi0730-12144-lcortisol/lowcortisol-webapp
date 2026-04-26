namespace LowCortisol.Application.Contexts.Monitoring.DTOs;

public class MonitoringSessionDto
{
    public Guid Id { get; set; }
    public Guid DeviceId { get; set; }
    public DateTime StartedAtUtc { get; set; }
    public List<SensorReadingDto> Readings { get; set; } = new();
    public List<string> Events { get; set; } = new();
}