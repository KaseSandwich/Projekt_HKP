using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Projekt_HKP.GUI.Common;

namespace Projekt_HKP.GUI.ViewModel
{
    public class SelectorItemViewModel : ViewModelBase
    {
        private string _displayMember;
        public string DisplayMember
        {
            get { return _displayMember; }
            set { _displayMember = value; }
        }

        private string _uid;
        public string Uid
        {
            get { return _uid; }
            set { _uid = value; }
        }

        public RelayCommand OpenDetailViewCommand { get; set; }

        public SelectorItemViewModel(string uid, string displayMember)
        {
            Uid = uid;
            DisplayMember = displayMember;
            OpenDetailViewCommand = new RelayCommand(OpenDetailViewExecute);
        }

        private void OpenDetailViewExecute(object obj)
        {
            MessageBox.Show($"ID {Uid}");
        }
    }
}
