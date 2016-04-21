using System;
using Projekt_HKP.GUI.Common;

namespace Projekt_HKP.Model.Hardware
{
    public  abstract class HardwareComponent
    {
        public string UID { get; set; }
        public string Bezeichnung { get; set; }
        public string Beschreibung { get; set; }
        public DateTime BeschaffungsDatum { get; set; }
        public DateTime WartungsDatum { get; set; }
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
