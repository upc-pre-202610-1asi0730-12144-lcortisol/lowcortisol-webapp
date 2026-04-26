namespace LowCortisol.Domain.Contexts.DeviceManagement.Events;

public class DeviceRegisteredDomainEvent
{
    public Guid DeviceId { get; }
    public DateTime OccurredOnUtc { get; }

    public DeviceRegisteredDomainEvent(Guid deviceId)
    {
        DeviceId = deviceId;
        OccurredOnUtc = DateTime.UtcNow;
    }
}