using LowCortisol.Domain.Contexts.Reporting.ValueObjects;

namespace LowCortisol.Domain.Contexts.Reporting.Entities;

public class ReportLine
{
    public Guid Id { get; private set; }
    public string Label { get; private set; }
    public ConsumptionValue WaterConsumption { get; private set; }
    public ConsumptionValue GasConsumption { get; private set; }

    private ReportLine()
    {
        Id = Guid.Empty;
        Label = string.Empty;
        WaterConsumption = null!;
        GasConsumption = null!;
    }

    public ReportLine(string label, ConsumptionValue waterConsumption, ConsumptionValue gasConsumption)
    {
        if (string.IsNullOrWhiteSpace(label))
            throw new ArgumentException("Report line label cannot be empty.", nameof(label));

        Id = Guid.NewGuid();
        Label = label.Trim();
        WaterConsumption = waterConsumption;
        GasConsumption = gasConsumption;
    }
}