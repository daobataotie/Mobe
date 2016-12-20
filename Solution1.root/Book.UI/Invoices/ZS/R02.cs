using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Book.UI.Invoices.ZS
{
    public partial class R02 : DevExpress.XtraReports.UI.XtraReport
    {
        protected BL.InvoiceZSManager invoiceManager = new Book.BL.InvoiceZSManager();

        public R02()
            : this(null)
        {

        }
        public R02(System.Collections.Generic.IList<Model.InvoiceZS> list)
        {
            InitializeComponent();

            if (list == null)
            {
                list = this.invoiceManager.Select(Helper.InvoiceStatus.Normal);
            }

            this.DataSource = list;

            this.xrTableCellNote.DataBindings.Add("Text", this.DataSource, Model.InvoiceZS.PROPERTY_INVOICEABSTRACT);
            //this.xrTableCellCompany.DataBindings.Add("Text", this.DataSource, "Company." + Model.Company.PROPERTY_COMPANYNAME0);
            this.xrTableCellEmployee.DataBindings.Add("Text", this.DataSource, "Employee0." + Model.Employee.PROPERTY_EMPLOYEENAME);
            this.xrTableCellInvoiceDate.DataBindings.Add("Text", this.DataSource, Model.InvoiceZS.PROPERTY_INVOICEDATE);
            this.xrTableCellInvoiceId.DataBindings.Add("Text", this.DataSource, Model.InvoiceZS.PROPERTY_INVOICEID);
            this.xrTableCellNote.DataBindings.Add("Text", this.DataSource, Model.InvoiceZS.PROPERTY_INVOICENOTE);
            
        }
    }
}
