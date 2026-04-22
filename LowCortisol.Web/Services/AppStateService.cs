namespace LowCortisol.Web.Services;

public class AppStateService
{
    public event Action? OnChange;

    public List<ValveState> Valves { get; } = new()
    {
        new()
        {
            Id = "water-main",
            Name = "Main water valve",
            Location = "Basement",
            Threshold = 12,
            Unit = "L/min",
            IsOn = true,
            Min = 0,
            Max = 20,
            Step = 1,
            Group = "Water"
        },
        new()
        {
            Id = "kitchen-line",
            Name = "Kitchen line",
            Location = "Ground floor",
            Threshold = 6,
            Unit = "L/min",
            IsOn = true,
            Min = 0,
            Max = 20,
            Step = 1,
            Group = "Water"
        },
        new()
        {
            Id = "gas-main",
            Name = "Gas main",
            Location = "Utility room",
            Threshold = 1.2,
            Unit = "bar",
            IsOn = true,
            Min = 0.1,
            Max = 3,
            Step = 0.1,
            Group = "Gas"
        },
        new()
        {
            Id = "garden-valve",
            Name = "Garden valve",
            Location = "Outdoor",
            Threshold = 8,
            Unit = "L/min",
            IsOn = false,
            Min = 0,
            Max = 20,
            Step = 1,
            Group = "Water"
        }
    };

    public List<DeviceState> Devices { get; } = new()
    {
        new() { Id = "water-main", Name = "Main water valve", Type = "Smart valve", Icon = "💧", IsOnline = true, Signal = 4 },
        new() { Id = "gas-main", Name = "Gas main", Type = "Smart valve", Icon = "🔥", IsOnline = true, Signal = 4 },
        new() { Id = "kitchen-sensor", Name = "Kitchen sensor", Type = "Leak sensor", Icon = "💧", IsOnline = true, Signal = 4 },
        new() { Id = "boiler-temp", Name = "Boiler temp", Type = "Sensor", Icon = "🌡", IsOnline = true, Signal = 2 },
        new() { Id = "pressure-gauge", Name = "Pressure gauge", Type = "Sensor", Icon = "◔", IsOnline = false, Signal = 1 },
        new() { Id = "hub-mini", Name = "Hub mini", Type = "Hub", Icon = "◉", IsOnline = true, Signal = 4 }
    };

    public int WaterPercentage { get; private set; } = 74;
    public int GasPercentage { get; private set; } = 37;
    
    

    public void SetWaterPercentage(int value)
    {
        WaterPercentage = Math.Clamp(value, 0, 100);
        NotifyStateChanged();
    }

    public void SetGasPercentage(int value)
    {
        GasPercentage = Math.Clamp(value, 0, 100);
        NotifyStateChanged();
    }

    public void EmergencyShutdown()
    {
        WaterPercentage = 0;
        GasPercentage = 0;

        foreach (var valve in Valves)
            valve.IsOn = false;

        foreach (var device in Devices.Where(d => d.Id == "water-main" || d.Id == "gas-main"))
            device.IsOnline = false;

        NotifyStateChanged();
    }

    public void ToggleValve(string id)
    {
        var valve = Valves.FirstOrDefault(v => v.Id == id);
        if (valve is null) return;

        valve.IsOn = !valve.IsOn;

        var device = Devices.FirstOrDefault(d => d.Id == id);
        if (device is not null)
            device.IsOnline = valve.IsOn;

        NotifyStateChanged();
    }

    public void UpdateValveThreshold(string id, double value)
    {
        var valve = Valves.FirstOrDefault(v => v.Id == id);
        if (valve is null) return;

        valve.Threshold = value;
        NotifyStateChanged();
    }

    public void ToggleDevice(string id)
    {
        var device = Devices.FirstOrDefault(d => d.Id == id);
        if (device is null) return;

        device.IsOnline = !device.IsOnline;

        // Si el dispositivo corresponde a una válvula, sincronízalo
        var valve = Valves.FirstOrDefault(v => v.Id == id);
        if (valve is not null)
            valve.IsOn = device.IsOnline;

        NotifyStateChanged();
    }

    public void AddDevice(DeviceState newDevice)
    {
        if (string.IsNullOrWhiteSpace(newDevice.Id))
            newDevice.Id = Guid.NewGuid().ToString("N");

        Devices.Add(newDevice);
        NotifyStateChanged();
    }

    public void NotifyStateChanged() => OnChange?.Invoke();
}

public class ValveState
{
    public string Id { get; set; } = "";
    public string Name { get; set; } = "";
    public string Location { get; set; } = "";
    public double Threshold { get; set; }
    public string Unit { get; set; } = "";
    public bool IsOn { get; set; }
    public double Min { get; set; }
    public double Max { get; set; }
    public double Step { get; set; }
    public string Group { get; set; } = "";
}

public class DeviceState
{
    public string Id { get; set; } = "";
    public string Name { get; set; } = "";
    public string Type { get; set; } = "";
    public string Icon { get; set; } = "";
    public bool IsOnline { get; set; }
    public int Signal { get; set; }
}