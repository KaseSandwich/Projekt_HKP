using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Projekt_HKP.Lib.ModelWrapper;
using Projekt_HKP.Model.Hardware;

namespace Projekt_HKP.Lib.DataAccess
{
    public interface IDataService
    {
        IEnumerable<HardwareComponent> GetAllComponents();
        IEnumerable<HardwareComponent> GetComponentsOfRoom(int roomNumber);
        IEnumerable<HardwareComponent> GetComponentsOfBuilding(string buildingUid);
    }
}
