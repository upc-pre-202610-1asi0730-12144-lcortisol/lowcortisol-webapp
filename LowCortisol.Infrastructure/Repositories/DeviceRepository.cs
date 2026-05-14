using LowCortisol.Domain.Entities;
using LowCortisol.Domain.Interfaces;

namespace LowCortisol.Infrastructure.Repositories;

public class DeviceRepository : IDeviceRepository
{
    private readonly List<Device> _devices = new()
    {
        new SmartValve("Water", 75.0),
        new SmartValve("Gas", 40.0)
    };

    public async Task<IEnumerable<Device>> GetAllDevicesAsync() => await Task.FromResult(_devices);

    public async Task UpdateDeviceStatusAsync(Guid id, bool isOpen) => await Task.CompletedTask;
    
    // Debes añadir esto dentro de la clase DeviceRepository : IDeviceRepository
    public async Task<IEnumerable<DeviceDetail>> GetUserDevicesAsync()
    {
        // Aquí implementas la lógica de obtención
        return await Task.FromResult(new List<DeviceDetail> 
        {
            new("Main water valve", "Smart valve", true),
            new("Kitchen sensor", "Leak sensor", true),
            new("Pressure gauge", "Sensor", false)
        });
    }

    public async Task<Report> GetReportsAsync()
    {
        // Aquí implementas la lógica de reporte
        return await Task.FromResult(new Report(1785, 965));
    }
}