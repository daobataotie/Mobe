using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Book.UI.Invoices.XT
{
    public partial class R02 : DevExpress.XtraReports.UI.XtraReport
    {
        protected BL.InvoiceXTManager invoiceManager = new Book.BL.InvoiceXTManager();

        public R02()
            : this(null)
        {

        }

        public R02(System.Collections.Generic.IList<Model.InvoiceXT> list)
        {
            InitializeComponent();

            if (list == null)
            {
                list = this.invoiceManager.Select(Helper.InvoiceStatus.Normal);
            }

            this.DataSource = list;

            //this.xrTableCellAbstract.DataBindings.Add("Text", this.DataSource, Model.InvoiceXT.PROPERTY_INVOICEABSTRACT);
            //this.xrTableCellCompany.DataBindings.Add("Text", this.DataSource, "Company." + Model.Company.PROPERTY_COMPANYNAME0);
            this.xrTableCellDepot.DataBindings.Add("Text", this.DataSource, "Depot." + Model.Depot.PRO_DepotName);
            this.xrTableCellEmployee.DataBindings.Add("Text", this.DataSource, "Employee0." + Model.Employee.PROPERTY_EMPLOYEENAME);
            this.xrTableCellInvoiceDate.DataBindings.Add("Text", this.DataSource, Model.InvoiceXT.PROPERTY_INVOICEDATE);
            this.xrTableCellInvoiceId.DataBindings.Add("Text", this.DataSource, Model.InvoiceXT.PROPERTY_INVOICEID);
            this.xrTableCellInvoiceOwed.DataBindings.Add("Text", this.DataSource, Model.InvoiceXT.PROPERTY_INVOICEOWED);            
            this.xrTableCellNote.DataBindings.Add("Text", this.DataSource, Model.InvoiceXT.PROPERTY_INVOICENOTE);
            this.xrTableCellPayTimeLimit.DataBindings.Add("Text", this.DataSource, Model.InvoiceXT.PROPERTY_INVOICEPAYTIMELIMIT);
            this.xrTableCellTotal.DataBindings.Add("Text", this.DataSource, Model.InvoiceXT.PROPERTY_INVOICEHEJI);

            //this.xrTableCellStatus.DataBindings.Add("Text", this.DataSource, Model.InvoiceXT.PROPERTY_INVOICESTATUS);
        }

    }
}
