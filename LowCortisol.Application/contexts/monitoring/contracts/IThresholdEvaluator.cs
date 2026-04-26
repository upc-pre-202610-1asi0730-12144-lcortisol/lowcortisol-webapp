using LowCortisol.Domain.Contexts.Monitoring.Entities;

namespace LowCortisol.Application.Contexts.Monitoring.Contracts;

public interface IThresholdEvaluator
{
    ThresholdEvaluationResult Evaluate(SensorReading reading, decimal threshold);
}