using System.Linq;
using GraphQL.QueryBuilder.Nodes;
using GraphQL.QueryBuilder.Tests.Queries;
using Xunit;

namespace GraphQL.QueryBuilder.Tests
{
    public class QueryNodeTests
    {
        [Fact]
        public void Query_Initialize()
        {
            var queryNode = new QueryNode<TestUser>();
            
            Assert.Null(queryNode.Value);
            Assert.Empty(queryNode.Childs);
        }
        
        [Fact]
        public void Query_AddChild()
        {
            var queryNode = new QueryNode<TestUser>();

            var addedNode = queryNode.AddChild("CHILD");

            Assert.Equal("CHILD", addedNode.Value);
            Assert.Equal("CHILD", queryNode.Childs.Single().Value);
        }
    }
}