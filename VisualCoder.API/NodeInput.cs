namespace VisualCoder.API
{
    public class NodeInput
    {
        public object Input { get; set; }
        public NodeInput(object input)
        {
            Input = input;
        }
        public override string ToString()
        {
            return Input.ToString();
        }
    }
}
