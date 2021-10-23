using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using VisualCoder.API;

namespace VisualCoder.Program.Models
{
    public class NodeItem
    {
        public DelegateCommand Command { get; private set; }
        private VisualCoderData _context;
        private void Execute()
        {
            _node.Inputs = Inputs;
            _node.Execute(_context);
            Result = new NodeResult(_node.Result);
        }
        private INode _node;
        public NodeInfo Info { get; private set; }
        public NodeResult Result { get; private set; }
        public List<NodeInput> Inputs { get; set; }
        public NodeItem(NodeInfo info, List<NodeInput> inputs, INode node, VisualCoderData context)
        {
            Inputs = inputs;
            Info = info;
            _node = node;
            _context = context;
            Command = new DelegateCommand(this.Execute);
        }

        public override string ToString()
        {
            return Info.ToString();
        }
    }
}
