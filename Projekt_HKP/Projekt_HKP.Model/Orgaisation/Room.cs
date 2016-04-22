using System;
using System.Collections.Generic;
using Projekt_HKP.Model.Hardware;

namespace Projekt_HKP.Model.Orgaisation
{
    public class Room
    {
        public string UID { get; set; }
        public string BuildingUID { get; set; }
        public int RoomNumber { get; set; }
        public List<HardwareComponent> Components { get; set; }

        public Room()
        {
            UID = Guid.NewGuid().ToString("D");
            Components = new List<HardwareComponent>();
        }

        public override string ToString()
        {
            return RoomNumber.ToString();
        }
    }
}
