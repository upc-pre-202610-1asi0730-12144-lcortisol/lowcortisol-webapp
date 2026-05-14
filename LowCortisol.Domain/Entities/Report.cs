public class Report
{
    public double WaterConsumption { get; private set; }
    public double GasConsumption { get; private set; }

    public Report(double water, double gas)
    {
        WaterConsumption = water;
        GasConsumption = gas;
    }
}