using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Book.UI.Invoices.HC
{
    public partial class ChooseInvoiceJrForm : Form
    {

        #region
        private BL.InvoiceJRManager _invoicejrManager = new Book.BL.InvoiceJRManager();
        private BL.InvoiceJRDetailManager _invoicejrDetailManager = new Book.BL.InvoiceJRDetailManager();
        #endregion

        public ChooseInvoiceJrForm()
        {
            InitializeComponent();
            this.newChooseContorl_Supper.Choose = new Settings.BasicData.Supplier.ChooseSupplier();
            this.startDate.DateTime = DateTime.Now.AddMonths(-1);
            this.endDate.DateTime = DateTime.Now.AddDays(1).AddSeconds(-1);
            this.mBind();
        }

        //绑定数据
        public void mBind()
        {
            this.bindingSourceInvoiceJr.DataSource = this._invoicejrManager.Select(this.startDate.DateTime, this.endDate.DateTime, this.newChooseContorl_Supper.EditValue as Model.Supplier);
            if (this.bindingSourceInvoiceJr.Current != null)
            {
                Model.InvoiceJR jr = this.bindingSourceInvoiceJr.Current as Model.InvoiceJR;
                IList<Model.InvoiceJRDetail> jrDetail = this._invoicejrDetailManager.Select(jr);
                (this.bindingSourceInvoiceJr.Current as Model.InvoiceJR).Details = jrDetail;
                this.DefaultSelectALL((this.bindingSourceInvoiceJr.Current as Model.InvoiceJR).Details);
                this.bindingSourceInvoiceJrDetail.DataSource = (this.bindingSourceInvoiceJr.Current as Model.InvoiceJR).Details;
            }
            else
            {
                this.bindingSourceInvoiceJrDetail.DataSource = null;
            }
        }

        public void DefaultSelectALL(IList<Model.InvoiceJRDetail> details)
        {
            foreach (Model.InvoiceJRDetail d in details)
            {
                d.IsChecked = true;
            }
        }

        private void simpleButton_SearCh_Click(object sender, EventArgs e)
        {
            if (this.startDate.EditValue == null || this.endDate.EditValue == null)
            {
                MessageBox.Show(Properties.Resources.DateIsNull);
                return;
            }
            this.mBind();
        }

        private void simpleButton_Sure_Click(object sender, EventArgs e)
        {
            EditForm.details.Clear();
            IList<Model.InvoiceJRDetail> templist = this.bindingSourceInvoiceJrDetail.DataSource as List<Model.InvoiceJRDetail>;
            foreach (Model.InvoiceJRDetail item in templist)
            {
                if (item.IsChecked == null) continue;
                if (item.IsChecked.Value)
                    EditForm.details.Add(item);
            }
            this.DialogResult = DialogResult.OK;
        }

        private void gridView2_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.ListSourceRowIndex < 0)
                return;
            IList<Model.InvoiceJRDetail> invoiceJrDetails = this.bindingSourceInvoiceJrDetail.DataSource as IList<Model.InvoiceJRDetail>;
            Model.Product p = invoiceJrDetails[e.ListSourceRowIndex].Product;
            if (invoiceJrDetails == null || invoiceJrDetails.Count <= 0)
            {
                switch (e.Column.Name)
                {
                    case "gridColumn8":
                        e.DisplayText = p.Id;
                        break;
                }
            }
        }

        private void simpleButton_Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bindingSourceInvoiceJr_CurrentChanged(object sender, EventArgs e)
        {
            if (this.bindingSourceInvoiceJr.Current != null)
            {
                if ((this.bindingSourceInvoiceJr.Current as Model.InvoiceJR).Details == null || (this.bindingSourceInvoiceJr.Current as Model.InvoiceJR).Details.Count == 0)
                {
                    Model.InvoiceJR jr = this.bindingSourceInvoiceJr.Current as Model.InvoiceJR;
                    IList<Model.InvoiceJRDetail> jrdetail = this._invoicejrDetailManager.Select(jr);
                    (this.bindingSourceInvoiceJr.Current as Model.InvoiceJR).Details = jrdetail;
                }
                this.DefaultSelectALL((this.bindingSourceInvoiceJr.Current as Model.InvoiceJR).Details);
                this.bindingSourceInvoiceJrDetail.DataSource = (this.bindingSourceInvoiceJr.Current as Model.InvoiceJR).Details;
            }
        }
    }
}
