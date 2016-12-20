using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.Query
{
    public partial class ConditionXChooseForm : ConditionAChooseForm
    {
        //  选择除销售报价单的 销售单据 依商品 客户订单号 客户  日期区间
        private ConditionX condition;

        public ConditionXChooseForm()
        {
            InitializeComponent();
            this.newChooseCustomer.Choose = new Settings.BasicData.Customs.ChooseCustoms();
            this.newChooseCustomer2.Choose = new Settings.BasicData.Customs.ChooseCustoms();
            this.newChooseEmp1.Choose = new Settings.BasicData.Employees.ChooseEmployee();
            this.newChooseEmp2.Choose = new Settings.BasicData.Employees.ChooseEmployee();
            this.dateEditStartDate.DateTime = DateTime.Now.Date.AddMonths(-1);
            this.dateEditEndDate.DateTime = DateTime.Now.Date.AddDays(1).AddSeconds(-1);
        }

        public override Condition Condition
        {
            get
            {
                return this.condition;
            }
            set
            {
                this.condition = value as ConditionX;
            }
        }

        protected override void OnOK()
        {
            if (this.condition == null)
                this.condition = new ConditionX();

            if (global::Helper.DateTimeParse.DateTimeEquls(this.dateEditStartDate.DateTime, new DateTime()))
            {
                this.condition.StartDate = global::Helper.DateTimeParse.NullDate;
            }

            else
            {
                this.condition.StartDate = this.dateEditStartDate.DateTime;
            }


            if (global::Helper.DateTimeParse.DateTimeEquls(this.dateEditEndDate.DateTime, new DateTime()))
            {
                this.condition.EndDate = global::Helper.DateTimeParse.EndDate;
            }

            else
            {
                this.condition.EndDate = this.dateEditEndDate.DateTime;
            }


            if (global::Helper.DateTimeParse.DateTimeEquls(this.dateEditJHDate1.DateTime, new DateTime()))
            {
                this.condition.Yjri1 = global::Helper.DateTimeParse.NullDate;
            }
            else
            {
                this.condition.Yjri1 = this.dateEditJHDate1.DateTime;
            }

            if (global::Helper.DateTimeParse.DateTimeEquls(this.dateEditJHDate2.DateTime, new DateTime()))
            {
                this.condition.Yjri2 = global::Helper.DateTimeParse.EndDate;
            }
            else
            {
                this.condition.Yjri2 = this.dateEditJHDate2.DateTime;
            }
            this.condition.Product = this.buttonEditPro.EditValue as Model.Product;
            this.condition.Product2 = this.buttonEditPro2.EditValue as Model.Product;
            this.condition.CusXOId = this.textEditCusXOId.Text == "" ? null : this.textEditCusXOId.Text;
            this.condition.Customer1 = this.newChooseCustomer.EditValue as Model.Customer;
            this.condition.Customer2 = this.newChooseCustomer2.EditValue as Model.Customer;
            this.condition.Employee1 = this.newChooseEmp1.EditValue as Model.Employee;
            this.condition.Employee2 = this.newChooseEmp2.EditValue as Model.Employee;
            this.condition.IsClose = this.ceisclose.Checked;
            this.condition.XOId1 = this.textEditCOID1.Text;
            this.condition.XOId2 = this.textEditCOID2.Text;
            this.condition.OrderColumn = this.ComboxOrderColumn.SelectedIndex == -1 ? 0 : this.ComboxOrderColumn.SelectedIndex;
            this.condition.OrderType = this.ComboxOrderType.SelectedIndex == -1 ? 0 : this.ComboxOrderType.SelectedIndex;
            this.condition.DetailFlag = this.checkEditDetailFlag.Checked;
        }

        private void buttonEditPro_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            Invoices.ChooseProductForm form = new Invoices.ChooseProductForm();
            if (form.ShowDialog(this) == DialogResult.OK)
            {
                this.buttonEditPro.EditValue = form.SelectedItem as Model.Product;
                this.buttonEditPro2.EditValue = this.buttonEditPro.EditValue;

            }
            form.Dispose();
            GC.Collect();
        }

        private void ConditionXChooseForm_Load(object sender, EventArgs e)
        {

        }

        private void newChooseCustomer_EditValueChanged(object sender, EventArgs e)
        {
            if (this.newChooseCustomer.EditValue != null)
                this.newChooseCustomer2.EditValue = this.newChooseCustomer.EditValue;
        }

        private void newChooseContorl2_EditValueChanged(object sender, EventArgs e)
        {
            if (this.newChooseEmp1.EditValue != null)
                this.newChooseEmp2.EditValue = this.newChooseEmp1.EditValue;
        }

        private void buttonEditPro2_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            Invoices.ChooseProductForm form = new Invoices.ChooseProductForm();
            if (form.ShowDialog(this) == DialogResult.OK)
            {
                this.buttonEditPro2.EditValue = form.SelectedItem as Model.Product;

            }
            form.Dispose();
            GC.Collect();
        }
    }
}