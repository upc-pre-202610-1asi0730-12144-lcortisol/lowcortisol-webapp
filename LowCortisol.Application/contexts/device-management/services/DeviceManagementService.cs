using LowCortisol.Application.Common.Interfaces;
using LowCortisol.Application.Contexts.DeviceManagement.Commands;
using LowCortisol.Application.Contexts.DeviceManagement.DTOs;
using LowCortisol.Domain.Contexts.DeviceManagement.Aggregates;
using LowCortisol.Domain.Contexts.DeviceManagement.Repositories;
using LowCortisol.Domain.Contexts.DeviceManagement.ValueObjects;

namespace LowCortisol.Application.Contexts.DeviceManagement.Services;

public class DeviceManagementService : IDeviceManagementService
{
    private readonly IDeviceRepository _deviceRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeviceManagementService(
        IDeviceRepository deviceRepository,
        IUnitOfWork unitOfWork)
    {
        _deviceRepository = deviceRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<(bool Success, string Error)> RegisterAsync(
        RegisterDeviceCommand command,
        CancellationToken cancellationToken = default)
    {
        try
        {
            if (command.OwnerUserId == Guid.Empty)
                return (false, "Owner user id is required.");

            Threshold? threshold = null;

            if (command.ThresholdValue.HasValue && !string.IsNullOrWhiteSpace(command.ThresholdUnit))
            {
                threshold = new Threshold(command.ThresholdValue.Value, command.ThresholdUnit!);
            }

            var device = new Device(
                Guid.NewGuid(),
                command.OwnerUserId,
                new DeviceName(command.Name),
                new DeviceType(command.Type),
                new DeviceStatus(command.Status),
                new Location(command.Location),
                threshold
            );

            await _deviceRepository.AddAsync(device, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return (true, string.Empty);
        }
        catch (Exception ex)
        {
            return (false, ex.Message);
        }
    }

    public async Task<(bool Success, string Error)> UpdateStatusAsync(
        UpdateDeviceStatusCommand command,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var device = await _deviceRepository.FindByIdAsync(command.DeviceId, cancellationToken);
            if (device is null)
                return (false, "Device not found.");

            device.ChangeStatus(new DeviceStatus(command.Status));

            await _deviceRepository.UpdateAsync(device, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return (true, string.Empty);
        }
        catch (Exception ex)
        {
            return (false, ex.Message);
        }
    }

    public async Task<IReadOnlyCollection<DeviceDto>> GetByOwnerAsync(
        Guid ownerUserId,
        CancellationToken cancellationToken = default)
    {
        var devices = await _deviceRepository.FindByOwnerUserIdAsync(ownerUserId, cancellationToken);

        return devices.Select(MapToDto).ToList();
    }

    private static DeviceDto MapToDto(Device device)
    {
        return new DeviceDto
        {
            Id = device.Id,
            OwnerUserId = device.OwnerUserId,
            Name = device.Name.Value,
            Type = device.Type.Value,
            Status = device.Status.Value,
            Location = device.Location.Value,
            ThresholdValue = device.Threshold?.Value,
            ThresholdUnit = device.Threshold?.Unit,
            SensorsCount = device.Sensors.Count
        };
    }
}