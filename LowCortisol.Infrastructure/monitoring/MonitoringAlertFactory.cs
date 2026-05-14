using LowCortisol.Application.Contexts.Monitoring.Contracts;
using LowCortisol.Domain.Contexts.Monitoring.Entities;

namespace LowCortisol.Infrastructure.Monitoring;

public class MonitoringAlertFactory : IMonitoringAlertFactory
{
    public string CreateAlertMessage(SensorReading reading)
    {
        return
            $"Anomaly detected for device {reading.DeviceId}. " +
            $"Sensor {reading.SensorId} reported {reading.Value.Value} {reading.Unit.Value} " +
            $"at {reading.RecordedAtUtc:O}.";
    }
}