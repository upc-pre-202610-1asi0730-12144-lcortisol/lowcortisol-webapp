using LowCortisol.Domain.Contexts.DeviceManagement.Aggregates;

namespace LowCortisol.Domain.Contexts.DeviceManagement.Services;

public class DevicePolicy : IDevicePolicy
{
    public bool CanBeActivated(Device device)
    {
        return device.Status.Value != "Maintenance";
    }

    public bool CanAcceptSensor(Device device)
    {
        return device.Sensors.Count < 10;
    }
}