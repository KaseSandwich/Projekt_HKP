namespace Projekt_HKP.Model.Hardware
{
    public abstract class NetworkComponent: HardwareComponent
    {
        public double NetworkSpeed { get; set; }
        public int MaxConnections { get; set; }
    }
}
