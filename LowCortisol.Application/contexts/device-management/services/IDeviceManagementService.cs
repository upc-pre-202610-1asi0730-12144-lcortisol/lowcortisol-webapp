using LowCortisol.Application.Contexts.DeviceManagement.Commands;
using LowCortisol.Application.Contexts.DeviceManagement.DTOs;

namespace LowCortisol.Application.Contexts.DeviceManagement.Services;

public interface IDeviceManagementService
{
    Task<(bool Success, string Error)> RegisterAsync(
        RegisterDeviceCommand command,
        CancellationToken cancellationToken = default);

    Task<(bool Success, string Error)> UpdateStatusAsync(
        UpdateDeviceStatusCommand command,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyCollection<DeviceDto>> GetByOwnerAsync(
        Guid ownerUserId,
        CancellationToken cancellationToken = default);
}