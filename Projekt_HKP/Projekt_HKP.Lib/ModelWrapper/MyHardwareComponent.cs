using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Projekt_HKP.Model.Hardware;
using Projekt_HKP.Model.Orgaisation;

namespace Projekt_HKP.Lib.ModelWrapper
{
    public class MyHardwareComponent
    {
        public HardwareComponent Component { get; set; }
        public Room Room { get; set; }
        public Building Building { get; set; }
        public Department Department { get; set; }
        public Company Company { get; set; }
    }
}
