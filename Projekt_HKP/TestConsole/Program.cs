using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
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
            ExportSetupJSON();
            //ExportTestJson();
        }

        private static void ExportSetupXML()
        {
            Company company = NewMethod();

            XmlSerializer ser = new XmlSerializer(typeof(Company));
            using (var stream = new FileStream($"{Environment.CurrentDirectory}\\SetupData.xml", FileMode.OpenOrCreate))
            {
                ser.Serialize(stream, company);
            }
        }

        private static void ExportSetupJSON()
        {
            Company company = NewMethod();

            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All
            };

            var jsonString = JsonConvert.SerializeObject(company, settings);

            if (File.Exists($"{Environment.CurrentDirectory}\\SetupData.json"))
                File.Delete($"{Environment.CurrentDirectory}\\SetupData.json");

            using (var writer = new StreamWriter($"{Environment.CurrentDirectory}\\SetupData.json"))
            {
                writer.Write(jsonString);
            }
        }

        private static Company NewMethod()
        {
            var company = new Company() { Name = "NewsPaper" };

            var building1 = new Building() { CompanyUID = company.UID, Name = "Verwaltung" };
            var building2 = new Building() { CompanyUID = company.UID, Name = "Vertrieb" };
            var building3 = new Building() { CompanyUID = company.UID, Name = "Textproduktion" };

            var room1 = new Room() { RoomNumber = 101, BuildingUID = building1.UID };
            var room2 = new Room() { RoomNumber = 102, BuildingUID = building1.UID };
            var room3 = new Room() { RoomNumber = 103, BuildingUID = building1.UID };

            var room4 = new Room() { RoomNumber = 201, BuildingUID = building2.UID };
            var room5 = new Room() { RoomNumber = 202, BuildingUID = building2.UID };
            var room6 = new Room() { RoomNumber = 203, BuildingUID = building2.UID };

            var room7 = new Room() { RoomNumber = 301, BuildingUID = building3.UID };
            var room8 = new Room() { RoomNumber = 302, BuildingUID = building3.UID };
            var room9 = new Room() { RoomNumber = 303, BuildingUID = building3.UID };

            building1.Rooms = new ObservableCollection<Room> { room1, room2, room3 };
            building2.Rooms = new ObservableCollection<Room> { room4, room5, room6 };
            building3.Rooms = new ObservableCollection<Room> { room7, room8, room9 };

            company.Buildings = new ObservableCollection<Building>() { building1, building2, building3 };
            return company;
        }

        private static void ExportTestJson()
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

            var room1 = new Room() {RoomNumber = 1, BuildingUID = buildings[0].UID};
            var room2 = new Room() {RoomNumber = 2, BuildingUID = buildings[1].UID};

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

            buildings[0].Rooms = new ObservableCollection<Room> {room1};
            buildings[1].Rooms = new ObservableCollection<Room> {room2};

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
