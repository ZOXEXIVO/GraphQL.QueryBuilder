# GraphQL.QueryBuilder

GraphQL.QueryBuilder is simple utility for creating strong typed GraphQL queries  for .NET objects in EntityFramework Include-style.

At current state work only on simple queries.


**Code**

```csharp
static void Main(string[] args)
{
    var renderedQuery = GraphQuery.Query<User>(user => user.FirstName == "FIRSTNAME")
        .Include(x => x.Friends).ThenInclude(x => x.Department)
        .Include(x => x.FirstName)
        .Include(x => x.Department.Id)
        .Include(x => x.Department.Name)
        .Include(x => x.Friends).ThenInclude(x => x.Department.Parent.Id)
        .Include(x => x.Friends).ThenInclude(x => x.Department.Parent.Name)
        .Include(x => x.Friends).ThenInclude(x => x.Department.Parent.Parent)
        .Render();

        Console.WriteLine(renderedQuery);
}
```

**Result**

```js
query (firstName : "FIRSTNAME"){
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
}
```

## License

[Apache Version 2.0](LICENSE)