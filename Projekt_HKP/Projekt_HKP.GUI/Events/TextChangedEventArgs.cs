using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_HKP.GUI.Events
{
    public class TextChangedEventArgs : EventArgs
    {
        public string NewText { get; set; }
    }
}
