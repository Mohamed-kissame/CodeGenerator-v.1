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
           return $"Server={ServerName};User Id={UserName};Password={Password};Initial Catalog=master";

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
                isConnected = false;
            }

            return isConnected;

        }

    }
}
