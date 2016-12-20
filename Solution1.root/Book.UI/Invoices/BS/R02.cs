using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Book.UI.Invoices.BS
{
    public partial class R02 : DevExpress.XtraReports.UI.XtraReport
    {
        protected BL.InvoiceBSManager invoiceManager = new Book.BL.InvoiceBSManager();
        public R02()
            : this(null)
        {

        }
        public R02(System.Collections.Generic.IList<Model.InvoiceBS> list)
        {
            InitializeComponent();

            if (null == list)
            {
                list = this.invoiceManager.Select(Helper.InvoiceStatus.Normal);
            }

            this.DataSource = list;

            this.xrTableCellAbstract.DataBindings.Add("Text", this.DataSource, Model.InvoiceBS.PROPERTY_INVOICEABSTRACT);
            this.xrTableCellDepot.DataBindings.Add("Text", this.DataSource, "Depot." + Model.Depot.PRO_DepotName);
            this.xrTableCellEmployee.DataBindings.Add("Text", this.DataSource, "Employee0." + Model.Employee.PROPERTY_EMPLOYEENAME);
            this.xrTableCellInvoiceDate.DataBindings.Add("Text", this.DataSource, Model.InvoiceBS.PROPERTY_INVOICEDATE);
            this.xrTableCellInvoiceId.DataBindings.Add("Text", this.DataSource, Model.InvoiceBS.PROPERTY_INVOICEID);
            this.xrTableCellNote.DataBindings.Add("Text", this.DataSource, Model.InvoiceBS.PROPERTY_INVOICENOTE);

            //this.xrTableCellStatus.DataBindings.Add("Text", this.DataSource, Model.InvoiceBS.PROPERTY_INVOICESTATUS);
        }

    }
}
