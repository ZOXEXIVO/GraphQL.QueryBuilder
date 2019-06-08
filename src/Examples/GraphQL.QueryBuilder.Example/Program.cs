using System;
using GraphQL.QueryBuilder.Builders;

namespace GraphQL.QueryBuilder.Example
{
    class Program
    {
        static void Main(string[] args)
        {
            var query = GraphQuery.Query<User>(user => user.FirstName == "FIRSTNAME")
                .Include(x => x.Friends).ThenInclude(x => x.Department)
                .Include(x => x.FirstName)
                .Include(x => x.Department.Id)
                .Include(x => x.Department.Name)
                .Include(x => x.Friends).ThenInclude(x => x.Department.Parent.Id)
                .Include(x => x.Friends).ThenInclude(x => x.Department.Parent.Name)
                .Include(x => x.Friends).ThenInclude(x => x.Department.Parent.Parent)
                .Render();

            Console.WriteLine(query);
        }
    }
}