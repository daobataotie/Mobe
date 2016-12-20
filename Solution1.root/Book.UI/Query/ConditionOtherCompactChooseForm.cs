using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Book.UI.Invoices;
using Book.UI.produceManager.ProduceOtherCompact;

namespace Book.UI.Query
{


    /*----------------------------------------------------------------
   // Copyright (C) 2008 - 2010  咸陽飛馳軟件有限公司
   //                     版權所有 圍著必究
   // 功能描述: 客戶設置
   // 文 件 名：ConditionOtherCompactChooseForm
   // 编 码 人: 刘永亮                   完成时间:2011-1-01-28
   // 修改原因：
   // 修 改 人:                          修改时间:
   // 修改原因：
   // 修 改 人:                          修改时间:
   //----------------------------------------------------------------*/
    public partial class ConditionOtherCompactChooseForm : ConditionAChooseForm
    {
        private ConditionOtherCompact condition;
        public ConditionOtherCompactChooseForm()
        {
            InitializeComponent();
            this.dateEditStartDate.DateTime = System.DateTime.Now.Date.AddMonths(-1);
            this.dateEditEndDate.DateTime = System.DateTime.Now.Date.AddDays(1).AddSeconds(-1);
            #region 廠商初始值
            Book.UI.Settings.BasicData.Supplier.ChooseSupplier chooseSupplier = new Settings.BasicData.Supplier.ChooseSupplier();
            this.newChooseSupplier1.Choose = chooseSupplier;
            this.newChooseSupplier2.Choose = chooseSupplier;
            #endregion
        }
        public override Condition Condition
        {
            get
            {
                return this.condition;
            }
            set
            {
                this.condition = value as ConditionOtherCompact;
            }
        }
        protected override void OnOK()
        {
            if (this.condition == null)
                this.condition = new ConditionOtherCompact();

            this.condition.StartDate = this.dateEditStartDate.EditValue == null ? global::Helper.DateTimeParse.NullDate : this.dateEditStartDate.DateTime;
            this.condition.EndDate = this.dateEditEndDate.EditValue == null ? global::Helper.DateTimeParse.EndDate : this.dateEditEndDate.DateTime;
            this.condition.SupplierId1 = this.newChooseSupplier1.EditValue == null ? null : (this.newChooseSupplier1.EditValue as Model.Supplier).Id;
            this.condition.SupplierId2 = this.newChooseSupplier2.EditValue == null ? null : (this.newChooseSupplier2.EditValue as Model.Supplier).Id;
            this.condition.ProduceOtherCompactId1 = this.buttonEditProduceOtherCompactId1.EditValue == null ? null : this.buttonEditProduceOtherCompactId1.EditValue.ToString();
            this.condition.ProduceOtherCompactId2 = this.buttonEditProduceOtherCompactId2.EditValue == null ? null : this.buttonEditProduceOtherCompactId2.EditValue.ToString();
            this.condition.ProductId1 = (this.buttonEditProduct1.EditValue as Model.Product) == null ? null : (this.buttonEditProduct1.EditValue as Model.Product).Id;
            this.condition.ProductId2 = (this.buttonEditProduct2.EditValue as Model.Product) == null ? null : (this.buttonEditProduct2.EditValue as Model.Product).Id;
        }

        private void buttonEditProduct1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            ChooseProductForm form = new ChooseProductForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                this.buttonEditProduct1.EditValue = form.SelectedItem;
                this.buttonEditProduct2.EditValue = form.SelectedItem;
            }
        }

        private void buttonEditProduct2_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            ChooseProductForm form = new ChooseProductForm();
            if (form.ShowDialog() == DialogResult.OK)
                this.buttonEditProduct2.EditValue = form.SelectedItem;
        }

        private void buttonEditProduceOtherCompactId1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            ChooseOutContract form = new ChooseOutContract();
            if (form.ShowDialog() == DialogResult.OK)
            {
                this.buttonEditProduceOtherCompactId1.EditValue = (form.SelectItem as Model.ProduceOtherCompact).ProduceOtherCompactId;
                this.buttonEditProduceOtherCompactId2.EditValue = (form.SelectItem as Model.ProduceOtherCompact).ProduceOtherCompactId;
            }
        }

        private void buttonEditProduceOtherCompactId2_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            ChooseOutContract form = new ChooseOutContract();
            if (form.ShowDialog() == DialogResult.OK)
                this.buttonEditProduceOtherCompactId2.EditValue = (form.SelectItem as Model.ProduceOtherCompact).ProduceOtherCompactId;
        }
    }
}