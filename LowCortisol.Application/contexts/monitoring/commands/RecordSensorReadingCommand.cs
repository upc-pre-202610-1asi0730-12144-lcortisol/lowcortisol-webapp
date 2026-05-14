namespace LowCortisol.Application.Contexts.Monitoring.Commands;

public class RecordSensorReadingCommand
{
    public Guid MonitoringSessionId { get; set; }
    public Guid DeviceId { get; set; }
    public Guid SensorId { get; set; }
    public string SensorType { get; set; } = string.Empty;
    public decimal Value { get; set; }
    public string Unit { get; set; } = string.Empty;
}