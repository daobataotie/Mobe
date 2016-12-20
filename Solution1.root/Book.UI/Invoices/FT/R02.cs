using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Book.UI.Invoices.FT
{
    public partial class R02 : DevExpress.XtraReports.UI.XtraReport
    {
        protected BL.InvoiceFTManager invoiceManager = new Book.BL.InvoiceFTManager();

        public R02()
           : this(null)
        {
            
        }
        public R02(System.Collections.Generic.IList<Model.InvoiceFT> list)
        {
            InitializeComponent();

            if (list == null)
            {
                list = this.invoiceManager.Select(Helper.InvoiceStatus.Normal);
            }

            this.DataSource = list;
            
            this.xrTableCellAbstract.DataBindings.Add("Text", this.DataSource, Model.InvoiceFT.PROPERTY_INVOICEABSTRACT);
            this.xrTableCellAccount1.DataBindings.Add("Text", this.DataSource, "Account1." + Model.Account.PROPERTY_ACCOUNTNAME);
            this.xrTableCellAccount0.DataBindings.Add("Text", this.DataSource, "Account2." + Model.Account.PROPERTY_ACCOUNTNAME);
            this.xrTableCellEmployee.DataBindings.Add("Text", this.DataSource, "Employee0." + Model.Employee.PROPERTY_EMPLOYEENAME);
            this.xrTableCellInvoiceDate.DataBindings.Add("Text", this.DataSource, Model.InvoiceFT.PROPERTY_INVOICEDATE);
            this.xrTableCellInvoiceId.DataBindings.Add("Text", this.DataSource, Model.InvoiceFT.PROPERTY_INVOICEID);
            this.xrTableCellNote.DataBindings.Add("Text", this.DataSource, Model.InvoiceFT.PROPERTY_INVOICENOTE);
            this.xrTableCellTotal.DataBindings.Add("Text", this.DataSource, Model.InvoiceFT.PROPERTY_INVOICETOTAL);
        }
    }
}
