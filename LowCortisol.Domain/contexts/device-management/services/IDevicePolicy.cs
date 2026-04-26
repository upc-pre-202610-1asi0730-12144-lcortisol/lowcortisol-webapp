using LowCortisol.Domain.Contexts.DeviceManagement.Aggregates;

namespace LowCortisol.Domain.Contexts.DeviceManagement.Services;

public interface IDevicePolicy
{
    bool CanBeActivated(Device device);
    bool CanAcceptSensor(Device device);
}