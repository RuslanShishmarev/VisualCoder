namespace VisualCoder.API
{
    public class NodeInfo
    {
        public string PathAssembly { get; set; }
        public string PathExecuteClass { get; set; }
        public string CoderName { get; set; }
        public string NodeName { get; set; }

        public override string ToString()
        {
            return NodeName ?? PathExecuteClass;
        }
    }
}
