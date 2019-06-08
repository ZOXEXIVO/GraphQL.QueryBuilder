using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace GraphQL.QueryBuilder.Nodes
{
    public class QueryNode<TEntity>
    {
        public string Value { get; }

        public Expression<Func<TEntity, bool>> FilterExpression { get; }

        public readonly List<QueryNode<TEntity>> Childs = new List<QueryNode<TEntity>>();

        public QueryNode(string value = null, Expression<Func<TEntity, bool>> filterExpression = null)
        {
            Value = value;

            FilterExpression = filterExpression;
        }

        public bool HasFilter()
        {
            return FilterExpression != null;
        }

        public QueryNode<TEntity> AddChild(string value)
        {
            var node = new QueryNode<TEntity>(value);
        
            Childs.Add(node);
        
            return node;
        }
    }
}