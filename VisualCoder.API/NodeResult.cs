namespace VisualCoder.API
{
    public struct NodeResult
    {
        public NodeResult(object result)
        {
            Result = result;
        }
        public object Result { get; set; }

        public override string ToString()
        {
            return Result?.ToString();
        }
    }
}
