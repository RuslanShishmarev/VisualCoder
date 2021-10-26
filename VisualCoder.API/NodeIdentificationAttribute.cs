using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualCoder.API
{
    public class NodeIdentificationAttribute : Attribute
    {
        public string NodeName { get; set; }

        public NodeIdentificationAttribute(string name)
        {
            NodeName = name;
        }
    }
}
