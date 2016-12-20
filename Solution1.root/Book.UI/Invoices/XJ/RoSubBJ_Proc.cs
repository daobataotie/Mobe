using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;

namespace Book.UI.Invoices.XJ
{
    public partial class RoSubBJ_Proc : DevExpress.XtraReports.UI.XtraReport
    {
        public Model.InvoiceXJ _InvoiceXJ { get; set; }

        public RoSubBJ_Proc()
        {
            InitializeComponent();
            this.BeforePrint += new System.Drawing.Printing.PrintEventHandler(RoSubCB_Proc_BeforePrint);
            if (this._InvoiceXJ != null)
                this.TCGuanXiao_Proc.Text += this._InvoiceXJ.GuanXiaoProc.ToString();

            //Binding
            this.TCInvoiceXJProcessPrice.DataBindings.Add("Text", this.DataSource, Model.InvoiceXJProcess.PROPERTY_INVOICEXJPROCESSPRICE, "{0:0.###}");
            this.TCInvoiceXJProcessQuantity.DataBindings.Add("Text", this.DataSource, Model.InvoiceXJProcess.PROPERTY_InvoiceXJProcessQuantity, "{0:0.###}");
            this.TCInvoiceXJProcessType.DataBindings.Add("Text", this.DataSource, Model.InvoiceXJProcess.PROPERTY_INVOICEXJPROCESSTYPE);
            this.TCProductName.DataBindings.Add("Text", this.DataSource, Model.InvoiceXJProcess.PROPERTY_ProductName);
            this.TCProcessCategory.DataBindings.Add("Text", this.DataSource, "ProcessCategory." + Model.ProcessCategory.PROPERTY_PROCESSCATEGORYNAME);
            this.TCSupplier.DataBindings.Add("Text", this.DataSource, "Supplier." + Model.Supplier.PROPERTY_SUPPLIERSHORTNAME);
            //this.TCInvoiceXJProcessDESC.DataBindings.Add("Text", this.DataSource, Model.InvoiceXJProcess.PROPERTY_INVOICEXJPROCESSDESC);
            this.chkIsMustProc.DataBindings.Add("Text", this.DataSource, Model.InvoiceXJProcess.PROPERTY_ISMUSTPROC);
            this.RTInvoiceXJProcessDesc.DataBindings.Add("Rtf", this.DataSource, Model.InvoiceXJProcess.PROPERTY_INVOICEXJPROCESSDESC);

            this.TCInvoiceXJProcessPriceTotal.Summary.FormatString = "{0:0.####}";
            this.TCInvoiceXJProcessPriceTotal.Summary.Func = SummaryFunc.Sum;
            this.TCInvoiceXJProcessPriceTotal.Summary.IgnoreNullValues = true;
            this.TCInvoiceXJProcessPriceTotal.Summary.Running = SummaryRunning.Report;
            this.TCInvoiceXJProcessPriceTotal.DataBindings.Add("Text", this.DataSource, Model.InvoiceXJProcess.PROPERTY_INVOICEXJPROCESSPRICE, "{0:0}");

            this.TCInvoiceXJProcessQuantityTotal.Summary.FormatString = "{0:0.####}";
            this.TCInvoiceXJProcessQuantityTotal.Summary.Func = SummaryFunc.Sum;
            this.TCInvoiceXJProcessQuantityTotal.Summary.IgnoreNullValues = true;
            this.TCInvoiceXJProcessQuantityTotal.Summary.Running = SummaryRunning.Report;
            this.TCInvoiceXJProcessQuantityTotal.DataBindings.Add("Text", this.DataSource, Model.InvoiceXJProcess.PROPERTY_InvoiceXJProcessQuantity, "{0:0}");

        }

        private void RoSubCB_Proc_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            this.DataSource = (from item in this._InvoiceXJ.DetailsProcess
                               where item.IsChecked == true
                               select item).ToList<Model.InvoiceXJProcess>();

            this.TCGuanXiao_Proc.Text += this._InvoiceXJ.GuanXiaoProc.HasValue ? this._InvoiceXJ.GuanXiaoProc.ToString() : "";
        }
    }
}
