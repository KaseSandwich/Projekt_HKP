using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Messaging;
using Projekt_HKP.GUI.Common;
using Projekt_HKP.GUI.Events;
using Projekt_HKP.Lib.DataAccess;
using Projekt_HKP.Model.Hardware;

namespace Projekt_HKP.GUI.ViewModel
{
    public class DetailsViewModel : ViewModelBase
    {
        private readonly IDataService DataService;
        private HardwareComponent _component;
        public HardwareComponent Component
        {
            get { return _component; }
            set { _component = value; RaisePropertyChanged(); }
        }

        public RelayCommand TextChangedCommand { get; set; }

        public DetailsViewModel()
        {
            DataService = App.DataService;
            TextChangedCommand = new RelayCommand(TextChangedExecute);
        }

        private void TextChangedExecute(object obj)
        {
            Messenger.Default.Send<ComponentNameChangedMessage>(new ComponentNameChangedMessage() {Uid = Component.UID, NewName = Component.Name});
        }

        public void Load(string componentUid)
        {
            this.Component = DataService.GetComponentByUid(componentUid);
        }
    }
}
