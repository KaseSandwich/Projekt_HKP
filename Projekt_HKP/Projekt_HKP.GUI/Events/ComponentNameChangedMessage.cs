using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_HKP.GUI.Events
{
    public class ComponentNameChangedMessage
    {
        public string Uid { get; set; }
        public string NewName { get; set; }
    }
}
