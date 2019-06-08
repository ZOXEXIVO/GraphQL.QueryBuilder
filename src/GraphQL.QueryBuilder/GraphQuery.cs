using System;
using System.Linq.Expressions;
using GraphQL.QueryBuilder.Builders;
using GraphQL.QueryBuilder.Nodes;

namespace GraphQL.QueryBuilder
{
    public static class GraphQuery
    {
        public static IGraphQueryBuilder<TEntity, TEntity> Query<TEntity>(
            Expression<Func<TEntity, bool>> filterExpression = null)
        {
            return new GraphGraphQueryBuilder<TEntity, TEntity>(new QueryNode<TEntity>(
                null, filterExpression), null);
        }
    }
}