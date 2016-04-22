using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using Autofac;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Win32;
using Projekt_HKP.GUI.Common;
using Projekt_HKP.GUI.Events;
using Projekt_HKP.Lib.DataAccess;

namespace Projekt_HKP.GUI.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public SelectorViewModel SelectorViewModel { get; set; }
        private DetailsViewModel _detailsViewModel;
        public DetailsViewModel DetailsViewModel
        {
            get { return _detailsViewModel; }
            set { _detailsViewModel = value; RaisePropertyChanged();}
        }
        
        public MainViewModel(SelectorViewModel selectorViewModel)
        {
            SelectorViewModel = selectorViewModel;
            ReadFileCommand = new RelayCommand(ReadFileExecute);
            SaveFileCommand = new RelayCommand(SaveFileExecute);
            Messenger.Default.Register<OpenDetailsMessage>(this, OpenDetailsHandler);
        }

        private void SaveFileExecute(object obj)
        {
            var dlg = new SaveFileDialog();
            dlg.Filter = "Json Dateien (*.json)|*.json|XML Dateien (*.xml)|*.xml";
            var result = dlg.ShowDialog();

            if (result == null || !result.Value)
                return;

            var info = new FileInfo(dlg.FileName);
            var company = App.DataService.GetCompany();

            if (info.Extension == ".json")
            {
                App.DataService = new JsonDataService(company);
                
            }
            else if (info.Extension == ".xml")
            {
                App.DataService = new XmlDataService(company);
            }

            App.DataService.SaveAllComponents(dlg.FileName);

            Load();
        }

        private void ReadFileExecute(object obj)
        {
            var dlg = new OpenFileDialog();
            dlg.Filter = "Json Dateien (*.json)|*.json|XML Dateien (*.xml)|*.xml";
            var result = dlg.ShowDialog();

            if (result.Value)
            {
                var info = new FileInfo(dlg.FileName);
                if (info.Extension == ".json")
                {
                    App.DataService = new JsonDataService(dlg.FileName);
                }
                else if (info.Extension == ".xml")
                {
                    App.DataService = new XmlDataService(dlg.FileName);
                }

                Load();
            }
        }

        public RelayCommand ReadFileCommand { get; set; }
        public RelayCommand SaveFileCommand { get; set; }
        
        private void OpenDetailsHandler(OpenDetailsMessage obj)
        {
            var dataservice = App.DataService;
            DetailsViewModel vm = new DetailsViewModel();
            vm.Load(obj.Uid);
            DetailsViewModel = vm;
        }

        public void Load()
        {
            SelectorViewModel.Load();
        }
            
    }
}
