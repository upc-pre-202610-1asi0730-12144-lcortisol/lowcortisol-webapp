using LowCortisol.Application.Contexts.DeviceManagement.DTOs;
using LowCortisol.Application.Contexts.DeviceManagement.Queries;
using LowCortisol.Application.Contexts.DeviceManagement.Services;

namespace LowCortisol.Application.Contexts.DeviceManagement.Handlers;

public class GetDevicesByOwnerHandler
{
    private readonly IDeviceManagementService _deviceManagementService;

    public GetDevicesByOwnerHandler(IDeviceManagementService deviceManagementService)
    {
        _deviceManagementService = deviceManagementService;
    }

    public Task<IReadOnlyCollection<DeviceDto>> HandleAsync(
        GetDevicesByOwnerQuery query,
        CancellationToken cancellationToken = default)
    {
        return _deviceManagementService.GetByOwnerAsync(query.OwnerUserId, cancellationToken);
    }
}