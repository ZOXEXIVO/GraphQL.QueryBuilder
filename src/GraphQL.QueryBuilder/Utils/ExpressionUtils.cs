using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace GraphQL.QueryBuilder.Utils
{
    internal static class ExpressionUtils
    {
        public static List<string> GetFieldsPath<TEntity, TProperty>(Expression<Func<TEntity, TProperty>> exp)
        {
            if (exp.Body is MemberExpression body)
            {
                return GetMemberPath(body);
            }

            return new List<string>();
            
            List<string> GetMemberPath(MemberExpression me)
            {
                var parts = new List<string>();

                while (me != null)
                {
                    parts.Add(me.Member.Name.ToCamelCase());
                    me = me.Expression as MemberExpression;
                }

                parts.Reverse();
                return parts;
            }
        }
    }
}