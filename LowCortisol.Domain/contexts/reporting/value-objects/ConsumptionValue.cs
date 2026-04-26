namespace LowCortisol.Domain.Contexts.Reporting.ValueObjects;

public sealed class ConsumptionValue : IEquatable<ConsumptionValue>
{
    public decimal Value { get; }
    public string Unit { get; }

    public ConsumptionValue(decimal value, string unit)
    {
        if (value < 0)
            throw new ArgumentException("Consumption value cannot be negative.", nameof(value));

        if (string.IsNullOrWhiteSpace(unit))
            throw new ArgumentException("Consumption unit cannot be empty.", nameof(unit));

        Value = value;
        Unit = unit.Trim();
    }

    public override string ToString() => $"{Value} {Unit}";

    public bool Equals(ConsumptionValue? other) =>
        other is not null &&
        Value == other.Value &&
        Unit == other.Unit;

    public override bool Equals(object? obj) => obj is ConsumptionValue other && Equals(other);
    public override int GetHashCode() => HashCode.Combine(Value, Unit);
}