namespace LowCortisol.Application.Contexts.DeviceManagement.DTOs;

public class DeviceDto
{
    public Guid Id { get; set; }
    public Guid OwnerUserId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public decimal? ThresholdValue { get; set; }
    public string? ThresholdUnit { get; set; }
    public int SensorsCount { get; set; }
}