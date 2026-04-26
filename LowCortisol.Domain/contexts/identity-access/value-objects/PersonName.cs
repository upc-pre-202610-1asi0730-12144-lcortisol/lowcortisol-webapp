namespace LowCortisol.Domain.Contexts.IdentityAccess.ValueObjects;

public sealed class PersonName : IEquatable<PersonName>
{
    public string Value { get; }

    public PersonName(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Name cannot be empty.", nameof(value));

        Value = value.Trim();
    }

    public override string ToString() => Value;

    public bool Equals(PersonName? other) => other is not null && Value == other.Value;
    public override bool Equals(object? obj) => obj is PersonName other && Equals(other);
    public override int GetHashCode() => Value.GetHashCode();

    public static implicit operator string(PersonName name) => name.Value;
}