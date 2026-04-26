namespace LowCortisol.Application.Contexts.DeviceManagement.Queries;

public class GetDevicesByOwnerQuery
{
    public Guid OwnerUserId { get; set; }
}