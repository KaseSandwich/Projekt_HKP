using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Projekt_HKP.GUI.Controls
{
    /// <summary>
    /// Interaction logic for LabeledTextbox.xaml
    /// </summary>
    public partial class LabeledTextbox : UserControl
    {
        public static readonly DependencyProperty LabelProperty = DependencyProperty
        .Register("Label",
                typeof(string),
                typeof(LabeledTextbox),
                new FrameworkPropertyMetadata("Unnamed Label"));

        public static readonly DependencyProperty TextProperty = DependencyProperty
            .Register("Text",
                    typeof(string),
                    typeof(LabeledTextbox),
                    new FrameworkPropertyMetadata("", FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        //public static readonly DependencyProperty InputScopeProperty = DependencyProperty
        //.Register("InputScope",
        //        typeof(InputScope),
        //        typeof(LabeledTextbox));

        public LabeledTextbox()
        {
            InitializeComponent();
            Root.DataContext = this;
        }

        public string Label
        {
            get { return (string)GetValue(LabelProperty); }
            set { SetValue(LabelProperty, value); }
        }

        //public new InputScope InputScope
        //{
        //    get { return (InputScope)GetValue(InputScopeProperty); }
        //    set { SetValue(InputScopeProperty, value); }
        //}

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }
    }
}
