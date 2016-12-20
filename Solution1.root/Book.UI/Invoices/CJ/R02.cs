using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Book.UI.Invoices.CJ
{
    public partial class R02 : DevExpress.XtraReports.UI.XtraReport
    {
        protected BL.InvoiceCJManager invoiceManager = new Book.BL.InvoiceCJManager();
        private R02()
            : this(null)
        {
        }

        public R02(System.Collections.Generic.IList<Model.InvoiceCJ> list)
        {
            InitializeComponent();

            if (list == null)
            {
                list = this.invoiceManager.Select(Helper.InvoiceStatus.Normal);
            }

            this.DataSource = list;
            
            this.xrTableCellCompany.DataBindings.Add("Text", this.DataSource, "Supplier." + Model.Supplier.PROPERTY_SUPPLIERSHORTNAME);
            this.xrTableCellEmployee.DataBindings.Add("Text", this.DataSource, "Employee0." + Model.Employee.PROPERTY_EMPLOYEENAME);
            this.xrTableCellInvoiceDate.DataBindings.Add("Text", this.DataSource, Model.InvoiceCJ.PROPERTY_INVOICEDATE);
            this.xrTableCellInvoiceId.DataBindings.Add("Text", this.DataSource, Model.InvoiceCJ.PROPERTY_INVOICEID);
            this.xrTableCellInvoiceTotal.DataBindings.Add("Text", this.DataSource, Model.InvoiceCJ.PRO_InvoiceTotal);
            this.xrTableCellNote.DataBindings.Add("Text", this.DataSource, Model.InvoiceCJ.PROPERTY_INVOICENOTE);
        }
    }
}
