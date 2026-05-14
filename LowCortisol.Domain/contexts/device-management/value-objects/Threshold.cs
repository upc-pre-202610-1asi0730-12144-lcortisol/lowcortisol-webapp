namespace LowCortisol.Domain.Contexts.DeviceManagement.ValueObjects;

public sealed class Threshold : IEquatable<Threshold>
{
    public decimal Value { get; }
    public string Unit { get; }

    public Threshold(decimal value, string unit)
    {
        if (value < 0)
            throw new ArgumentException("Threshold cannot be negative.", nameof(value));

        if (string.IsNullOrWhiteSpace(unit))
            throw new ArgumentException("Threshold unit cannot be empty.", nameof(unit));

        Value = value;
        Unit = unit.Trim();
    }

    public override string ToString() => $"{Value} {Unit}";

    public bool Equals(Threshold? other) =>
        other is not null &&
        Value == other.Value &&
        Unit == other.Unit;

    public override bool Equals(object? obj) => obj is Threshold other && Equals(other);
    public override int GetHashCode() => HashCode.Combine(Value, Unit);
}