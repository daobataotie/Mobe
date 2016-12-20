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
    public partial class SQLServerConnectionEditForm : ConnectionEditForm
    {
        #region Data
        private SQLServerConnection connection;

        #endregion

        public SQLServerConnectionEditForm()
        {
            InitializeComponent();
        }

        public SQLServerConnectionEditForm(SQLServerConnection connection)
            : this()
        {
            this.connection = connection;
        }


        private void simpleButtonOK_Click(object sender, EventArgs e)
        {
            if (this.connection == null)
                this.connection = new SQLServerConnection();
            this.connection.Name = this.textEditName.Text;
            this.connection.InitialCatalog = this.textEditDatabaseName.Text;
            this.connection.DataSource = this.textEditDataSource.Text;
            this.connection.UserName = this.textEditUserName.Text;
            this.connection.Password = this.textEditPassword.Text;
            this.connection.Authentication = (SQLServerAuthentication)System.Enum.Parse(typeof(SQLServerAuthentication), this.comboBoxEditAuthentication.SelectedItem.ToString());

            this.DialogResult = DialogResult.OK;
        }

        public override Connection Connection
        {
            get
            {
                return this.connection;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.comboBoxEditAuthentication.Properties.Items.Add(SQLServerAuthentication.SQLServer);
            this.comboBoxEditAuthentication.Properties.Items.Add(SQLServerAuthentication.Windows);

            this.comboBoxEditAuthentication.SelectedItem = SQLServerAuthentication.SQLServer;

            if (this.connection != null)
            {
                this.textEditName.Text = this.connection.Name;
                this.textEditDatabaseName.Text = this.connection.InitialCatalog;
                this.textEditDataSource.Text = this.connection.DataSource;
                this.textEditUserName.Text = this.connection.UserName;
                this.textEditPassword.Text = this.connection.Password;
                this.comboBoxEditAuthentication.SelectedItem = this.connection.Authentication;
            }
        }

        private void comboBoxEditAuthentication_SelectedIndexChanged(object sender, EventArgs e)
        {
            DevExpress.XtraEditors.ComboBoxEdit edit = sender as DevExpress.XtraEditors.ComboBoxEdit;
            SQLServerAuthentication authentication = (SQLServerAuthentication)System.Enum.Parse(typeof(SQLServerAuthentication), edit.SelectedItem.ToString());
            this.textEditPassword.Enabled = authentication == SQLServerAuthentication.SQLServer;
            this.textEditUserName.Enabled = authentication == SQLServerAuthentication.SQLServer;
        }
    }
}