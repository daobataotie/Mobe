using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Book.UI.Invoices.ZZ
{
    public partial class R02 : DevExpress.XtraReports.UI.XtraReport
    {
        protected BL.InvoiceZZManager invoiceManager = new Book.BL.InvoiceZZManager();

        public R02()
            : this(null)
        {

        }
        public R02(System.Collections.Generic.IList<Model.InvoiceZZ> list)
        {
            InitializeComponent();

            if (list == null)
            {
                list = this.invoiceManager.Select(Helper.InvoiceStatus.Normal);
            }

            this.DataSource = list;

            //this.xrTableCellAbstract.DataBindings.Add("Text", this.DataSource, Model.InvoiceZZ.PROPERTY_INVOICEABSTRACT);
            this.xrTableCellDepot0.DataBindings.Add("Text", this.DataSource, "Depot1." + Model.Depot.PRO_DepotName);
            this.xrTableCellDepot1.DataBindings.Add("Text", this.DataSource, "Depot2." + Model.Depot.PRO_DepotName);
            this.xrTableCellEmployee.DataBindings.Add("Text", this.DataSource, "Employee0." + Model.Employee.PROPERTY_EMPLOYEENAME);
            this.xrTableCellInvoiceDate.DataBindings.Add("Text", this.DataSource, Model.InvoiceZZ.PROPERTY_INVOICEDATE);
            this.xrTableCellInvoiceId.DataBindings.Add("Text", this.DataSource, Model.InvoiceZZ.PROPERTY_INVOICEID);
            this.xrTableCellNote.DataBindings.Add("Text", this.DataSource, Model.InvoiceZZ.PROPERTY_INVOICENOTE);
            
        }
    }
}
