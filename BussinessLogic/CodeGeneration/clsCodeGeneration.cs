using System;
using System.Data;
using System.Text;

namespace BussinessLogic.CodeGeneration
{
    public class clsCodeGeneration
    {


        private static string GenerateConnectionStringCode(GenerationSettings settings)
        {
            StringBuilder connectionCode = new StringBuilder();

            connectionCode.AppendLine("        private static string connectionString");
            connectionCode.AppendLine("        {");
            connectionCode.AppendLine("            get");
            connectionCode.AppendLine("            {");
            connectionCode.AppendLine("                System.Data.SqlClient.SqlConnectionStringBuilder builder = new System.Data.SqlClient.SqlConnectionStringBuilder();");
            connectionCode.AppendLine();
            connectionCode.AppendLine($"                builder.DataSource = \"{settings.ServerName}\";");
            connectionCode.AppendLine($"                builder.InitialCatalog = \"{settings.DatabaseName}\";");
            connectionCode.AppendLine();

            if (settings.UseWindowsAuthentication)
            {
                connectionCode.AppendLine("                // Using Windows Authentication");
                connectionCode.AppendLine("                builder.IntegratedSecurity = true;");
            }
            else
            {
                connectionCode.AppendLine("                // Using SQL Server Authentication");
                connectionCode.AppendLine($"                builder.UserID = \"{settings.UserName}\";");
                connectionCode.AppendLine($"                builder.Password = \"{settings.Password}\";");
                connectionCode.AppendLine("                builder.IntegratedSecurity = false;");
            }

            connectionCode.AppendLine("                return builder.ToString();");
            connectionCode.AppendLine("            }");
            connectionCode.AppendLine("        }");

            return connectionCode.ToString();
        }

        public static string GenerateDAL(GenerationSettings settings)
        {
            StringBuilder code = new StringBuilder();

           
            code.AppendLine("using System;");
            code.AppendLine("using System.Data;");
            code.AppendLine("using System.Data.SqlClient;");
            code.AppendLine();

          
            code.AppendLine("namespace " + settings.UserNamespace);
            code.AppendLine("{");

           
            code.AppendLine("    public class " + settings.UserClassName);
            code.AppendLine("    {");

           
            code.Append(GenerateConnectionStringCode(settings));
            code.AppendLine();

          

            if (settings.GenerateGetAll)
            {
                code.Append(GenerateGetAllMethod(settings));
                code.AppendLine();
            }

            if (settings.GenerateGetById)
            {
                code.Append(GenerateGetByIdMethod(settings));
                code.AppendLine();
            }

            if (settings.GenerateInsert)
            {
                code.Append(GenerateInsertMethod(settings));
                code.AppendLine();
            }

            if (settings.GenerateUpdate)
            {
                code.Append(GenerateUpdateMethod(settings));
                code.AppendLine();
            }

            if (settings.GenerateDelete)
            {
                code.Append(GenerateDeleteMethod(settings));
                code.AppendLine();
            }

          
            code.AppendLine("    }");
            code.AppendLine("}");

            return code.ToString();
        }

        private static string GenerateGetAllMethod(GenerationSettings settings)
        {
            StringBuilder method = new StringBuilder();

            method.AppendLine($"        static public DataTable {settings.GetAllMethodName}()");
            method.AppendLine("        {");
            method.AppendLine("            DataTable dt = new DataTable();");
            method.AppendLine();
            method.AppendLine("            using (SqlConnection connection = new SqlConnection(connectionString))");
            method.AppendLine("            {");
            method.Append("                string query = \"SELECT ");

           
            bool firstColumn = true;
            foreach (DataRow row in settings.SelectedColumns.Rows)
            {
                if (!firstColumn) method.Append(", ");
                method.Append(row["ColumnName"].ToString());
                firstColumn = false;
            }

            method.AppendLine($" FROM {settings.TableName}\";");
            method.AppendLine();
            method.AppendLine("                using (SqlCommand command = new SqlCommand(query, connection))");
            method.AppendLine("                {");
            method.AppendLine("                    try");
            method.AppendLine("                    {");
            method.AppendLine("                        connection.Open();");
            method.AppendLine();
            method.AppendLine("                        using (SqlDataReader reader = command.ExecuteReader())");
            method.AppendLine("                        {");
            method.AppendLine("                            if (reader.HasRows)");
            method.AppendLine("                            {");
            method.AppendLine("                                dt.Load(reader);");
            method.AppendLine("                            }");
            method.AppendLine("                        }");
            method.AppendLine("                    }");
            method.AppendLine("                    catch (Exception ex)");
            method.AppendLine("                    {");
            method.AppendLine("                        // Handle error");
            method.AppendLine("                        Console.WriteLine(ex.Message);");
            method.AppendLine("                    }");
            method.AppendLine("                }");
            method.AppendLine("            }");
            method.AppendLine();
            method.AppendLine("            return dt;");
            method.AppendLine("        }");

            return method.ToString();
        }

        private static string GenerateGetByIdMethod(GenerationSettings settings)
        {
            StringBuilder method = new StringBuilder();

          
            string pkColumnName = "";
            string pkCSharpType = "";

            foreach (DataRow row in settings.SelectedColumns.Rows)
            {
                bool isPrimaryKey = Convert.ToBoolean(row["IsPrimaryKey"]);
                if (isPrimaryKey)
                {
                    pkColumnName = row["ColumnName"].ToString();
                    pkCSharpType = row["CSharpType"].ToString();
                    break;
                }
            }

            if (string.IsNullOrEmpty(pkColumnName))
            {
                return "        // No primary key found for GetById method\n";
            }

          
            method.Append($"        static public bool {settings.GetByIdMethodName}({pkCSharpType} {pkColumnName}");

            
            foreach (DataRow row in settings.SelectedColumns.Rows)
            {
                string columnName = row["ColumnName"].ToString();
                string csharpType = row["CSharpType"].ToString();
                bool isPrimaryKey = Convert.ToBoolean(row["IsPrimaryKey"]);

                if (!isPrimaryKey)
                {
                    method.Append($", ref {csharpType} {columnName}");
                }
            }

            method.AppendLine(")");
            method.AppendLine("        {");
            method.AppendLine("            bool isFound = false;");
            method.AppendLine();
            method.AppendLine("            using (SqlConnection connection = new SqlConnection(connectionString))");
            method.AppendLine("            {");
            method.AppendLine($"                string query = \"SELECT * FROM {settings.TableName} WHERE {pkColumnName} = @{pkColumnName}\";");
            method.AppendLine();
            method.AppendLine("                using (SqlCommand command = new SqlCommand(query, connection))");
            method.AppendLine("                {");
            method.AppendLine($"                    command.Parameters.AddWithValue(\"@{pkColumnName}\", {pkColumnName});");
            method.AppendLine();
            method.AppendLine("                    try");
            method.AppendLine("                    {");
            method.AppendLine("                        connection.Open();");
            method.AppendLine();
            method.AppendLine("                        using (SqlDataReader reader = command.ExecuteReader())");
            method.AppendLine("                        {");
            method.AppendLine("                            if (reader.Read())");
            method.AppendLine("                            {");
            method.AppendLine("                                isFound = true;");
            method.AppendLine();

            
            foreach (DataRow row in settings.SelectedColumns.Rows)
            {
                string columnName = row["ColumnName"].ToString();
                string csharpType = row["CSharpType"].ToString();
                bool isPrimaryKey = Convert.ToBoolean(row["IsPrimaryKey"]);

                if (!isPrimaryKey)
                {
                    if (csharpType == "string")
                    {
                        method.AppendLine($"                                {columnName} = (string)reader[\"{columnName}\"];");
                    }
                    else if (csharpType == "int")
                    {
                        method.AppendLine($"                                {columnName} = (int)reader[\"{columnName}\"];");
                    }
                    else if (csharpType == "DateTime")
                    {
                        method.AppendLine($"                                {columnName} = (DateTime)reader[\"{columnName}\"];");
                    }
                    else if (csharpType == "bool")
                    {
                        method.AppendLine($"                                {columnName} = Convert.ToBoolean(reader[\"{columnName}\"]);");
                    }
                    else if (csharpType == "decimal")
                    {
                        method.AppendLine($"                                {columnName} = (decimal)reader[\"{columnName}\"];");
                    }
                    else
                    {
                        method.AppendLine($"                                {columnName} = ({csharpType})reader[\"{columnName}\"];");
                    }
                }
            }

            method.AppendLine("                            }");
            method.AppendLine("                        }");
            method.AppendLine("                    }");
            method.AppendLine("                    catch (Exception ex)");
            method.AppendLine("                    {");
            method.AppendLine("                        Console.WriteLine(ex.Message);");
            method.AppendLine("                    }");
            method.AppendLine("                }");
            method.AppendLine("            }");
            method.AppendLine();
            method.AppendLine("            return isFound;");
            method.AppendLine("        }");

            return method.ToString();
        }

        private static string GenerateInsertMethod(GenerationSettings settings)
        {
            StringBuilder method = new StringBuilder();

           

            method.AppendLine($"        static public int {settings.InsertMethodName}(");

         
            bool firstParam = true;
            foreach (DataRow row in settings.SelectedColumns.Rows)
            {
                string columnName = row["ColumnName"].ToString();
                string csharpType = row["CSharpType"].ToString();
                bool isPrimaryKey = Convert.ToBoolean(row["IsPrimaryKey"]);

               
                if (isPrimaryKey) continue;

                if (!firstParam) method.AppendLine(",");
                method.Append($"                                 {csharpType} {columnName}");
                firstParam = false;
            }

            method.AppendLine(")");
            method.AppendLine("        {");
            method.AppendLine("            int newId = -1;");
            method.AppendLine();
            method.AppendLine("            using (SqlConnection connection = new SqlConnection(connectionString))");
            method.AppendLine("            {");

            
            method.Append("                string query = \"INSERT INTO " + settings.TableName + " (");

          
            firstParam = true;
            foreach (DataRow row in settings.SelectedColumns.Rows)
            {
                string columnName = row["ColumnName"].ToString();
                bool isPrimaryKey = Convert.ToBoolean(row["IsPrimaryKey"]);

                if (isPrimaryKey) continue; 

                if (!firstParam) method.Append(", ");
                method.Append(columnName);
                firstParam = false;
            }

            method.Append(") VALUES (");

         
            firstParam = true;
            foreach (DataRow row in settings.SelectedColumns.Rows)
            {
                string columnName = row["ColumnName"].ToString();
                bool isPrimaryKey = Convert.ToBoolean(row["IsPrimaryKey"]);

                if (isPrimaryKey) continue;

                if (!firstParam) method.Append(", ");
                method.Append("@" + columnName);
                firstParam = false;
            }

            method.AppendLine("); SELECT SCOPE_IDENTITY();\";");
            method.AppendLine();
            method.AppendLine("                using (SqlCommand command = new SqlCommand(query, connection))");
            method.AppendLine("                {");

          
            foreach (DataRow row in settings.SelectedColumns.Rows)
            {
                string columnName = row["ColumnName"].ToString();
                bool isPrimaryKey = Convert.ToBoolean(row["IsPrimaryKey"]);

                if (isPrimaryKey) continue;

                method.AppendLine($"                    command.Parameters.AddWithValue(\"@{columnName}\", {columnName});");
            }

            method.AppendLine();
            method.AppendLine("                    try");
            method.AppendLine("                    {");
            method.AppendLine("                        connection.Open();");
            method.AppendLine("                        object result = command.ExecuteScalar();");
            method.AppendLine();
            method.AppendLine("                        if (result != null && int.TryParse(result.ToString(), out int insertedId))");
            method.AppendLine("                        {");
            method.AppendLine("                            newId = insertedId;");
            method.AppendLine("                        }");
            method.AppendLine("                        else");
            method.AppendLine("                        {");
            method.AppendLine("                            newId = -1;");
            method.AppendLine("                        }");
            method.AppendLine("                    }");
            method.AppendLine("                    catch (Exception ex)");
            method.AppendLine("                    {");
            method.AppendLine("                        newId = -1;");
            method.AppendLine("                    }");
            method.AppendLine("                }");
            method.AppendLine("            }");
            method.AppendLine();
            method.AppendLine("            return newId;");
            method.AppendLine("        }");

            return method.ToString();
        }

        private static string GenerateUpdateMethod(GenerationSettings settings)
        {
            StringBuilder method = new StringBuilder();

          
            string pkColumnName = "";
            string pkCSharpType = "";

            foreach (DataRow row in settings.SelectedColumns.Rows)
            {
                bool isPrimaryKey = Convert.ToBoolean(row["IsPrimaryKey"]);
                if (isPrimaryKey)
                {
                    pkColumnName = row["ColumnName"].ToString();
                    pkCSharpType = row["CSharpType"].ToString();
                    break;
                }
            }

            if (string.IsNullOrEmpty(pkColumnName))
            {
                return "        // No primary key found for Update method\n";
            }

            method.AppendLine($"        static public bool {settings.UpdateMethodName}({pkCSharpType} {pkColumnName},");

          
            bool firstParam = true;
            foreach (DataRow row in settings.SelectedColumns.Rows)
            {
                string columnName = row["ColumnName"].ToString();
                string csharpType = row["CSharpType"].ToString();
                bool isPrimaryKey = Convert.ToBoolean(row["IsPrimaryKey"]);

                if (isPrimaryKey) continue;

                if (!firstParam) method.AppendLine(",");
                method.Append($"                                 {csharpType} {columnName}");
                firstParam = false;
            }

            method.AppendLine(")");
            method.AppendLine("        {");
            method.AppendLine("            int rowsAffected = 0;");
            method.AppendLine();
            method.AppendLine("            using (SqlConnection connection = new SqlConnection(connectionString))");
            method.AppendLine("            {");

           
            method.AppendLine($"                string query = \"UPDATE {settings.TableName} SET \" +");
            method.Append("                               \"");

           
            firstParam = true;
            foreach (DataRow row in settings.SelectedColumns.Rows)
            {
                string columnName = row["ColumnName"].ToString();
                bool isPrimaryKey = Convert.ToBoolean(row["IsPrimaryKey"]);

                if (isPrimaryKey) continue;

                if (!firstParam) method.Append(", ");
                method.Append(columnName + " = @" + columnName);
                firstParam = false;
            }

            method.AppendLine($" WHERE {pkColumnName} = @{pkColumnName}\";");
            method.AppendLine();
            method.AppendLine("                using (SqlCommand command = new SqlCommand(query, connection))");
            method.AppendLine("                {");

           
            method.AppendLine($"                    command.Parameters.AddWithValue(\"@{pkColumnName}\", {pkColumnName});");

            foreach (DataRow row in settings.SelectedColumns.Rows)
            {
                string columnName = row["ColumnName"].ToString();
                bool isPrimaryKey = Convert.ToBoolean(row["IsPrimaryKey"]);

                if (isPrimaryKey) continue;

                method.AppendLine($"                    command.Parameters.AddWithValue(\"@{columnName}\", {columnName});");
            }

            method.AppendLine();
            method.AppendLine("                    try");
            method.AppendLine("                    {");
            method.AppendLine("                        connection.Open();");
            method.AppendLine("                        rowsAffected = command.ExecuteNonQuery();");
            method.AppendLine("                    }");
            method.AppendLine("                    catch (Exception ex)");
            method.AppendLine("                    {");
            method.AppendLine("                        return false;");
            method.AppendLine("                    }");
            method.AppendLine("                }");
            method.AppendLine("            }");
            method.AppendLine();
            method.AppendLine("            return (rowsAffected > 0);");
            method.AppendLine("        }");

            return method.ToString();
        }

        private static string GenerateDeleteMethod(GenerationSettings settings)
        {
            StringBuilder method = new StringBuilder();

           
            string pkColumnName = "";
            string pkCSharpType = "";

            foreach (DataRow row in settings.SelectedColumns.Rows)
            {
                bool isPrimaryKey = Convert.ToBoolean(row["IsPrimaryKey"]);
                if (isPrimaryKey)
                {
                    pkColumnName = row["ColumnName"].ToString();
                    pkCSharpType = row["CSharpType"].ToString();
                    break;
                }
            }

            if (string.IsNullOrEmpty(pkColumnName))
            {
                return "        // No primary key found for Delete method\n";
            }

            method.AppendLine($"        static public bool {settings.DeleteMethodName}({pkCSharpType} {pkColumnName})");
            method.AppendLine("        {");
            method.AppendLine("            int rowsAffected = 0;");
            method.AppendLine();
            method.AppendLine("            using (SqlConnection connection = new SqlConnection(connectionString))");
            method.AppendLine("            {");
            method.AppendLine($"                string query = \"DELETE FROM {settings.TableName} WHERE {pkColumnName} = @{pkColumnName}\";");
            method.AppendLine();
            method.AppendLine("                using (SqlCommand command = new SqlCommand(query, connection))");
            method.AppendLine("                {");
            method.AppendLine($"                    command.Parameters.AddWithValue(\"@{pkColumnName}\", {pkColumnName});");
            method.AppendLine();
            method.AppendLine("                    try");
            method.AppendLine("                    {");
            method.AppendLine("                        connection.Open();");
            method.AppendLine("                        rowsAffected = command.ExecuteNonQuery();");
            method.AppendLine("                    }");
            method.AppendLine("                    catch (Exception ex)");
            method.AppendLine("                    {");
            method.AppendLine("                        return false;");
            method.AppendLine("                    }");
            method.AppendLine("                }");
            method.AppendLine("            }");
            method.AppendLine();
            method.AppendLine("            return (rowsAffected > 0);");
            method.AppendLine("        }");

            return method.ToString();
        }
    }
}