using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using GalaSoft.MvvmLight.Messaging;
using Projekt_HKP.GUI.Common;
using Projekt_HKP.GUI.Events;

namespace Projekt_HKP.GUI.ViewModel
{
    public class SelectorItemViewModel : ViewModelBase
    {
        private string _displayMember;
        public string DisplayMember
        {
            get { return _displayMember; }
            set { _displayMember = value; RaisePropertyChanged();}
        }

        private string _uid;
        public string Uid
        {
            get { return _uid; }
            set { _uid = value; RaisePropertyChanged();}
        }

        public RelayCommand OpenDetailViewCommand { get; set; }
        public RelayCommand DeleteCommand { get; set; }

        public SelectorItemViewModel(string uid, string displayMember)
        {
            Uid = uid;
            DisplayMember = displayMember;
            OpenDetailViewCommand = new RelayCommand(OpenDetailViewExecute);
            DeleteCommand = new RelayCommand(DeleteExecute);
        }

        private void DeleteExecute(object obj)
        {
            Messenger.Default.Send<ComponentDeletedMessage>(new ComponentDeletedMessage() {Uid = Uid});
        }

        private void OpenDetailViewExecute(object obj)
        {
            Messenger.Default.Send<OpenDetailsMessage>(new OpenDetailsMessage() {Uid = Uid});
        }
    }
}
