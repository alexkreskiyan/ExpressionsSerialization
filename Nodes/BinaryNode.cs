namespace ExpressionsSerialization.Nodes
{
    public class BinaryNode : INode
    {
        public INode Left { get; set; }

        public INode Right { get; set; }
    }
}