using LowCortisol.Domain.Contexts.Monitoring.Entities;

namespace LowCortisol.Domain.Contexts.Monitoring.Services;

public class LeakDetectionPolicy : ILeakDetectionPolicy
{
    public bool IsAnomalous(SensorReading reading, decimal threshold)
    {
        return reading.Value.Value > threshold;
    }
}