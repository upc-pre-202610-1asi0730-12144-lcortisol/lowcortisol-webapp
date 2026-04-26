using LowCortisol.Domain.Contexts.DeviceManagement.Entities;
using LowCortisol.Domain.Contexts.DeviceManagement.Events;
using LowCortisol.Domain.Contexts.DeviceManagement.ValueObjects;

namespace LowCortisol.Domain.Contexts.DeviceManagement.Aggregates;

public class Device
{
    private readonly List<object> _domainEvents = new();
    private readonly List<Sensor> _sensors = new();

    public Guid Id { get; private set; }
    public Guid OwnerUserId { get; private set; }
    public DeviceName Name { get; private set; }
    public DeviceType Type { get; private set; }
    public DeviceStatus Status { get; private set; }
    public Location Location { get; private set; }
    public Threshold? Threshold { get; private set; }

    public IReadOnlyCollection<Sensor> Sensors => _sensors.AsReadOnly();
    public IReadOnlyCollection<object> DomainEvents => _domainEvents.AsReadOnly();

    private Device()
    {
        Id = Guid.Empty;
        Name = null!;
        Type = null!;
        Status = null!;
        Location = null!;
    }

    public Device(
        Guid id,
        Guid ownerUserId,
        DeviceName name,
        DeviceType type,
        DeviceStatus status,
        Location location,
        Threshold? threshold = null)
    {
        if (ownerUserId == Guid.Empty)
            throw new ArgumentException("Owner user id cannot be empty.", nameof(ownerUserId));

        Id = id == Guid.Empty ? Guid.NewGuid() : id;
        OwnerUserId = ownerUserId;
        Name = name;
        Type = type;
        Status = status;
        Location = location;
        Threshold = threshold;

        AddDomainEvent(new DeviceRegisteredDomainEvent(Id));
    }

    public void Rename(DeviceName name) => Name = name;

    public void Relocate(Location location) => Location = location;

    public void ChangeStatus(DeviceStatus status)
    {
        if (Status.Equals(status))
            return;

        Status = status;
        AddDomainEvent(new DeviceStatusChangedDomainEvent(Id, status.Value));
    }

    public void SetThreshold(Threshold threshold)
    {
        Threshold = threshold;
    }

    public void AddSensor(Sensor sensor)
    {
        _sensors.Add(sensor);
    }

    public void ClearDomainEvents() => _domainEvents.Clear();

    private void AddDomainEvent(object domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }
}