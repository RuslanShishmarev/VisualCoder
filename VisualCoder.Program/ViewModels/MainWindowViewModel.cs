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
using VisualCoder.API;
using VisualCoder.Program.Models;

namespace VisualCoder.Program.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        public MainWindowViewModel()
        {
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

        public ObservableCollection<NodeItem> AllNodes { get; set; } = new ObservableCollection<NodeItem>();

        private object resultTest = 0;

        public object ResultTest
        {
            get { return resultTest; }
            set 
            { 
                resultTest = value;
                RaisePropertyChanged(nameof(ResultTest));
            }
        }

        private NodeItem CreateNewNode(string assemlyPath, string classPath, NodeInfo nodeInfo, List<NodeInput> nodeInputs)
        {
            Assembly assembly = Assembly.LoadFrom(assemlyPath);
            Type myType = assembly.GetType(classPath);

            ConstructorInfo info = myType.GetConstructor(Array.Empty<Type>());

            object myObj = info.Invoke(Array.Empty<object>());

            INode node = (INode)myObj;

            NodeItem nodeResult = new NodeItem(nodeInfo, nodeInputs, node);

            return nodeResult;
        }

    }
}
