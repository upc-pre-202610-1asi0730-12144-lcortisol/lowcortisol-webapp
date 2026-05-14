namespace LowCortisol.Domain.Contexts.Reporting.ValueObjects;

public sealed class ReportPeriod : IEquatable<ReportPeriod>
{
    public static readonly ReportPeriod SevenDays = new("7D");
    public static readonly ReportPeriod ThirtyDays = new("30D");
    public static readonly ReportPeriod NinetyDays = new("90D");

    public string Value { get; }

    public ReportPeriod(string value)
    {
        value = value?.Trim() ?? string.Empty;

        var allowed = new[]
        {
            "7D",
            "30D",
            "90D"
        };

        if (!allowed.Contains(value))
            throw new ArgumentException("Report period is invalid.", nameof(value));

        Value = value;
    }

    public override string ToString() => Value;

    public bool Equals(ReportPeriod? other) => other is not null && Value == other.Value;
    public override bool Equals(object? obj) => obj is ReportPeriod other && Equals(other);
    public override int GetHashCode() => Value.GetHashCode();

    public static implicit operator string(ReportPeriod reportPeriod) => reportPeriod.Value;
}