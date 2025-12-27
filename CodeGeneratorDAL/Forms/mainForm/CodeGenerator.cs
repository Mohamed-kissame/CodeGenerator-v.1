using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using BussinessLogic;
using BussinessLogic.CodeGeneration;
using BussinessLogic.Models;
using Guna.UI2.WinForms;

namespace CodeGeneratorDAL
{
    public partial class CodeGenerator : Form
    {

        private Login _Login;

        private TableListModel _dtTableInformation;
     

        public CodeGenerator(Login login)
        {
            InitializeComponent();
            _Login = login;
        }

        private void FillDatabases()
        {

            var dbs = ClsDbExplorer.GetDatabses();
        
            foreach (DataRow row in dbs.Databases.Rows)
            {

                cbDataChose.Items.Add(row["name"]);

            }

        }

        private void FillTables()
        {

            var table = ClsDbExplorer.GetTablesInsideTheSelectedDB();

            foreach (DataRow row in table.Tables.Rows)
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
           
            dgv.Rows.Clear();

          
            DataTable columnsInfo = ClsDbExplorer.GetTableInformation().ColumnsInfo;

            if (columnsInfo != null && columnsInfo.Rows.Count > 0)
            {
                foreach (DataRow row in columnsInfo.Rows)
                {
                    
                    string columnName = row["ColumnName"].ToString();
                    string csharpType = row["CSharpType"].ToString();

                   
                    bool isPrimaryKey = false;
                    if (row["IsPrimaryKey"] != DBNull.Value)
                    {
                        isPrimaryKey = Convert.ToBoolean(row["IsPrimaryKey"]);
                    }

                   
                    int rowIndex = dgv.Rows.Add(
                        columnName,    
                        csharpType,   
                        isPrimaryKey,  
                        true          
                    );

                   
                    dgv.Rows[rowIndex].Cells["ColumnName"].ToolTipText = columnName;

                    
                    dgv.Rows[rowIndex].Tag = row;
                }

               
                dgv.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders);
            }
        }

        private void CodeGenerator_Load(object sender, EventArgs e)
        {
           

            FillDatabases();

            LoadServerInformation();

            _StyleGrid();

            checkAddNew.CheckedChanged += CheckBox_CheckedChanged;
            checkUpdate.CheckedChanged += CheckBox_CheckedChanged;
            CheckDelete.CheckedChanged += CheckBox_CheckedChanged;
            checkGetAll.CheckedChanged += CheckBox_CheckedChanged;
            checkGetByID.CheckedChanged += CheckBox_CheckedChanged;

           
            UpdateTextBoxStates();

        }

        private void _StyleGrid()
        {
           
            dgv.Columns.Clear();

           
            DataGridViewTextBoxColumn colName = new DataGridViewTextBoxColumn();
            colName.Name = "ColumnName";
            colName.HeaderText = "Column Name";
            colName.ReadOnly = true;
            colName.Width = 180; 
            colName.AutoSizeMode = DataGridViewAutoSizeColumnMode.None; 
            colName.DefaultCellStyle.WrapMode = DataGridViewTriState.True; 
            dgv.Columns.Add(colName);

           
            DataGridViewTextBoxColumn colType = new DataGridViewTextBoxColumn();
            colType.Name = "CSharpType";
            colType.HeaderText = "C# Type";
            colType.ReadOnly = true;
            colType.Width = 100;
            colType.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dgv.Columns.Add(colType);

          
            DataGridViewCheckBoxColumn colPK = new DataGridViewCheckBoxColumn();
            colPK.Name = "IsPrimaryKey";
            colPK.HeaderText = "PK";
            colPK.ReadOnly = true;
            colPK.Width = 50;
            colPK.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dgv.Columns.Add(colPK);

            
            DataGridViewCheckBoxColumn colInclude = new DataGridViewCheckBoxColumn();
            colInclude.Name = "Include";
            colInclude.HeaderText = "Include";
            colInclude.TrueValue = true;
            colInclude.FalseValue = false;
            colInclude.ReadOnly = false;
            colInclude.Width = 70;
            colInclude.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dgv.Columns.Add(colInclude);

            
            dgv.BorderStyle = BorderStyle.FixedSingle;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; 
            dgv.Columns["ColumnName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None; 
            dgv.Columns["CSharpType"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dgv.Columns["IsPrimaryKey"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dgv.Columns["Include"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;

            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.MultiSelect = false;
            dgv.ReadOnly = false;
            dgv.AllowUserToAddRows = false;
            dgv.RowHeadersVisible = false;

           
            dgv.RowTemplate.Height = 30;

           
            dgv.DefaultCellStyle.WrapMode = DataGridViewTriState.True;

           
            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(0, 122, 204);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgv.ColumnHeadersHeight = 40;

           
            dgv.DefaultCellStyle.Font = new Font("Segoe UI", 9);
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

                ClsDbExplorer.DbName = cbDataChose.SelectedItem.ToString();
                cbTableChose.Items.Clear();
                //FillTables(cbDataChose.SelectedItem.ToString());
                FillTables();
                cbTableChose.SelectedIndex = 0;

               

            }
            else
            {
                lbldbName.Text = "Not Chosen Yet";
            }

        }

        private void cbTableChose_SelectedValueChanged(object sender, EventArgs e)
        {
            ClsDbExplorer.TableName = cbTableChose.SelectedItem.ToString();
            LoadTableInformation();
        }

        private void checkAddNew_CheckedChanged(object sender, EventArgs e)
        {

            checkAddNew.Enabled = true ? txtMethodAdd.Enabled = true : txtMethodAdd.Enabled = false;

        }

        private DataTable GetSelectedColumns()
        {
           
            DataTable selectedColumns = new DataTable();
            selectedColumns.Columns.Add("ColumnName", typeof(string));
            selectedColumns.Columns.Add("CSharpType", typeof(string));
            selectedColumns.Columns.Add("IsPrimaryKey", typeof(bool));

          
            foreach (DataGridViewRow row in dgv.Rows)
            {
              
                DataGridViewCheckBoxCell includeCell = row.Cells["Include"] as DataGridViewCheckBoxCell;

                if (includeCell != null && includeCell.Value != null)
                {
                    bool isIncluded = Convert.ToBoolean(includeCell.Value);

                    if (isIncluded)
                    {
                       
                        string columnName = row.Cells["ColumnName"].Value.ToString();
                        string csharpType = row.Cells["CSharpType"].Value.ToString();

                       
                        bool isPrimaryKey = false;
                        DataGridViewCheckBoxCell pkCell = row.Cells["IsPrimaryKey"] as DataGridViewCheckBoxCell;
                        if (pkCell != null && pkCell.Value != null)
                        {
                            isPrimaryKey = Convert.ToBoolean(pkCell.Value);
                        }

                       
                        DataRow newRow = selectedColumns.NewRow();
                        newRow["ColumnName"] = columnName;
                        newRow["CSharpType"] = csharpType;
                        newRow["IsPrimaryKey"] = isPrimaryKey;
                        selectedColumns.Rows.Add(newRow);
                    }
                }
            }

            return selectedColumns;
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {

            try
            {
              
                if (string.IsNullOrWhiteSpace(txtNameSpace.Text))
                {
                    MessageBox.Show("Please enter a namespace name", "Error",
                                   MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtClassName.Text))
                {
                    MessageBox.Show("Please enter a class name", "Error",
                                   MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

             
                DataTable selectedColumns = GetSelectedColumns();

                if (selectedColumns.Rows.Count == 0)
                {
                    MessageBox.Show("Please select at least one column", "Error",
                                   MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

               
                if (!checkAddNew.Checked && !checkUpdate.Checked &&
                    !CheckDelete.Checked && !checkGetAll.Checked && !checkGetByID.Checked)
                {
                    MessageBox.Show("Please select at least one CRUD operation", "Error",
                                   MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

             
                GenerationSettings settings = new GenerationSettings();

                settings.ServerName = LoginInfo.SelectedLoginInfo.ServerName;
                settings.DatabaseName = cbDataChose.SelectedItem.ToString();
                settings.UserName = LoginInfo.SelectedLoginInfo.UserName;
                settings.Password = LoginInfo.SelectedLoginInfo.Password;

               
                settings.UseWindowsAuthentication =
                    string.IsNullOrWhiteSpace(LoginInfo.SelectedLoginInfo.UserName) ||
                    LoginInfo.SelectedLoginInfo.UserName == "WindowsAuth";


                settings.SelectedColumns = selectedColumns;

              
                settings.TableName = cbTableChose.SelectedItem.ToString();

                
                settings.UserNamespace = txtNameSpace.Text.Trim();
                settings.UserClassName = txtClassName.Text.Trim();

               
                settings.GenerateInsert = checkAddNew.Checked;
                if (checkAddNew.Checked && !string.IsNullOrWhiteSpace(txtMethodAdd.Text))
                {
                    settings.InsertMethodName = txtMethodAdd.Text.Trim();
                }
                else
                {
                    settings.InsertMethodName = "Insert"; 
                }

              
                settings.GenerateUpdate = checkUpdate.Checked;
                if (checkUpdate.Checked && !string.IsNullOrWhiteSpace(txtMethodUpdate.Text))
                {
                    settings.UpdateMethodName = txtMethodUpdate.Text.Trim();
                }
                else
                {
                    settings.UpdateMethodName = "Update"; 
                }

               
                settings.GenerateDelete = CheckDelete.Checked;
                if (CheckDelete.Checked && !string.IsNullOrWhiteSpace(txtMethodDelete.Text))
                {
                    settings.DeleteMethodName = txtMethodDelete.Text.Trim();
                }
                else
                {
                    settings.DeleteMethodName = "Delete"; 
                }

               
                settings.GenerateGetAll = checkGetAll.Checked;
                if (checkGetAll.Checked && !string.IsNullOrWhiteSpace(txtMethodGetAll.Text))
                {
                    settings.GetAllMethodName = txtMethodGetAll.Text.Trim();
                }
                else
                {
                    settings.GetAllMethodName = "GetAll"; 
                }

              
                settings.GenerateGetById = checkGetByID.Checked;
                if (checkGetByID.Checked && !string.IsNullOrWhiteSpace(txtMethodGetById.Text))
                {
                    settings.GetByIdMethodName = txtMethodGetById.Text.Trim();
                }
                else
                {
                    settings.GetByIdMethodName = "GetById"; 
                }

                
                string generatedCode = clsCodeGeneration.GenerateDAL(settings);

               
                DisplayGeneratedCode(generatedCode);

                MessageBox.Show("Code generated successfully!", "Success",
                               MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error generating code: " + ex.Message, "Error",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void DisplayGeneratedCode(string code)
        {
            
            rtbGeneratedCode.Clear();

           
            rtbGeneratedCode.Font = new Font("Consolas", 10);

           
            rtbGeneratedCode.Text = code;

            
            ApplySyntaxHighlighting();

          
            rtbGeneratedCode.SelectionStart = 0;
            rtbGeneratedCode.ScrollToCaret();
        }

        private void ApplySyntaxHighlighting()
        {
           
            string[] keywords = { "using", "namespace", "class", "public", "private",
                         "protected", "static", "void", "int", "string", "bool",
                         "DateTime", "decimal", "double", "float", "return",
                         "new", "if", "else", "try", "catch", "finally", "using" };

            foreach (string keyword in keywords)
            {
                HighlightWord(keyword, Color.Blue, FontStyle.Bold);
            }

           
            HighlightWord("//", Color.Green, FontStyle.Regular);
        }

        private void HighlightWord(string word, Color color, FontStyle style)
        {
            int startIndex = 0;

            while (startIndex < rtbGeneratedCode.TextLength)
            {
                int wordIndex = rtbGeneratedCode.Find(word, startIndex,
                                                      RichTextBoxFinds.WholeWord);

                if (wordIndex == -1) break;

                rtbGeneratedCode.SelectionStart = wordIndex;
                rtbGeneratedCode.SelectionLength = word.Length;
                rtbGeneratedCode.SelectionColor = color;
                rtbGeneratedCode.SelectionFont = new Font(rtbGeneratedCode.Font, style);

                startIndex = wordIndex + word.Length;
            }
        }

        private void UpdateTextBoxStates()
        {
            
            txtMethodAdd.Enabled = checkAddNew.Checked;
            txtMethodUpdate.Enabled = checkUpdate.Checked;
            txtMethodDelete.Enabled = CheckDelete.Checked;
            txtMethodGetAll.Enabled = checkGetAll.Checked;
            txtMethodGetById.Enabled = checkGetByID.Checked;

           
            if (!checkAddNew.Checked) txtMethodAdd.Text = "Insert";
            if (!checkUpdate.Checked) txtMethodUpdate.Text = "Update";
            if (!CheckDelete.Checked) txtMethodDelete.Text = "Delete";
            if (!checkGetAll.Checked) txtMethodGetAll.Text = "GetAll";
            if (!checkGetByID.Checked) txtMethodGetById.Text = "GetById";
        }

        private void CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            UpdateTextBoxStates();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
         
            
            checkAddNew.Checked = false;
            checkUpdate.Checked = false;
            CheckDelete.Checked = false;
            checkGetAll.Checked = false;
            checkGetByID.Checked = false;

            rtbGeneratedCode.Clear();


            UpdateTextBoxStates();


            txtNameSpace.Text = "";
            txtClassName.Text = "";

           
            foreach (DataGridViewRow row in dgv.Rows)
            {
                DataGridViewCheckBoxCell includeCell = row.Cells["Include"] as DataGridViewCheckBoxCell;
                if (includeCell != null)
                {
                    includeCell.Value = false;
                }
            }

            MessageBox.Show("All selections cleared", "Cleared",
                           MessageBoxButtons.OK, MessageBoxIcon.Information);
        
        }

        private void checkGetAll_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            try
            {
                
                if (string.IsNullOrWhiteSpace(rtbGeneratedCode.Text))
                {
                    MessageBox.Show("No code to copy. Please generate code first.",
                                   "No Code", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

               
                Clipboard.SetText(rtbGeneratedCode.Text);

                
                MessageBox.Show("Code copied to clipboard!", "Copied",
                               MessageBoxButtons.OK, MessageBoxIcon.Information);

               
                btnCopy.Text = "Copied!";

              
                Timer timer = new Timer();
                timer.Interval = 2000; 
                timer.Tick += (s, args) =>
                {
                    btnCopy.Text = "Copy Code";
                    timer.Stop();
                    timer.Dispose();
                };
                timer.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error copying to clipboard: " + ex.Message,
                               "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
