namespace LowCortisol.Domain.Contexts.IdentityAccess.ValueObjects;

public sealed class PhoneNumber : IEquatable<PhoneNumber>
{
    public string Value { get; }

    public PhoneNumber(string value)
    {
        value ??= string.Empty;
        Value = value.Trim();
    }

    public override string ToString() => Value;

    public bool Equals(PhoneNumber? other) => other is not null && Value == other.Value;
    public override bool Equals(object? obj) => obj is PhoneNumber other && Equals(other);
    public override int GetHashCode() => Value.GetHashCode();

    public static implicit operator string(PhoneNumber phone) => phone.Value;
}