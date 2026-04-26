using LowCortisol.Application.Contexts.Monitoring.Contracts;
using LowCortisol.Domain.Contexts.Monitoring.Entities;

namespace LowCortisol.Infrastructure.Monitoring;

public class ThresholdEvaluator : IThresholdEvaluator
{
    public ThresholdEvaluationResult Evaluate(SensorReading reading, decimal threshold)
    {
        if (reading.Value.Value > threshold)
        {
            return ThresholdEvaluationResult.Exceeded(
                $"Threshold exceeded. Reading value {reading.Value.Value} is greater than allowed threshold {threshold}.");
        }

        return ThresholdEvaluationResult.NotExceeded(
            $"Reading value {reading.Value.Value} is within the allowed threshold {threshold}.");
    }
}