using LowCortisol.Domain.Contexts.Reporting.Aggregates;

namespace LowCortisol.Domain.Contexts.Reporting.Services;

public class ReportingPolicy : IReportingPolicy
{
    public bool CanGenerate(ConsumptionReport consumptionReport)
    {
        return consumptionReport.OwnerUserId != Guid.Empty;
    }
}