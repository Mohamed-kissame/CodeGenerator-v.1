using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogic.Models
{
    public class TableInfoModel
    {

        public string DatabaseName { get; set; }
        public string TableName { get; set; }
        public DataTable ColumnsInfo { get; set; }

    }
}
