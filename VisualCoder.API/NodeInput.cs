namespace VisualCoder.API
{
    public class NodeInput: NodeControl
    {
        public NodeInput(object input)
        {
            Value = input;
        }
        
        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
