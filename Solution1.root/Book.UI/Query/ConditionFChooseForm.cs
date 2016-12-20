using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Book.UI.Query
{
    /*----------------------------------------------------------------
    // Copyright (C) 2008 - 2010  咸阳飛馳軟件有限公司
    //                     版權所有 圍著必究

    // 编 码 人: 裴盾            完成时间:2009-4-16
    // 修改原因：
    // 修 改 人:                          修改时间:
    // 修改原因：
    // 修 改 人:                          修改时间:
    //----------------------------------------------------------------*/

    public partial class ConditionFChooseForm : ConditionAChooseForm
    {
        private ConditionF condition;

        private global::Helper.CompanyKind kind;

        //private BL.CompanyManager companyManager = new Book.BL.CompanyManager();

        public ConditionFChooseForm(global::Helper.CompanyKind kind)
        {
            InitializeComponent();
            this.kind = kind;
            switch (this.kind)
            {
                case global::Helper.CompanyKind.Supplier:
                    this.nccCustomerStart.Choose = new Settings.BasicData.Supplier.ChooseSupplier();
                    this.nccCustomerEnd.Choose = new Settings.BasicData.Supplier.ChooseSupplier();
                    this.nccCustomerChuHuoStart.Enabled = false;
                    this.nccCustomerChuHuoEnd.Enabled = false;

                    IList<Model.SupplierCategory> categoryList = (new BL.SupplierCategoryManager()).Select();
                    foreach (Model.SupplierCategory supplierCategory in categoryList)
                    {
                        this.comboBoxSupplierCategory.Properties.Items.Add(supplierCategory.SupplierCategoryId, supplierCategory.SupplierCategoryName);
                    }
                    this.comboBoxSupplierCategory.Properties.ReadOnly = false;
                    break;
                case global::Helper.CompanyKind.Customer:
                default:
                    this.nccCustomerStart.Choose = new Settings.BasicData.Customs.ChooseCustoms();
                    this.nccCustomerEnd.Choose = new Settings.BasicData.Customs.ChooseCustoms();
                    this.nccCustomerChuHuoStart.Choose = new Settings.BasicData.Customs.ChooseCustoms();
                    this.nccCustomerChuHuoEnd.Choose = new Settings.BasicData.Customs.ChooseCustoms();
                    this.nccCustomerChuHuoStart.Enabled = true;
                    this.nccCustomerChuHuoEnd.Enabled = true;
                    break;
            }

        }

        protected override void OnOK()
        {
            if (this.condition == null)
                condition = new ConditionF();
            condition.StartDate = this.dateEditStartDate.DateTime;
            condition.EndDate = this.dateEditEndDate.DateTime;
            if (this.kind == global::Helper.CompanyKind.Supplier)
            {
                condition.StartId = (this.nccCustomerStart.EditValue as Model.Supplier) == null ? null : (this.nccCustomerStart.EditValue as Model.Supplier).Id;
                condition.EndId = (this.nccCustomerEnd.EditValue as Model.Supplier) == null ? null : (this.nccCustomerEnd.EditValue as Model.Supplier).Id;
                condition.StartChuHuoId = string.Empty;
                condition.EndChuHuoId = string.Empty;

                foreach (string str in comboBoxSupplierCategory.Properties.Items.GetCheckedValues())
                {
                    condition.CategoryId +='"'+ str + '"'+',';
                }
            }
            else
            {
                condition.StartId = (this.nccCustomerStart.EditValue as Model.Customer) == null ? null : (this.nccCustomerStart.EditValue as Model.Customer).Id;
                condition.EndId = (this.nccCustomerEnd.EditValue as Model.Customer) == null ? null : (this.nccCustomerEnd.EditValue as Model.Customer).Id;
                condition.StartChuHuoId = (this.nccCustomerChuHuoStart.EditValue as Model.Customer) == null ? "" : (this.nccCustomerChuHuoStart.EditValue as Model.Customer).Id;
                condition.EndChuHuoId = (this.nccCustomerChuHuoEnd.EditValue as Model.Customer) == null ? "" : (this.nccCustomerChuHuoEnd.EditValue as Model.Customer).Id;
            }
        }

        public override Condition Condition
        {
            get
            {
                return this.condition;
            }
            set
            {
                this.condition = value as ConditionF;
            }
        }

        private void nccCustomerStart_EditValueChanged(object sender, EventArgs e)
        {
            if (this.nccCustomerChuHuoStart.EditValue != null && this.nccCustomerEnd.EditValue == null)
                this.nccCustomerEnd.EditValue = this.nccCustomerStart.EditValue;
        }

        private void nccCustomerEnd_EditValueChanged(object sender, EventArgs e)
        {
            if (this.nccCustomerChuHuoEnd.EditValue != null && this.nccCustomerStart.EditValue == null)
                this.nccCustomerStart.EditValue = this.nccCustomerEnd.EditValue;
        }

        private void nccCustomerChuHuoStart_EditValueChanged(object sender, EventArgs e)
        {
            if (this.nccCustomerChuHuoStart.EditValue != null && this.nccCustomerChuHuoEnd.EditValue == null)
                this.nccCustomerChuHuoEnd.EditValue = this.nccCustomerChuHuoStart.EditValue;
        }

        private void nccCustomerChuHuoEnd_EditValueChanged(object sender, EventArgs e)
        {
            if (this.nccCustomerChuHuoEnd.EditValue != null && this.nccCustomerChuHuoStart.EditValue == null)
                this.nccCustomerChuHuoStart.EditValue = this.nccCustomerChuHuoEnd.EditValue;
        }

    }
}