namespace LowCortisol.Domain.Contexts.Monitoring.ValueObjects;

public sealed class MeasurementUnit : IEquatable<MeasurementUnit>
{
    public static readonly MeasurementUnit LitersPerMinute = new("L/min");
    public static readonly MeasurementUnit Bar = new("bar");
    public static readonly MeasurementUnit Ppm = new("ppm");
    public static readonly MeasurementUnit Celsius = new("°C");

    public string Value { get; }

    public MeasurementUnit(string value)
    {
        value = value?.Trim() ?? string.Empty;

        var allowed = new[]
        {
            "L/min",
            "bar",
            "ppm",
            "°C"
        };

        if (!allowed.Contains(value))
            throw new ArgumentException("Measurement unit is invalid.", nameof(value));

        Value = value;
    }

    public override string ToString() => Value;

    public bool Equals(MeasurementUnit? other) => other is not null && Value == other.Value;
    public override bool Equals(object? obj) => obj is MeasurementUnit other && Equals(other);
    public override int GetHashCode() => Value.GetHashCode();

    public static implicit operator string(MeasurementUnit unit) => unit.Value;
}