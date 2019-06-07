using GraphQL.QueryBuilder.Builders;
using GraphQL.QueryBuilder.Nodes;

namespace GraphQL.QueryBuilder
{
    public static class GraphQuery
    {
        public static IGraphQueryBuilder<TEntity, TEntity> Query<TEntity>()
        {
            return new GraphGraphQueryBuilder<TEntity, TEntity>(new QueryNode(), null);
        }
    }
}