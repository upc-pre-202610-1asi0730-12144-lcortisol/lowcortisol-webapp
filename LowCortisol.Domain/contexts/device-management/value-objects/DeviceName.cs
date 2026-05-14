namespace LowCortisol.Domain.Contexts.DeviceManagement.ValueObjects;

public sealed class DeviceName : IEquatable<DeviceName>
{
    public string Value { get; }

    public DeviceName(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Device name cannot be empty.", nameof(value));

        Value = value.Trim();
    }

    public override string ToString() => Value;

    public bool Equals(DeviceName? other) => other is not null && Value == other.Value;
    public override bool Equals(object? obj) => obj is DeviceName other && Equals(other);
    public override int GetHashCode() => Value.GetHashCode();

    public static implicit operator string(DeviceName deviceName) => deviceName.Value;
}