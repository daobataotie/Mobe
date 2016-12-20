using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Book.UI.Query
{
    public partial class Q18 : BaseReport
    {
        private BL.InvoiceCGManager invoiceManager = new Book.BL.InvoiceCGManager();

        //一参构造
        public Q18(ConditionA condition)
        {            
            DateTime start = condition.StartDate;
            DateTime end = condition.EndDate;
            InitializeComponent();

            this.xrLabelReportName.Text = Properties.Resources.JHZL; 
            this.xrLabelDateRange.Text = string.Format(Properties.Resources.DateRange, start.ToString("yyyy/MM/dd"), end.ToString("yyyy/MM/dd"));

            System.Collections.Generic.IList<Model.InvoiceCG> list = this.invoiceManager.Select1(start, end);

            if (list == null || list.Count <= 0)
            {
                throw new global::Helper.InvalidValueException();
            }

            this.bindingSource1.DataSource = list;

            this.xrTableCellKind.DataBindings.Add("Text", this.DataSource, Model.InvoiceCG.PROPERTY_KIND);
            this.xrTableCellInvoiceId.DataBindings.Add("Text", this.DataSource, Model.Invoice.PROPERTY_INVOICEID);
            this.xrTableCellInvoiceDate.DataBindings.Add("Text", this.DataSource, Model.Invoice.PROPERTY_INVOICEDATE, "{0:yyyy-MM-dd}");
            //this.xrTableCellCustomName.DataBindings.Add("Text", this.DataSource, "Company." + Model.Company.PROPERTY_COMPANYNAME1);

            //this.xrLabelZongji.DataBindings.Add("Text", this.DataSource, Model.InvoiceCG.PROPERTY_INVOICEZONGJI, "{0:0}");
            //this.xrLabelHeji.DataBindings.Add("Text", this.DataSource, Model.InvoiceCG.PROPERTY_INVOICEHEJI, "{0:0}");
            //this.xrLabelTax.DataBindings.Add("Text", this.DataSource, Model.InvoiceCG.PROPERTY_INVOICETAX, "{0:0}");
            //this.xrLabelZS.DataBindings.Add("Text", this.DataSource, Model.InvoiceCG.PROPERTY_INVOICEZSE, "{0:0}");


            this.xrLabelTotalHeJi.Summary.FormatString = "{0:0}";
            this.xrLabelTotalHeJi.Summary.Func = SummaryFunc.Sum;
            this.xrLabelTotalHeJi.Summary.IgnoreNullValues = true;
            this.xrLabelTotalHeJi.Summary.Running = SummaryRunning.Report;
            //this.xrLabelTotalHeJi.DataBindings.Add("Text", this.DataSource, Model.InvoiceCG.PROPERTY_INVOICEHEJI, "{0:0}");

            this.xrLabelTotalTax.Summary.FormatString = "{0:0}";
            this.xrLabelTotalTax.Summary.Func = SummaryFunc.Sum;
            this.xrLabelTotalTax.Summary.IgnoreNullValues = true;
            this.xrLabelTotalTax.Summary.Running = SummaryRunning.Report;
            //this.xrLabelTotalTax.DataBindings.Add("Text", this.DataSource, Model.InvoiceCG.PROPERTY_INVOICETAX, "{0:0}");
            
            this.xrLabelTotalZongJi.Summary.FormatString = "{0:0}";
            this.xrLabelTotalZongJi.Summary.Func = SummaryFunc.Sum;
            this.xrLabelTotalZongJi.Summary.IgnoreNullValues = true;
            this.xrLabelTotalZongJi.Summary.Running = SummaryRunning.Report;
            //this.xrLabelTotalZongJi.DataBindings.Add("Text", this.DataSource, Model.InvoiceCG.PROPERTY_D:\易达ERP\Solution1.root\Solution1\Book.UI\Query\Q18_2.csINVOICEZONGJI, "{0:0}");

            this.xrLabelTotalZSE.Summary.FormatString = "{0:0}";
            this.xrLabelTotalZSE.Summary.Func = SummaryFunc.Sum;
            this.xrLabelTotalZSE.Summary.IgnoreNullValues = true;
            this.xrLabelTotalZSE.Summary.Running = SummaryRunning.Report;
            //this.xrLabelTotalZSE.DataBindings.Add("Text", this.DataSource, Model.InvoiceCG.PROPERTY_INVOICEZSE, "{0:0}");

        }

        private void xrSubreport1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            Model.InvoiceCG invce = this.GetCurrentRow() as Model.InvoiceCG;
            switch (invce.Kind)
            {
                case "採購":
                    Q18_1 q18_1_1 = new Q18_1();
                    this.xrSubreport1.ReportSource = q18_1_1;
                    q18_1_1.Invoice = invce;
                    break;
                case "采购":
                    Q18_1 q18_1_2 = new Q18_1();
                    this.xrSubreport1.ReportSource = q18_1_2;
                    q18_1_2.Invoice = invce;
                    break;
                case "采退":
                    Q18_2 q18_2_1 = new Q18_2();
                    this.xrSubreport1.ReportSource = q18_2_1;
                    q18_2_1.Invoice = invce;
                    break;
                case "採退":
                    Q18_2 q18_2_2 = new Q18_2();
                    this.xrSubreport1.ReportSource = q18_2_2;
                    q18_2_2.Invoice = invce;
                    break;
                default:
                    break;
            }
            //Q18_1 subReport = this.xrSubreport1.ReportSource as Q18_1;
            //subReport.Invoice = 

        }
    }
}