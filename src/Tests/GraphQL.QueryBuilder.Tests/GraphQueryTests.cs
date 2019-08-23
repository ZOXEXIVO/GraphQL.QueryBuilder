using Xunit;

namespace GraphQL.QueryBuilder.Tests
{
    public class GraphQueryTests
    {
        [Fact]
        public void Query_Create_IsCorrect()
        {
            var query = GraphQuery.Query<GraphQueryTests>("queryName");

            Assert.Null(query.LastNode);
            Assert.Null(query.Node.Value);
            Assert.Empty(query.Node.Childs);
        }
    }
}