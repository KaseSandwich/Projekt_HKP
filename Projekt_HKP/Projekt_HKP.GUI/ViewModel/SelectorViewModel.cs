using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Projekt_HKP.GUI.Common;
using Projekt_HKP.GUI.Events;
using Projekt_HKP.GUI.Views;
using Projekt_HKP.Lib;
using Projekt_HKP.Lib.DataAccess;
using Projekt_HKP.Model.Hardware;
using Projekt_HKP.Model.Orgaisation;
using RelayCommand = Projekt_HKP.GUI.Common.RelayCommand;

namespace Projekt_HKP.GUI.ViewModel
{
    public class SelectorViewModel : ViewModelBase
    {
        private readonly IDataService DataService;
        public SelectorItemViewModel CurrentItem { get; set; }
        public ObservableCollection<SelectorItemViewModel> Components { get; set; }

        public RelayCommand AddComponentCommand { get; set; }

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

        private Building _currentBuilding;
        public Building CurrentBuilding
        {
            get { return _currentBuilding; }
            set { _currentBuilding = value; RaisePropertyChanged(); }
        }

        private Room _currentRoom;
        public Room CurrentRoom
        {
            get { return _currentRoom; }
            set
            {
                _currentRoom = value;
                RaisePropertyChanged();
                RoomSelectionChangedExecute(this);
            }
        }

        public RelayCommand BuildingSelectionChangedCommand { get; set; }
        public RelayCommand RoomSelectionChangedCommand { get; set; }
        public RelayCommand ClearFilterCommand { get; set; }


        public SelectorViewModel()
        {
            DataService = App.DataService;
            Components = new ObservableCollection<SelectorItemViewModel>();
            AddComponentCommand = new RelayCommand(AddComponentExecute);
            BuildingSelectionChangedCommand = new RelayCommand(BuildingSelectionChangedExecute);
            RoomSelectionChangedCommand = new RelayCommand(RoomSelectionChangedExecute);
            ClearFilterCommand = new RelayCommand(ClearFilterExecute);
            Messenger.Default.Register<ComponentNameChangedMessage>(this, ComponentNameChangedHandler);

            Buildings = new ObservableCollection<Building>();
            Rooms = new ObservableCollection<Room>();
            var buildings = App.DataService.GetAllBuildings().ToList();
            foreach (var buildling in buildings)
                Buildings.Add(buildling);
        }

        private void ClearFilterExecute(object obj)
        {
            Components.Clear();
            CurrentRoom = null;
            CurrentBuilding = null;

            var comps = App.DataService?.GetAllComponents();
            if (comps == null)
                return;

            foreach (var comp in comps)
                Components.Add(new SelectorItemViewModel(comp.UID, comp.Name));
        }

        private void RoomSelectionChangedExecute(object obj)
        {
            if (CurrentRoom == null)
                return;

            Components.Clear();
            var comps = App.DataService.GetAllComponents().Where(c => c.RoomUID == CurrentRoom.UID);
            foreach(var comp in comps)
                Components.Add(new SelectorItemViewModel(comp.UID, comp.Name));
        }

        private void BuildingSelectionChangedExecute(object obj)
        {
            RefreshRooms();
        }

        private void RefreshRooms()
        {
            Rooms.Clear();
            var rooms = App.DataService.GetAllRoomsForBuilding(CurrentBuilding?.UID);

            if (rooms == null)
                return;

            foreach (var room in rooms)
                Rooms.Add(room);
        }

        private void ComponentNameChangedHandler(ComponentNameChangedMessage obj)
        {
            var item = Components.FirstOrDefault(c => c.Uid == obj.Uid);
            if (item != null)
                item.DisplayMember = obj.NewName;
        }

        private void AddComponentExecute(object obj)
        {
            var dlg = new TypeSelectionPopup(new DefaultTypeProvider());
            var result = dlg.ShowDialog();

            if (result.Value)
            {
                var comp = Helper.CreateComponentFromTypeString(dlg.Result);
                App.DataService.AddComponent(comp);
                Components.Add(new SelectorItemViewModel(comp.UID, comp.Name??"{ Leer }"));
            }
        }

        public void Load()
        {
            Components.Clear();

            CurrentBuilding = null;
            CurrentRoom = null;

            var comps = App.DataService?.GetAllComponents();
            if (comps == null)
                return;

            foreach (var comp in comps)
                Components.Add(new SelectorItemViewModel(comp.UID, comp.Name));
        }
    }
}
