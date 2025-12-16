using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DataSet
{
    public class ClsDatabaseExplorer
    {

        private static SqlConnection ConnectToDatabase(string servername , string dbName) {

            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();


            builder.DataSource = servername;
            builder.InitialCatalog = dbName;
            builder.IntegratedSecurity = true;

            

            string connectionString = builder.ToString();

            return new SqlConnection(connectionString); 
        
        
        }

        public static DataTable GetAllDataBases(string servername , string dbname)
        {

            DataTable db = new DataTable();

            using (SqlConnection connection = new SqlConnection(ClsConectionString.ConnectionString))
            {

                string Query = "select name From sys.databases where database_id > 4";

                using (SqlCommand command = new SqlCommand(Query, connection))
                {

                    try
                    {

                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {

                            if (reader.HasRows)
                            {
                                db.Load(reader);
                            }

                        }

                    }
                    catch (Exception ex) { Console.WriteLine("error message : " + ex.Message); db = null; }


                }
            }

            return db;
        }

        public static DataTable GetTables(string Servername ,string DbName)
        {

            DataTable table = new DataTable();

            using(SqlConnection connection = ConnectToDatabase(Servername, DbName))
            {


                string Query = "select tables.name from sys.tables ";

                using(SqlCommand command = new SqlCommand(Query, connection))
                {


                    try
                    {

                        connection.Open();

                        using(SqlDataReader reader = command.ExecuteReader())
                        {

                            if (reader.HasRows)
                                table.Load(reader);
                        }

                    }catch(Exception ex) { Console.WriteLine("error message : " + ex.Message); table = null; }
                }



            }

            return table;

        }

        public static DataTable GetTableInformation(string ServerName ,string DbName , string TableName)
        {

            DataTable Info = new DataTable();


            using (SqlConnection connection = ConnectToDatabase(ServerName, DbName))
            {

                string Query = $@"SELECT c.name AS ColumnName, t.name AS DataType
                                      FROM sys.columns c
                                      JOIN sys.types t ON c.user_type_id = t.user_type_id
                                      WHERE c.object_id = OBJECT_ID(@TableName)";


                using (SqlCommand command = new SqlCommand(Query, connection)) {

                    command.Parameters.AddWithValue("@TableName", TableName);

                    try
                    {

                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {

                            if (reader.HasRows)
                            {
                                Info.Load(reader);
                            }

                        }
                    } catch (Exception ex) { Console.WriteLine("error message : " + ex.Message); Info = null; }

                }

            }

            return Info;

        }

        

    }
}
