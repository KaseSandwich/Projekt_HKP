using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Projekt_HKP.Model.Hardware;
using Projekt_HKP.Model.Orgaisation;

namespace Projekt_HKP.Lib.DataAccess
{
    public interface IDataService
    {
        //Create
        bool AddComponent(HardwareComponent component);

        //Read
        Company GetCompany();
        IEnumerable<HardwareComponent> GetAllComponents();
        HardwareComponent GetComponentByUid(string uid);
        IEnumerable<HardwareComponent> GetComponentsOfRoom(string roomUid);
        IEnumerable<HardwareComponent> GetComponentsOfBuilding(string buildingUid);
        IEnumerable<Building> GetAllBuildings();
        IEnumerable<Room> GetAllRoomsForBuilding(string buildingUid);
        Room GetRoomByUid(string uid);
        Building GetBuildingByUid(string uid);

        //Update
        bool SaveAllComponents(string fileName);

        //Delete
        bool DeleteComponent(string uid);
        
    }
}

