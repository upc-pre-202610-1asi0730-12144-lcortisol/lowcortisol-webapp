namespace LowCortisol.Domain.Entities;

public abstract class Device
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Type { get; private set; }
    public bool IsOnline { get; private set; }

    protected Device(string name, string type)
    {
        Id = Guid.NewGuid();
        Name = name;
        Type = type;
        IsOnline = true;
    }
}