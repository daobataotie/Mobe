using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Book.UI.Invoices.QK
{
    public partial class R02 : DevExpress.XtraReports.UI.XtraReport
    {
        protected BL.InvoiceQKManager invoiceManager = new Book.BL.InvoiceQKManager();

        public R02()
            : this(null)
        {

        }
        public R02(System.Collections.Generic.IList<Model.InvoiceQK> list)
        {
            InitializeComponent();

            if (list == null)
            {
                list = this.invoiceManager.Select(Helper.InvoiceStatus.Normal);
            }

            this.DataSource = list;

            this.xrTableCellInvoiceId.DataBindings.Add("Text", this.DataSource, Model.InvoiceQK.PROPERTY_INVOICEID);
            this.xrTableCellCompany.DataBindings.Add("Text", this.DataSource, "Company");
            this.xrTableCellEmployee.DataBindings.Add("Text", this.DataSource, "Employee0." + Model.Employee.PROPERTY_EMPLOYEENAME);
            this.xrTableCellInvoiceDate.DataBindings.Add("Text", this.DataSource, Model.InvoiceQK.PROPERTY_INVOICEDATE);
            this.xrTableCellAbstract.DataBindings.Add("Text", this.DataSource, Model.InvoiceQK.PROPERTY_INVOICEABSTRACT);
            this.xrTableCellTotal0.DataBindings.Add("Text", this.DataSource, Model.InvoiceQK.PROPERTY_INVOICETOTAL0);
            this.xrTableCellTotal1.DataBindings.Add("Text", this.DataSource, Model.InvoiceQK.PROPERTY_INVOICETOTAL0);

        }
    }
}
