using System;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using GraphQL.QueryBuilder.Nodes;
using GraphQL.QueryBuilder.Utils;

namespace GraphQL.QueryBuilder.Builders
{
    public interface IGraphQuery<TEntity>
    {
        QueryNode<TEntity> Node { get; }
    }

    public interface IGraphQueryBuilder<TEntity, out TProperty> : IGraphQuery<TEntity>
    {
        QueryNode<TEntity> LastNode { get; }
        
        string Render();
    }
    
    internal class GraphGraphQueryBuilder<TEntity, TProperty> : IGraphQueryBuilder<TEntity, TProperty>
    {
        public QueryNode<TEntity> Node { get; }
        public QueryNode<TEntity> LastNode { get; }

        public GraphGraphQueryBuilder(QueryNode<TEntity> node, QueryNode<TEntity> lastNode)
        {
            Node = node;
            LastNode = lastNode;
        }
                
        //TODO - Use Visitor
        public string Render()
        {
            var builder = new StringBuilder();

            builder.Append("query");
            
            if (Node.HasFilter())
            {
                builder.AppendFormat(" {0}",  RenderFilter(Node));
            }
            
            builder.Append(" {");
           
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

        private string RenderFilter(QueryNode<TEntity> node)
        {
            if (node.FilterExpression == null)
                return string.Empty;
            
            var builder = new StringBuilder();

            builder.Append("(");
            
            var expressionBody = node.FilterExpression.Body;

            if (expressionBody.NodeType != ExpressionType.Equal)
            {
                throw new NotSupportedException();
            }

            if (expressionBody is BinaryExpression binaryExpression)
            {
                var left = binaryExpression.Left as MemberExpression;
                var right = binaryExpression.Right  as ConstantExpression;

                builder.Append($"{left.Member.Name.ToCamelCase()} : {GetTypedValue(right)}");
            }
            else
            {
                //TODO - Logical expression
                throw new NotSupportedException();
            }
            
            builder.Append(")");
            
            return builder.ToString();

            string GetTypedValue(ConstantExpression expr)
            {
                if (expr.Type == typeof(string))
                {
                    return $"\"{expr.Value}\"";
                }

                return expr.Value.ToString();
            }
        }

        private string RenderObject(QueryNode<TEntity> root, bool printDot, int padding)
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