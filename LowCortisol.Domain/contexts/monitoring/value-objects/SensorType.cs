namespace LowCortisol.Domain.Contexts.Monitoring.ValueObjects;

public sealed class SensorType : IEquatable<SensorType>
{
    public static readonly SensorType Flow = new("Flow");
    public static readonly SensorType Pressure = new("Pressure");
    public static readonly SensorType Gas = new("Gas");
    public static readonly SensorType Temperature = new("Temperature");

    public string Value { get; }

    public SensorType(string value)
    {
        value = value?.Trim() ?? string.Empty;

        var allowed = new[]
        {
            "Flow",
            "Pressure",
            "Gas",
            "Temperature"
        };

        if (!allowed.Contains(value))
            throw new ArgumentException("Sensor type is invalid.", nameof(value));

        Value = value;
    }

    public override string ToString() => Value;

    public bool Equals(SensorType? other) => other is not null && Value == other.Value;
    public override bool Equals(object? obj) => obj is SensorType other && Equals(other);
    public override int GetHashCode() => Value.GetHashCode();

    public static implicit operator string(SensorType sensorType) => sensorType.Value;
}