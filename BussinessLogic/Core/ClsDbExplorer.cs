using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using BussinessLogic.Mapping;
using BussinessLogic.Models;
using DataSet;

namespace BussinessLogic
{
    public class ClsDbExplorer 
    {

        public static string DbName { get; set; }

        public static string TableName { get; set; }
        
        public static string ServerName { get; set; }

        public static DatabaseModel GetDatabses()
        {

            DatabaseModel db = new DatabaseModel();
            db.Databases = ClsDatabaseExplorer.GetAllDataBases();
            return db;

        }

        public static TableListModel GetTablesInsideTheSelectedDB()
        {

            TableListModel model = new TableListModel();
            model.DatabaseName = DbName;
            model.Tables = ClsDatabaseExplorer.GetTables(ServerName, model.DatabaseName);
            return model;

        }

        public static TableInfoModel GetTableInformation()
        {
            TableInfoModel tableInfo = new TableInfoModel();
            tableInfo.DatabaseName = DbName;
            tableInfo.TableName = TableName;

           
            DataTable columnsData = ClsDatabaseExplorer.GetTableInformation(ServerName, DbName, TableName);

            
            DataTable primaryKeysData = ClsDatabaseExplorer.GetPrimaryKeys(ServerName, DbName, TableName);

            
            DataTable enhancedColumns = new DataTable();

           
            enhancedColumns.Columns.Add("ColumnName", typeof(string));
            enhancedColumns.Columns.Add("SqlType", typeof(string));
            enhancedColumns.Columns.Add("CSharpType", typeof(string));
            enhancedColumns.Columns.Add("IsPrimaryKey", typeof(bool));

            if (columnsData != null && columnsData.Rows.Count > 0)
            {
                foreach (DataRow columnRow in columnsData.Rows)
                {
                   
                    DataRow newRow = enhancedColumns.NewRow();

                    
                    string columnName = columnRow["ColumnName"].ToString();
                    string sqlType = columnRow["DataType"].ToString();

                   
                    newRow["ColumnName"] = columnName;
                    newRow["SqlType"] = sqlType;

                   
                    newRow["CSharpType"] = ClsSqlTypeMapper.GetCSharpType(sqlType);

                   
                    newRow["IsPrimaryKey"] = IsPrimaryKeyColumn(columnName, primaryKeysData);

                    
                    enhancedColumns.Rows.Add(newRow);
                }
            }

           
            tableInfo.ColumnsInfo = enhancedColumns;

            return tableInfo;
        }

       
        private static bool IsPrimaryKeyColumn(string columnName, DataTable primaryKeysData)
        {
            if (primaryKeysData == null || primaryKeysData.Rows.Count == 0)
                return false;

           
            foreach (DataRow pkRow in primaryKeysData.Rows)
            {
                string pkColumnName = pkRow["ColumnName"].ToString();

                
                if (string.Equals(pkColumnName, columnName, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }



    }
}
