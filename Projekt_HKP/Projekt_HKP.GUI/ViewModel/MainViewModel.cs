using System;
using System.Threading.Tasks;
using System.Windows.Media;
using Projekt_HKP.GUI.Common;

namespace Projekt_HKP.GUI.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public SelectorViewModel SelectorViewModel { get; set; }
        public DetailsViewModel DetailsViewModel { get; set; }

        public MainViewModel(SelectorViewModel selectorViewModel, DetailsViewModel detialsViewModel)
        {
            SelectorViewModel = selectorViewModel;
            DetailsViewModel = detialsViewModel;
        }

        public void Load()
        {
            SelectorViewModel.Load();
        }
            
    }
}
