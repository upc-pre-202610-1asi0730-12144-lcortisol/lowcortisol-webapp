using LowCortisol.Domain.Contexts.Monitoring.Entities;

namespace LowCortisol.Application.Contexts.Monitoring.Contracts;

public interface IMonitoringAlertFactory
{
    string CreateAlertMessage(SensorReading reading);
}