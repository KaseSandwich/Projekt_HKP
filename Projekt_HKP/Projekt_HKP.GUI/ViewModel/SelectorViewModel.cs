using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Projekt_HKP.GUI.Common;
using Projekt_HKP.GUI.Events;
using Projekt_HKP.GUI.Views;
using Projekt_HKP.Lib;
using Projekt_HKP.Lib.DataAccess;
using Projekt_HKP.Model.Hardware;
using RelayCommand = Projekt_HKP.GUI.Common.RelayCommand;

namespace Projekt_HKP.GUI.ViewModel
{
    public class SelectorViewModel : ViewModelBase
    {
        private readonly IDataService DataService;
        public SelectorItemViewModel CurrentItem { get; set; }
        public ObservableCollection<SelectorItemViewModel> Components { get; set; }

        public RelayCommand AddComponentCommand { get; set; }

        public SelectorViewModel()
        {
            DataService = App.DataService;
            Components = new ObservableCollection<SelectorItemViewModel>();
            AddComponentCommand = new RelayCommand(AddComponentExecute);
            Messenger.Default.Register<ComponentNameChangedMessage>(this, ComponentNameChangedHandler);
        }

        private void ComponentNameChangedHandler(ComponentNameChangedMessage obj)
        {
            var item = Components.FirstOrDefault(c => c.Uid == obj.Uid);
            if (item != null)
                item.DisplayMember = obj.NewName;
        }

        private void AddComponentExecute(object obj)
        {
            var dlg = new TypeSelectionPopup(new DefaultTypeProvider());
            var result = dlg.ShowDialog();

            if (result.Value)
            {
                var comp = Helper.CreateComponentFromTypeString(dlg.Result);
                App.DataService.AddComponent(comp);
                Components.Add(new SelectorItemViewModel(comp.UID, comp.Name??"{ Leer }"));
            }
        }

        public void Load()
        {
            Components.Clear();

            var comps = App.DataService?.GetAllComponents();
            if (comps == null)
                return;

            foreach (var comp in comps)
                Components.Add(new SelectorItemViewModel(comp.UID, comp.Name));
        }
    }
}
