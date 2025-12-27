using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogic.Models.ColumnModels
{
    public class ColumnInfoModel
    {

        public string ColumnName { get; set; }

        public string SqlType { get; set; }

        public string CSharpType { get; set; }

        public bool IsPrimaryKey { get; set; }

    }
}
