using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using Newtonsoft.Json;
using NUnit.Framework;
using Projekt_HKP.Lib.DataAccess;
using Projekt_HKP.Model.Hardware;
using Projekt_HKP.Model.Hardware.Implementations;
using Projekt_HKP.Model.Orgaisation;

namespace Projekt_HKP.Tests
{
    [TestFixture]
    public class XmlDataServiceTests
    {
        const string xmlString =
            "<?xml version=\"1.0\" encoding=\"utf-16\"?>\r\n<Company xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">\r\n  <UID>28e95769-3e8e-4100-b497-089d065151f8</UID>\r\n  <Name>Meine Firma</Name>\r\n  <Buildings>\r\n    <Building>\r\n      <UID>e6ceba76-a9d3-4337-9bfe-d182e7d90a4e</UID>\r\n      <CompanyUID>28e95769-3e8e-4100-b497-089d065151f8</CompanyUID>\r\n      <Name>Gebäude1</Name>\r\n      <Rooms>\r\n        <Room>\r\n          <UID>5f3e95a7-844e-4cb8-a031-d233642ce2c4</UID>\r\n          <BuildingUID>e6ceba76-a9d3-4337-9bfe-d182e7d90a4e</BuildingUID>\r\n          <RoomNumber>1</RoomNumber>\r\n          <Components>\r\n            <HardwareComponent xsi:type=\"DesktopPc\">\r\n              <UID>4eb60a27-d0de-4f8e-a1b8-b67ec2991c98</UID>\r\n              <RoomUID>5f3e95a7-844e-4cb8-a031-d233642ce2c4</RoomUID>\r\n              <Name>Mein Desktop PC</Name>\r\n              <Description>Toller Rechner</Description>\r\n              <AcquisitionDate>2016-01-01T00:00:00</AcquisitionDate>\r\n              <MaintenanceDate>2016-04-01T00:00:00</MaintenanceDate>\r\n              <CpuClockSpeed>3</CpuClockSpeed>\r\n              <RamAmount>16</RamAmount>\r\n              <HardDiskSpace>500</HardDiskSpace>\r\n              <NetworkSpeed>100</NetworkSpeed>\r\n            </HardwareComponent>\r\n            <HardwareComponent xsi:type=\"Switch\">\r\n              <UID>3c25b438-92b1-40eb-a31d-4db6da54d836</UID>\r\n              <RoomUID>5f3e95a7-844e-4cb8-a031-d233642ce2c4</RoomUID>\r\n              <Name>Mein Switch</Name>\r\n              <Description>Toller Switch</Description>\r\n              <AcquisitionDate>2016-02-01T00:00:00</AcquisitionDate>\r\n              <MaintenanceDate>2016-03-01T00:00:00</MaintenanceDate>\r\n              <NetworkSpeed>50</NetworkSpeed>\r\n              <MaxConnections>24</MaxConnections>\r\n              <NumberOfPorts>24</NumberOfPorts>\r\n            </HardwareComponent>\r\n          </Components>\r\n        </Room>\r\n      </Rooms>\r\n    </Building>\r\n    <Building>\r\n      <UID>8c64bb74-f4f5-4d94-8788-6ea06d76514c</UID>\r\n      <CompanyUID>28e95769-3e8e-4100-b497-089d065151f8</CompanyUID>\r\n      <Name>Gebäude2</Name>\r\n      <Rooms>\r\n        <Room>\r\n          <UID>6ea8c6b6-c951-405d-b30b-0353b048dca2</UID>\r\n          <BuildingUID>8c64bb74-f4f5-4d94-8788-6ea06d76514c</BuildingUID>\r\n          <RoomNumber>2</RoomNumber>\r\n          <Components />\r\n        </Room>\r\n      </Rooms>\r\n    </Building>\r\n  </Buildings>\r\n</Company>";


        private XmlDataService Service { get; set; }

        [SetUp]
        public void SetUp()
        {
            XmlSerializer ser = new XmlSerializer(typeof(Company));
            Company result;

            using (TextReader reader = new StringReader(xmlString))
            { 
                result = (Company)ser.Deserialize(reader);
            }
            Service = new XmlDataService(result);
        }

        #region Components

        [Test]
        [Category("Component")]
        public void GetAllComponents_ValidJsonFile_NotNull()
        {
            var comps = Service.GetAllComponents();
            
            Assert.That(comps, Is.Not.Null);
        }

        [Test]
        [Category("Component")]
        public void GetAllComponents_ValidJsonFileWithTwoComponentens_ListWithTwoComponents()
        {
            var comps = Service.GetAllComponents();

            Assert.That(comps.ToList(), Has.Count.EqualTo(2));
        }

        [Test]
        [Category("Component")]
        public void GetAllComponents_InvalidJsonFile_ThrowsFileNotFOundException()
        {
            Assert.Throws(typeof(FileNotFoundException), () => new JsonDataService("C:\\Test"));
        }

        [Test]
        [Category("Component")]
        public void GetComponentByUid_ValidUid_IsSwitch()
        {
            var comp = Service.GetComponentByUid("3c25b438-92b1-40eb-a31d-4db6da54d836");

            Assert.That(comp, Is.TypeOf<Switch>());
        }

        [Test]
        [Category("Component")]
        public void GetComponentsOfBuilding_ValidUid_TwoComponents()
        {
            var comps = Service.GetComponentsOfBuilding("e6ceba76-a9d3-4337-9bfe-d182e7d90a4e");

            Assert.That(comps.Count, Is.EqualTo(2));
        }

        [Test]
        [Category("Component")]
        public void GetComponentsOfRoom_ValidUid_OneComponents()
        {
            var comps = Service.GetComponentsOfRoom("5f3e95a7-844e-4cb8-a031-d233642ce2c4");

            Assert.That(comps.Count, Is.EqualTo(2));
        }

        [Test]
        [Category("Component")]
        public void AddComponent_ValidComponent_ThreeComponents()
        {
            var component = new Switch();
            Service.AddComponent(component);
            var comps = Service.GetAllComponents();


            Assert.That(comps.Count, Is.EqualTo(3));
        }

        [Test]
        [Category("Component")]
        public void DeleteComponent_ValidComponent_OneComponent()
        {
            var comps = Service.GetAllComponents();
            var comp = comps.ToList()[0];
            Service.DeleteComponent(comp.UID);
            comps = Service.GetAllComponents();

            Assert.That(comps.Count, Is.EqualTo(1));
        }

        #endregion

        #region RoomTests
        [Test]
        [Category("Room")]
        public void GetAllRooms_ValdiJsonWith2Rooms_TwoRooms()
        {
            var rooms = Service.GetAllRooms();

            Assert.That(rooms.Count, Is.EqualTo(2));
        }

        [Test]
        [Category("Room")]
        public void GetAllRoomsForBuilding_ValidBuilding_TwoRooms()
        {
            var rooms = Service.GetAllRoomsForBuilding("e6ceba76-a9d3-4337-9bfe-d182e7d90a4e");

            Assert.That(rooms.Count, Is.EqualTo(1));
        }
        
        [Test]
        [Category("Room")]
        public void GetRoomByUID_ExistingUID_RommNumber1()
        {
            var room = Service.GetRoomByUid("5f3e95a7-844e-4cb8-a031-d233642ce2c4");

            Assert.That(room.RoomNumber, Is.EqualTo(1));
        }

        [Test]
        [Category("Room")]
        public void GetRoomByUID_NonExistingUID_RoomIsNull()
        {
            var room = Service.GetRoomByUid("12345");

            Assert.That(room, Is.Null);
        }
        #endregion

        #region BuildingTests
        [Test]
        [Category("Building")]
        public void GetAllBuildings_ValdiJsonWith2Rooms_TwoBuildings()
        {
            var buildings = Service.GetAllBuildings();

            Assert.That(buildings.Count, Is.EqualTo(2));
        }

        [Test]
        [Category("Building")]
        public void GetBuildingByUid_ExistingUID_Gebaeude1()
        {
            var building = Service.GetBuildingByUid("e6ceba76-a9d3-4337-9bfe-d182e7d90a4e");
            Assert.That(building.Name, Is.EqualTo("Gebäude1"));
        }

        [Test]
        [Category("Building")]
        public void GetBuildingByUid_NonExistingUID_BuildingIsNull()
        {
            var building = Service.GetBuildingByUid("12345");

            Assert.That(building, Is.Null);
        }
        #endregion
    }
}
