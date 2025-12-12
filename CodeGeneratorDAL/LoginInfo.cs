using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BussinessLogic;

namespace CodeGeneratorDAL
{
    public class LoginInfo
    {

        
        private static ClsServerConnection _SelectedLoginInfo;


        public static ClsServerConnection SelectedLoginInfo { get { return _SelectedLoginInfo; } }


        public static void Set(ClsServerConnection ServerInfo)
        {

            _SelectedLoginInfo = ServerInfo;

        }


    }
}
