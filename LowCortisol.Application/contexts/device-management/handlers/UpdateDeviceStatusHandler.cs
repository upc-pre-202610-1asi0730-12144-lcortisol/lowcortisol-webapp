using LowCortisol.Application.Contexts.DeviceManagement.Commands;
using LowCortisol.Application.Contexts.DeviceManagement.Services;

namespace LowCortisol.Application.Contexts.DeviceManagement.Handlers;

public class UpdateDeviceStatusHandler
{
    private readonly IDeviceManagementService _deviceManagementService;

    public UpdateDeviceStatusHandler(IDeviceManagementService deviceManagementService)
    {
        _deviceManagementService = deviceManagementService;
    }

    public Task<(bool Success, string Error)> HandleAsync(
        UpdateDeviceStatusCommand command,
        CancellationToken cancellationToken = default)
    {
        return _deviceManagementService.UpdateStatusAsync(command, cancellationToken);
    }
}