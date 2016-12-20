using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Book.UI.Invoices;

namespace Book.UI.Query
{
    /*----------------------------------------------------------------
// Copyright (C) 2008 - 2010  咸阳飛馳軟件有限公司
//                     版權所有 圍著必究

// 编 码 人: 裴盾            完成时间:2009-5-6
// 修改原因：
// 修 改 人:                          修改时间:
// 修改原因：
// 修 改 人:                          修改时间:
//----------------------------------------------------------------*/
    public partial class ConditionOtherExitChooseForm : ConditionAChooseForm
    {
        private ConditionOtherExit condition;
        //无参构造
        public ConditionOtherExitChooseForm()
        {
            InitializeComponent();
            Book.UI.Settings.BasicData.Supplier.ChooseSupplier chooseSupplier = new Settings.BasicData.Supplier.ChooseSupplier();
            this.newChooseContorlSupplier1.Choose = chooseSupplier;
            this.newChooseContorlSupplier2.Choose = chooseSupplier;
            this.dateEditStartDate.DateTime = System.DateTime.Now.AddMonths(-3);
            this.dateEditEndDate.DateTime = System.DateTime.Now;
        }

        #region 重写父类方法
        public override Condition Condition
        {
            get
            {
                return this.condition;
            }
            set
            {
                this.condition = value as ConditionOtherExit;
            }
        }
        protected override void OnOK()
        {
            if (this.condition == null)
                this.condition = new ConditionOtherExit();

            this.condition.StartDate = this.dateEditStartDate.EditValue == null ? global::Helper.DateTimeParse.NullDate : this.dateEditStartDate.DateTime;
            this.condition.EndDate = this.dateEditEndDate.EditValue == null ? System.DateTime.Now : this.dateEditEndDate.DateTime;
            this.condition.SupplierId1 = this.newChooseContorlSupplier1.EditValue == null ? null : (this.newChooseContorlSupplier1.EditValue as Model.Supplier).SupplierId;
            this.condition.SupplierId2 = this.newChooseContorlSupplier2.EditValue == null ? null : (this.newChooseContorlSupplier2.EditValue as Model.Supplier).SupplierId;
            this.condition.ProduceOtherCompactId1 = this.buttonEditProduceOtherCompactId1.EditValue == null ? null : this.buttonEditProduceOtherCompactId1.Text;
            this.condition.ProduceOtherCompactId2 = this.buttonEditProduceOtherCompactId2.EditValue == null ? null : this.buttonEditProduceOtherCompactId2.Text;
            this.condition.ProductId1 = this.StartProductId.EditValue == null ? null : (this.StartProductId.EditValue as Model.Product).Id;
            this.condition.ProductId2 = this.EndProductId.EditValue == null ? null : (this.EndProductId.EditValue as Model.Product).Id;
        }
        #endregion

        private void StartProductId_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            ChooseProductForm form = new ChooseProductForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                this.StartProductId.EditValue = form.SelectedItem;
                this.EndProductId.EditValue = form.SelectedItem;
            }
        }

        private void EndProductId_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            ChooseProductForm form = new ChooseProductForm();
            if (form.ShowDialog() == DialogResult.OK)
                this.EndProductId.EditValue = form.SelectedItem;
        }
    }
}