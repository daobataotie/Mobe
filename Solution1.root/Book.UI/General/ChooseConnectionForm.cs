using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Book.UI.General
{
    public partial class ChooseConnectionForm : DevExpress.XtraEditors.XtraForm
    {
        private IList<Common.Connection> connections;

        public ChooseConnectionForm(IList<Common.Connection> connections)
        {
            InitializeComponent();
            this.connections = connections;
        }

        private void PopulateConnections()
        {
            this.comboBoxEditConnections.Properties.Items.Clear();
            foreach (Common.Connection connection in this.connections)
            {              
                this.comboBoxEditConnections.Properties.Items.Add(connection);
            }
        }
        private void ChooseConnectionForm_Load(object sender, EventArgs e)
        {
            this.PopulateConnections();
        }

        public Common.Connection SelectedConnection
        {
            get
            {
                return this.comboBoxEditConnections.SelectedItem as Common.Connection;
            }
        }

        private void simpleButtonLogin_Click(object sender, EventArgs e)
        {
            if (this.comboBoxEditConnections.SelectedItem == null)
            {
                MessageBox.Show(Properties.Resources.RequireChooseConnection);
                return;
            }
            //正在检测连接...
            this.labelControl1.Text = Properties.Resources.CheckIngConnection;
            Cursor.Current = Cursors.WaitCursor;
            Application.DoEvents();
            Common.Connection connection = this.comboBoxEditConnections.SelectedItem as Common.Connection;
            if (!connection.Awailable)
            {
                this.labelControl1.Text = Properties.Resources.ConnectionCannotUse;
                Cursor.Current = Cursors.Default;
                return;
            }
            Cursor.Current = Cursors.Default;
            this.DialogResult = DialogResult.OK;
        }

        private void comboBoxEditConnections_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.labelControl1.Text = "";
        }
    }
}