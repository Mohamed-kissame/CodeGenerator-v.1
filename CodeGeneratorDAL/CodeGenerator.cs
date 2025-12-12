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
    public partial class CodeGenerator : Form
    {

        private Login _Login;

        private DataTable _dtTableInformation;
     

        public CodeGenerator(Login login)
        {
            InitializeComponent();
            _Login = login;
        }

        private void FillDatabases()
        {

            DataTable dt = ClsDbExplorer.Databses();

            foreach(DataRow row in dt.Rows)
            {

                cbDataChose.Items.Add(row["name"]);

            }

        }

        private void FillTables(string DbName)
        {

            DataTable table = ClsDbExplorer.TablesInsideTheSelectedDB(DbName);

            foreach(DataRow row in table.Rows)
            {
                cbTableChose.Items.Add(row["name"]);

            }

        }

        private void LoadServerInformation()
        {

            lblDate.Text = DateTime.Now.ToString();

            if (LoginInfo.SelectedLoginInfo.ServerName == ".")
            {
                lblServerName.Text = "Local Host";
            }
            else
            {
                lblServerName.Text = LoginInfo.SelectedLoginInfo.ServerName;
            }

            lblUserName.Text = LoginInfo.SelectedLoginInfo.UserName;

            if(cbDataChose.SelectedIndex != 0)
            {
               
                lbldbName.Text = cbDataChose.SelectedItem.ToString();


            }
            else
            {
                lbldbName.Text = "Not Chosen Yet";
            }

        }

        private void LoadTableInformation()
        {
            

            _dtTableInformation = ClsDbExplorer.TableInformation(cbDataChose.SelectedItem.ToString(), cbTableChose.SelectedItem.ToString());

           
            dgv.DataSource = _dtTableInformation;

        }

        private void CodeGenerator_Load(object sender, EventArgs e)
        {
           

            FillDatabases();

            LoadServerInformation();

            _StyleGrid();
          
        }

        private void _StyleGrid()
        {
            
            dgv.BorderStyle = BorderStyle.FixedSingle;

            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.MultiSelect = false;
            dgv.ReadOnly = true; 
            dgv.AllowUserToAddRows = false;
            dgv.RowHeadersVisible = false;

           
            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(0, 122, 204);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgv.ColumnHeadersHeight = 36;

            
            dgv.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(229, 241, 251);
            dgv.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245);

          
            typeof(DataGridView).GetProperty("DoubleBuffered",
                System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)
                ?.SetValue(dgv, true, null);
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
                string message = @"Choose a table to display columns here";
                using (Font font = new Font("Segoe UI", 11, FontStyle.Bold))
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

        private void cbDataChose_SelectedValueChanged(object sender, EventArgs e)
        {

            if (cbDataChose.SelectedIndex != 0)
            {
                
                lbldbName.Text = cbDataChose.SelectedItem.ToString();

                
                cbTableChose.Items.Clear();
                FillTables(cbDataChose.SelectedItem.ToString());
                cbTableChose.SelectedIndex = 0;

               

            }
            else
            {
                lbldbName.Text = "Not Chosen Yet";
            }

        }

        private void cbTableChose_SelectedValueChanged(object sender, EventArgs e)
        {
            LoadTableInformation();
        }
    }
}
