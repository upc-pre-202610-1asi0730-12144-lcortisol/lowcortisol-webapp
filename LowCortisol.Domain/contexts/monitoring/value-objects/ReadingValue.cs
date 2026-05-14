namespace LowCortisol.Domain.Contexts.Monitoring.ValueObjects;

public sealed class ReadingValue : IEquatable<ReadingValue>
{
    public decimal Value { get; }

    public ReadingValue(decimal value)
    {
        Value = value;
    }

    public override string ToString() => Value.ToString();

    public bool Equals(ReadingValue? other) => other is not null && Value == other.Value;
    public override bool Equals(object? obj) => obj is ReadingValue other && Equals(other);
    public override int GetHashCode() => Value.GetHashCode();

    public static implicit operator decimal(ReadingValue readingValue) => readingValue.Value;
}