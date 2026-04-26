using LowCortisol.Domain.Contexts.Reporting.Aggregates;

namespace LowCortisol.Domain.Contexts.Reporting.Services;

public interface IReportingPolicy
{
    bool CanGenerate(ConsumptionReport consumptionReport);
}