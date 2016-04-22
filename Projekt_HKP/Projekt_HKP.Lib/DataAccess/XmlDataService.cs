using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Projekt_HKP.Model.Hardware;
using Projekt_HKP.Model.Orgaisation;

namespace Projekt_HKP.Lib.DataAccess
{
    public class XmlDataService : IDataService
    {
        public XmlDataService(string fileName)
        {
                
        }

        public XmlDataService(Company company)
        {

        }

        public bool AddComponent(HardwareComponent component)
        {
            throw new NotImplementedException();
        }

        public Company GetCompany()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<HardwareComponent> GetAllComponents()
        {
            throw new NotImplementedException();
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

        public bool SaveAllComponents(string fileName)
        {
            throw new NotImplementedException();
        }

        public void UpdateComponent(HardwareComponent component)
        {
            throw new NotImplementedException();
        }

        public bool DeleteComponent(string uid)
        {
            throw new NotImplementedException();
        }
    }
}
