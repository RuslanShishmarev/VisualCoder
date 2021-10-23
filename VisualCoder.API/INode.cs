using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualCoder.API
{
    public interface INode
    {
        NodeResult Result { get; set; }
        List<NodeInput> Inputs { get; set; }
        void Execute(VisualCoderData context);
    }
}
