using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Book.UI.Invoices.BY
{
    public partial class R02 : DevExpress.XtraReports.UI.XtraReport
    {
        protected BL.InvoiceBYManager invoiceManager = new Book.BL.InvoiceBYManager();

        public R02()
            : this(null)
        {

        }

        public R02(System.Collections.Generic.IList<Model.InvoiceBY> list)
        {
            InitializeComponent();

            if (list == null)
            {
                list = this.invoiceManager.Select(Helper.InvoiceStatus.Normal);
            }

            this.DataSource = list;

            this.xrTableCellAbstract.DataBindings.Add("Text", this.DataSource, Model.InvoiceBY.PROPERTY_INVOICEABSTRACT);
            this.xrTableCellDepot.DataBindings.Add("Text", this.DataSource, "Depot." + Model.Depot.PRO_DepotName);
            this.xrTableCellEmployee.DataBindings.Add("Text", this.DataSource, "Employee0." + Model.Employee.PROPERTY_EMPLOYEENAME);
            this.xrTableCellInvoiceDate.DataBindings.Add("Text", this.DataSource, Model.InvoiceBY.PROPERTY_INVOICEDATE);
            this.xrTableCellInvoiceId.DataBindings.Add("Text", this.DataSource, Model.InvoiceBY.PROPERTY_INVOICEID);
            this.xrTableCellNote.DataBindings.Add("Text", this.DataSource, Model.InvoiceBY.PROPERTY_INVOICENOTE);

            //this.xrTableCellStatus.DataBindings.Add("Text", this.DataSource, Model.InvoiceBY.PROPERTY_INVOICESTATUS);
        }

    }
}
