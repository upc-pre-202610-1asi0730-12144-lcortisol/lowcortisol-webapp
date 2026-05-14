namespace LowCortisol.Domain.Contexts.DeviceManagement.ValueObjects;

public sealed class DeviceStatus : IEquatable<DeviceStatus>
{
    public static readonly DeviceStatus Online = new("Online");
    public static readonly DeviceStatus Offline = new("Offline");
    public static readonly DeviceStatus Maintenance = new("Maintenance");

    public string Value { get; }

    public DeviceStatus(string value)
    {
        value = value?.Trim() ?? string.Empty;

        var allowed = new[]
        {
            "Online",
            "Offline",
            "Maintenance"
        };

        if (!allowed.Contains(value))
            throw new ArgumentException("Device status is invalid.", nameof(value));

        Value = value;
    }

    public override string ToString() => Value;

    public bool Equals(DeviceStatus? other) => other is not null && Value == other.Value;
    public override bool Equals(object? obj) => obj is DeviceStatus other && Equals(other);
    public override int GetHashCode() => Value.GetHashCode();

    public static implicit operator string(DeviceStatus deviceStatus) => deviceStatus.Value;
}