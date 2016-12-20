using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.Invoices.XS
{
    public partial class XSDetailForm : DevExpress.XtraEditors.XtraForm
    {
        private Model.InvoiceXO invoicexo;
        private BL.InvoiceXSManager xsManager = new Book.BL.InvoiceXSManager();
        private BL.InvoiceXSDetailManager xsDetailManager = new Book.BL.InvoiceXSDetailManager();
        private BL.InvoiceXODetailManager xoDetailManager = new Book.BL.InvoiceXODetailManager();
        public XSDetailForm()
        {
            InitializeComponent();
        }
        public XSDetailForm(Model.InvoiceXO invoicexo)
            : this()
        {
            this.invoicexo = invoicexo;
        }

        private void XSDetailForm_Load(object sender, EventArgs e)
        {
            this.bindingSourceXSID.DataSource = xsManager.Select(invoicexo);
            this.bindingSourceXODetail.DataSource = xoDetailManager.Select(this.invoicexo,false);
        }
        private void bindingSourceXSID_CurrentChanged(object sender, EventArgs e)
        {
            Model.InvoiceXS invoicexs = this.bindingSourceXSID.Current as Model.InvoiceXS;
            if (invoicexs == null)
                return;
            this.dateEditInvoiceDate.Text = invoicexs.InvoiceDate.ToString();
            this.textEditEmployee0.Text = invoicexs.Employee0.EmployeeName;
            this.textEditDepot.Text = invoicexs.Depot.DepotName;
            this.textEditInvoiceId.Text = invoicexo.InvoiceId;
            this.textEditCustomerInvoiceXOID.Text = invoicexo.CustomerInvoiceXOId;
            this.textEditCustomer.Text = invoicexs.Customer.CustomerShortName;
            this.bindingSourceXSList.DataSource = this.xsDetailManager.Select(invoicexs);
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {

            EditForm f = new EditForm(invoicexo);
            this.Close();
            f.ShowDialog();
           // f.MdiParent = new MainForm();

        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            XSForm f = new XSForm();
            this.Close();
            f.Show();
            f.MdiParent = this.MdiParent;
        }

        private void listBoxControlXSID_DoubleClick(object sender, EventArgs e)
        {
            Model.InvoiceXS invoicexs = this.bindingSourceXSID.Current as Model.InvoiceXS;
            if (invoicexs == null)
                return;
            EditForm f = new EditForm(invoicexs.InvoiceId);
            if (f.ShowDialog(this) != DialogResult.OK) return;
        }

        private void gridView1_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.ListSourceRowIndex < 0) return;
            IList<Model.InvoiceXSDetail> details = this.bindingSourceXSList.DataSource as IList<Model.InvoiceXSDetail>;
            if (details == null || details.Count < 1) return;
            //Model.CustomerProducts detail = details[e.ListSourceRowIndex].PrimaryKey;
            Model.Product detail = details[e.ListSourceRowIndex].Product;
            switch (e.Column.Name)
            {
                case "gridColumn1":
                    if (detail == null) return;
                    e.DisplayText = detail == null ? "" : detail.Id;
                    break;
                case "gridColumn10":
                    e.DisplayText = detail == null ? "" : detail.CustomerProductName;
                    break;
            }
        }

        private void gridView2_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.ListSourceRowIndex < 0) return;
            IList<Model.InvoiceXODetail> details = this.bindingSourceXODetail.DataSource as IList<Model.InvoiceXODetail>;
            if (details == null || details.Count < 1) return;
            //Model.CustomerProducts detail = details[e.ListSourceRowIndex].PrimaryKey;
            Model.Product detail = details[e.ListSourceRowIndex].Product;
            switch (e.Column.Name)
            {
                case "gridColumn5":
                    e.DisplayText = detail == null ? "" : detail.Id;
                    break;
                case "gridColumn11":
                    e.DisplayText = detail == null ? "" : detail.CustomerProductName;
                    break;
            }
        }
    }
}