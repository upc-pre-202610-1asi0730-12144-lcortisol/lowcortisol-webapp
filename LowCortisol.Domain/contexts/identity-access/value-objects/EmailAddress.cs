namespace LowCortisol.Domain.Contexts.IdentityAccess.ValueObjects;

public sealed class EmailAddress : IEquatable<EmailAddress>
{
    public string Value { get; }

    public EmailAddress(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Email cannot be empty.", nameof(value));

        value = value.Trim().ToLowerInvariant();

        if (!value.Contains('@'))
            throw new ArgumentException("Email format is invalid.", nameof(value));

        Value = value;
    }

    public override string ToString() => Value;

    public bool Equals(EmailAddress? other) => other is not null && Value == other.Value;
    public override bool Equals(object? obj) => obj is EmailAddress other && Equals(other);
    public override int GetHashCode() => Value.GetHashCode();

    public static implicit operator string(EmailAddress email) => email.Value;
}