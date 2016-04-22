using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Projekt_HKP.GUI.Common;
using Projekt_HKP.GUI.Views;
using Projekt_HKP.Lib;
using Projekt_HKP.Lib.DataAccess;
using Projekt_HKP.Model.Hardware;

namespace Projekt_HKP.GUI.ViewModel
{
    public class SelectorViewModel : ViewModelBase
    {
        private readonly IDataService DataService;
        public SelectorItemViewModel CurrentItem { get; set; }
        public ObservableCollection<SelectorItemViewModel> Components { get; set; }

        public RelayCommand AddComponentCommand { get; set; }
        public RelayCommand DeleteComponentCommand { get; set; }

        public SelectorViewModel()
        {
            DataService = App.DataService;
            Components = new ObservableCollection<SelectorItemViewModel>();
            AddComponentCommand = new RelayCommand(AddComponentExecute);
            DeleteComponentCommand = new RelayCommand(DeleteComponentExecute);
        }

        private void DeleteComponentExecute(object obj)
        {
            
        }

        private void AddComponentExecute(object obj)
        {
            var dlg = new TypeSelectionPopup(new DefaultTypeProvider());
            var result = dlg.ShowDialog();

            if (result.Value)
            {
                var comp = Helper.CreateComponentFromTypeString(dlg.Result);
                Components.Add(new SelectorItemViewModel(comp.UID, comp.Name??"{ Leer }"));
            }
        }

        public void Load()
        {
            Components.Clear();

            var comps = DataService.GetAllComponents();
            foreach(var comp in comps)
                Components.Add(new SelectorItemViewModel(comp.UID, comp.Name));
        }
    }
}
