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
    public partial class Access2003ConnectionEditForm : ConnectionEditForm
    {
        private AccessConnection connection;

        public Access2003ConnectionEditForm()
        {
            InitializeComponent();
        }

        public Access2003ConnectionEditForm(AccessConnection connection)
            : this()
        {
            this.connection = connection;
        }

        private void simpleButtonOK_Click(object sender, EventArgs e)
        {
            if (this.textEditName.Text == "")
            {
                MessageBox.Show(Properties.Resources.KeyConnectionName, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (this.buttonEditDataFile.Text == "")
            {
                MessageBox.Show(Properties.Resources.SpecialAccessFile, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (System.IO.File.Exists(this.buttonEditDataFile.Text) == false)
            {
                MessageBox.Show(Properties.Resources.FileNotFount, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            if (this.connection == null)
                this.connection = new AccessConnection();

            this.connection.Name = this.textEditName.Text;
            this.connection.DataFile = this.buttonEditDataFile.Text;

            this.DialogResult = DialogResult.OK;
        }

        private void EditForm_Load(object sender, EventArgs e)
        {
            if (this.connection != null)
            {
                this.buttonEditDataFile.Text = this.connection.DataFile;
                this.textEditName.Text = this.connection.Name;
            }
            
        }

        public override Connection Connection
        {
            get 
            {
                return this.connection;
            }
        }

        private void buttonEditDataFile_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (this.openFileDialog1.ShowDialog(this) == DialogResult.OK)
            {
                (sender as DevExpress.XtraEditors.ButtonEdit).Text = this.openFileDialog1.FileName;
            }
        }

    }
}