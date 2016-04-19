using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Projekt_HKP.GUI.Common;
using Projekt_HKP.GUI.Views;

namespace Projekt_HKP.GUI.Startup
{
    public class Bootstrapper
    {
        public IContainer BootStrap()
        {
            var builder = new ContainerBuilder();

            // Register Dependencies
            builder.RegisterType<MainWindow>().AsSelf();

            return builder.Build();
        }
            
    }
}
