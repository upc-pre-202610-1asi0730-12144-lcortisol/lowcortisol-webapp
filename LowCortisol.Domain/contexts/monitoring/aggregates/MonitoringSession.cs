using LowCortisol.Domain.Contexts.Monitoring.Entities;
using LowCortisol.Domain.Contexts.Monitoring.Events;

namespace LowCortisol.Domain.Contexts.Monitoring.Aggregates;

public class MonitoringSession
{
    private readonly List<object> _domainEvents = new();
    private readonly List<SensorReading> _readings = new();
    private readonly List<MonitoringEvent> _events = new();

    public Guid Id { get; private set; }
    public Guid DeviceId { get; private set; }
    public DateTime StartedAtUtc { get; private set; }

    public IReadOnlyCollection<SensorReading> Readings => _readings.AsReadOnly();
    public IReadOnlyCollection<MonitoringEvent> Events => _events.AsReadOnly();
    public IReadOnlyCollection<object> DomainEvents => _domainEvents.AsReadOnly();

    private MonitoringSession()
    {
        Id = Guid.Empty;
    }

    public MonitoringSession(Guid deviceId)
    {
        if (deviceId == Guid.Empty)
            throw new ArgumentException("Device id cannot be empty.", nameof(deviceId));

        Id = Guid.NewGuid();
        DeviceId = deviceId;
        StartedAtUtc = DateTime.UtcNow;
    }

    public void RecordReading(SensorReading reading)
    {
        _readings.Add(reading);
        AddDomainEvent(new SensorReadingRecordedDomainEvent(reading.DeviceId, reading.SensorId));
    }

    public void RegisterAnomaly(string description)
    {
        var monitoringEvent = new MonitoringEvent(DeviceId, "AnomalyDetected", description);
        _events.Add(monitoringEvent);
        AddDomainEvent(new AnomalyDetectedDomainEvent(DeviceId, description));
    }

    public void ClearDomainEvents() => _domainEvents.Clear();

    private void AddDomainEvent(object domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }
}