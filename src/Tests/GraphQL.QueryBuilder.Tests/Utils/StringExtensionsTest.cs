using GraphQL.QueryBuilder.Utils;
using Xunit;

namespace GraphQL.QueryBuilder.Tests.Utils
{
    public class StringExtensionsTest
    {
        [Fact]
        public void ToCamelCase_PascalCaseInput()
        {
            var pascalCaseString = "PascalCase";

            var camelCaseString = pascalCaseString.ToCamelCase();
            
            Assert.Equal("pascalCase", camelCaseString);
        }
        
        [Fact]
        public void ToCamelCase_CamelCaseInput()
        {
            var camelCaseString = "camelCase";

            var camelCasedString = camelCaseString.ToCamelCase();
            
            Assert.Equal("camelCase", camelCasedString);
        }
    }
}