namespace LowCortisol.Domain.Contexts.IncidentManagement.ValueObjects;

public sealed class IncidentSeverity : IEquatable<IncidentSeverity>
{
    public static readonly IncidentSeverity Low = new("Low");
    public static readonly IncidentSeverity Medium = new("Medium");
    public static readonly IncidentSeverity High = new("High");
    public static readonly IncidentSeverity Critical = new("Critical");

    public string Value { get; }

    public IncidentSeverity(string value)
    {
        value = value?.Trim() ?? string.Empty;

        var allowed = new[]
        {
            "Low",
            "Medium",
            "High",
            "Critical"
        };

        if (!allowed.Contains(value))
            throw new ArgumentException("Incident severity is invalid.", nameof(value));

        Value = value;
    }

    public override string ToString() => Value;

    public bool Equals(IncidentSeverity? other) => other is not null && Value == other.Value;
    public override bool Equals(object? obj) => obj is IncidentSeverity other && Equals(other);
    public override int GetHashCode() => Value.GetHashCode();

    public static implicit operator string(IncidentSeverity severity) => severity.Value;
}