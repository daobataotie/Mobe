using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.produceManager.ProduceInDepot
{
    public partial class ChooseDefectRate : Query.ConditionChooseForm
    {
        public ChooseDefectRateCls condition;

        public override Book.UI.Query.Condition Condition
        {
            get
            {
                return this.condition;
            }
            set
            {
                this.condition = value as ChooseDefectRateCls;
            }
        }

        public ChooseDefectRate()
        {
            InitializeComponent();

            this.nccWorkHouseStart.Choose = new Settings.ProduceManager.Workhouselog.ChooseWorkHouse();
            this.nccWorkHouseEnd.Choose = new Settings.ProduceManager.Workhouselog.ChooseWorkHouse();
            this.nccCustomerStart.Choose = new Settings.BasicData.Customs.ChooseCustoms();
            this.nccCustomerEnd.Choose = new Settings.BasicData.Customs.ChooseCustoms();
        }

        private void ChooseDefectRate_Load(object sender, EventArgs e)
        {
            for (int i = 0; i <= 10; i++)
            {
                this.comboxBuLianglv.Properties.Items.Add((10 * i).ToString() + "%");
            }

            this.DateStart.DateTime = DateTime.Now.Date.AddMonths(-1);
            this.DateEnd.DateTime = DateTime.Now.Date.AddDays(1).AddSeconds(-1);
            this.radioGroupJiLuFangShi.SelectedIndex = 0;
            this.radioGroupProductStates.SelectedIndex = 0;
            this.radioGroupDingDanJieAn.SelectedIndex = 0;
            this.comboxOrderCol.SelectedIndex = 0;
            this.comboxOrderType.SelectedIndex = 0;
            this.comboxBuLianglv.SelectedIndex = 0;
            this.comboxShaiXuan.SelectedIndex = 0;

            this.comboxBuLianglv.Enabled = false;
            this.comboxShaiXuan.Enabled = false;
        }

        private void btnEditProductStart_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            Invoices.ChooseProductForm f = new Invoices.ChooseProductForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                this.btnEditProductStart.EditValue = f.SelectedItem as Model.Product;
                this.btnEditProductEnd.EditValue = f.SelectedItem as Model.Product;
            }
            f.Dispose();
            GC.Collect();
        }

        private void btnEditProductEnd_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            Invoices.ChooseProductForm f = new Invoices.ChooseProductForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                this.btnEditProductEnd.EditValue = f.SelectedItem as Model.Product;
            }
            f.Dispose();
            GC.Collect();
        }

        protected override void OnOK()
        {
            if (this.condition == null)
                this.condition = new ChooseDefectRateCls();

            this.condition.attrJiLuFangShi = this.radioGroupJiLuFangShi.SelectedIndex == 0 ? true : false;
            this.condition.attrProductStates = this.radioGroupProductStates.SelectedIndex;
            this.condition.InvoiceStates = this.radioGroup_DateType.SelectedIndex == 1 ? 1 : this.radioGroupDingDanJieAn.SelectedIndex;
            this.condition.attrQiangHua = this.chkEditQiangHua.Checked;
            this.condition.attrWuDu = this.chkEditWuDu.Checked;
            this.condition.attrWuQiangHuaWuDu = this.chkEditWuQiangHuaWuDu.Checked;

            this.condition.attrOrderColumn = this.comboxOrderCol.SelectedIndex == -1 ? 0 : this.comboxOrderCol.SelectedIndex;
            this.condition.attrOrderType = this.comboxOrderType.SelectedIndex == -1 ? 0 : this.comboxOrderType.SelectedIndex;

            this.condition.StartCustomer = this.nccCustomerStart.EditValue as Model.Customer;
            this.condition.EndCustomer = this.nccCustomerEnd.EditValue as Model.Customer;
            this.condition.StartProduct = this.btnEditProductStart.EditValue as Model.Product;
            this.condition.EndProduct = this.btnEditProductEnd.EditValue as Model.Product;
            this.condition.StartWorkHouse = this.nccWorkHouseStart.EditValue as Model.WorkHouse;
            this.condition.EndWorkHouse = this.nccWorkHouseEnd.EditValue as Model.WorkHouse;

            this.condition.DateType = this.radioGroup_DateType.SelectedIndex;

            this.condition.StartDate = this.DateStart.EditValue == null ? global::Helper.DateTimeParse.NullDate : this.DateStart.DateTime.Date;
            this.condition.EndDate = this.DateEnd.EditValue == null ? global::Helper.DateTimeParse.EndDate : this.DateEnd.DateTime.AddDays(1).Date.AddSeconds(-1);
            this.condition.StartProduceInDepotId = this.txtProduceInDepotStart.Text;
            this.condition.EndProduceInDepotId = this.txtProduceInDepotEnd.Text;
            this.condition.StartPronoteHeaderId = this.txtPronoteHeaderStart.Text;
            this.condition.EndPronoteHeaderId = this.txtPronoteHeaderEnd.Text;

            this.condition.EnableBLV = this.chkEditEnableBLV.Checked;

            if (this.condition.EnableBLV)
            {
                //不良率筛选
                string strRejectionRate = this.comboxBuLianglv.Text.Replace("%", "");
                try
                {
                    this.condition.RejectionRate = double.Parse(strRejectionRate);
                }
                catch
                {
                    this.condition.RejectionRate = 0;
                }
            }

            if (this.comboxShaiXuan.Text.Contains("(>)"))
                this.condition.RejectionRateCompare = ">";
            if (this.comboxShaiXuan.Text.Contains("(<)"))
                this.condition.RejectionRateCompare = "<";
            if (this.comboxShaiXuan.Text.Contains("(=)"))
                this.condition.RejectionRateCompare = "=";
            if (this.comboxShaiXuan.Text.Contains("(!=)"))
                this.condition.RejectionRateCompare = "<>";
        }

        private void nccWorkHouseStart_EditValueChanged(object sender, EventArgs e)
        {
            if (this.nccWorkHouseEnd.EditValue == null)
            {
                this.nccWorkHouseEnd.EditValue = this.nccWorkHouseStart.EditValue;
            }
        }

        private void nccCustomerStart_EditValueChanged(object sender, EventArgs e)
        {
            if (this.nccCustomerEnd.EditValue == null)
            {
                this.nccCustomerEnd.EditValue = nccCustomerStart.EditValue;
            }
        }

        private void comboxBuLianglv_EditValueChanged(object sender, EventArgs e)
        {
            if (!this.comboxBuLianglv.Text.Contains("%"))
            {
                this.comboxBuLianglv.Text += "%";
            }
        }

        //启用不良率筛选
        private void chkEditEnableBLV_CheckedChanged(object sender, EventArgs e)
        {
            this.comboxBuLianglv.Enabled = this.chkEditEnableBLV.Checked;
            this.comboxShaiXuan.Enabled = this.chkEditEnableBLV.Checked;
        }

        //选择日期类型
        private void radioGroup_DateType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.radioGroup_DateType.SelectedIndex == 1)
            {
                this.radioGroupDingDanJieAn.SelectedIndex = 1;
                this.radioGroupDingDanJieAn.Enabled = false;
            }
            else
            {
                this.radioGroupDingDanJieAn.Enabled = true;
            }
        }
    }
}