namespace LowCortisol.Domain.Entities;

public class SmartValve : Device
{
    public bool IsOpen { get; private set; }
    public double AutoShutThreshold { get; private set; }

    public SmartValve(string name, double threshold) : base(name, "Smart Valve")
    {
        AutoShutThreshold = threshold;
        IsOpen = true;
    }

    public void Toggle() => IsOpen = !IsOpen;
}