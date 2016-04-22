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

        public Room GetRoomFromCompanyByUID(string uid)
        {          
                Room currentRoom = Company.Buildings.SelectMany(b => b.Rooms).FirstOrDefault(r => r.UID == uid);
                return currentRoom;   
        }

        public HardwareComponent GetComponentByUid(string uid)
        {
            return GetAllComponents().FirstOrDefault(c => c.UID == uid);
        }

        public IEnumerable<HardwareComponent> GetComponentsOfRoom(string roomUid)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<HardwareComponent> GetComponentsOfBuilding(string buildingUid)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Building> GetAllBuildings()
        {
            return Company.Buildings;
        }

        public IEnumerable<Room> GetAllRoomsForBuilding(string buildingUid)
        {
            return Company.Buildings.FirstOrDefault(b => b.UID == buildingUid)?.Rooms;
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

        public void UpdateComponent(HardwareComponent component)
        {
            var uid = component.UID;

            //var room = Company.Buildings.SelectMany(b => b.Rooms).FirstOrDefault(r => r.UID == component.RoomUID);
            //room.Components.Remove(GetAllComponents().ToList().FirstOrDefault(c => c.UID == uid));
            //room.Components.Add(component);

        }

        public bool DeleteComponent(string uid)
        {
            try
            {
                foreach(HardwareComponent comp in this.GetAllComponents())
                {
                    Room room;
                    if (comp.UID == uid)
                         room = GetRoomFromCompanyByUID(comp.RoomUID);
                }

                return true;
            } catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }
    }
}
