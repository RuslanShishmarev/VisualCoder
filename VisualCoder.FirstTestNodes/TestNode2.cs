using System;
using System.Collections.Generic;
using System.Linq;
using VisualCoder.API;

namespace VisualCoder.FirstTestNodes
{
    [NodeIdentification("Умножение")]
    public class TestNode2 : INode
    {
        public List<NodeInput> Inputs { get; set ; } = new List<int>() { 1, 6, 1 }.Select(t => new NodeInput(t, "int")).ToList();
        public NodeResult Result { get; set; }

        public void Execute(VisualCoderData context)
        {
            Result = new NodeResult(null);
            Result.Name = "int";
            int result = 1;
            var inputs = Inputs.Select(inpObj => int.Parse(inpObj.ToString()));
            foreach (int input in inputs)
                result *= input;

            Result = new NodeResult(result);
            context.Result = result;
        }
    }
}
