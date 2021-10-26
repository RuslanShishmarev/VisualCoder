using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualCoder.API
{
    public class NodeControl
    {
        public string Name { get; set; }
        public object Value { get; set; }
        public Type ValueType
        {
            get => Value?.GetType();
        }
    }
}
