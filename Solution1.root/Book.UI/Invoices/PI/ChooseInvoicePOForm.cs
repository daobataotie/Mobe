using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Book.UI.Invoices.PI
{
    public partial class ChooseInvoicePOForm : Form
    {

        #region
        private BL.InvoicePODetailManager _invoicepoDetailManager = new Book.BL.InvoicePODetailManager();
        private BL.InvoicePOManager _invoicePOManager = new Book.BL.InvoicePOManager();
        #endregion
        public ChooseInvoicePOForm()
        {
            InitializeComponent();
            this.bindingSourceInvoicePO.DataSource = this._invoicePOManager.Select();
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            EditForm.invoicepo = this.bindingSourceInvoicePO.Current as Model.InvoicePO;
            if (EditForm.invoicepo == null) return;
            this.bindingSourceInvoicePODetail.DataSource = this._invoicepoDetailManager.Select(EditForm.invoicepo);
        }

        private void gridView1_Click(object sender, EventArgs e)
        {
            //Model.InvoicePO temp = this.bindingSourceInvoicePO.Current as Model.InvoicePO;
            //if (temp == null) return;
            //this.bindingSourceInvoicePODetail.DataSource = this._invoicepoDetailManager.Select(temp);
        }

        private void simpleButton_Sure_Click(object sender, EventArgs e)
        {
            if (EditForm.invoicepo != null)
                EditForm.invoicepo.Details.Clear();
            IList<Model.InvoicePODetail> list = this.bindingSourceInvoicePODetail.DataSource as List<Model.InvoicePODetail>;
            foreach (Model.InvoicePODetail item in list)
            {
                if (item.IsChecked != null)
                    if (item.IsChecked.Value)
                        EditForm.invoicepo.Details.Add(item);
            }
            this.DialogResult = DialogResult.OK;
        }

        private void simpleButton_Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void gridView2_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.ListSourceRowIndex < 0) return;
            IList<Model.InvoicePODetail> details = this.bindingSourceInvoicePODetail.DataSource as IList<Model.InvoicePODetail>;
            if (details == null || details.Count < 1) return;
            Model.Product p = details[e.ListSourceRowIndex].Product;
            switch (e.Column.Name)
            {
                case "gridColumn7":
                    e.DisplayText = p==null ? "" : p.Id;
                    break;
                case "gridColumn8":
                    e.DisplayText = p == null ? "" : p.ToString();
                    break;
                case "gridColumn9":
                    e.DisplayText = details[e.ListSourceRowIndex].DepotPosition == null ? "" : details[e.ListSourceRowIndex].DepotPosition.ToString();
                    break;
            }
        }
    }
}
