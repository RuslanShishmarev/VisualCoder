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
using System.Windows.Media;
using System.Windows.Shapes;
using VisualCoder.API;
using VisualCoder.Program.Models;
using VisualCoder.Program.Services;
using VisualCoder.Program.Views.Components;

namespace VisualCoder.Program.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private readonly NodesService _nodesService;

        public VisualCoderData Context { get; private set; }
        public ObservableCollection<NodeItem> AllNodes { get; set; } = new ObservableCollection<NodeItem>();
        public ObservableCollection<NodeItemView> AllPlacedViewNodes { get; set; } = new ObservableCollection<NodeItemView>();



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

        private Window _mainWindow;

        public DelegateCommand<object> AddNodeViewToPanelCommand { get; private set; }

        private NodeResult _selectedNodeResult;

        public DelegateCommand ExecuteCodeCommand { get; private set; }

        public NodeResult SelectedNodeResult
        {
            get => _selectedNodeResult;
            set 
            { 
                _selectedNodeResult = value;
                RaisePropertyChanged(nameof(SelectedNodeResult));
            }
        }

        private NodeInput _selectedNodeInput;

        public NodeInput SelectedNodeInput
        {
            get => _selectedNodeInput;
            set
            {
                _selectedNodeInput = value;
                RaisePropertyChanged(nameof(SelectedNodeInput));
            }
        }


        public MainWindowViewModel()
        {
            Context = new VisualCoderData();

            _nodesService = new NodesService(Context);

            AddNodeViewToPanelCommand = new DelegateCommand<object> (AddNodeViewToPanel);

            ExecuteCodeCommand = new DelegateCommand(ExecuteCode);
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

                        var node = _nodesService.LoadNode(assemlyPath, classPath, nodeInfo);

                        AllNodes.Add(node);
                    }
                }
            }
        }

               

        private void AddNodeViewToPanel(object canvasObj)
        {
            var canvas = canvasObj as Canvas;
            NodePlacement = canvas;

            _mainWindow ??= Window.GetWindow(NodePlacement);

            NodeItemView nodeView = new NodeItemView();
            nodeView.DataContext = new NodeItemViewModel(nodeView, SelectedNode, this);
            nodeView.MouseLeftButtonDown += (s, e) =>
            {
                MouseDownNode(s as NodeItemView, e);
            };
            nodeView.MouseMove += (s, e) =>
            {
                 MoveNode(e);
            };
            nodeView.MouseUp += (s, e) =>
            {
                StopMove();
            };

            nodeView.MouseLeave += (s, e) =>
            {
                StopMove();
            };

            nodeView.MouseDoubleClick += (s, e) =>
            {
                RemoveSelectedNode(s as NodeItemView);
            };

            canvas.Children.Add(nodeView);
            Canvas.SetTop(nodeView, 10);
            Canvas.SetLeft(nodeView, 10);

            AllPlacedViewNodes.Add(nodeView);
        }

        private void MouseDownNode(NodeItemView nodeItemView, MouseButtonEventArgs e)
        {
            SelectedNodeToMove = nodeItemView;
            StartSelectedPositionPoint = e.GetPosition(SelectedNodeToMove);
        }

        private Point _startSelectedPositionPoint;
        public Point StartSelectedPositionPoint
        {
            get => _startSelectedPositionPoint; 
            set 
            { 
                _startSelectedPositionPoint = value;
                RaisePropertyChanged(nameof(StartSelectedPositionPoint));
            }
        }


        private void MoveNode(MouseEventArgs e)
        {
            if (SelectedNodeToMove != null)
            {
                var mouseCanvasPosition = e.GetPosition(NodePlacement);

                var newY = mouseCanvasPosition.Y - StartSelectedPositionPoint.Y;
                var newX = mouseCanvasPosition.X - StartSelectedPositionPoint.X;

                Canvas.SetTop(SelectedNodeToMove, newY < 0 ? 0: newY);
                Canvas.SetLeft(SelectedNodeToMove, newX < 0 ? 0 : newX);
            }
            
        }
        
        private void StopMove()
        {
            SelectedNodeToMove = null;
        }

        private void RemoveSelectedNode(NodeItemView nodeItemView)
        {
            if(nodeItemView != null)
            {
                AllPlacedViewNodes.Remove(nodeItemView);
                NodePlacement.Children.Remove(nodeItemView);
                SelectedNodeToMove = null;
            }
        }

        private void ExecuteCode()
        {
            var lastNode = AllPlacedViewNodes.FirstOrDefault();
            lastNode.Node.Command.Execute();
            MessageBox.Show(lastNode.Node.Result.Value.ToString());
        }

        private Line CreateLine(Point start, Point end)
        {
            Line newLine = new Line();

            newLine.X1 = start.X;
            newLine.Y1 = start.Y;

            newLine.X2 = end.X;
            newLine.Y2 = end.Y;

            return newLine;
        }
    }
}
