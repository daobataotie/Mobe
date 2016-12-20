using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Tool.DBManager
{
    public partial class Form2 : DevExpress.XtraEditors.XtraForm
    {
        private Step prevStep;
        private Step activeStep
        {
            get
            {
                return (Step)Enum.Parse(typeof(Step), (string)this.xtraTabControl1.SelectedTabPage.Tag);
            }
            set
            {
                this.xtraTabControl1.SelectedTabPage = this.steps[value];
                switch (value)
                {
                    case Step.ChooseDBMS:
                        this.prevStep = value;
                        break;

                    case Step.AccessDatabase:
                        this.prevStep = Step.ChooseDBMS;
                        break;

                    case Step.SQLServerDatabase:
                        this.prevStep = Step.ChooseDBMS;
                        break;

                    case Step.Summary:
                        DBMS activeDBMS = (DBMS)this.listView1.SelectedItems[0].Tag;
                        if (activeDBMS == DBMS.Access)
                            this.prevStep = Step.AccessDatabase;
                        else
                            this.prevStep = Step.SQLServerDatabase;
                        break;
                }
             
                // 按钮状态
                this.simpleButtonPrev.Enabled = this.activeStep != Step.ChooseDBMS;
                this.simpleButtonNext.Enabled = this.activeStep != Step.Summary;
                this.simpleButtonFinish.Enabled = this.activeStep == Step.Summary;
            }
        }

        private IDictionary<Step, DevExpress.XtraTab.XtraTabPage> steps;

        public Form2()
        {
            InitializeComponent();

            this.steps = new Dictionary<Step, DevExpress.XtraTab.XtraTabPage>();
            this.steps.Add(Step.ChooseDBMS, this.xtraTabPage1);
            this.steps.Add(Step.AccessDatabase, this.xtraTabPage2);
            this.steps.Add(Step.SQLServerDatabase, this.xtraTabPage3);
            this.steps.Add(Step.Summary, this.xtraTabPage4);

            foreach (KeyValuePair<Step,DevExpress.XtraTab.XtraTabPage> p in this.steps)
            {
                p.Value.Tag = p.Key.ToString();
            }

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            // 数据库类型
            IList<DBMS> dbmss = new List<DBMS>();
            dbmss.Add(DBMS.SQLServer);
            dbmss.Add(DBMS.Access);

            foreach (DBMS dbms in dbmss)
            {
                ListViewItem item = new ListViewItem(dbms.Name, 0);
                item.Tag = dbms;
                this.listView1.Items.Add(item);
            }

            this.activeStep = Step.ChooseDBMS;

            this.comboBoxEditSQLServerAuthentication.Properties.Items.Clear();
            this.comboBoxEditSQLServerAuthentication.Properties.Items.Add(SQLServerAuthentication.SQLServer);
            this.comboBoxEditSQLServerAuthentication.Properties.Items.Add(SQLServerAuthentication.Windows);
            this.comboBoxEditSQLServerAuthentication.SelectedItem = SQLServerAuthentication.SQLServer;

        }

        private void simpleButtonNext_Click(object sender, EventArgs e)
        {
            Step nextStep = Step.ChooseDBMS;
            switch (this.activeStep)
            {
                case Step.ChooseDBMS:
                    if (this.listView1.SelectedItems.Count == 0)
                    {
                        MessageBox.Show(Properties.Resources.ChooseDataBaseType, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    ListViewItem item = this.listView1.SelectedItems[0];
                    DBMS dbms = item.Tag as DBMS;
                    if (dbms == DBMS.Access)
                        nextStep = Step.AccessDatabase;
                    else
                        nextStep = Step.SQLServerDatabase;

                    break;


                case Step.AccessDatabase:
                    if (this.buttonEditAccessFileName.EditValue == null)
                    {
                        MessageBox.Show(Properties.Resources.RequeirFileSavePath, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    nextStep = Step.Summary;
                    break;


                case Step.SQLServerDatabase:
                    if (this.buttonEditSQLServerDataFileLocation.Text == "")
                    {
                        MessageBox.Show(Properties.Resources.RequeirDateFileSavePath, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    if (this.buttonEditSQLServerLogFileLocation.Text == "")
                    {
                        MessageBox.Show(Properties.Resources.RequeirLogFileSavePath, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    if (this.comboBoxEditSQLServerAuthentication.EditValue == null)
                    {
                        MessageBox.Show(Properties.Resources.RequeirChooseSqlValidateType, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    if ((SQLServerAuthentication)this.comboBoxEditSQLServerAuthentication.SelectedItem == SQLServerAuthentication.SQLServer)
                    {
                        if (this.textEditSQLServerUserName.Text == "")
                        {
                            MessageBox.Show(Properties.Resources.RequeirKeyTheUserForSql, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }

                    if (this.textEditSQLServerDatabaseName.Text == "")
                    {
                        MessageBox.Show(Properties.Resources.RequeirKeyDataBaseName, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    nextStep = Step.Summary;
                    break;

                case Step.Summary:
                    break;
            }
            this.activeStep = nextStep;

        }

        enum Step
        {
            ChooseDBMS,
            AccessDatabase,
            SQLServerDatabase,
            Summary
        }

        private void simpleButtonPrev_Click(object sender, EventArgs e)
        {
            this.activeStep = this.prevStep;
        }

        private void buttonEditAccessFileName_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (this.saveFileDialog1.ShowDialog(this) != DialogResult.OK)
                return;

            this.buttonEditAccessFileName.EditValue = this.saveFileDialog1.FileName;
        }

        private void comboBoxEditSQLServerAuthentication_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.textEditSQLServerUserName.Enabled = (SQLServerAuthentication)this.comboBoxEditSQLServerAuthentication.SelectedItem == SQLServerAuthentication.SQLServer;
            this.textEditSQLServerPassword.Enabled = (SQLServerAuthentication)this.comboBoxEditSQLServerAuthentication.SelectedItem == SQLServerAuthentication.SQLServer;
        }

        private void simpleButtonFinish_Click(object sender, EventArgs e)
        {
            System.Collections.Hashtable parameters = new System.Collections.Hashtable();

            DBMS activeDBMS = (DBMS)this.listView1.SelectedItems[0].Tag;
            if (activeDBMS == DBMS.SQLServer)
            {
                string datasource = this.textEditSQLServerServerIP.Text;
                if (this.textEditSQLServerServerInstance.Text != "")
                {
                    datasource = datasource + @"\" + this.textEditSQLServerServerInstance.Text;
                }
                if (this.textEditSQLServerServerPort.Text != "")
                {
                    datasource = datasource + @"," + this.textEditSQLServerServerPort.Text;
                }

                parameters.Add("datasource", datasource);
                parameters.Add("authentication", System.Enum.Parse(typeof(SQLServerAuthentication), this.comboBoxEditSQLServerAuthentication.SelectedItem.ToString()));
                parameters.Add("username", this.textEditSQLServerUserName.Text);
                parameters.Add("password", this.textEditSQLServerPassword.Text);
                parameters.Add("databasename", this.textEditSQLServerDatabaseName.Text);
                parameters.Add("datafilelocation", this.buttonEditSQLServerDataFileLocation.Text);
                parameters.Add("logfilelocation", this.buttonEditSQLServerLogFileLocation.Text);
            }
            else
            {
                parameters.Add("databasefile", this.buttonEditAccessFileName.Text);
            }

            try
            {
                Cursor.Current = Cursors.WaitCursor;
                activeDBMS.CreateDatabase(parameters);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }

            MessageBox.Show(Properties.Resources.DataBaseCreateSuccess, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.DialogResult = DialogResult.OK;
        }

        private void buttonEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (this.folderBrowserDialog1.ShowDialog(this) != DialogResult.OK)
                return;

            (sender as DevExpress.XtraEditors.ButtonEdit).Text = this.folderBrowserDialog1.SelectedPath;
        }

        
    }
}