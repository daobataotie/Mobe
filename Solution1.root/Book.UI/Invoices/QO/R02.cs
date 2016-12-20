using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Book.UI.Invoices.QO
{
    public partial class R02 : DevExpress.XtraReports.UI.XtraReport
    {
        protected BL.InvoiceQOManager invoiceManager = new Book.BL.InvoiceQOManager();

        public R02()
            : this(null)
        {

        }
        public R02(System.Collections.Generic.IList<Model.InvoiceQO> list)
        {
            InitializeComponent();

            if (list == null)
            {
                list = this.invoiceManager.Select(Helper.InvoiceStatus.Normal);
            }

            this.DataSource = list;

            this.xrTableCellAbstract.DataBindings.Add("Text", this.DataSource, Model.InvoiceQO.PROPERTY_INVOICEABSTRACT);
            this.xrTableCellAccount.DataBindings.Add("Text", this.DataSource, "Account." + Model.Account.PROPERTY_ACCOUNTNAME);
            this.xrTableCellEmployee.DataBindings.Add("Text", this.DataSource, "Employee0." + Model.Employee.PROPERTY_EMPLOYEENAME);
            this.xrTableCellInvoiceDate.DataBindings.Add("Text", this.DataSource, Model.InvoiceQO.PROPERTY_INVOICEDATE);
            this.xrTableCellInvoiceId.DataBindings.Add("Text", this.DataSource, Model.InvoiceQO.PROPERTY_INVOICEID);
            this.xrTableCellNote.DataBindings.Add("Text", this.DataSource, Model.InvoiceQO.PROPERTY_INVOICENOTE);
            this.xrTableCellTotal.DataBindings.Add("Text", this.DataSource, Model.InvoiceQO.PROPERTY_INVOICETOTAL);
            
        }
    }
}
