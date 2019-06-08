using GraphQL.QueryBuilder.Builders;
using Xunit;

namespace GraphQL.QueryBuilder.Tests.Queries
{
    public class QueriesTests
    {
        [Fact]
        public void Query_Render_IsCorrect()
        {
            var renderedQuery = GraphQuery.Query<TestUser>(user => user.FirstName == "FIRSTNAME")
                .Include(x => x.Friends).ThenInclude(x => x.Department)
                .Include(x => x.FirstName)
                .Include(x => x.Department.Id)
                .Include(x => x.Department.Name)
                .Include(x => x.Friends).ThenInclude(x => x.Department.Parent.Id)
                .Include(x => x.Friends).ThenInclude(x => x.Department.Parent.Name)
                .Include(x => x.Friends).ThenInclude(x => x.Department.Parent.Parent)
                .Render();

            string correctQuery = @"query (firstName : ""FIRSTNAME"") {
                                        friends {
                                            department {
                                                parent {
                                                    id,
                                                    name,
                                                    parent
                                                }
                                            }
                                        },
                                        firstName,
                                        department {
                                            id,
                                            name
                                        }
                                    }";
            
            
            Assert.Equal(ClearQuery(correctQuery), ClearQuery(renderedQuery));

            string ClearQuery(string query)
            {
                return query.Replace("\r\n", "").Replace(" ", "");
            }
        }
    }
}