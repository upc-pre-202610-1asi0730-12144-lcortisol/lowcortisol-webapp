using LowCortisol.Application.Contexts.DeviceManagement.Commands;
using LowCortisol.Application.Contexts.DeviceManagement.Services;

namespace LowCortisol.Application.Contexts.DeviceManagement.Handlers;

public class RegisterDeviceHandler
{
    private readonly IDeviceManagementService _deviceManagementService;

    public RegisterDeviceHandler(IDeviceManagementService deviceManagementService)
    {
        _deviceManagementService = deviceManagementService;
    }

    public Task<(bool Success, string Error)> HandleAsync(
        RegisterDeviceCommand command,
        CancellationToken cancellationToken = default)
    {
        return _deviceManagementService.RegisterAsync(command, cancellationToken);
    }
}