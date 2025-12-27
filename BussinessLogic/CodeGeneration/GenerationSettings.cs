using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogic.CodeGeneration
{
    public class GenerationSettings
    {

            public DataTable SelectedColumns { get; set; }

           
            public bool GenerateInsert { get; set; }
            public string InsertMethodName { get; set; }

            public bool GenerateUpdate { get; set; }
            public string UpdateMethodName { get; set; }

            public bool GenerateDelete { get; set; }
            public string DeleteMethodName { get; set; }

            public bool GenerateGetAll { get; set; }
            public string GetAllMethodName { get; set; }

            public bool GenerateGetById { get; set; }
            public string GetByIdMethodName { get; set; }

            
            public string UserNamespace { get; set; }
            public string UserClassName { get; set; }

           
            public string TableName { get; set; }


           public string ServerName { get; set; }
           public string DatabaseName { get; set; }
           public string UserName { get; set; }
           public string Password { get; set; }
           public bool UseWindowsAuthentication { get; set; }

        public GenerationSettings() {
                
                InsertMethodName = "Insert";
                UpdateMethodName = "Update";
                DeleteMethodName = "Delete";
                GetAllMethodName = "GetAll";
                GetByIdMethodName = "GetById";
                UserClassName = "DAL";
                UserNamespace = "GeneratedCode";
            UseWindowsAuthentication = true;
        }
    }

    
}
