using System;
using System.Xml.Serialization;
using Projekt_HKP.Model.Hardware.Implementations;

namespace Projekt_HKP.Model.Hardware
{
    [Serializable]
    [XmlInclude(typeof(DesktopPc))]
    [XmlInclude(typeof(Server))]
    [XmlInclude(typeof(Notebook))]
    public class Computer : HardwareComponent
    {
        public double CpuClockSpeed { get; set; }
        public double RamAmount { get; set; }
        public double HardDiskSpace { get; set; }
        public double NetworkSpeed { get; set; }

    }
}
