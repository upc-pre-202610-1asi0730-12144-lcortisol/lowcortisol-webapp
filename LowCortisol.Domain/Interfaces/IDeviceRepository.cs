using LowCortisol.Domain.Entities;

namespace LowCortisol.Domain.Interfaces;

public interface IDeviceRepository
{
    Task<IEnumerable<Device>> GetAllDevicesAsync();
    
    Task UpdateDeviceStatusAsync(Guid id, bool isOpen);
    
    Task<IEnumerable<DeviceDetail>> GetUserDevicesAsync();
    
    Task<Report> GetReportsAsync();
}