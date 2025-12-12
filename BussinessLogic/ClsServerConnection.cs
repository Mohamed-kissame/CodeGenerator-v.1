using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataSet;

namespace BussinessLogic
{
    public class ClsServerConnection
    {

        public string ServerName { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }


        private ClsServerConnection()
        {

            this.ServerName = null;
            this.UserName = null;
            this.Password = null;

        }

        public ClsServerConnection(string ServerName , string Username , string Pssword)
         {

            this.ServerName = ServerName;
            this.UserName = Username;
            this.Password = Pssword;

         }

        public bool ServerConnected()
        {
            return ClsConectionString.ConnecteToTheServer(this.ServerName, this.UserName, this.Password);
        }

    }
}
