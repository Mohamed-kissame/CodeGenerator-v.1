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

            cbDataChose.SelectedIndex = 0;
            cbTableChose.SelectedIndex = 0;

            if(LoginInfo.SelectedLoginInfo.ServerName == ".")
            {
                lblServerName.Text = "Local Host";
            }
            else
            {
                lblServerName.Text = LoginInfo.SelectedLoginInfo.ServerName;
            }

            lblUserName.Text = LoginInfo.SelectedLoginInfo.UserName;
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            this.Close();
            _Login.Show();
        }

        private void guna2Panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2DataGridView1_Paint(object sender, PaintEventArgs e)
        {
            if (dgv.Rows.Count == 0)
            {
                string message = "No Table Selected Yet.";
                using (Font font = new Font("Segoe UI", 12, FontStyle.Bold))
                {
                    TextRenderer.DrawText(
                        e.Graphics,
                        message,
                        font,
                        dgv.ClientRectangle,
                        Color.Gray,
                        TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
                }
            }
        }

        private void guna2ComboBox2_Validating(object sender, CancelEventArgs e)
        {
        }
    }
}
