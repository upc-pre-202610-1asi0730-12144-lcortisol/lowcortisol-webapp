namespace LowCortisol.Domain.Contexts.DeviceManagement.Events;

public class DeviceStatusChangedDomainEvent
{
    public Guid DeviceId { get; }
    public string NewStatus { get; }
    public DateTime OccurredOnUtc { get; }

    public DeviceStatusChangedDomainEvent(Guid deviceId, string newStatus)
    {
        DeviceId = deviceId;
        NewStatus = newStatus;
        OccurredOnUtc = DateTime.UtcNow;
    }
}