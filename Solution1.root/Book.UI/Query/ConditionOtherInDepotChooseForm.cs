using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Book.UI.produceManager.ProduceOtherCompact;

namespace Book.UI.Query
{

    /*----------------------------------------------------------------
    // Copyright (C) 2008 - 2010  咸阳飛馳軟件有限公司
    //                     版權所有 圍著必究
    // 编 码 人: 裴盾              完成时间:2009-5-6
    // 修改原因：
    // 修  改 人:刘永亮                    修改时间:2010-12-09
    // 修改原因：需求变动
    // 修 改 人:                          修改时间:
    //----------------------------------------------------------------*/

    public partial class ConditionOtherInDepotChooseForm : ConditionAChooseForm
    {
        private ConditionOtherInDepot condition;
        BL.ProduceOtherCompactManager otherCompactManager = new BL.ProduceOtherCompactManager();

        public ConditionOtherInDepotChooseForm()
        {
            InitializeComponent();

            this.newChooseContorlSupper1.Choose = new Settings.BasicData.Supplier.ChooseSupplier();
            this.newChooseContorlSupper2.Choose = new Settings.BasicData.Supplier.ChooseSupplier();
            this.dateEditStartDate.DateTime = System.DateTime.Now.Date.AddMonths(-3);
            this.dateEditEndDate.DateTime = System.DateTime.Now.Date.AddDays(1).AddSeconds(-1);
        }

        public override Condition Condition
        {
            get
            {
                return this.condition;
            }
            set
            {
                this.condition = value as ConditionOtherInDepot;
            }
        }

        protected override void OnOK()
        {
            if (this.condition == null)
                this.condition = new ConditionOtherInDepot();

            this.condition.StartDate = this.dateEditStartDate.EditValue == null ? global::Helper.DateTimeParse.NullDate : this.dateEditStartDate.DateTime;
            this.condition.EndDate = this.dateEditEndDate.EditValue == null ? System.DateTime.Now : this.dateEditEndDate.DateTime;
            this.condition.ProduceOtherCompactId1 = this.buttonEditProduceOtherCompactId1.EditValue == null ? null : this.buttonEditProduceOtherCompactId1.EditValue.ToString();
            this.condition.ProduceOtherCompactId2 = this.buttonEditProduceOtherCompactId2.EditValue == null ? null : this.buttonEditProduceOtherCompactId2.EditValue.ToString();
            this.condition.Supplier1 = this.newChooseContorlSupper1.EditValue as Model.Supplier;
            this.condition.Supplier2 = this.newChooseContorlSupper2.EditValue as Model.Supplier;
            this.condition.Product1 = this.StartProductId.EditValue as Model.Product;
            this.condition.Product2 = this.EndProductId.EditValue as Model.Product;
            this.condition.InvouceCusIdStart = this.txtInvouceCusIdStart.Text;
            this.condition.InvoiceCusIdEnd = this.txtInvouceCusIdEnd.Text;
        }

        private void buttonEditProduceOtherCompactId1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            ChooseOutContract form = new ChooseOutContract();
            if (form.ShowDialog() == DialogResult.OK)
            {
                this.buttonEditProduceOtherCompactId1.EditValue = (form.SelectItem as Model.ProduceOtherCompact).ProduceOtherCompactId;
                this.buttonEditProduceOtherCompactId2.EditValue = (form.SelectItem as Model.ProduceOtherCompact).ProduceOtherCompactId;
            }
            form.Dispose();
            GC.Collect();
        }

        private void buttonEditProduceOtherCompactId2_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            ChooseOutContract form = new ChooseOutContract();
            if (form.ShowDialog() == DialogResult.OK)
                this.buttonEditProduceOtherCompactId2.EditValue = (form.SelectItem as Model.ProduceOtherCompact).ProduceOtherCompactId;
            form.Dispose();
            GC.Collect();
        }

        private void StartProductId_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            Invoices.ChooseProductForm form = new Invoices.ChooseProductForm();
            if (form.ShowDialog(this) == DialogResult.OK)
            {
                this.StartProductId.EditValue = form.SelectedItem as Model.Product;
                this.EndProductId.EditValue = form.SelectedItem as Model.Product;
            }
            form.Dispose();
            GC.Collect();
        }

        private void EndProductId_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            Invoices.ChooseProductForm form = new Invoices.ChooseProductForm();
            if (form.ShowDialog(this) == DialogResult.OK)
                this.EndProductId.EditValue = form.SelectedItem as Model.Product;
            form.Dispose();
            GC.Collect();
        }

        private void newChooseContorlSupper1_EditValueChanged(object sender, EventArgs e)
        {
            if (this.newChooseContorlSupper2.EditValue == null)
                this.newChooseContorlSupper2.EditValue = this.newChooseContorlSupper1.EditValue;
        }
    }
}