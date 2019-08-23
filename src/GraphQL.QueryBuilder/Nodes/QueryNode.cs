using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace GraphQL.QueryBuilder.Nodes
{
    public class QueryNode<TEntity>
    {
        public string Name { get; }
        public string Value { get; }

        public Expression<Func<TEntity, bool>> FilterExpression { get; }

        public readonly List<QueryNode<TEntity>> Childs = new List<QueryNode<TEntity>>();

        public QueryNode(string name = null,string value = null, Expression<Func<TEntity, bool>> filterExpression = null)
        {
            Name = name;
            Value = value;

            FilterExpression = filterExpression;
        }

        public bool HasFilter()
        {
            return FilterExpression != null;
        }

        public QueryNode<TEntity> AddChild(string value)
        {
            var node = new QueryNode<TEntity>(name: null, value: value);
        
            Childs.Add(node);
        
            return node;
        }
    }
}