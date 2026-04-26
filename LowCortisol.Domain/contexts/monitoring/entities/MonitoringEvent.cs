namespace LowCortisol.Domain.Contexts.Monitoring.Entities;

public class MonitoringEvent
{
    public Guid Id { get; private set; }
    public Guid DeviceId { get; private set; }
    public string EventType { get; private set; }
    public string Description { get; private set; }
    public DateTime OccurredOnUtc { get; private set; }

    private MonitoringEvent()
    {
        Id = Guid.Empty;
        EventType = string.Empty;
        Description = string.Empty;
    }

    public MonitoringEvent(Guid deviceId, string eventType, string description)
    {
        if (deviceId == Guid.Empty)
            throw new ArgumentException("Device id cannot be empty.", nameof(deviceId));

        if (string.IsNullOrWhiteSpace(eventType))
            throw new ArgumentException("Event type cannot be empty.", nameof(eventType));

        if (string.IsNullOrWhiteSpace(description))
            throw new ArgumentException("Description cannot be empty.", nameof(description));

        Id = Guid.NewGuid();
        DeviceId = deviceId;
        EventType = eventType.Trim();
        Description = description.Trim();
        OccurredOnUtc = DateTime.UtcNow;
    }
}