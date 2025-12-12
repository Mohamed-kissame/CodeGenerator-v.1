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

        public string DbName { get; set; }

        public string TableName { get; set; }

        public ClsDbExplorer()
        {

            this.DbName = null;
            this.TableName = null;
        }

        public ClsDbExplorer(string dbName , string tableName)
        {

            this.DbName = dbName;
            this.TableName = tableName;
        }


        public static DataTable Databses()
        {

            return ClsDatabaseExplorer.GetAllDataBases();
        }


        public static DataTable TablesInsideTheSelectedDB(string DbName)
        {

            return ClsDatabaseExplorer.GetTables(DbName);
        }

        public static DataTable TableInformation(string DbNme , string Table)
        {
            return ClsDatabaseExplorer.GetTableInformation(DbNme, Table);
        }

    }
}
