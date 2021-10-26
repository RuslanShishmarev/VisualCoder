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
using VisualCoder.API;

namespace VisualCoder.Program.Views.Components
{
    /// <summary>
    /// Логика взаимодействия для NodeInputView.xaml
    /// </summary>
    public partial class NodeInputView : UserControl
    {

        public NodeInput Input { get; set; }

        public NodeInputView()
        {
            InitializeComponent();
        }
    }
}
