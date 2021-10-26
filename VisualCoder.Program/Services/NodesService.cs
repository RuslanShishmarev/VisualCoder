using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using VisualCoder.API;
using VisualCoder.Program.Models;

namespace VisualCoder.Program.Services
{
    public class NodesService
    {
        private VisualCoderData _context;

        public NodesService(VisualCoderData context)
        {
            _context = context;
        }

        public NodeItem LoadNode(string assemlyPath, string classPath, NodeInfo nodeInfo)
        {
            Assembly assembly = Assembly.LoadFrom(assemlyPath);
            Type myType = assembly.GetType(classPath);

            var attr = myType.GetCustomAttribute(typeof(NodeIdentificationAttribute)) as NodeIdentificationAttribute;
            nodeInfo.NodeName = attr.NodeName;

            ConstructorInfo info = myType.GetConstructor(Array.Empty<Type>());

            object myObj = info.Invoke(Array.Empty<object>());

            INode node = (INode)myObj;

            NodeItem nodeResult = new NodeItem(nodeInfo, node, _context);

            return nodeResult;
        }
    }
}
