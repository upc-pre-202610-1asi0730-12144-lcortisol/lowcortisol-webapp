namespace LowCortisol.Application.Contexts.DeviceManagement.Commands;

public class RegisterDeviceCommand
{
    public Guid OwnerUserId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public string Status { get; set; } = "Online";
    public string Location { get; set; } = string.Empty;
    public decimal? ThresholdValue { get; set; }
    public string? ThresholdUnit { get; set; }
}