using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Book.UI.Query
{
    public partial class Q32 : BaseReport
    {
        private BL.InvoiceDetail01Manager invoiceDetail01Manager = new Book.BL.InvoiceDetail01Manager();

        public Q32(ConditionA condition)
        {
            InitializeComponent();

            this.xrLabelDateRange.Text = string.Format(Properties.Resources.DateRange,condition.StartDate.ToString("yyyy-MM-dd"),condition.EndDate.ToString("yyyy-MM-dd"));
            this.xrLabelReportName.Text = Properties.Resources.YWYSJYB;

            System.Collections.Generic.IList<Model.InvoiceDetail01> list = this.invoiceDetail01Manager.Select(condition.StartDate,condition.EndDate);

            if(list == null || list.Count<=0)
                throw new Helper.InvalidValueException();

            this.bindingSource1.DataSource = list;

            this.xrTableCellEmployeeName.DataBindings.Add("Text", this.DataSource, "Employee." + Model.Employee.PROPERTY_EMPLOYEENAME);
            //this.xrTableCellCompanyName.DataBindings.Add("Text", this.DataSource, "Company." + Model.Company.PROPERTY_COMPANYNAME1);
            this.xrTableCellHeji.DataBindings.Add("Text", this.DataSource, Model.InvoiceDetail01.PROPERTY_INVOICEHEJI, "{0:0}");
            this.xrTableCellTax.DataBindings.Add("Text", this.DataSource, Model.InvoiceDetail01.PROPERTY_INVOICETAX, "{0:0}");
            this.xrTableCellYingShou.DataBindings.Add("Text", this.DataSource, Model.InvoiceDetail01.PROPERTY_INVOICEOWED, "{0:0}");
            this.xrTableCellYiShou.DataBindings.Add("Text", this.DataSource, Model.InvoiceDetail01.PROPERTY_YISHOU, "{0:0}");

            this.xrTableCellToalTax.Summary.FormatString="{0:0}";
            this.xrTableCellTotalHeJi.Summary.FormatString="{0:0}";
            this.xrTableCellTotalYingShou.Summary.FormatString="{0:0}";
            this.xrTableCellTotalYiShou.Summary.FormatString="{0:0}";

            this.xrTableCellToalTax.Summary.Func = SummaryFunc.Sum;
            this.xrTableCellTotalHeJi.Summary.Func = SummaryFunc.Sum;
            this.xrTableCellTotalYingShou.Summary.Func = SummaryFunc.Sum;
            this.xrTableCellTotalYiShou.Summary.Func = SummaryFunc.Sum;

            this.xrTableCellToalTax.Summary.IgnoreNullValues = true;
            this.xrTableCellTotalHeJi.Summary.IgnoreNullValues = true;
            this.xrTableCellTotalYingShou.Summary.IgnoreNullValues = true;
            this.xrTableCellTotalYiShou.Summary.IgnoreNullValues = true;

            this.xrTableCellToalTax.Summary.Running = SummaryRunning.Report;
            this.xrTableCellTotalHeJi.Summary.Running = SummaryRunning.Report;
            this.xrTableCellTotalYingShou.Summary.Running = SummaryRunning.Report;
            this.xrTableCellTotalYiShou.Summary.Running = SummaryRunning.Report;

            this.xrTableCellToalTax.DataBindings.Add("Text", this.DataSource, Model.InvoiceDetail01.PROPERTY_INVOICETAX, "{0:0}");
            this.xrTableCellTotalHeJi.DataBindings.Add("Text", this.DataSource, Model.InvoiceDetail01.PROPERTY_INVOICEHEJI, "{0:0}");
            this.xrTableCellTotalYingShou.DataBindings.Add("Text", this.DataSource, Model.InvoiceDetail01.PROPERTY_INVOICEOWED, "{0:0}");
            this.xrTableCellTotalYiShou.DataBindings.Add("Text", this.DataSource, Model.InvoiceDetail01.PROPERTY_YISHOU, "{0:0}");
        }
    }
}
