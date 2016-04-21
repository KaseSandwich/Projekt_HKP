namespace Projekt_HKP.Model.Hardware
{
    public abstract class Computer : HardwareComponent
    {
        public double CpuClockSpeed { get; set; }
        public double RamAmount { get; set; }
        public double HardDiskSpace { get; set; }
        public double NetworkSpeed { get; set; }

    }
}
