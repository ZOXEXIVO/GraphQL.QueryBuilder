using System.Collections.Generic;

namespace GraphQL.QueryBuilder.Nodes
{
    public class QueryNode
    {
        public string Value { get; }
    
        public readonly List<QueryNode> Childs = new List<QueryNode>();

        public QueryNode(string value = null)
        {
            Value = value;
        }

        public QueryNode AddChild(string value)
        {
            var node = new QueryNode(value);
        
            Childs.Add(node);
        
            return node;
        }
    }
}