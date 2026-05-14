namespace LowCortisol.Domain.Contexts.Reporting.Events;

public class ConsumptionReportGeneratedDomainEvent
{
    public Guid ReportId { get; }
    public DateTime OccurredOnUtc { get; }

    public ConsumptionReportGeneratedDomainEvent(Guid reportId)
    {
        ReportId = reportId;
        OccurredOnUtc = DateTime.UtcNow;
    }
}