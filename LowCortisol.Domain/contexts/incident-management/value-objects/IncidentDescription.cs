namespace LowCortisol.Domain.Contexts.IncidentManagement.ValueObjects;

public sealed class IncidentDescription : IEquatable<IncidentDescription>
{
    public string Value { get; }

    public IncidentDescription(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Incident description cannot be empty.", nameof(value));

        Value = value.Trim();
    }

    public override string ToString() => Value;

    public bool Equals(IncidentDescription? other) => other is not null && Value == other.Value;
    public override bool Equals(object? obj) => obj is IncidentDescription other && Equals(other);
    public override int GetHashCode() => Value.GetHashCode();

    public static implicit operator string(IncidentDescription description) => description.Value;
}