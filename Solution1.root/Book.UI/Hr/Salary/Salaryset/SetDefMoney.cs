using System;
using System.Windows.Forms;
using System.Xml;

namespace Book.UI.Hr.Salary.Salaryset
{
    public partial class SetDefMoney : DevExpress.XtraEditors.XtraForm
    {
        string ConfigFile = Application.ExecutablePath + ".config";
        private BL.SettingManager settingManager = new Book.BL.SettingManager();
        public SetDefMoney()
        {
            InitializeComponent();
        }
        //保存
        private void barBtn_Save_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            bool canSave = false;
            if (this.txtManDefDayPay.Text != "" && this.txtManDefMonthPay.Text != "" && this.txtWomanDefDaypay.Text != "" && this.txtWomanDefMonthPay.Text != "")
            {
                canSave = true;
            }
            else
            {
                canSave = false;
                MessageBox.Show("信息不完整,請查驗");
                return;
            }
            if (canSave)
            {
                //XmlDocument document = new XmlDocument();
                //document.Load(ConfigFile);
                //XmlNode xn = document.SelectSingleNode("/configuration/userSettings/Book.UI.Properties.Settings/setting[@name='Pay']");
                //((XmlElement)xn.SelectSingleNode("add[@key='manDayilyPay']")).SetAttribute("value", this.txtManDefDayPay.Text);
                //((XmlElement)xn.SelectSingleNode("add[@key='manMonthPay']")).SetAttribute("value", this.txtManDefMonthPay.Text);
                //((XmlElement)xn.SelectSingleNode("add[@key='womanDayilyPay']")).SetAttribute("value", this.txtWomanDefDaypay.Text);
                //((XmlElement)xn.SelectSingleNode("add[@key='womanMonthPay']")).SetAttribute("value", this.txtWomanDefMonthPay.Text);
                //document.Save(ConfigFile);


                this.settingManager.Update("PaymanDayily", this.txtManDefDayPay.Value.ToString("0.##"));
                this.settingManager.Update("manMonthPay", this.txtManDefMonthPay.Value.ToString("0.##"));
                this.settingManager.Update("womanDayilyPay", this.txtWomanDefDaypay.Value.ToString("0.##"));
                this.settingManager.Update("womanMonthPay", this.txtWomanDefMonthPay.Value.ToString("0.##"));

                MessageBox.Show(Properties.Resources.SaveSuccess);
            }
        }

        private void SetDefMoney_Load(object sender, EventArgs e)
        {
            Model.Setting setting = this.settingManager.Get("PaymanDayily");// 男 日薪
            this.txtManDefDayPay.Text =setting.SettingCurrentValue;
            setting = this.settingManager.Get("manMonthPay");
            this.txtManDefMonthPay.Text = setting.SettingCurrentValue;
            setting = this.settingManager.Get("womanDayilyPay");
            this.txtWomanDefDaypay.Text = setting.SettingCurrentValue;
            setting = this.settingManager.Get("womanMonthPay");
            this.txtWomanDefMonthPay.Text = setting.SettingCurrentValue; ;

            //XmlDocument document = new XmlDocument();
            //document.Load(ConfigFile);
            //XmlNode xn = document.SelectSingleNode("/configuration/userSettings/Book.UI.Properties.Settings/setting[@name='Pay']");
            //this.txtManDefDayPay.Text = xn.SelectSingleNode("add[@key='manDayilyPay']").Attributes["value"].Value;
            //this.txtManDefMonthPay.Text = xn.SelectSingleNode("add[@key='manMonthPay']").Attributes["value"].Value;
            //this.txtWomanDefDaypay.Text = xn.SelectSingleNode("add[@key='womanDayilyPay']").Attributes["value"].Value;
            //this.txtWomanDefMonthPay.Text = xn.SelectSingleNode("add[@key='womanMonthPay']").Attributes["value"].Value;
        }
    }
}