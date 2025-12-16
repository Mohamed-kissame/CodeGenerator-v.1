using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSet
{
    public class ClsConectionString
    {

        public static string ConnectionString;

        private static string GenerateConnectionToServer(string ServerName , string UserName , string Password)
        {

            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

            builder.InitialCatalog = "master";
            builder.DataSource = ServerName;
            builder.UserID = UserName;
            builder.Password = Password;
            builder.InitialCatalog = "master";

            return builder.ToString();


        }

        public static bool ConnecteToTheServer(string ServerName, string UserName, string Password)
        {

            bool isConnected = false;

            ConnectionString = GenerateConnectionToServer(ServerName , UserName , Password);

            try
            {

                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {

                    connection.Open();

                    if (connection != null && connection.State == System.Data.ConnectionState.Open)
                    {

                        isConnected = true;

                    }


                }

            } catch (Exception ex)
            {
                Console.WriteLine("error message : " + ex.Message);
                isConnected = false;
            }

            return isConnected;

        }

    }
}
