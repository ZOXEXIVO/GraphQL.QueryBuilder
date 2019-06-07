using System.Collections.Generic;

namespace GraphQL.QueryBuilder.Tests.Queries
{
    public class TestUser
    {
        public string FirstName { get; set; }

        public TestDepartment Department { get; set; }

        public List<TestFriend> Friends { get; set; }
    }

    public class TestFriend
    {
        public string Name { get; set; }
        
        public TestDepartment Department { get; set; }
    }

    public class TestDepartment
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public TestDepartment Parent { get; set; }
    }
}