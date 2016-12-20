using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;
namespace Book.UI.Query
{
    /*----------------------------------------------------------------
// Copyright (C) 2008 - 2010  咸阳飛馳軟件有限公司
//                     版權所有 圍著必究

// 编 码 人:  够波涛             完成时间:2009-6-10
// 修改原因：
// 修 改 人:                          修改时间:
// 修改原因：
// 修 改 人:                          修改时间:
//----------------------------------------------------------------*/
    public partial class ROInvoiceCO : BaseReport
    {

        protected BL.InvoiceCOManager invoiceManager = new Book.BL.InvoiceCOManager();
        protected BL.InvoiceCODetailManager detailManager = new Book.BL.InvoiceCODetailManager();

        //无参构造
        public ROInvoiceCO()
        {
            InitializeComponent();
        }

        public ROInvoiceCO(ConditionCO condition)
            : this()
        {
            DateTime start = condition.StartDate;
            DateTime end = condition.EndDate;

            this.xrLabelReportName.Text = Properties.Resources.InvoiceCODetail;

            this.xrLabelDateRange.Text = string.Format(Properties.Resources.DateRange, start.ToString("yyyy-MM-dd"), end.ToString("yyyy/MM/dd"));
            IList<Model.InvoiceCO> Details = this.invoiceManager.SelectDateRangAndWhere(condition.COStartId, condition.COEndId, condition.SupplierStart, condition.SupplierEnd, condition.StartDate, condition.EndDate, condition.ProductStart, condition.ProductEnd, condition.CusXOId, condition.StartJHDate, condition.EndJHDate, condition.InvoiceFlag.Value);

            if (Details == null || Details.Count <= 0)
            {
                throw new global::Helper.InvalidValueException();
            }

            this.bindingSource1.DataSource = Details;

            this.lblInvoiceId.DataBindings.Add("Text", this.DataSource, Model.Invoice.PROPERTY_INVOICEID);
            this.lblInvoiceDate.DataBindings.Add("Text", this.DataSource, Model.Invoice.PROPERTY_INVOICEDATE, "{0:yyyy-MM-dd}");
            this.lblCustomer.DataBindings.Add("Text", this.DataSource, "Customer." + Model.Customer.PRO_CustomerFullName);
            // this.xrLabelEmp0.DataBindings.Add("Text", this.DataSource, "Employee0." + Model.Employee.PROPERTY_EMPLOYEENAME);
            this.lblEmployee.DataBindings.Add("Text", this.DataSource, "Employee0." + Model.Employee.PROPERTY_EMPLOYEENAME);
            this.lblYJRQ.DataBindings.Add("Text", this.DataSource, Model.InvoiceXO.PRO_InvoiceYjrq, "{0:yyyy-MM-dd}");
            this.lblSupplier.DataBindings.Add("Text", this.DataSource, "Supplier." + Model.Supplier.PROPERTY_SUPPLIERSHORTNAME);
            this.lblInvoiceHeji.DataBindings.Add("Text", this.DataSource, Model.InvoiceCO.PRO_InvoiceHeji, "{0:0}");
            this.lblInvoiceTax.DataBindings.Add("Text", this.DataSource, Model.InvoiceCO.PRO_InvoiceTax, "{0:0}");
            this.lblInvoiceTotal.DataBindings.Add("Text", this.DataSource, Model.InvoiceCO.PRO_InvoiceTotal, "{0:0}");
            this.lblCustomerInvoiceXOID.DataBindings.Add("Text", this.DataSource, Model.InvoiceCO.PRO_InvoiceCustomXOId);

            this.xrLabelTotalHeJi.Summary.FormatString = "{0:0}";
            this.xrLabelTotalHeJi.Summary.Func = SummaryFunc.Sum;
            this.xrLabelTotalHeJi.Summary.IgnoreNullValues = true;
            this.xrLabelTotalHeJi.Summary.Running = SummaryRunning.Report;
            this.xrLabelTotalHeJi.DataBindings.Add("Text", this.DataSource, Model.InvoiceCO.PRO_InvoiceHeji, "{0:0}");

            this.xrLabelTotalTax.Summary.FormatString = "{0:0}";
            this.xrLabelTotalTax.Summary.Func = SummaryFunc.Sum;
            this.xrLabelTotalTax.Summary.IgnoreNullValues = true;
            this.xrLabelTotalTax.Summary.Running = SummaryRunning.Report;
            this.xrLabelTotalTax.DataBindings.Add("Text", this.DataSource, Model.InvoiceCO.PRO_InvoiceTax, "{0:0}");

            this.xrLabelTotalZongJi.Summary.FormatString = "{0:0}";
            this.xrLabelTotalZongJi.Summary.Func = SummaryFunc.Sum;
            this.xrLabelTotalZongJi.Summary.IgnoreNullValues = true;
            this.xrLabelTotalZongJi.Summary.Running = SummaryRunning.Report;
            this.xrLabelTotalZongJi.DataBindings.Add("Text", this.DataSource, Model.InvoiceCO.PRO_InvoiceTotal, "{0:0}");

            this.xrSubreport1.ReportSource = new ROInvoiceCO_1(condition);
        }

        //子报表打印前事件
        private void xrSubreport1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            ROInvoiceCO_1 subReport = this.xrSubreport1.ReportSource as ROInvoiceCO_1;
            subReport.Invoice = this.GetCurrentRow() as Model.InvoiceCO;
        }
    }
}
