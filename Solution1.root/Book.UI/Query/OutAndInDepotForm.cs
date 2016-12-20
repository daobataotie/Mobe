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
    public partial class OutAndInDepotForm : ConditionChooseForm
    {
        private OutAndInDepot condition;

        public override Condition Condition
        {
            get
            {
                return condition;
            }
            set
            {
                condition = value as OutAndInDepot;
            }
        }

        public OutAndInDepotForm()
        {
            InitializeComponent();

            this.bindingSourceDepot.DataSource = (new BL.DepotManager()).Select();
            this.bindingSourceProductCategory.DataSource = (new BL.ProductCategoryManager()).Select();
            this.date_Start.DateTime = DateTime.Now.Date.AddDays(-7);
            this.date_End.DateTime = DateTime.Now.Date.AddDays(1).AddSeconds(-1);
        }

        protected override void OnOK()
        {
            if (this.condition == null)
                this.condition = new OutAndInDepot();
            if (global::Helper.DateTimeParse.DateTimeEquls(this.date_Start.DateTime, new DateTime()))
            {
                this.condition.StartDate = global::Helper.DateTimeParse.NullDate;
            }

            else
            {
                this.condition.StartDate = this.date_Start.DateTime;
            }


            if (global::Helper.DateTimeParse.DateTimeEquls(this.date_End.DateTime, new DateTime()))
            {
                this.condition.EndDate = global::Helper.DateTimeParse.EndDate;
            }

            else
            {
                this.condition.EndDate = this.date_End.DateTime;
            }

            //this.condition.OutDepotIdStart = this.txt_DepotOutIdStart.Text;
            //this.condition.OutDepotIdEnd = this.txt_DepotOutIdEnd.Text;
            this.condition.DepotEnd = this.lookUpEditDepotEnd.EditValue == null ? null : this.lookUpEditDepotEnd.EditValue.ToString();
            this.condition.DepotStart = this.lookUpEditDepotStar.EditValue == null ? null : this.lookUpEditDepotStar.EditValue.ToString();
            this.condition.ProductNameStart = this.btn_ProductNameStart.Text;
            this.condition.ProductNameEnd = this.btn_ProductNameEnd.Text;
            this.condition.ProduceCategoryStart = this.LookUpProductCategoryStart.EditValue == null ? null : this.LookUpProductCategoryStart.EditValue.ToString();
            this.condition.ProductCategoryEnd = this.lookUpProductCategoryEnd.EditValue == null ? null : this.lookUpProductCategoryEnd.EditValue.ToString();
            this.condition.ProductIdStart = this.txt_ProductIdStart.Text;
            this.condition.ProductIdEnd = this.txt_ProductIdEnd.Text;
        }

        private void simpleButtonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_ProductNameStart_Click(object sender, EventArgs e)
        {
            Invoices.ChooseProductForm f = new Book.UI.Invoices.ChooseProductForm();
            if (f.ShowDialog(this) == DialogResult.OK)
                this.btn_ProductNameStart.Text = (f.SelectedItem as Model.Product).ProductName;
        }

        private void btn_ProductNameEnd_Click(object sender, EventArgs e)
        {
            Invoices.ChooseProductForm f = new Book.UI.Invoices.ChooseProductForm();
            if (f.ShowDialog(this) == DialogResult.OK)
                this.btn_ProductNameEnd.Text = (f.SelectedItem as Model.Product).ProductName;
        }
    }
}