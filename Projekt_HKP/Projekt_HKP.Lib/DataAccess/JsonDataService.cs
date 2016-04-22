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

        public bool AddComponent(HardwareComponent component)
        {
            try{
                Room currentRoom = Company.Buildings.SelectMany(b => b.Rooms).FirstOrDefault(r => r.UID == component.RoomUID);
                currentRoom.Components.Add(component);
                return true;
            } catch(Exception e)
            {
                Console.WriteLine(e);
                return false;
            }

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
            throw new NotImplementedException();
        }

        public IEnumerable<HardwareComponent> GetComponentsOfRoom(string roomUid)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<HardwareComponent> GetComponentsOfBuilding(string buildingUid)
        {
            throw new NotImplementedException();
        }

        public bool SaveAllComponents()
        {
            throw new NotImplementedException();
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
                return false;
                Console.WriteLine(e);
            }
        }
    }
}
