namespace LowCortisol.Application.Contexts.Reporting.DTOs;

public class ReportLineDto
{
    public Guid Id { get; set; }
    public string Label { get; set; } = string.Empty;
    public decimal WaterConsumption { get; set; }
    public string WaterUnit { get; set; } = string.Empty;
    public decimal GasConsumption { get; set; }
    public string GasUnit { get; set; } = string.Empty;
}