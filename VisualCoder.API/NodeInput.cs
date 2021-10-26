namespace VisualCoder.API
{
    public class NodeInput: NodeControl
    {
        public NodeInput(object input, string name)
        {
            Value = input;
            Name = name;
        }
        
        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
