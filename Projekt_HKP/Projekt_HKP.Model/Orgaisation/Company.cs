using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Projekt_HKP.Model.Orgaisation
{
    public class Company
    {
        public string UID { get; set; }
        public string Name { get; set; }
        public ObservableCollection<Building> Buildings { get; set; }
    }
}
