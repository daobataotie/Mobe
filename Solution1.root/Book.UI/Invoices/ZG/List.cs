using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.Invoices.ZG
{
    public partial class List : DevExpress.XtraEditors.XtraForm
    {
        BL.CompanyManager companyManager = new Book.BL.CompanyManager();
        BL.InvoiceZGManager _invoiceZGManager = new Book.BL.InvoiceZGManager();
        BL.InvoiceZGDetailManager _invoiceZGDetailManager = new Book.BL.InvoiceZGDetailManager();

        public List()
        {
            InitializeComponent();

            this.StartPosition = FormStartPosition.CenterParent;
            this.bindingSourceCompany.DataSource = this.companyManager.Select();
            this.newChooseXOCustomer.Choose = new Settings.BasicData.Customs.ChooseCustoms();
            this.date_End.DateTime = DateTime.Now;
            this.date_Start.DateTime = DateTime.Now.Date.AddDays(-15);
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            this.bindingSourceInvoiceZGDetail.DataSource = this._invoiceZGDetailManager.SelectByInvoiceZGId((this.bindingSourceInvoiceZG.Current as Model.InvoiceZG).InvoiceZGId);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            this.bindingSourceInvoiceZG.DataSource = this._invoiceZGManager.SelectInvoiceZG(this.date_Start.EditValue == null ? DateTime.Now.Date.AddDays(-15) : this.date_Start.DateTime, this.date_End.EditValue == null ? DateTime.Now : this.date_End.DateTime, this.newChooseXOCustomer.EditValue as Model.Customer, this.txt_InvoiceId.EditValue == null ? null : this.txt_InvoiceId.Text, this.lookUpEditCompany.EditValue == null ? null : this.lookUpEditCompany.EditValue.ToString());
        }
    }
}