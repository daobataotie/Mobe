using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.produceManager.ExportSendMail
{
    public partial class MailParameterSet : DevExpress.XtraEditors.XtraForm
    {
        public MailParameterSet()
        {
            InitializeComponent();
            //取得服务器附件存储地址
            if (System.Configuration.ConfigurationManager.OpenExeConfiguration(System.Configuration.ConfigurationUserLevel.None) != null && System.Configuration.ConfigurationManager.OpenExeConfiguration(System.Configuration.ConfigurationUserLevel.None).AppSettings.Settings["accessoriesPath"] != null)
            {
                this.txtDefSender.Text = System.Configuration.ConfigurationManager.AppSettings["DefSender"];
                this.txtDefSenderAddress.Text = System.Configuration.ConfigurationManager.AppSettings["DefSenderAddress"];
                this.txtSMTPServer.Text = System.Configuration.ConfigurationManager.AppSettings["SMTPHost"];
                this.txtSMTPServerUser.Text = System.Configuration.ConfigurationManager.AppSettings["SMTPUser"];
                this.txtSMTPServerPwd.Text = System.Configuration.ConfigurationManager.AppSettings["SMTPPassword"];
            }
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            System.Configuration.ConfigurationManager.AppSettings.Set("DefSender", this.txtDefSender.Text);
            System.Configuration.ConfigurationManager.AppSettings.Set("DefSenderAddress", this.txtDefSenderAddress.Text);
            System.Configuration.ConfigurationManager.AppSettings.Set("SMTPHost", this.txtSMTPServer.Text);
            System.Configuration.ConfigurationManager.AppSettings.Set("SMTPUser", this.txtSMTPServerUser.Text);
            System.Configuration.ConfigurationManager.AppSettings.Set("SMTPPassword", this.txtSMTPServerPwd.Text);
            MessageBox.Show(Properties.Resources.SaveSuccess);
        }


    }
}