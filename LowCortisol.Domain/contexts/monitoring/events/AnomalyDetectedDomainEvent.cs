namespace LowCortisol.Domain.Contexts.Monitoring.Events;

public class AnomalyDetectedDomainEvent
{
    public Guid DeviceId { get; }
    public string Description { get; }
    public DateTime OccurredOnUtc { get; }

    public AnomalyDetectedDomainEvent(Guid deviceId, string description)
    {
        DeviceId = deviceId;
        Description = description;
        OccurredOnUtc = DateTime.UtcNow;
    }
}