using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogic.Models
{
    public class TableListModel
    {

        public string DatabaseName { get; set; }
        public DataTable Tables { get; set; }


    }
}
