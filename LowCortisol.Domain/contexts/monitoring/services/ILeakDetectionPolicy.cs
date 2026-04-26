using LowCortisol.Domain.Contexts.Monitoring.Entities;

namespace LowCortisol.Domain.Contexts.Monitoring.Services;

public interface ILeakDetectionPolicy
{
    bool IsAnomalous(SensorReading reading, decimal threshold);
}