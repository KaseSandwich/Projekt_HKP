﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NUnit.Framework;
using Projekt_HKP.Lib.DataAccess;
using Projekt_HKP.Model.Hardware;
using Projekt_HKP.Model.Hardware.Implementations;
using Projekt_HKP.Model.Orgaisation;

namespace Projekt_HKP.Tests
{
    [TestFixture]
    public class JsonDataServiceTests
    {
        const string jsonString = "{\"$type\":\"Projekt_HKP.Model.Orgaisation.Company, Projekt_HKP.Model\",\"UID\":\"d7d9af8b-90fc-4290-b254-6b4e6cd1a267\",\"Name\":\"Meine Firma\",\"Buildings\":{\"$type\":\"System.Collections.ObjectModel.ObservableCollection`1[[Projekt_HKP.Model.Orgaisation.Building, Projekt_HKP.Model]], System\",\"$values\":[{\"$type\":\"Projekt_HKP.Model.Orgaisation.Building, Projekt_HKP.Model\",\"UID\":\"a3fce9dd-c3f9-4583-9050-ff9b71577301\",\"CompanyUID\":\"d7d9af8b-90fc-4290-b254-6b4e6cd1a267\",\"Name\":\"Gebäude1\",\"Rooms\":{\"$type\":\"System.Collections.ObjectModel.ObservableCollection`1[[Projekt_HKP.Model.Orgaisation.Room, Projekt_HKP.Model]], System\",\"$values\":[{\"$type\":\"Projekt_HKP.Model.Orgaisation.Room, Projekt_HKP.Model\",\"UID\":\"69f332ed-d072-47e2-8667-e693437f4089\",\"BuildingUID\":\"a3fce9dd-c3f9-4583-9050-ff9b71577301\",\"RoomNumber\":1,\"Components\":{\"$type\":\"System.Collections.Generic.List`1[[Projekt_HKP.Model.Hardware.HardwareComponent, Projekt_HKP.Model]], mscorlib\",\"$values\":[{\"$type\":\"Projekt_HKP.Model.Hardware.Implementations.DesktopPc, Projekt_HKP.Model\",\"CpuClockSpeed\":3.0,\"RamAmount\":16.0,\"HardDiskSpace\":500.0,\"NetworkSpeed\":100.0,\"UID\":\"668f3f5e-c41d-437d-9fc1-890e32d66d8f\",\"RoomUID\":\"69f332ed-d072-47e2-8667-e693437f4089\",\"Name\":\"Mein Desktop PC\",\"Description\":\"Toller Rechner\",\"AcquisitionDate\":\"2016-01-01T00:00:00\",\"MaintenanceDate\":\"2016-04-01T00:00:00\",\"Log\":null},{\"$type\":\"Projekt_HKP.Model.Hardware.Implementations.Switch, Projekt_HKP.Model\",\"NumberOfPorts\":24,\"NetworkSpeed\":50.0,\"MaxConnections\":24,\"UID\":\"9ab9b4b9-2d3c-497c-a8a3-3fe4c5312f2b\",\"RoomUID\":\"69f332ed-d072-47e2-8667-e693437f4089\",\"Name\":\"Mein Switch\",\"Description\":\"Toller Switch\",\"AcquisitionDate\":\"2016-02-01T00:00:00\",\"MaintenanceDate\":\"2016-03-01T00:00:00\",\"Log\":null}]}}]}},{\"$type\":\"Projekt_HKP.Model.Orgaisation.Building, Projekt_HKP.Model\",\"UID\":\"08726fcc-c0c4-49a0-83ca-575898f1d76b\",\"CompanyUID\":\"d7d9af8b-90fc-4290-b254-6b4e6cd1a267\",\"Name\":\"Gebäude2\",\"Rooms\":{\"$type\":\"System.Collections.ObjectModel.ObservableCollection`1[[Projekt_HKP.Model.Orgaisation.Room, Projekt_HKP.Model]], System\",\"$values\":[{\"$type\":\"Projekt_HKP.Model.Orgaisation.Room, Projekt_HKP.Model\",\"UID\":\"e5ebe64c-3a04-4566-ae56-ab6a833c5b56\",\"BuildingUID\":\"08726fcc-c0c4-49a0-83ca-575898f1d76b\",\"RoomNumber\":2,\"Components\":{\"$type\":\"System.Collections.Generic.List`1[[Projekt_HKP.Model.Hardware.HardwareComponent, Projekt_HKP.Model]], mscorlib\",\"$values\":[]}}]}}]}}";

        private JsonDataService Service { get; set; }

        [SetUp]
        public void SetUp()
        {
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All
            };

            var company = JsonConvert.DeserializeObject<Company>(jsonString, settings);
            Service = new JsonDataService(company);
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
            var comp = Service.GetComponentByUid("9ab9b4b9-2d3c-497c-a8a3-3fe4c5312f2b");

            Assert.That(comp, Is.TypeOf<Switch>());
        }

        [Test]
        [Category("Component")]
        public void GetComponentsOfBuilding_ValidUid_TwoComponents()
        {
            var comps = Service.GetComponentsOfBuilding("a3fce9dd-c3f9-4583-9050-ff9b71577301");

            Assert.That(comps.Count, Is.EqualTo(2));
        }

        [Test]
        [Category("Component")]
        public void GetComponentsOfRoom_ValidUid_OneComponents()
        {
            var comps = Service.GetComponentsOfRoom("69f332ed-d072-47e2-8667-e693437f4089");

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
            var rooms = Service.GetAllRoomsForBuilding("a3fce9dd-c3f9-4583-9050-ff9b71577301");

            Assert.That(rooms.Count, Is.EqualTo(1));
        }
        
        [Test]
        [Category("Room")]
        public void GetRoomByUID_ExistingUID_RommNumber1()
        {
            var room = Service.GetRoomByUid("69f332ed-d072-47e2-8667-e693437f4089");

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
            var building = Service.GetBuildingByUid("a3fce9dd-c3f9-4583-9050-ff9b71577301");
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
