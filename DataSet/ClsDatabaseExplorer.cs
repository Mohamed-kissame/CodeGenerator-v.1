using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace DataSet
{
    public class ClsDatabaseExplorer
    {

        public SqlConnection ConnectToDatabase(string dbName)
        {
            string cs = $"Server=.;Database={dbName};Integrated Security=True;";
            return new SqlConnection(cs);
        }


        public static DataTable GetAllDataBases()
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
                    catch (Exception ex) { db = null; }


                }
            }

            return db;
        }

        public static DataTable GetTables(string DbName)
        {

            DataTable table = new DataTable();

            using(SqlConnection connection = new SqlConnection(ClsConectionString.ConnectionString))
            {


                string Query = $"select tables.name from {DbName}.sys.tables ";

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

                    }catch(Exception ex) { table = null; }
                }



            }

            return table;

        }

        public static DataTable GetTableInformation(string DbName , string TableName)
        {

            DataTable Info = new DataTable();


            using(SqlConnection connection = new SqlConnection(ClsConectionString.ConnectionString))
            {

                string Query = $@"SELECT c.name AS ColumnName, t.name AS DataType
                                      FROM {DbName}.sys.columns c
                                      JOIN {DbName}.sys.types t ON c.user_type_id = t.user_type_id
                                      WHERE c.object_id = OBJECT_ID('{DbName}.dbo.{TableName}')";


                using(SqlCommand command = new SqlCommand(Query , connection)) {


                    try
                    {

                        connection.Open();

                        using(SqlDataReader reader = command.ExecuteReader())
                        {

                            if (reader.HasRows)
                            {
                                Info.Load(reader);
                            }

                        }
                    }catch(Exception ex) { Info = null; }

                }

            }

            return Info;

        }

        

    }
}
