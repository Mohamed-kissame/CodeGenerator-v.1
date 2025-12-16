using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using DataSet;

namespace BussinessLogic
{
    public class ClsDbExplorer
    {

        public static string DbName { get; set; }

        public static string TableName { get; set; }
        
        public static string ServerName { get; set; }

        public static DataTable Databses()
        {

            return ClsDatabaseExplorer.GetAllDataBases(ServerName , DbName);
        }


        public static DataTable TablesInsideTheSelectedDB(string DbName)
        {

            return ClsDatabaseExplorer.GetTables(ServerName,DbName);
        }

        public static DataTable TableInformation()
        {
            return ClsDatabaseExplorer.GetTableInformation(ServerName, DbName, TableName);
        }

    }
}
