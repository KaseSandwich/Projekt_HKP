using System;
using System.Xml.Serialization;
using Projekt_HKP.Model.Hardware.Implementations;

namespace Projekt_HKP.Model.Hardware
{
    [Serializable]
    [XmlInclude(typeof(Computer))]
    [XmlInclude(typeof(NetworkComponent))]
    [XmlInclude(typeof(Drucker))]
    public class HardwareComponent
    {
        public string UID { get; set; }
        public string RoomUID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime AcquisitionDate { get; set; }
        public DateTime MaintenanceDate { get; set; }
        public string Log { get; set; } 

        public HardwareComponent()
        {
            UID = Guid.NewGuid().ToString("D");
            AcquisitionDate = DateTime.Now;
            MaintenanceDate = DateTime.Now;
        }

        public HardwareComponent(string uid)
        {
            UID = uid;
            AcquisitionDate = DateTime.Now;
            MaintenanceDate = DateTime.Now;
        }

        public void AddLogEntry(string logEntry)
        {
            Log +=  $"\r\n{logEntry}";
        }
    }
}
