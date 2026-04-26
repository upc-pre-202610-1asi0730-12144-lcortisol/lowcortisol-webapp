namespace LowCortisol.Application.Contexts.Reporting.Queries;

public class GetConsumptionReportsByOwnerAndPeriodQuery
{
    public Guid OwnerUserId { get; set; }
    public string Period { get; set; } = string.Empty;
}