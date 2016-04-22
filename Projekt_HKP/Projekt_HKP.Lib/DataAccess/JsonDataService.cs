using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Projekt_HKP.Model.Hardware;
using Projekt_HKP.Model.Orgaisation;

namespace Projekt_HKP.Lib.DataAccess
{
    public class JsonDataService : IDataService
    {
        private Company Company;

        public JsonDataService(string fileName)
        {
            if(!File.Exists(fileName))
                throw new FileNotFoundException("Datei wurde nicht gefunden!");
            var jsonStringLines = File.ReadAllLines(fileName);
            var jsonString = "";
            foreach (var line in jsonStringLines)
                jsonString += line;

            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All
            };

            Company = JsonConvert.DeserializeObject<Company>(jsonString, settings);
        }

        public JsonDataService(Company company)
        {
            Company = company;
        }

        public bool AddComponent(HardwareComponent component)
        {
            try
            {
                if (String.IsNullOrEmpty(component.RoomUID))
                {
                    var room = Company.Buildings[0]?.Rooms[0];
                    if (room != null)
                    {
                        component.RoomUID = room.UID;
                        room.Components.Add(component);
                    }
                }
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return false;
            }
        }

        public Company GetCompany()
        {
            return Company;
        }

        public IEnumerable<HardwareComponent> GetAllComponents()
        {
            return Company.Buildings.SelectMany(b => b.Rooms).SelectMany(r => r.Components);
        }
        
        public IEnumerable<Room> GetAllRooms()
        {
            return Company.Buildings.SelectMany(b => b.Rooms);
        } 

        public HardwareComponent GetComponentByUid(string uid)
        {
            return GetAllComponents().FirstOrDefault(c => c.UID == uid);
        }

        public IEnumerable<HardwareComponent> GetComponentsOfRoom(string roomUid)
        {
            return GetRoomByUid(roomUid).Components;
        }

        public IEnumerable<HardwareComponent> GetComponentsOfBuilding(string buildingUid)
        {
            return GetBuildingByUid(buildingUid).Rooms.SelectMany(r => r.Components);
        }

        public IEnumerable<Building> GetAllBuildings()
        {
            return Company.Buildings;
        }

        public IEnumerable<Room> GetAllRoomsForBuilding(string buildingUid)
        {
            return GetBuildingByUid(buildingUid)?.Rooms;
        }

        public bool SaveAllComponents(string fileName)
        {
            try
            {
                JsonSerializerSettings settings = new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.All
                };

                var jsonString = JsonConvert.SerializeObject(Company, settings);

                if (File.Exists(fileName))
                    File.Delete(fileName);

                using (var writer = new StreamWriter(fileName))
                {
                    writer.Write(jsonString);
                }
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return false;
            }
        }

        public bool DeleteComponent(string uid)
        {
            try
            {
                var comp = GetComponentByUid(uid);
                var room = GetRoomByUid(comp.RoomUID);
                room.Components.Remove(comp);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public Building GetBuildingByUid(string uid)
        {
            return GetAllBuildings().FirstOrDefault(b => b.UID == uid);
        }

        public Room GetRoomByUid(string uid)
        {
            return GetAllRooms().FirstOrDefault(r => r.UID == uid);
        }
    }
}
