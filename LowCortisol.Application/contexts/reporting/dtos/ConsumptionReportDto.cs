namespace LowCortisol.Application.Contexts.Reporting.DTOs;

public class ConsumptionReportDto
{
    public Guid Id { get; set; }
    public Guid OwnerUserId { get; set; }
    public string Period { get; set; } = string.Empty;
    public decimal TotalWaterConsumption { get; set; }
    public string TotalWaterUnit { get; set; } = string.Empty;
    public decimal TotalGasConsumption { get; set; }
    public string TotalGasUnit { get; set; } = string.Empty;
    public DateTime GeneratedAtUtc { get; set; }
    public List<ReportLineDto> Lines { get; set; } = new();
}