using LowCortisol.Domain.Entities;
using LowCortisol.Domain.Interfaces;

namespace LowCortisol.Application.Services;

public class DeviceService
{
    private readonly IDeviceRepository _repository;

    public DeviceService(IDeviceRepository repository) => _repository = repository;

    // Métodos existentes
    public async Task<IEnumerable<Device>> GetDashboardDevicesAsync() => await _repository.GetAllDevicesAsync();
    public async Task ToggleValveAsync(Guid id) => await _repository.UpdateDeviceStatusAsync(id, true);

    // --- AGREGA ESTOS MÉTODOS NUEVOS ---
    public async Task<IEnumerable<DeviceDetail>> GetUserDevicesAsync() 
    {
        return await _repository.GetUserDevicesAsync();
    }

    public async Task<Report> GetReportsAsync(string period) 
    {
        // Aquí simulas el filtrado real. En una app real, 
        // pasarías este 'period' a tu repositorio (DB).
        var baseData = await _repository.GetReportsAsync();
    
        // Multiplicamos para simular que 90D es más consumo que 7D
        double multiplier = period switch { "7D" => 0.3, "30D" => 1.0, "90D" => 2.5, _ => 1.0 };
    
        return new Report(baseData.WaterConsumption * multiplier, baseData.GasConsumption * multiplier);
    }
}