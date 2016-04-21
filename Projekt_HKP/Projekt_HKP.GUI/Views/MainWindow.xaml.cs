using System.Windows;
using MahApps.Metro.Controls;
using Projekt_HKP.GUI.ViewModel;

namespace Projekt_HKP.GUI.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        private MainViewModel _viewModel;

        public MainWindow(MainViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            DataContext = _viewModel;
        }

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            _viewModel.Load();
        }
    }
}
