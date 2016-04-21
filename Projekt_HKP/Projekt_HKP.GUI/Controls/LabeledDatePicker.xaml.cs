using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using UserControl = System.Windows.Controls.UserControl;

namespace Projekt_HKP.GUI.Controls
{
    /// <summary>
    /// Interaction logic for LabeledDatePicker.xaml
    /// </summary>
    public partial class LabeledDatePicker : UserControl
    {
        public static readonly DependencyProperty LabelProperty = DependencyProperty
       .Register("Label",
               typeof(string),
               typeof(LabeledDatePicker),
               new FrameworkPropertyMetadata("Unnamed Label"));
        
        public static readonly DependencyProperty CurrentDateTimeProperty = DependencyProperty
        .Register("CurrentDateTime",
            typeof(DateTime),
            typeof(LabeledDatePicker));

        public LabeledDatePicker()
        {
            InitializeComponent();
            Root.DataContext = this;
        }

        public string Label
        {
            get { return (string)GetValue(LabelProperty); }
            set { SetValue(LabelProperty, value); }
        }

        public DateTime CurrentDateTime
        {
            get { return (DateTime)GetValue(CurrentDateTimeProperty); }
            set { SetValue(CurrentDateTimeProperty, value); }
        }
    }
}
