namespace LowCortisol.Domain.Contexts.IdentityAccess.ValueObjects;

public sealed class PasswordHash : IEquatable<PasswordHash>
{
    public string Value { get; }

    public PasswordHash(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Password hash cannot be empty.", nameof(value));

        Value = value.Trim();
    }

    public override string ToString() => Value;

    public bool Equals(PasswordHash? other) => other is not null && Value == other.Value;
    public override bool Equals(object? obj) => obj is PasswordHash other && Equals(other);
    public override int GetHashCode() => Value.GetHashCode();

    public static implicit operator string(PasswordHash passwordHash) => passwordHash.Value;
}