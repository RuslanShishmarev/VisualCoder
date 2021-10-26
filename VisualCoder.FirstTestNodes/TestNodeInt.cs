using System;
using System.Collections.Generic;
using System.Linq;
using VisualCoder.API;

namespace VisualCoder.FirstTestNodes
{
    [NodeIdentification("Число 5")]
    public class TestNodeInt : INode
    {
        public List<NodeInput> Inputs { get; set ; }
        public NodeResult Result { get; set; }

        public TestNodeInt()
        {
            var result = new NodeResult(5);
            result.Name = "int";
            Result = result;
        }
        public void Execute(VisualCoderData context)
        {
            Result.Name = "int";
        }
    }
}
