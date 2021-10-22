using System;
using System.Collections.Generic;
using System.Linq;
using VisualCoder.API;

namespace VisualCoder.FirstTestNodes
{
    public class TestNode2 : INode
    {
        public List<NodeInput> Inputs { get; set ; }
        public NodeResult Result { get; set; }

        public void Execute()
        {
            int result = 1;
            var inputs = Inputs.Select(inpObj => int.Parse(inpObj.ToString()));
            foreach (int input in inputs)
                result *= input;

            Result = new NodeResult(result);
        }
    }
}
