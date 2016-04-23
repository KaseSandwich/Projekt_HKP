using System;
using System.Collections.ObjectModel;

namespace Projekt_HKP.Model.Orgaisation
{
    [Serializable]
    public class Building
    {
        public string UID { get; set; }
        public string CompanyUID { get; set; }
        public string Name { get; set; }
        public ObservableCollection<Room> Rooms { get; set; }

        public Building()
        {
            UID = Guid.NewGuid().ToString("D");
            Rooms = new ObservableCollection<Room>();
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
