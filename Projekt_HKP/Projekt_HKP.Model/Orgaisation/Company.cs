using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Projekt_HKP.Model.Orgaisation
{
    [Serializable]
    public class Company
    {
        public string UID { get; set; }
        public string Name { get; set; }
        public ObservableCollection<Building> Buildings { get; set; }

        public Company()
        {
            UID = Guid.NewGuid().ToString("D");
            Buildings = new ObservableCollection<Building>();
        }
    }
}
