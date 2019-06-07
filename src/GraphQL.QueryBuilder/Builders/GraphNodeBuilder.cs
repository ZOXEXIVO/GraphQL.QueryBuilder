using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using GraphQL.QueryBuilder.Nodes;
using GraphQL.QueryBuilder.Utils;

namespace GraphQL.QueryBuilder.Builders
{
    public static class GraphNodeBuilder
    {
        public static IGraphQueryBuilder<TEntity, TProperty> Include<TEntity, TProperty>(
            this IGraphQuery<TEntity> builder, Expression<Func<TEntity, TProperty>> field) where TEntity : class
        {
            var paths = ExpressionUtils.GetFieldsPath(field);

            var lastNode = BuildTreeForNode(builder.Node, paths.FirstOrDefault(), paths);

            return new GraphGraphQueryBuilder<TEntity, TProperty>(builder.Node, lastNode);
        }

        public static IGraphQueryBuilder<TEntity, TProperty> ThenInclude<TEntity, TPreviousProperty, TProperty>(
            this IGraphQueryBuilder<TEntity, IEnumerable<TPreviousProperty>> builder,
            Expression<Func<TPreviousProperty, TProperty>> field) where TEntity : class
        {
            var paths = ExpressionUtils.GetFieldsPath(field);

            var lastNode = BuildTreeForNode(builder.LastNode, paths.FirstOrDefault(), paths);
            
            return new GraphGraphQueryBuilder<TEntity, TProperty>(builder.Node, lastNode);
        }
        
        private static QueryNode BuildTreeForNode(QueryNode node, string currentPath, List<string> paths)
        {
            if (currentPath == null || node == null)
                return null;

            var lastPaths = paths.Skip(1).ToList();
            var lastPath = lastPaths.FirstOrDefault();

            var existingNode = node.Childs.FirstOrDefault(x => x.Value == currentPath);
            if (existingNode == null)
            {
                var newNode = node.AddChild(currentPath);

                return BuildTreeForNode(newNode, lastPath, lastPaths) ?? newNode;
            }

            return BuildTreeForNode(existingNode, lastPath, lastPaths) ?? existingNode;
        }
    }
}