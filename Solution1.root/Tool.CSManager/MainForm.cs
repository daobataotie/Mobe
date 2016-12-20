using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Common;

namespace Tool.CSManager
{
    public partial class MainForm : DevExpress.XtraEditors.XtraForm
    {
        #region Data

        private IList<Connection> connections;

        #endregion

        public MainForm()
        {
            InitializeComponent();
        }
        private void PopulateConnections()
        {
            this.listView1.BeginUpdate();
            this.listView1.Items.Clear();
            foreach (Connection connection in connections)
            {
                ListViewItem lvi = new ListViewItem();
                lvi.Text = connection.Name;
                lvi.SubItems.Add(connection.Type);
                lvi.SubItems.Add(connection.ToString("d"));
                lvi.Tag = connection;

                this.listView1.Items.Add(lvi);
            }
            this.listView1.EndUpdate();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            Program.WriteLog("正在取得数据库连接配置信息文件名信息");
            string file = System.Configuration.ConfigurationManager.AppSettings["connectioninfofile"];
            if (System.IO.File.Exists(file))
            {
                Program.WriteLog("已得到数据库连接配置信息文件名");
                try
                {
                    Program.WriteLog("准备加载数据库连接配置信息...");
                    this.connections = ConnectionInfoAccessor.Load(file);
                    Program.WriteLog("已成功加载数据库连接配置信息");
                }
                catch
                {
                    MessageBox.Show(Properties.Resources.ConfigError, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                Program.WriteLog("数据库连接信息配置文件不存在！");
            }

            if (this.connections == null)
            {
                this.connections = new List<Connection>();
            }

            this.PopulateConnections();
            this.SetModified(false);
        }

        private Common.Connection SelectedConnection
        {
            get
            {
                if (this.listView1.SelectedItems.Count == 0)
                    return null;

                return this.listView1.SelectedItems[0].Tag as Connection;
            }
        }

        private void simpleButton_Click(object sender, EventArgs e)
        {
            DevExpress.XtraEditors.SimpleButton button = sender as DevExpress.XtraEditors.SimpleButton;
            switch ((string)button.Tag)
            {
                case "AddConnection":
                    Point p = button.PointToScreen(new Point(0, button.ClientRectangle.Bottom));
                    this.popupMenu1.ShowPopup(p);
                    break;

                case "RemoveConnection":
                    if (this.SelectedConnection == null)
                        return;

                    this.connections.Remove(this.SelectedConnection);
                    this.PopulateConnections();
                    this.SetModified(true);
                    break;

                case "ConfigureConnection":
                    if (this.SelectedConnection == null)
                        return;

                    System.Reflection.Assembly assembly = this.GetType().Assembly;
                    string typename = string.Format("{0}.{1}ConnectionEditForm", assembly.GetName().Name, this.SelectedConnection.Type);
                    ConnectionEditForm f = (ConnectionEditForm)assembly.CreateInstance(typename, false, System.Reflection.BindingFlags.CreateInstance, null, new object[] { this.SelectedConnection }, null, null);
                    if (f != null && f.ShowDialog(this) == DialogResult.OK)
                    {
                        this.PopulateConnections();
                        this.SetModified(true);
                    }
                    break;

                case "TestConnection":
                    if (this.SelectedConnection == null)
                        return;                    
                    if (this.SelectedConnection.Awailable)
                    {
                        MessageBox.Show(Properties.Resources.Success, Properties.Resources.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show(Properties.Resources.Fail, Properties.Resources.Caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    break;
                default:
                    break;
            }
        }

        private void barManager1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            System.Reflection.Assembly assembly = this.GetType().Assembly;
            string typename = string.Format("{0}.{1}", assembly.GetName().Name, (string)e.Item.Tag);
            ConnectionEditForm f = (ConnectionEditForm)assembly.CreateInstance(typename);
            if (f != null && f.ShowDialog(this) == DialogResult.OK)
            {
                this.connections.Add(f.Connection);
                this.PopulateConnections();
                this.SetModified(true);
            }

        }

        private void simpleButtonOK_Click(object sender, EventArgs e)
        {           
            string file = System.Configuration.ConfigurationManager.AppSettings["connectioninfofile"];
            ConnectionInfoAccessor.Save(Application.StartupPath + @"\" + file, this.connections);
            this.Close();
        }

        private void simpleButtonApply_Click(object sender, EventArgs e)
        {
            string file = System.Configuration.ConfigurationManager.AppSettings["connectioninfofile"];
            ConnectionInfoAccessor.Save(Application.StartupPath + @"\" + file, this.connections);
            this.SetModified(false);
        }

        private bool modified;

        private void SetModified(bool modified)
        {
            this.modified = modified;
            this.simpleButtonApply.Enabled = modified;
        }

        private void simpleButtonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}