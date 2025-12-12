using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataSet;

namespace BussinessLogic
{
    public class ClsDbExplorer
    {

        public static DataTable Databses()
        {

            return ClsDatabaseExplorer.GetAllDataBases();
        }


        public static DataTable TablesInsideTheSelectedDB(string DbName)
        {

            return ClsDatabaseExplorer.GetTables(DbName);
        }

    }
}
