using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Projekt_HKP.Model.Hardware;

namespace Projekt_HKP.Lib.DataAccess
{
    public interface IDataService
    {
        //Create
        bool AddComponent(HardwareComponent component);

        //Read
        IEnumerable<HardwareComponent> GetAllComponents();
        HardwareComponent GetComponentByUid(string uid);
        IEnumerable<HardwareComponent> GetComponentsOfRoom(int roomNumber);
        IEnumerable<HardwareComponent> GetComponentsOfBuilding(string buildingUid);

        //Update
        bool SaveAllComponents();

        //Delete
        bool DeleteComponent(string uid);
    }
}
