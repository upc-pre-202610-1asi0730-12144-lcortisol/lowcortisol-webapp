using LowCortisol.Domain.Contexts.Monitoring.ValueObjects;

namespace LowCortisol.Domain.Contexts.Monitoring.Entities;

public class SensorReading
{
    public Guid Id { get; private set; }
    public Guid DeviceId { get; private set; }
    public Guid SensorId { get; private set; }
    public SensorType SensorType { get; private set; }
    public ReadingValue Value { get; private set; }
    public MeasurementUnit Unit { get; private set; }
    public DateTime RecordedAtUtc { get; private set; }

    private SensorReading()
    {
        Id = Guid.Empty;
        SensorType = null!;
        Value = null!;
        Unit = null!;
    }

    public SensorReading(
        Guid deviceId,
        Guid sensorId,
        SensorType sensorType,
        ReadingValue value,
        MeasurementUnit unit,
        DateTime? recordedAtUtc = null)
    {
        if (deviceId == Guid.Empty)
            throw new ArgumentException("Device id cannot be empty.", nameof(deviceId));

        if (sensorId == Guid.Empty)
            throw new ArgumentException("Sensor id cannot be empty.", nameof(sensorId));

        Id = Guid.NewGuid();
        DeviceId = deviceId;
        SensorId = sensorId;
        SensorType = sensorType;
        Value = value;
        Unit = unit;
        RecordedAtUtc = recordedAtUtc ?? DateTime.UtcNow;
    }
}