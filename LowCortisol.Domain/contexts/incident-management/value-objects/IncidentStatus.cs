namespace LowCortisol.Domain.Contexts.IncidentManagement.ValueObjects;

public sealed class IncidentStatus : IEquatable<IncidentStatus>
{
    public static readonly IncidentStatus Open = new("Open");
    public static readonly IncidentStatus Assigned = new("Assigned");
    public static readonly IncidentStatus InProgress = new("InProgress");
    public static readonly IncidentStatus Resolved = new("Resolved");
    public static readonly IncidentStatus Closed = new("Closed");

    public string Value { get; }

    public IncidentStatus(string value)
    {
        value = value?.Trim() ?? string.Empty;

        var allowed = new[]
        {
            "Open",
            "Assigned",
            "InProgress",
            "Resolved",
            "Closed"
        };

        if (!allowed.Contains(value))
            throw new ArgumentException("Incident status is invalid.", nameof(value));

        Value = value;
    }

    public override string ToString() => Value;

    public bool Equals(IncidentStatus? other) => other is not null && Value == other.Value;
    public override bool Equals(object? obj) => obj is IncidentStatus other && Equals(other);
    public override int GetHashCode() => Value.GetHashCode();

    public static implicit operator string(IncidentStatus status) => status.Value;
}