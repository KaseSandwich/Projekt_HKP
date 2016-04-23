using System;
using System.Xml.Serialization;
using Projekt_HKP.Model.Hardware.Implementations;

namespace Projekt_HKP.Model.Hardware
{
    [Serializable]
    [XmlInclude(typeof(Router))]
    [XmlInclude(typeof(AccessPoint))]
    [XmlInclude(typeof(Switch))]
    public class NetworkComponent: HardwareComponent
    {
        public double NetworkSpeed { get; set; }
        public int MaxConnections { get; set; }
    }
}
