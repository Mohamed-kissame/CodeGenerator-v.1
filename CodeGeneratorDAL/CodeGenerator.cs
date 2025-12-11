using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CodeGeneratorDAL
{
    public partial class CodeGenerator : Form
    {

        private Login _Login;

        public CodeGenerator(Login login)
        {
            InitializeComponent();
            _Login = login;
        }

        private void CodeGenerator_Load(object sender, EventArgs e)
        {
            lblDate.Text = DateTime.Now.ToString();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            this.Close();
            _Login.Show();
        }
    }
}
