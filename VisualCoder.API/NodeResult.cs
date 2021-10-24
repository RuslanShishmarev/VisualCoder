namespace VisualCoder.API
{
    public class NodeResult: NodeControl
    {
        public NodeResult(object result)
        {
            Value = result;
        }

        public override string ToString()
        {
            return Value?.ToString();
        }
    }
}
