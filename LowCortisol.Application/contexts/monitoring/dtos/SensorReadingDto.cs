namespace LowCortisol.Application.Contexts.Monitoring.DTOs;

public class SensorReadingDto
{
    public Guid Id { get; set; }
    public Guid DeviceId { get; set; }
    public Guid SensorId { get; set; }
    public string SensorType { get; set; } = string.Empty;
    public decimal Value { get; set; }
    public string Unit { get; set; } = string.Empty;
    public DateTime RecordedAtUtc { get; set; }
}