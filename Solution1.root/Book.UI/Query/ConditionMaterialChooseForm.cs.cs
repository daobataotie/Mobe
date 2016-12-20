using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Book.UI.Invoices;
using Book.UI.Settings.BasicData;
namespace Book.UI.Query
{
    /*----------------------------------------------------------------
// Copyright (C) 2008 - 2010  咸阳飛馳軟件有限公司
//                     版權所有 圍著必究

// 编 码 人: 裴盾              完成时间:2009-4-26
// 修改原因：
// 修 改 人:                          修改时间:
// 修改原因：
// 修 改 人:                          修改时间:
//----------------------------------------------------------------*/
    public partial class ConditionMaterialChooseForm : ConditionAChooseForm
    {
        //*----------------------------------------------------------------
        // Copyright (C) 2008 - 2010  咸阳飛馳軟件有限公司
        //                     版權所有 圍著必究

        // 编 码 人: 刘永亮              完成时间:2011-01-20
        // 修改原因：
        // 修 改 人:                          修改时间:
        // 修改原因：
        // 修 改 人:                          修改时间:
        //----------------------------------------------------------------*/

        private ConditionMaterial condition;
        
        public ConditionMaterialChooseForm()
        {
            InitializeComponent();

            this.newChooseWorkHouse.Choose = new Settings.ProduceManager.Workhouselog.ChooseWorkHouse();

            //this.bindingSourcePronoteHeader.DataSource = new BL.PronoteHeaderManager().Select();
            //this.bindingSourceProduceMaterialID.DataSource = new BL.ProduceMaterialManager().Select();
            this.dateEditStartDate.DateTime = DateTime.Now.Date.AddDays(-3);
            this.dateEditEndDate.DateTime = DateTime.Now.Date.AddDays(1).AddSeconds(-1);

        }

        private void ConditionMaterialChooseForm_Load(object sender, EventArgs e)
        {

        }

        public override Condition Condition
        {
            get
            {
                return this.condition;
            }
            set
            {
                this.condition = value as ConditionMaterial;
            }
        }

        protected override void OnOK()
        {
            if (this.condition == null)
                this.condition = new ConditionMaterial();

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
            
            this.condition.ProduceMaterialId0 = this.buttonEditMaterial1.EditValue == null ? null : this.buttonEditMaterial1.EditValue.ToString();
            this.condition.ProduceMaterialId1 = this.buttonEditMaterial2.EditValue == null ? null : this.buttonEditMaterial2.EditValue.ToString();
            this.condition.Product0 = this.buttonEditProduct1.EditValue == null ? null : this.buttonEditProduct1.EditValue as Model.Product;
            this.condition.Product1 = this.buttonEditProduct2.EditValue == null ? null : this.buttonEditProduct2.EditValue as Model.Product;
            this.condition.PronoteHeaderId0 = this.buttonEditPronoteHeader1.EditValue == null ? null : this.buttonEditPronoteHeader1.Text;
            this.condition.PronoteHeaderId1 = this.buttonEditPronoteHeader2.EditValue == null ? null : this.buttonEditPronoteHeader2.Text;
            this.condition.DepartmentId0 = (this.newChooseWorkHouse.EditValue as Model.WorkHouse) == null ? null : (this.newChooseWorkHouse.EditValue as Model.WorkHouse).WorkHouseId;

            this.condition.CusInvoiceXOId = this.txtCusInvoiceXOId.Text;
        }

        private void buttonEditProduct1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            ChooseProductForm form = new ChooseProductForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                this.buttonEditProduct1.EditValue = form.SelectedItem as Model.Product;
                this.buttonEditProduct2.EditValue = form.SelectedItem as Model.Product;
            }
        }

        private void buttonEditProduct2_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            ChooseProductForm form = new ChooseProductForm();
            if (form.ShowDialog() == DialogResult.OK)
                this.buttonEditProduct2.EditValue = form.SelectedItem as Model.Product;
        }

        private void buttonEditMaterial1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            produceManager.ProduceMaterial.ChooseMaterialForm form = new produceManager.ProduceMaterial.ChooseMaterialForm();
            //Settings.StockLimitations.TakeMaterialChooseForm form = new Settings.StockLimitations.TakeMaterialChooseForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                this.buttonEditMaterial1.EditValue = form.SelectItem == null ? null : (form.SelectItem).ProduceMaterialID;
                this.buttonEditMaterial2.EditValue = form.SelectItem == null ? null : (form.SelectItem).ProduceMaterialID;
            }
            GC.Collect();
            form.Dispose();
        }

        private void buttonEditMaterial2_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            produceManager.ProduceMaterial.ChooseMaterialForm form = new produceManager.ProduceMaterial.ChooseMaterialForm();
            //Settings.StockLimitations.TakeMaterialChooseForm form = new Settings.StockLimitations.TakeMaterialChooseForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                this.buttonEditMaterial2.EditValue = form.SelectItem == null ? null : (form.SelectItem).ProduceMaterialID;
            }
            GC.Collect();
            form.Dispose();
        }

        private void buttonEditPronoteHeader1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            produceManager.PronoteHeader.ChoosePronoteHeaderDetailsForm form = new produceManager.PronoteHeader.ChoosePronoteHeaderDetailsForm();

            if (form.ShowDialog() == DialogResult.OK)
            {
                this.buttonEditPronoteHeader1.EditValue = form.SelectItem == null ? null : (form.SelectItem).PronoteHeaderID;
            }
            GC.Collect();
            form.Dispose();
        }

        private void buttonEditPronoteHeader2_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            produceManager.PronoteHeader.ChoosePronoteHeaderDetailsForm form = new produceManager.PronoteHeader.ChoosePronoteHeaderDetailsForm();

            if (form.ShowDialog() == DialogResult.OK)
            {
                this.buttonEditPronoteHeader2.EditValue = form.SelectItem == null ? null : (form.SelectItem).PronoteHeaderID;
            }
            GC.Collect();
            form.Dispose();
        }
    }
}