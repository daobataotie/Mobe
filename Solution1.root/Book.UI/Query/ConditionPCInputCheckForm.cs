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
    public partial class ConditionPCInputCheckForm : DevExpress.XtraEditors.XtraForm
    {
        public ConditionPCInputCheckForm()
        {
            InitializeComponent();
            this.date_Start.EditValue = DateTime.Now.AddMonths(-1);
            this.date_End.EditValue = DateTime.Now;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.nccSupplier.Choose = new Settings.BasicData.Supplier.ChooseSupplier();

        }
        public ConditionPCInputCheck condition { get; set; }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            if (condition == null)
                condition = new ConditionPCInputCheck();

            condition.StartDate = this.date_Start.EditValue == null ? global::Helper.DateTimeParse.NullDate : this.date_Start.DateTime;
            condition.EndDate = this.date_End.EditValue == null ? global::Helper.DateTimeParse.EndDate : this.date_End.DateTime;
            condition.ProductId = (this.btn_Product.EditValue as Model.Product) == null ? null : (this.btn_Product.EditValue as Model.Product).ProductId;
            condition.TestProductId = (this.btn_TestProduct.EditValue as Model.Product) == null ? null : (this.btn_TestProduct.EditValue as Model.Product).ProductId;
            condition.SupplierId = (this.nccSupplier.EditValue as Model.Supplier) == null ? null : (this.nccSupplier.EditValue as Model.Supplier).SupplierId;
            condition.LotNumber = this.txt_LotNumber.Text;
            condition.IsClosed = this.che_IsClosed.Checked;

            this.DialogResult = DialogResult.OK;
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void btn_Product_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            Invoices.ChooseProductForm f = new Book.UI.Invoices.ChooseProductForm();
            if (f.ShowDialog(this) == DialogResult.OK)
                this.btn_Product.EditValue = f.SelectedItem;
        }

        private void btn_TestProduct_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            Invoices.ChooseProductForm f = new Book.UI.Invoices.ChooseProductForm();
            if (f.ShowDialog(this) == DialogResult.OK)
                this.btn_TestProduct.EditValue = f.SelectedItem;
        }
    }
}