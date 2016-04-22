using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Projekt_HKP.Lib.DataAccess;

namespace Projekt_HKP.GUI.Views
{
    /// <summary>
    /// Interaction logic for TypeSelectionPopup.xaml
    /// </summary>
    public partial class TypeSelectionPopup : Window
    {
        public ObservableCollection<string> Types { get; set; }
        private readonly ITypeProvider _typeProvider;

        public TypeSelectionPopup(ITypeProvider typeProvider)
        {
            InitializeComponent();
            _typeProvider = typeProvider;

            Types = new ObservableCollection<string>();

            var typesTemp = typeProvider.GetAllTypes();
            foreach(var type in typesTemp)
                Types.Add(type);


        }
    }
}
