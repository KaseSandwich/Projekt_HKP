using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Projekt_HKP.Model.Hardware;
using Projekt_HKP.Model.Hardware.Implementations;
using Projekt_HKP.Model.Orgaisation;

namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var company = new Company() {Name = "Meine Firma"};
            var buildings = new ObservableCollection<Building>
            {
                new Building()
                {
                    Name = "Gebäude1",
                    CompanyUID = company.UID
                },
                new Building()
                {
                    Name = "Gebäude2",
                    CompanyUID = company.UID
                }
            };

            var room1 = new Room() { RoomNumber = 1, BuildingUID = buildings[0].UID };
            var room2 = new Room() { RoomNumber = 2, BuildingUID = buildings[1].UID };

            var comps = new List<HardwareComponent>()
            {
                new DesktopPc()
                {
                    Name = "Mein Desktop PC",
                    Description = "Toller Rechner",
                    AcquisitionDate = new DateTime(2016, 1, 1),
                    MaintenanceDate = new DateTime(2016, 4, 1),
                    CpuClockSpeed = 3.0,
                    HardDiskSpace = 500,
                    NetworkSpeed = 100,
                    RamAmount = 16,
                    RoomUID = room1.UID
                },
                new Switch()
                {
                    Name = "Mein Switch",
                    Description = "Toller Switch",
                    AcquisitionDate = new DateTime(2016, 2, 1),
                    MaintenanceDate = new DateTime(2016, 3, 1),
                    NetworkSpeed = 50,
                    MaxConnections = 24,
                    NumberOfPorts = 24,
                    RoomUID = room1.UID
                }
            };

            room1.Components = comps;

            buildings[0].Rooms = new ObservableCollection<Room> { room1 };
            buildings[1].Rooms = new ObservableCollection<Room> { room2 };
            
            company.Buildings = buildings;

            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All
            };

            var jsonString = JsonConvert.SerializeObject(company, settings);

            if (File.Exists($"{Environment.CurrentDirectory}TestJson.json")) 
                File.Delete($"{Environment.CurrentDirectory}TestJson.json");

            using (var writer = new StreamWriter($"{Environment.CurrentDirectory}TestJson.json"))
            {
                writer.Write(jsonString);
            }

        }
    }
}
