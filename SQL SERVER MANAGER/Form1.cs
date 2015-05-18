using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace SQL_SERVER_MANAGER
{
    public partial class Form1 : Form
    {
        private TabPage tabPage1;
        private DataGridView dataGridView1;
        private TabControl tabControl1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem newSQLEditorToolStripMenuItem;
        private ToolStripMenuItem closeToolStripMenuItem;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem closeToolStripMenuItem1;
        private ToolStripMenuItem runSQLToolStripMenuItem;
        private ContextMenuStrip contextMenuStrip1;
        private IContainer components;
        private ToolStripMenuItem addNewToolStripMenuItem;
        private TreeView treeView1;

        public Form1()
        {
            InitializeComponent();
            AddDBNames();
        }



        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (treeView1.SelectedNode.Parent == null) return;
            var str = treeView1.SelectedNode.Parent.ToString().Split(' ')[1];
            if (str.Equals("Tables") || str.Equals("Views"))
            {
                dataGridView1.Refresh();
                ShowSelectedTable(treeView1.SelectedNode.ToString().Split(' ')[1],
                    treeView1.SelectedNode.Parent.Parent.ToString().Split(' ')[1]);
            }
            else
            {
                switch (str)
                {
                    case "Triggers":
                        ShowSelectedTrigger(treeView1.SelectedNode.Parent.Parent.Parent.Parent.ToString().Split(' ')[1], treeView1.SelectedNode.ToString().Split(' ')[1]);
                        break;
                    case "Procedures":
                        ShowSelectedProcedure(treeView1.SelectedNode.Parent.Parent.ToString().Split(' ')[1], treeView1.SelectedNode.ToString().Split(' ')[1]);
                        break;
                    case "Functions":
                        ShowSelectedFunction(treeView1.SelectedNode.Parent.Parent.ToString().Split(' ')[1], treeView1.SelectedNode.ToString().Split(' ')[1]);
                        break;
                }
            }
        }

        private void treeView1_RightClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right) return;
            treeView1.SelectedNode = treeView1.GetNodeAt(e.X, e.Y);
            if (treeView1.SelectedNode != null)
            {
                contextMenuStrip1.Show(treeView1, e.Location);
            }
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newSQLEditorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.runSQLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addNewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // treeView1
            // 
            this.treeView1.Location = new System.Drawing.Point(12, 49);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(198, 553);
            this.treeView1.TabIndex = 1;
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            this.treeView1.NodeMouseClick += treeView1_RightClick;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dataGridView1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1024, 553);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Data Grid View";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(3, 3);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(1018, 547);
            this.dataGridView1.TabIndex = 0;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Location = new System.Drawing.Point(218, 27);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1032, 579);
            this.tabControl1.TabIndex = 2;
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newSQLEditorToolStripMenuItem,
            this.closeToolStripMenuItem,
            this.runSQLToolStripMenuItem,
            this.closeToolStripMenuItem1});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newSQLEditorToolStripMenuItem
            // 
            this.newSQLEditorToolStripMenuItem.Name = "newSQLEditorToolStripMenuItem";
            this.newSQLEditorToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.newSQLEditorToolStripMenuItem.Text = "New SQL Editor";
            this.newSQLEditorToolStripMenuItem.Click += new System.EventHandler(this.newSQLEditorToolStripMenuItem_Click);
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.closeToolStripMenuItem.Text = "Close SQL Editor";
            this.closeToolStripMenuItem.Click += new System.EventHandler(this.closeToolStripMenuItem_Click);
            // 
            // runSQLToolStripMenuItem
            // 
            this.runSQLToolStripMenuItem.Name = "runSQLToolStripMenuItem";
            this.runSQLToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.runSQLToolStripMenuItem.Text = "Run SQL Query";
            this.runSQLToolStripMenuItem.Click += new System.EventHandler(this.runSQLToolStripMenuItem_Click);
            // 
            // closeToolStripMenuItem1
            // 
            this.closeToolStripMenuItem1.Name = "closeToolStripMenuItem1";
            this.closeToolStripMenuItem1.Size = new System.Drawing.Size(161, 22);
            this.closeToolStripMenuItem1.Text = "Close";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1262, 24);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addNewToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(153, 48);
            // 
            // addNewToolStripMenuItem
            // 
            this.addNewToolStripMenuItem.Name = "addNewToolStripMenuItem";
            this.addNewToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.addNewToolStripMenuItem.Text = "Add New...";
            this.addNewToolStripMenuItem.Click += new System.EventHandler(this.addNewToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(1262, 626);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.treeView1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        //Void to Add DB Names to TreeNode
        public void AddDBNames()
        {
            const string connectionString = "Server=localhost;Integrated security=SSPI;database=master";
            const string query = "SELECT name FROM Sys.Databases";
            var cnn = new SqlConnection(connectionString);
            var myCommand = new SqlCommand(query, cnn);
            cnn.Open();
            var reader = myCommand.ExecuteReader();
            while (reader.Read())
            {
                this.treeView1.Nodes.Add(reader[0].ToString());

            }
            reader.Close();
            cnn.Close();

            AddTables();
            AddViews();
            AddStoredProcedures();
            AddFunctions();
            AddSequences();
        }

        //Void to Add tables to the databases
        public void AddTables()
        {
            const string connectionString = "Server=localhost;Integrated security=SSPI;database=master";
            var cnn = new SqlConnection(connectionString);
            cnn.Open();
            var cont = 0;
            for (int i = 0; i < treeView1.Nodes.Count; i++)
            {
                treeView1.Nodes[i].Nodes.Add("Tables");
                var str = treeView1.Nodes[i].ToString().Split(':')[1];
                var query = "USE" + str +
                            " SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE'";
                var myCommand = new SqlCommand(query, cnn);
                var reader = myCommand.ExecuteReader();
                while (reader.Read())
                {
                    treeView1.Nodes[i].Nodes[0].Nodes.Add(reader[0].ToString());
                    var triggers = GetTableTriggers(treeView1.Nodes[i].ToString(), reader[0].ToString());
                    treeView1.Nodes[i].Nodes[0].Nodes[cont].Nodes.Add("Triggers");
                    foreach (var s in triggers)
                    {
                        treeView1.Nodes[i].Nodes[0].Nodes[cont].Nodes[0].Nodes.Add(s);
                    }
                    cont = cont + 1;
                }
                cont = 0;
                reader.Close();
             

            }
            cnn.Close();


        }

        //Void To Add Data to the selected Table

        public void AddViews()
        {
            const string connectionString = "Server=localhost;Integrated security=SSPI;database=master";
            var cnn = new SqlConnection(connectionString);
            cnn.Open();

            for (int i = 0; i < treeView1.Nodes.Count; i++)
            {
                treeView1.Nodes[i].Nodes.Add("Views");
                var str = treeView1.Nodes[i].ToString().Split(':')[1];
                var query = "USE" + str + " SELECT TABLE_NAME FROM INFORMATION_SCHEMA.Views";
                var myCommand = new SqlCommand(query, cnn);
                var reader = myCommand.ExecuteReader();
                while (reader.Read())
                {
                    treeView1.Nodes[i].Nodes[1].Nodes.Add(reader[0].ToString());
                }
                reader.Close();
                Console.WriteLine(reader.IsClosed);

            }
            cnn.Close();

        }

        public void ShowSelectedTable(string tableName, string dbName)
        {
            const string connectionString = "Server=localhost;Integrated security=SSPI;database=master";
            var cnn = new SqlConnection(connectionString);
            var buildQuery = "USE " + dbName + " SELECT * FROM " + tableName;
            cnn.Open();
            var command = new SqlCommand(buildQuery, cnn);
            var dataAdapter = new SqlDataAdapter {SelectCommand = command};
            var dataset = new DataTable();
            dataAdapter.Fill(dataset);
            var bSource = new BindingSource {DataSource = dataset};
            dataGridView1.DataSource = bSource;
            dataAdapter.Update(dataset);
            tabControl1.SelectTab(tabPage1);
            cnn.Close();
        }

        private void newSQLEditorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var tp = new TabPage("SQL Editor");
            var rtb = new RichTextBox {Dock = DockStyle.Fill};
            tp.Controls.Add(rtb);
            tabControl1.TabPages.Add(tp);
            tabControl1.SelectTab(tp);
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab != tabPage1)
            {
                tabControl1.TabPages.Remove(tabControl1.SelectedTab);
            }
        }

        public IEnumerable<string> GetTableTriggers(string dbName, string tableName)
        {
            var stringToReturn = new List<string>();
            const string connectionString = "Server=localhost;Integrated security=SSPI;database=master";
            var cnn = new SqlConnection(connectionString);
            var buildQuery = "USE " + dbName.Split(' ')[1] + " SELECT s.name FROM sys.triggers s INNER JOIN " +
                             "(SELECT OBJECT_ID FROM sys.tables WHERE name = '" + tableName + "') i ON s.parent_id = i.object_id";
            var myCommand = new SqlCommand(buildQuery, cnn);
            cnn.Open();
            var reader = myCommand.ExecuteReader();
            while (reader.Read())
            {
                stringToReturn.Add(reader[0].ToString());

            }
            reader.Close();
            cnn.Close();

            return stringToReturn;
        }

        public void ShowSelectedTrigger(string dbName, string triggerName)
        {
            for (var i = 0; i < tabControl1.TabCount; i++)
            {
                if (tabControl1.TabPages[i].Text != (triggerName + " SQL")) continue;
                tabControl1.SelectTab(tabControl1.TabPages[i]);
                return;
            }
            const string connectionString = "Server=localhost;Integrated security=SSPI;database=master";
            var cnn = new SqlConnection(connectionString);
            var buildQuery = "USE " + dbName +" SELECT c.text FROM sys.syscomments c INNER JOIN " +
                             "(SELECT object_id FROM SYS.triggers WHERE name = '" + triggerName + "') t ON c.id = t.object_id";
            var myCommand = new SqlCommand(buildQuery, cnn);
            cnn.Open();
            var reader = myCommand.ExecuteReader();
            reader.Read();
            var tp = new TabPage(triggerName + " SQL");
            var rtb = new RichTextBox {Dock = DockStyle.Fill, Text = reader[0].ToString()};
            tp.Controls.Add(rtb);
            tabControl1.TabPages.Add(tp);
            tabControl1.SelectTab(tp);
            reader.Close();
            cnn.Close();
        }

        public void AddStoredProcedures()
        {
            const string connectionString = "Server=localhost;Integrated security=SSPI;database=master";
            var cnn = new SqlConnection(connectionString);
            cnn.Open();

            for (int i = 0; i < treeView1.Nodes.Count; i++)
            {
                treeView1.Nodes[i].Nodes.Add("Procedures");
                var str = treeView1.Nodes[i].ToString().Split(' ')[1];
                var query = "SELECT SPECIFIC_NAME FROM " + str + ".INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_TYPE = 'PROCEDURE'";
                var myCommand = new SqlCommand(query, cnn);
                var reader = myCommand.ExecuteReader();
                while (reader.Read())
                {
                    treeView1.Nodes[i].Nodes[2].Nodes.Add(reader[0].ToString());
                }
                reader.Close();
                Console.WriteLine(reader.IsClosed);

            }
            cnn.Close();
        }

        public void ShowSelectedProcedure(string dbName, string procedureName)
        {
            for (var i = 0; i < tabControl1.TabCount; i++)
            {
                if (tabControl1.TabPages[i].Text != (procedureName + " SQL")) continue;
                tabControl1.SelectTab(tabControl1.TabPages[i]);
                return;
            }
            const string connectionString = "Server=localhost;Integrated security=SSPI;database=master";
            var cnn = new SqlConnection(connectionString);
            var buildQuery = "USE " + dbName +" SELECT c.text FROM sys.syscomments c INNER JOIN " +
                             "(SELECT object_id FROM SYS.procedures WHERE name = '" + procedureName + "') t ON c.id = t.object_id";
            var myCommand = new SqlCommand(buildQuery, cnn);
            cnn.Open();
            var reader = myCommand.ExecuteReader();
            reader.Read();
            var tp = new TabPage(procedureName + " SQL");
            var rtb = new RichTextBox {Dock = DockStyle.Fill, Text = reader[0].ToString()};
            tp.Controls.Add(rtb);
            tabControl1.TabPages.Add(tp);
            tabControl1.SelectTab(tp);
            reader.Close();
            cnn.Close();
        }

        public void AddFunctions()
        {
            const string connectionString = "Server=localhost;Integrated security=SSPI;database=master";
            var cnn = new SqlConnection(connectionString);
            cnn.Open();

            for (int i = 0; i < treeView1.Nodes.Count; i++)
            {
                treeView1.Nodes[i].Nodes.Add("Functions");
                var str = treeView1.Nodes[i].ToString().Split(' ')[1];
                var query = "SELECT SPECIFIC_NAME FROM " + str + ".INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_TYPE = 'FUNCTION'";
                var myCommand = new SqlCommand(query, cnn);
                var reader = myCommand.ExecuteReader();
                while (reader.Read())
                {
                    treeView1.Nodes[i].Nodes[3].Nodes.Add(reader[0].ToString());
                }
                reader.Close();
                Console.WriteLine(reader.IsClosed);

            }
            cnn.Close();
        }

        public void ShowSelectedFunction(string dbName, string functionName)
        {
            for (var i = 0; i < tabControl1.TabCount; i++)
            {
                if (tabControl1.TabPages[i].Text != (functionName + " SQL")) continue;
                tabControl1.SelectTab(tabControl1.TabPages[i]);
                return;
            }
            const string connectionString = "Server=localhost;Integrated security=SSPI;database=master";
            var cnn = new SqlConnection(connectionString);
            var buildQuery = "USE " + dbName + " SELECT c.text FROM sys.syscomments c INNER JOIN " +
                             "(SELECT object_id FROM SYS.objects WHERE type = 'FN' AND name = '" + functionName + "') t ON c.id = t.object_id";
            var myCommand = new SqlCommand(buildQuery, cnn);
            cnn.Open();
            var reader = myCommand.ExecuteReader();
            reader.Read();
            var tp = new TabPage(functionName + " SQL");
            var rtb = new RichTextBox { Dock = DockStyle.Fill, Text = reader[0].ToString() };
            tp.Controls.Add(rtb);
            tabControl1.TabPages.Add(tp);
            tabControl1.SelectTab(tp);
            reader.Close();
            cnn.Close();
        }

        public void AddSequences()
        {
            const string connectionString = "Server=localhost;Integrated security=SSPI;database=master";
            var cnn = new SqlConnection(connectionString);
            cnn.Open();

            for (int i = 0; i < treeView1.Nodes.Count; i++)
            {
                treeView1.Nodes[i].Nodes.Add("Sequences");
                var str = treeView1.Nodes[i].ToString().Split(' ')[1];
                var query = "SELECT SEQUENCE_NAME FROM " + str + ".INFORMATION_SCHEMA.SEQUENCES";
                var myCommand = new SqlCommand(query, cnn);
                var reader = myCommand.ExecuteReader();
                while (reader.Read())
                {
                    treeView1.Nodes[i].Nodes[4].Nodes.Add(reader[0].ToString());
                }
                reader.Close();
                Console.WriteLine(reader.IsClosed);

            }
            cnn.Close();
        }

        public void runSQLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab.Text.Equals("Data Grid View")) return;

            var query = tabControl1.SelectedTab.Controls[0].Text;
            const string connectionString = "Server=localhost;Integrated security=SSPI;database=master";
            var cnn = new SqlConnection(connectionString);
            var myCommand = new SqlCommand(query, cnn);
            cnn.Open();
            if (!query.ToUpper().Contains("CREATE") && !query.ToUpper().Contains("REPLACE") && !query.ToUpper().Contains("ALTER") &&
                !query.ToUpper().Contains("INSERT") && !query.ToUpper().Contains("UPDATE")
                && !query.ToUpper().Contains("DELETE") && !query.ToUpper().Contains("EXECUTE"))
            {
                try
                {
                    var dataAdapter = new SqlDataAdapter {SelectCommand = myCommand};
                    var dataset = new DataTable();
                    dataAdapter.Fill(dataset);
                    var bSource = new BindingSource {DataSource = dataset};
                    var f2 = new Form2 {dataGridView1 = {DataSource = bSource}};
                    dataAdapter.Update(dataset);
                    f2.ShowDialog();
                    cnn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                try
                {
                    myCommand.ExecuteNonQuery();
                    MessageBox.Show("Success Executing Query");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Right) treeView1.SelectedNode = e.Node;
        }

        private void addNewToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
 