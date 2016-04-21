using System;

namespace Projekt_HKP.Model.Hardware
{
    public class HardwareComponent
    {
        public string UID { get; set; }
        public string RoomUID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime AcquisitionDate { get; set; }
        public DateTime MaintenanceDate { get; set; }
        public string Log { get; private set; } //ToDo refactor to own class or to list

        public HardwareComponent()
        {
            UID = Guid.NewGuid().ToString("D");
        }

        public HardwareComponent(string uid)
        {
            UID = uid;
        }

        public void AddLogEntry(string logEntry)
        {
            Log +=  $"\r\n{logEntry}";
        }
    }
}
