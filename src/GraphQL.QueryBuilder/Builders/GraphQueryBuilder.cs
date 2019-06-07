using System;
using System.Linq;
using System.Text;
using GraphQL.QueryBuilder.Nodes;

namespace GraphQL.QueryBuilder.Builders
{
    public interface IGraphQuery<out TEntity>
    {
        QueryNode Node { get; }
    }

    public interface IGraphQueryBuilder<out TEntity, out TProperty> : IGraphQuery<TEntity>
    {
        QueryNode LastNode { get; }
        string Render();
    }
    
    internal class GraphGraphQueryBuilder<TEntity, TProperty> : IGraphQueryBuilder<TEntity, TProperty>
    {
        public QueryNode Node { get; }
        public QueryNode LastNode { get; }

        public GraphGraphQueryBuilder(QueryNode node, QueryNode lastNode)
        {
            Node = node;
            LastNode = lastNode;
        }
                
        //TODO - Use Visitor
        public string Render()
        {
            var builder = new StringBuilder();

            builder.Append("query {");
            builder.Append(Environment.NewLine);

            const int startPadding = 1;
            
            for (var index = 0; index < Node.Childs.Count; index++)
            {
                var childrens = Node.Childs[index];
                builder.Append(RenderObject(childrens, index == Node.Childs.Count - 1, startPadding));
            }

            builder.Append("}");
            builder.Append(Environment.NewLine);

            return builder.ToString();
        }
        
        private string RenderObject(QueryNode root, bool printDot, int padding)
        {
            var builder = new StringBuilder();

            string dotValue = (!printDot ? "," : "");
            
            string paddingString = String.Join(" ", Enumerable.Range(0, padding * 2).Select(x => " "));

            if (root.Childs.Any())
            {
                builder.Append($"{paddingString}{root.Value} {{");
                builder.Append(Environment.NewLine);

                for (var index = 0; index < root.Childs.Count; index++)
                {
                    var child = root.Childs[index];
                    builder.Append(RenderObject(child, index == root.Childs.Count - 1, padding + 1));
                }

                builder.Append(paddingString + "}" + dotValue);
                builder.Append(Environment.NewLine);
            }
            else
            {
                builder.Append($"{paddingString}{root.Value}{dotValue}{Environment.NewLine}");
            }

            return builder.ToString();
        }
    }
}