using LowCortisol.Domain.Contexts.DeviceManagement.ValueObjects;

namespace LowCortisol.Domain.Contexts.DeviceManagement.Entities;

public class Sensor
{
    public Guid Id { get; private set; }
    public string SensorType { get; private set; }
    public DeviceStatus Status { get; private set; }
    public Location Location { get; private set; }

    private Sensor()
    {
        Id = Guid.Empty;
        SensorType = string.Empty;
        Status = null!;
        Location = null!;
    }

    public Sensor(Guid id, string sensorType, DeviceStatus status, Location location)
    {
        if (string.IsNullOrWhiteSpace(sensorType))
            throw new ArgumentException("Sensor type cannot be empty.", nameof(sensorType));

        Id = id == Guid.Empty ? Guid.NewGuid() : id;
        SensorType = sensorType.Trim();
        Status = status;
        Location = location;
    }

    public void SetStatus(DeviceStatus status)
    {
        Status = status;
    }

    public void Relocate(Location location)
    {
        Location = location;
    }
}