using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Messaging;
using Projekt_HKP.GUI.Common;
using Projekt_HKP.GUI.Events;
using Projekt_HKP.Lib.DataAccess;
using Projekt_HKP.Model.Hardware;
using Projekt_HKP.Model.Orgaisation;

namespace Projekt_HKP.GUI.ViewModel
{
    public class DetailsViewModel : ViewModelBase
    {
        private ObservableCollection<Building> _buildings;
        public ObservableCollection<Building> Buildings
        {
            get { return _buildings; }
            set { _buildings = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<Room> _rooms;
        public ObservableCollection<Room> Rooms
        {
            get { return _rooms; }
            set { _rooms = value; RaisePropertyChanged(); }
        }
        
        private HardwareComponent _component;
        public HardwareComponent Component
        {
            get { return _component; }
            set { _component = value; RaisePropertyChanged(); }
        }

        private Building _currentBuilding;
        public Building CurrentBuilding
        {
            get { return _currentBuilding; }
            set { _currentBuilding = value; RaisePropertyChanged();}
        }
        
        private Room _currentRoom;
        public Room CurrentRoom
        {
            get { return _currentRoom; }
            set
            {
                _currentRoom = value;
                RaisePropertyChanged();
                if(CurrentRoom != null)
                    Component.RoomUID = CurrentRoom.UID;
            }
        }
        
        public RelayCommand TextChangedCommand { get; set; }
        public RelayCommand BuildingSelectionChangedCommand { get; set; }

        public DetailsViewModel()
        {
            TextChangedCommand = new RelayCommand(TextChangedExecute);
            BuildingSelectionChangedCommand = new RelayCommand(BuildingSelectionChangedExecute);
            Buildings = new ObservableCollection<Building>();
            Rooms = new ObservableCollection<Room>();
            var buildings = App.DataService.GetAllBuildings().ToList();
            foreach (var buildling in buildings)
                Buildings.Add(buildling);
        }

        private void BuildingSelectionChangedExecute(object obj)
        {
            RefreshRooms();
        }

        private void RefreshRooms()
        {
            Rooms.Clear();
            var rooms = App.DataService.GetAllRoomsForBuilding(CurrentBuilding.UID);
            foreach (var room in rooms)
                Rooms.Add(room);
        }

        private void TextChangedExecute(object obj)
        {
            Messenger.Default.Send<ComponentNameChangedMessage>(new ComponentNameChangedMessage() {Uid = Component.UID, NewName = Component.Name});
        }

        public void Load(string componentUid)
        {
            this.Component = App.DataService.GetComponentByUid(componentUid);
            var room = App.DataService.GetRoomByUid(Component.RoomUID);
            var building = App.DataService.GetBuildingByUid(room.BuildingUID);

            CurrentBuilding = building;
            RefreshRooms();
            CurrentRoom = room;
        }
    }
}
