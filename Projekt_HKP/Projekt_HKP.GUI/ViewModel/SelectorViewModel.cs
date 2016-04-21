using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Projekt_HKP.GUI.Common;
using Projekt_HKP.Lib.DataAccess;
using Projekt_HKP.Model.Hardware;

namespace Projekt_HKP.GUI.ViewModel
{
    public class SelectorViewModel : ViewModelBase
    {
        private readonly IDataService DataService;
        public ObservableCollection<SelectorItemViewModel> Components { get; set; }

        public SelectorViewModel(IDataService dataService)
        {
            DataService = dataService;
            Components = new ObservableCollection<SelectorItemViewModel>();
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
