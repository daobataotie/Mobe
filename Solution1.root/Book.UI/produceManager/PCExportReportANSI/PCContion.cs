using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.produceManager.PCExportReportANSI
{
    public partial class PCContion : DevExpress.XtraEditors.XtraForm
    {
        public PCContion()
        {
            InitializeComponent();
            this.DateEdit_Start.EditValue = DateTime.Now.AddDays(-7);
            this.DateEdit_End.EditValue = DateTime.Now;
        }

        public Condition Condition { get; set; }

        private void btn_Ok_Click(object sender, EventArgs e)
        {
            if (this.DateEdit_Start.EditValue == null)
                this.Condition.Date_Start = DateTime.Now.Date.AddDays(-7);
            else
                this.Condition.Date_Start = this.DateEdit_Start.DateTime.Date;

            if (this.DateEdit_End.EditValue == null)
                this.Condition.Date_End = DateTime.Now.Date.AddDays(1);
            else
                this.Condition.Date_End = this.DateEdit_End.DateTime.Date.AddDays(1);

            this.Condition.CusInvoiceXOId = this.txtInvoiceCusId.Text;
            this.Condition.Product = this.btnEdit_Product.EditValue as Model.Product;

            this.DialogResult = DialogResult.OK;
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnEdit_Product_Click(object sender, EventArgs e)
        {
            Invoices.ChooseProductForm f = new Invoices.ChooseProductForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                this.btnEdit_Product.EditValue = f.SelectedItem as Model.Product;
            }
            f.Dispose();
            GC.Collect();
        }
    }
}