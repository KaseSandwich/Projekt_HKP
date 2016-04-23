using System;
using System.Collections.ObjectModel;
using System.Threading;

namespace Projekt_HKP.Model.Orgaisation
{
    [Serializable]
    public class Department
    {
        public string UID { get; set; }
        public string Bezeichnung { get; set; }
        public ObservableCollection<Room> Rooms { get; set; }

        public Department()
        {
            UID = Guid.NewGuid().ToString("D");
            Rooms = new ObservableCollection<Room>();
        }
    }
}
