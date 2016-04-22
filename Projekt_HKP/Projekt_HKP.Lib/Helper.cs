using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Projekt_HKP.Model.Hardware;
using Projekt_HKP.Model.Hardware.Implementations;

namespace Projekt_HKP.Lib
{
    public static class Helper
    {
        public static HardwareComponent CreateComponentFromTypeString(string typeString)
        {
            HardwareComponent component = null;
            switch (typeString)
            {
                case "Router":
                    component = new Router();
                    break;
                case "Switch":
                    component = new Switch();
                    break;
                case "AccessPoint":
                    component = new AccessPoint();
                    break;
                case "DesktopPc":
                    component = new DesktopPc();
                    break;
                case "Notebook":
                    component = new Notebook();
                    break;
                case "Server":
                    component = new Server();
                    break;
                case "Drucker":
                    component = new Drucker();
                    break;
            }

            return component;
        }
    }
}
