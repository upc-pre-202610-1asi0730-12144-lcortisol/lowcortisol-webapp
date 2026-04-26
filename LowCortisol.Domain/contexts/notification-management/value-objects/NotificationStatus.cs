namespace LowCortisol.Domain.Contexts.NotificationManagement.ValueObjects;

public sealed class NotificationStatus : IEquatable<NotificationStatus>
{
    public static readonly NotificationStatus Pending = new("Pending");
    public static readonly NotificationStatus Sent = new("Sent");
    public static readonly NotificationStatus Failed = new("Failed");

    public string Value { get; }

    public NotificationStatus(string value)
    {
        value = value?.Trim() ?? string.Empty;

        var allowed = new[]
        {
            "Pending",
            "Sent",
            "Failed"
        };

        if (!allowed.Contains(value))
            throw new ArgumentException("Notification status is invalid.", nameof(value));

        Value = value;
    }

    public override string ToString() => Value;

    public bool Equals(NotificationStatus? other) => other is not null && Value == other.Value;
    public override bool Equals(object? obj) => obj is NotificationStatus other && Equals(other);
    public override int GetHashCode() => Value.GetHashCode();

    public static implicit operator string(NotificationStatus status) => status.Value;
}