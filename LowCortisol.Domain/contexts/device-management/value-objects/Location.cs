namespace LowCortisol.Domain.Contexts.DeviceManagement.ValueObjects;

public sealed class Location : IEquatable<Location>
{
    public string Value { get; }

    public Location(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Location cannot be empty.", nameof(value));

        Value = value.Trim();
    }

    public override string ToString() => Value;

    public bool Equals(Location? other) => other is not null && Value == other.Value;
    public override bool Equals(object? obj) => obj is Location other && Equals(other);
    public override int GetHashCode() => Value.GetHashCode();

    public static implicit operator string(Location location) => location.Value;
}