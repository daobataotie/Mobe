using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Book.UI.Invoices.HR
{
    public partial class ChooseInvoiceJcDetaliForm : Form
    {

        private BL.InvoiceJCManager _invoicejcManager = new Book.BL.InvoiceJCManager();
        private BL.InvoiceJCDetailManager _invoicejcDetailManager = new Book.BL.InvoiceJCDetailManager();
        public ChooseInvoiceJcDetaliForm()
        {
            InitializeComponent();

            this.bindingSourceinvoiceJc.DataSource = this._invoicejcManager.Select();
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            EditForm.invoicejc = this.bindingSourceinvoiceJc.Current as Model.InvoiceJC;
            if (EditForm.invoicejc == null) return;
            this.bindingSourceInvoiceJcDetail.DataSource = this._invoicejcDetailManager.Select(EditForm.invoicejc);
        }

        private void simpleButton_Sure_Click(object sender, EventArgs e)
        {
            EditForm.invoicejc.Details = new List<Model.InvoiceJCDetail>();
            IList<Model.InvoiceJCDetail> temp = this.bindingSourceInvoiceJcDetail.DataSource as IList<Model.InvoiceJCDetail>;
            foreach (Model.InvoiceJCDetail item in temp)
            {
                if (item.IsChecked != null)
                    if (item.IsChecked.Value)
                        EditForm.invoicejc.Details.Add(item);
            }
            this.DialogResult = DialogResult.OK;
        }

        private void simpleButton_Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void gridView2_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.ListSourceRowIndex < 0)
                return;

            IList<Model.InvoiceJCDetail> invoiceJcDetails = this.bindingSourceInvoiceJcDetail.DataSource as IList<Model.InvoiceJCDetail>;

            if (invoiceJcDetails == null || invoiceJcDetails.Count <= 0)
                return;

            Model.Product p = invoiceJcDetails[e.ListSourceRowIndex].Product;

            switch (e.Column.Name)
            {
                case "gridColumn7":
                    e.DisplayText = p.Id;
                    break;
                case "gridColumn10":
                    e.DisplayText = p.ProduceUnit.CnName;
                    break;
                case "gridColumn9":
                    e.DisplayText = invoiceJcDetails[e.ListSourceRowIndex].DepotPosition.ToString();
                    break;
            }
        }
    }
}
