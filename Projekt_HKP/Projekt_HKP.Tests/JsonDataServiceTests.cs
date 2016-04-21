using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        [Test]
        public void GetAllComponents_ValidJsonFile_NotNull()
        {
            var dataService = new JsonDataService(@"C:\Users\Marco\Source\Repos\Projekt_HKP\Projekt_HKP\Projekt_HKP.Tests\DebugTestJson.json");//ToDo bessere Pfadangabe
            var comps = dataService.GetAllComponents();
            
            Assert.That(comps, Is.Not.Null);
        }

        [Test]
        public void GetAllComponents_ValidJsonFileWithTwoComponentens_ListWithTwoComponents()
        {
            var dataService = new JsonDataService(@"C:\Users\Marco\Source\Repos\Projekt_HKP\Projekt_HKP\Projekt_HKP.Tests\DebugTestJson.json");//ToDo bessere Pfadangabe
            var comps = dataService.GetAllComponents();

            Assert.That(comps.ToList(), Has.Count.EqualTo(2));
        }

        [Test]
        public void GetAllComponents_InvalidJsonFile_ThrowsFileNotFOundException()
        {
            Assert.Throws(typeof (FileNotFoundException), () => new JsonDataService("C:\\Test"));
        }
    }
}
