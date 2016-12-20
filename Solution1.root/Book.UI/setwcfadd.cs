using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Xml;

namespace Book.UI
{
    public partial class setwcfadd : DevExpress.XtraEditors.XtraForm
    {
        private BL.SettingManager settingManager=new BL.SettingManager();
        Model.Setting seting;
        public setwcfadd()
        {

            InitializeComponent();
            XmlDocument doc = new XmlDocument();
            doc.Load(Application.ExecutablePath + ".config");
            XmlNode node = doc.SelectSingleNode("/configuration/system.serviceModel/client/endpoint");
          
           // seting= settingManager.Get("wcfadd");
            this.textProductNameOrId.Text = node.Attributes["address"].Value;
        }

        private void simpleButton_Search_Click(object sender, EventArgs e)
        {
            if (this.textProductNameOrId.Text == "")
            {
                MessageBox.Show("請輸入地址!");
                return;
            }
            //if (seting == null)
            //{
            //    seting = new Book.Model.Setting();
            //    seting.SettingId = "wcfadd";
            //    seting.SettingCurrentValue = textProductNameOrId.Text;
            //    seting.PictrueLogo=new byte[]{};
            //    seting.SettingName = "web服務地址";
            //    settingManager.Insert(seting);
            //}
            //else
            //settingManager.Update("wcfadd", this.textProductNameOrId.Text);
            XmlDocument doc = new XmlDocument();
            doc.Load(Application.ExecutablePath + ".config");
            XmlNode node = doc.SelectSingleNode("/configuration/system.serviceModel/client/endpoint");
            node.Attributes["address"].Value = textProductNameOrId.Text;
            doc.Save(Application.ExecutablePath + ".config");



            MessageBox.Show(this, "保存成功,設置將在軟件重啟后生效", "提示");
            this.Close();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        { 
            this.Close();

        }
    }
}