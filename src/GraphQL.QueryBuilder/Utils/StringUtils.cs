using System;

namespace GraphQL.QueryBuilder.Utils
{
    public static class StringUtils
    {
        public static string ToCamelCase(this string str)
        {
            if (string.IsNullOrEmpty(str))
                return string.Empty;
            
            char firstChar = str[0];

            if (Char.IsLower(firstChar))
                return str;
            
            return Char.ToLowerInvariant(str[0]) + str.Substring(1);
        }
    }
}