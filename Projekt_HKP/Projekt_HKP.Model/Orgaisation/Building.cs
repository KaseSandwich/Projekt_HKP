using System.Collections.ObjectModel;

namespace Projekt_HKP.Model.Orgaisation
{
    public class Building
    {
        public string UID { get; set; }
        public string Name { get; set; }
        public ObservableCollection<Room> Rooms { get; set; }
    }
}
