using System.Collections.Generic;

namespace GraphQL.QueryBuilder.Example
{
    public class User
    {
        public string FirstName { get; set; }

        public Department Department { get; set; }

        public List<Friend> Friends { get; set; }
    }

    public class Friend
    {
        public string Name { get; set; }
        
        public Department Department { get; set; }
    }

    public class Department
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public Department Parent { get; set; }
    }
}