namespace LowCortisol.Domain.Contexts.Monitoring.Events;

public class SensorReadingRecordedDomainEvent
{
    public Guid DeviceId { get; }
    public Guid SensorId { get; }
    public DateTime OccurredOnUtc { get; }

    public SensorReadingRecordedDomainEvent(Guid deviceId, Guid sensorId)
    {
        DeviceId = deviceId;
        SensorId = sensorId;
        OccurredOnUtc = DateTime.UtcNow;
    }
}