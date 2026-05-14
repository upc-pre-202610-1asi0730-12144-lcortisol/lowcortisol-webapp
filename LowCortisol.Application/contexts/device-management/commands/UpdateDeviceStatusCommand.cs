namespace LowCortisol.Application.Contexts.DeviceManagement.Commands;

public class UpdateDeviceStatusCommand
{
    public Guid DeviceId { get; set; }
    public string Status { get; set; } = string.Empty;
}