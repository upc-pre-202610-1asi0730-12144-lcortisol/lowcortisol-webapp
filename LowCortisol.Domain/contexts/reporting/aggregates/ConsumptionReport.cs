using LowCortisol.Domain.Contexts.Reporting.Entities;
using LowCortisol.Domain.Contexts.Reporting.Events;
using LowCortisol.Domain.Contexts.Reporting.ValueObjects;

namespace LowCortisol.Domain.Contexts.Reporting.Aggregates;

public class ConsumptionReport
{
    private readonly List<object> _domainEvents = new();
    private readonly List<ReportLine> _lines = new();

    public Guid Id { get; private set; }
    public Guid OwnerUserId { get; private set; }
    public ReportPeriod Period { get; private set; }
    public ConsumptionValue TotalWaterConsumption { get; private set; }
    public ConsumptionValue TotalGasConsumption { get; private set; }
    public DateTime GeneratedAtUtc { get; private set; }

    public IReadOnlyCollection<ReportLine> Lines => _lines.AsReadOnly();
    public IReadOnlyCollection<object> DomainEvents => _domainEvents.AsReadOnly();

    private ConsumptionReport()
    {
        Id = Guid.Empty;
        Period = null!;
        TotalWaterConsumption = null!;
        TotalGasConsumption = null!;
    }

    public ConsumptionReport(
        Guid ownerUserId,
        ReportPeriod period,
        ConsumptionValue totalWaterConsumption,
        ConsumptionValue totalGasConsumption)
    {
        if (ownerUserId == Guid.Empty)
            throw new ArgumentException("Owner user id cannot be empty.", nameof(ownerUserId));

        Id = Guid.NewGuid();
        OwnerUserId = ownerUserId;
        Period = period;
        TotalWaterConsumption = totalWaterConsumption;
        TotalGasConsumption = totalGasConsumption;
        GeneratedAtUtc = DateTime.UtcNow;

        AddDomainEvent(new ConsumptionReportGeneratedDomainEvent(Id));
    }

    public void AddLine(ReportLine line)
    {
        _lines.Add(line);
    }

    public void ClearDomainEvents() => _domainEvents.Clear();

    private void AddDomainEvent(object domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }
}