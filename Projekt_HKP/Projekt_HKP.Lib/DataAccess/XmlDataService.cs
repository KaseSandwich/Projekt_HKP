using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Newtonsoft.Json;
using Projekt_HKP.Model.Hardware;
using Projekt_HKP.Model.Orgaisation;

namespace Projekt_HKP.Lib.DataAccess
{
    public class XmlDataService : IDataService
    {
        private Company Company { get; set; }

        public XmlDataService(string fileName)
        {
            XmlSerializer ser = new XmlSerializer(typeof(Company));
            using (var stream = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                Company = (Company)ser.Deserialize(stream);
            }
        }

        public XmlDataService(Company company)
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
                XmlSerializer ser = new XmlSerializer(typeof(Company));
                using (var stream = new FileStream(fileName, FileMode.OpenOrCreate))
                {
                    ser.Serialize(stream, Company);
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
