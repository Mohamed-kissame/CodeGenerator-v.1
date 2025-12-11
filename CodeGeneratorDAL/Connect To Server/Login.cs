using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BussinessLogic;
using Guna.UI2.WinForms;

namespace CodeGeneratorDAL
{
     
    public partial class Login : Form
    {

        ClsServerConnection connection;

        public Login()
        {
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            ServerName.Focus();
        }

        private void ValideFiled(Guna2TextBox BoxText , CancelEventArgs e)
        {


            if (string.IsNullOrEmpty(BoxText.Text))
            {

                e.Cancel = true;

                errorProvider1.SetError(BoxText, "This is Required");

            }
            else
            {
                errorProvider1.SetError(BoxText, null);
            }

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {

            
            if(ServerName.Text.Trim() != "" && Username.Text.Trim() != "" && txtPassword.Text.Trim() != "")
            {

                connection = new ClsServerConnection(ServerName.Text.Trim(), Username.Text.Trim(), txtPassword.Text.Trim());

            }
            else
            {
                MessageBox.Show("All Fileds Are Required", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            if (connection.ServerConnected())
            {
                this.Hide();
                CodeGenerator Start = new CodeGenerator(this);
                Start.ShowDialog();


            }

            else
            {
                MessageBox.Show("Connection Filed Check Your Information", "Error Connection", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void guna2ToggleSwitch1_CheckedChanged(object sender, EventArgs e)
        {
            if (guna2ToggleSwitch1.Checked)
            {

                txtPassword.UseSystemPasswordChar = false;

            }
            else
            {
                txtPassword.UseSystemPasswordChar = true;
            }
        }

        private void checkLoaclHost_CheckedChanged(object sender, EventArgs e)
        {
            if (checkLoaclHost.Checked)
            {

                ServerName.Text = ".";
                ServerName.Enabled = false;
                Username.Focus();
            }
        }

        private void ServerName_Validating(object sender, CancelEventArgs e)
        {
        }

        private void Username_Validating(object sender, CancelEventArgs e)
        {
            ValideFiled(Username , e);
        }

        private void txtPassword_Validating(object sender, CancelEventArgs e)
        {
            ValideFiled(txtPassword, e);
        }

        private void guna2ControlBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
