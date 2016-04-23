using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_HKP.Model.Hardware.Implementations
{
    [Serializable]
    public class Router : NetworkComponent
    {
        public int NumberOfPorts { get; set; }
    }
}
