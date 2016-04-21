using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Projekt_HKP.GUI.Common;
using Projekt_HKP.GUI.ViewModel;
using Projekt_HKP.GUI.Views;
using Projekt_HKP.Lib.DataAccess;

namespace Projekt_HKP.GUI.Startup
{
    public class Bootstrapper
    {
        public IContainer BootStrap()
        {
            var builder = new ContainerBuilder();

            // Register Dependencies
            builder.Register(j => new JsonDataService(@"D:\Projects\GitHub\Projekt_HKP\Projekt_HKP\Projekt_HKP.Tests\DebugTestJson.json")).As<IDataService>();
            builder.RegisterType<SelectorViewModel>().AsSelf();
            builder.RegisterType<DetailsViewModel>().AsSelf();
            builder.RegisterType<MainViewModel>().AsSelf();
            builder.RegisterType<MainWindow>().AsSelf();

            return builder.Build();
        }
            
    }
}
