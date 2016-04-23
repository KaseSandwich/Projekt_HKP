using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_HKP.Model.Hardware.Implementations
{
    [Serializable]
    public class Switch : NetworkComponent
    {
        public int NumberOfPorts { get; set; }
    }
}
