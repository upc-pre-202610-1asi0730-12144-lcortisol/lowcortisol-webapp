namespace LowCortisol.Domain.Contexts.NotificationManagement.ValueObjects;

public sealed class NotificationChannel : IEquatable<NotificationChannel>
{
    public static readonly NotificationChannel Push = new("Push");
    public static readonly NotificationChannel Email = new("Email");
    public static readonly NotificationChannel Sms = new("Sms");

    public string Value { get; }

    public NotificationChannel(string value)
    {
        value = value?.Trim() ?? string.Empty;

        var allowed = new[]
        {
            "Push",
            "Email",
            "Sms"
        };

        if (!allowed.Contains(value))
            throw new ArgumentException("Notification channel is invalid.", nameof(value));

        Value = value;
    }

    public override string ToString() => Value;

    public bool Equals(NotificationChannel? other) => other is not null && Value == other.Value;
    public override bool Equals(object? obj) => obj is NotificationChannel other && Equals(other);
    public override int GetHashCode() => Value.GetHashCode();

    public static implicit operator string(NotificationChannel channel) => channel.Value;
}