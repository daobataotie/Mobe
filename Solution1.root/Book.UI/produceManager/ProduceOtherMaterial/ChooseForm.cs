using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.produceManager.ProduceOtherMaterial
{
    public partial class ChooseForm : DevExpress.XtraEditors.XtraForm
    {
        public ChooseForm()
        {
            InitializeComponent();
            this.nccSupplierStart.Choose = new Settings.BasicData.Supplier.ChooseSupplier();
            this.nccSupplierEnd.Choose = new Settings.BasicData.Supplier.ChooseSupplier();
            this.dateEditStart.EditValue = DateTime.Now.AddMonths(-1);
            this.dateEditEnd.EditValue = DateTime.Now;
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        public ChooseCondition condition = new ChooseCondition();

        private void btnOK_Click(object sender, EventArgs e)
        {
            condition.StartDate = this.dateEditStart.EditValue == null ? DateTime.Now.AddMonths(-1) : this.dateEditStart.DateTime;
            condition.EndDate = this.dateEditEnd.EditValue == null ? DateTime.Now : this.dateEditEnd.DateTime;
            condition.SupplierStartId = this.nccSupplierStart.EditValue == null ? null : (this.nccSupplierStart.EditValue as Model.Supplier).Id;
            condition.SupplierEndId = this.nccSupplierEnd.EditValue == null ? null : (this.nccSupplierEnd.EditValue as Model.Supplier).Id;
            condition.ProductStartId = this.btneProductStart.EditValue == null ? null : (this.btneProductStart.EditValue as Model.Product).Id;
            condition.ProductEndId = this.btenProductEnd.EditValue == null ? null : (this.btenProductEnd.EditValue as Model.Product).Id;
            condition.ProduceOtherCompactStartId = this.txtProduceOtherCompactStart.EditValue == null ? null : this.txtProduceOtherCompactStart.Text;
            condition.ProduceOtherCompactEndId = this.txtProduceOtherCompactEnd.EditValue == null ? null : this.txtProduceOtherCompactEnd.Text;
            condition.InvoiceCusID = this.txt_InvoiceCusID.Text;
            this.DialogResult = DialogResult.OK;
        }

        private void btneProductStart_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            Invoices.ChooseProductForm f = new Book.UI.Invoices.ChooseProductForm();
            if (f.ShowDialog(this) == DialogResult.OK)
                this.btneProductStart.EditValue = f.SelectedItem as Model.Product;

        }

        private void btenProductEnd_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            Invoices.ChooseProductForm f = new Book.UI.Invoices.ChooseProductForm();
            if (f.ShowDialog(this) == DialogResult.OK)
                this.btenProductEnd.EditValue = f.SelectedItem as Model.Product;
        }

        private void nccSupplierStart_EditValueChanged(object sender, EventArgs e)
        {
            this.nccSupplierEnd.EditValue = this.nccSupplierStart.EditValue;
        }

        private void btneProductStart_EditValueChanged(object sender, EventArgs e)
        {
            this.btenProductEnd.EditValue = this.btneProductStart.EditValue;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}