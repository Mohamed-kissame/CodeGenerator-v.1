using System;

namespace BussinessLogic.Mapping
{
    public class ClsSqlTypeMapper
    {
      
        public static string GetCSharpType(string sqlType)
        {
            if (string.IsNullOrWhiteSpace(sqlType))
                return "object";

          
            string cleanType = CleanSqlType(sqlType);

            
            cleanType = cleanType.ToLower();

            
            switch (cleanType)
            {
                // Integer types
                case "int":
                case "integer":
                    return "int";

                case "bigint":
                    return "long";

                case "smallint":
                    return "short";

                case "tinyint":
                    return "byte";

                // Boolean
                case "bit":
                    return "bool";

                // Decimal types
                case "decimal":
                case "numeric":
                case "money":
                case "smallmoney":
                    return "decimal";

                // Floating point
                case "float":
                    return "double";

                case "real":
                    return "float";

                // Date and time
                case "datetime":
                case "datetime2":
                case "smalldatetime":
                case "date":
                    return "DateTime";

                case "time":
                    return "TimeSpan";

                case "timestamp":
                case "rowversion":
                    return "byte[]";

                // Character strings
                case "char":
                case "varchar":
                case "text":
                case "nchar":
                case "nvarchar":
                case "ntext":
                case "xml":
                    return "string";

                // Binary data
                case "binary":
                case "varbinary":
                case "image":
                    return "byte[]";

                // Other types
                case "uniqueidentifier":
                    return "Guid";

                case "sql_variant":
                    return "object";

                // Default for unknown types
                default:
                    return "object";
            }
        }

        
        private static string CleanSqlType(string sqlType)
        {
            string cleanType = sqlType.Trim();

            // Remove brackets if present [type]
            cleanType = cleanType.Replace("[", "").Replace("]", "");

            // Remove size information like (50) in varchar(50)
            int parenIndex = cleanType.IndexOf('(');
            if (parenIndex > 0)
            {
                cleanType = cleanType.Substring(0, parenIndex);
            }

            return cleanType;
        }

       
        public static bool IsValueType(string csharpType)
        {
            string type = csharpType.ToLower();

            if (type == "int") return true;
            if (type == "long") return true;
            if (type == "short") return true;
            if (type == "byte") return true;
            if (type == "bool") return true;
            if (type == "decimal") return true;
            if (type == "double") return true;
            if (type == "float") return true;
            if (type == "datetime") return true;
            if (type == "timespan") return true;
            if (type == "guid") return true;

            return false;
        }

      
        public static bool IsStringType(string csharpType)
        {
            return csharpType.ToLower() == "string";
        }

        
        public static bool IsByteArrayType(string csharpType)
        {
            return csharpType.ToLower() == "byte[]";
        }
    }
}