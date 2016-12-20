using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Book.UI.Invoices.CG
{
    public partial class R02 : DevExpress.XtraReports.UI.XtraReport
    {
        protected BL.InvoiceCGManager invoiceManager = new Book.BL.InvoiceCGManager();

        public R02()
            : this(null)
        {

        }

        public R02(System.Collections.Generic.IList<Model.InvoiceCG> list)
        {
            InitializeComponent();

            if (list == null)
            {
                list = this.invoiceManager.Select(Helper.InvoiceStatus.Normal);
            }

            this.DataSource = list;
            this.xrTableCellCompany.DataBindings.Add("Text", this.DataSource, "Supplier." + Model.Supplier.PROPERTY_SUPPLIERSHORTNAME);
            this.xrTableCellDepot.DataBindings.Add("Text", this.DataSource, "Depot" + Model.Depot.PRO_DepotName);
            this.xrTableCellEmployee.DataBindings.Add("Text", this.DataSource, "Employee0." + Model.Employee.PROPERTY_EMPLOYEENAME);
            this.xrTableCellInvoiceDate.DataBindings.Add("Text", this.DataSource, Model.InvoiceCG.PROPERTY_INVOICEDATE);
            this.xrTableCellInvoiceId.DataBindings.Add("Text", this.DataSource, Model.InvoiceCG.PROPERTY_INVOICEID);            
            this.xrTableCellNote.DataBindings.Add("Text", this.DataSource, Model.InvoiceCG.PROPERTY_INVOICENOTE);
        }

    }
}
