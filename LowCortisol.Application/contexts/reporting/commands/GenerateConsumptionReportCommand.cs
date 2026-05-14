namespace LowCortisol.Application.Contexts.Reporting.Commands;

public class GenerateConsumptionReportCommand
{
    public Guid OwnerUserId { get; set; }
    public string Period { get; set; } = string.Empty;
    public decimal TotalWaterConsumption { get; set; }
    public string TotalWaterUnit { get; set; } = "L";
    public decimal TotalGasConsumption { get; set; }
    public string TotalGasUnit { get; set; } = "m3";
}