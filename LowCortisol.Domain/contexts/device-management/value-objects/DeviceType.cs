namespace LowCortisol.Domain.Contexts.DeviceManagement.ValueObjects;

public sealed class DeviceType : IEquatable<DeviceType>
{
    public static readonly DeviceType SmartValve = new("SmartValve");
    public static readonly DeviceType LeakSensor = new("LeakSensor");
    public static readonly DeviceType TemperatureSensor = new("TemperatureSensor");
    public static readonly DeviceType PressureGauge = new("PressureGauge");
    public static readonly DeviceType Hub = new("Hub");

    public string Value { get; }

    public DeviceType(string value)
    {
        value = value?.Trim() ?? string.Empty;

        var allowed = new[]
        {
            "SmartValve",
            "LeakSensor",
            "TemperatureSensor",
            "PressureGauge",
            "Hub"
        };

        if (!allowed.Contains(value))
            throw new ArgumentException("Device type is invalid.", nameof(value));

        Value = value;
    }

    public override string ToString() => Value;

    public bool Equals(DeviceType? other) => other is not null && Value == other.Value;
    public override bool Equals(object? obj) => obj is DeviceType other && Equals(other);
    public override int GetHashCode() => Value.GetHashCode();

    public static implicit operator string(DeviceType deviceType) => deviceType.Value;
}