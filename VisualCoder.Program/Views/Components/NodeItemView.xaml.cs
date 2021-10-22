using System.Windows.Controls;
using VisualCoder.Program.Models;

namespace VisualCoder.Program.Views.Components
{
    /// <summary>
    /// Логика взаимодействия для NodeItemView.xaml
    /// </summary>
    public partial class NodeItemView : UserControl
    {
        public NodeItem Node { get; set; }
        public NodeItemView()
        {
            InitializeComponent();
        }
    }
}
