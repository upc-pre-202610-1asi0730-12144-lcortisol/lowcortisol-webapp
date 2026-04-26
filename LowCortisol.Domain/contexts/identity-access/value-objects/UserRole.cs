namespace LowCortisol.Domain.Contexts.IdentityAccess.ValueObjects;

public sealed class UserRole : IEquatable<UserRole>
{
    public static readonly UserRole Homeowner = new("Homeowner");
    public static readonly UserRole Admin = new("Admin");
    public static readonly UserRole Technician = new("Technician");

    public string Value { get; }

    public UserRole(string value)
    {
        value = value?.Trim() ?? string.Empty;

        if (value != "Homeowner" && value != "Admin" && value != "Technician")
            throw new ArgumentException("User role is invalid.", nameof(value));

        Value = value;
    }

    public override string ToString() => Value;

    public bool Equals(UserRole? other) => other is not null && Value == other.Value;
    public override bool Equals(object? obj) => obj is UserRole other && Equals(other);
    public override int GetHashCode() => Value.GetHashCode();

    public static implicit operator string(UserRole role) => role.Value;
}