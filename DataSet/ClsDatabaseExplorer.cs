using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSet
{
    public class ClsDatabaseExplorer
    {

        public static DataTable GetAllDataBases()
        {

            DataTable db = new DataTable();

            using (SqlConnection connection = new SqlConnection(ClsConectionString.ConnectionString))
            {

                string Query = "select * From sys.databases";

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

    }
}
