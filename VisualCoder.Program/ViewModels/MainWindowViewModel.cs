using Newtonsoft.Json;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using VisualCoder.API;
using VisualCoder.Program.Models;
using VisualCoder.Program.Views.Components;

namespace VisualCoder.Program.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {

        public VisualCoderData Context { get; private set; }
        public ObservableCollection<NodeItem> AllNodes { get; set; } = new ObservableCollection<NodeItem>();

        private NodeItem _selectedNode;

        public NodeItem SelectedNode
        {
            get => _selectedNode;
            set 
            { 
                _selectedNode = value;
                RaisePropertyChanged(nameof(SelectedNode));
            }
        }

        private NodeItemView _selectedNodeToMove;

        public NodeItemView SelectedNodeToMove
        {
            get => _selectedNodeToMove;
            set
            {
                _selectedNodeToMove = value;
                RaisePropertyChanged(nameof(SelectedNodeToMove));
            }
        }

        public DelegateCommand<object> AddNodeViewToPanelCommand { get; private set; }

        public MainWindowViewModel()
        {
            Context = new VisualCoderData();
            AddNodeViewToPanelCommand = new DelegateCommand<object> (AddNodeViewToPanel);
            //download assambles
            //get all files from test file path
            string pathToAssemblies = "C://Users//Admin//Desktop//Programming//c#//VisualCoder//VisualCoder.FirstTestNodes";
            string filePattern = "*.nodeitem";

            var allNodeInfos = Directory.GetFiles(pathToAssemblies, filePattern);

            if (allNodeInfos.Any())
            {
                //deserialize
                foreach(var infoPath in allNodeInfos)
                {
                    NodeInfo nodeInfo = JsonConvert.DeserializeObject<NodeInfo>(File.ReadAllText(infoPath));
                    if(nodeInfo != null)
                    {
                        //get path to assembly
                        string assemlyPath = nodeInfo.PathAssembly;

                        string classPath = nodeInfo.PathExecuteClass;

                        List<NodeInput> nodeInputs = new List<int>() { 5, 6, 540 }.Select(t => new NodeInput(t)).ToList();
                        var node = CreateNewNode(assemlyPath, classPath, nodeInfo, nodeInputs);

                        AllNodes.Add(node);
                    }
                }
            }
        }

        

        private NodeItem CreateNewNode(string assemlyPath, string classPath, NodeInfo nodeInfo, List<NodeInput> nodeInputs)
        {
            Assembly assembly = Assembly.LoadFrom(assemlyPath);
            Type myType = assembly.GetType(classPath);

            ConstructorInfo info = myType.GetConstructor(Array.Empty<Type>());

            object myObj = info.Invoke(Array.Empty<object>());

            INode node = (INode)myObj;

            NodeItem nodeResult = new NodeItem(nodeInfo, nodeInputs, node, Context);

            return nodeResult;
        }

        private Canvas _nodePlacement;

        public Canvas NodePlacement
        {
            get { return _nodePlacement; }
            set 
            { 
                _nodePlacement = value;
                RaisePropertyChanged(nameof(NodePlacement));
            }
        }

        private void AddNodeViewToPanel(object canvasObj)
        {
            var canvas = canvasObj as Canvas;
            NodePlacement = canvas;

            NodeItemView nodeView = new NodeItemView();
            nodeView.DataContext = SelectedNode;
            nodeView.MouseLeftButtonDown += (s, e) =>
            {
                MouseDownNode(s as NodeItemView);
            };
            /*nodeView.MouseLeftButtonUp += (s, e) =>
            {
                MouseUpNode(s as NodeItemView);
                //MouseMoveNode(e);
            };*/

            /*nodeView.MouseMove += (s, e) => 
            {
                MouseMoveNode(e);
            };*/

            NodePlacement.MouseUp += (s, e) => 
            {
                DropNode(e);
            };

            canvas.Children.Add(nodeView);
            Canvas.SetTop(nodeView, 10);
            Canvas.SetLeft(nodeView, 10);

        }

        private void MouseDownNode(NodeItemView nodeItemView)
        {
            SelectedNodeToMove = nodeItemView;
            DragDrop.DoDragDrop(SelectedNodeToMove, nodeItemView, DragDropEffects.Move);
        }

        private void DropNode(MouseButtonEventArgs e)
        {
            //get mouse points
            if(SelectedNodeToMove != null)
            {
                //get position for X

                //var x = SelectedNodeToMove

                Canvas.SetTop(SelectedNodeToMove, e.GetPosition(SelectedNodeToMove).Y);
                Canvas.SetLeft(SelectedNodeToMove, e.GetPosition(SelectedNodeToMove).X);
            }
            SelectedNodeToMove = null;
        }
    }
}
