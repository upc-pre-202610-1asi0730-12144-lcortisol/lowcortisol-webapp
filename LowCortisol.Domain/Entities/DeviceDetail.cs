public class DeviceDetail
{
    public string Name { get; private set; }
    public string Type { get; private set; }
    public bool IsOnline { get; private set; }

    public DeviceDetail(string name, string type, bool isOnline)
    {
        Name = name;
        Type = type;
        IsOnline = isOnline;
    }
}