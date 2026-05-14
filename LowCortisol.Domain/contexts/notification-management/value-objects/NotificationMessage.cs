namespace LowCortisol.Domain.Contexts.NotificationManagement.ValueObjects;

public sealed class NotificationMessage : IEquatable<NotificationMessage>
{
    public string Title { get; }
    public string Body { get; }

    public NotificationMessage(string title, string body)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Notification title cannot be empty.", nameof(title));

        if (string.IsNullOrWhiteSpace(body))
            throw new ArgumentException("Notification body cannot be empty.", nameof(body));

        Title = title.Trim();
        Body = body.Trim();
    }

    public override string ToString() => $"{Title}: {Body}";

    public bool Equals(NotificationMessage? other) =>
        other is not null &&
        Title == other.Title &&
        Body == other.Body;

    public override bool Equals(object? obj) => obj is NotificationMessage other && Equals(other);
    public override int GetHashCode() => HashCode.Combine(Title, Body);
}