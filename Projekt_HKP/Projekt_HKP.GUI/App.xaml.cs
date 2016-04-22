using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;
using Autofac;
using Projekt_HKP.GUI.Startup;
using Projekt_HKP.GUI.Views;
using Projekt_HKP.Lib.DataAccess;

namespace Projekt_HKP.GUI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static IContainer DependencyContainer { get; private set; }
        public static IDataService DataService { get; set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var bootstrapper = new Bootstrapper();
            DependencyContainer = bootstrapper.BootStrap();
            DataService = DependencyContainer.Resolve<IDataService>();

            var mainWindow = DependencyContainer.Resolve<MainWindow>();
            mainWindow.Show();
        }
    }
}
