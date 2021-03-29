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
    public partial class Q20 : BaseReport
    {
        IList<Model.CustomerProducts> customerProducts = new List<Model.CustomerProducts>();
        IList<Model.Customer> customerDetails;
        protected BL.InvoiceXSManager invoiceManager = new Book.BL.InvoiceXSManager();
        protected BL.InvoiceXSDetailManager detailManager = new Book.BL.InvoiceXSDetailManager();

        //无参构造
        public Q20()
        {
            InitializeComponent();
        }
        //一参构造
        public Q20(IList<Model.Customer> customerDetails)
            : this()
        {
            this.customerDetails = customerDetails;

            IList<Model.InvoiceXS> xsdetails = new List<Model.InvoiceXS>();
            IList<Model.InvoiceXS> xsdetail = new List<Model.InvoiceXS>();

            foreach (Model.Customer customer in this.customerDetails)
            {
                xsdetail = invoiceManager.Select(customer);
                if (xsdetail != null)
                {
                    foreach (Model.InvoiceXS xsInvoice in xsdetail)
                    {
                        xsdetails.Add(xsInvoice);
                    }
                    xsdetail.Clear();
                }
            }

            if (xsdetails == null || xsdetails.Count <= 0)
            {
                    throw new global::Helper.InvalidValueException("無數據");
            }

            this.bindingSource1.DataSource = xsdetails;
            this.xrLabelReportName.Text = Properties.Resources.CHDetail;
            //  this.xrLabelDateRange.Text = string.Format(Properties.Resources.DateRange, start.ToString("yyyy/MM/dd"), end.ToString("yyyy/MM/dd"));
            //this.xrTableCellInvoiceId.DataBindings.Add("Text", this.DataSource, Model.Invoice.PROPERTY_INVOICEID);
            //this.xrTableCellInvoiceDate.DataBindings.Add("Text", this.DataSource, Model.Invoice.PROPERTY_INVOICEDATE, "{0:yyyy-MM-dd}");



            this.xrLabelTotalHeJi.Summary.FormatString = "{0:0}";
            this.xrLabelTotalHeJi.Summary.Func = SummaryFunc.Sum;
            this.xrLabelTotalHeJi.Summary.IgnoreNullValues = true;
            this.xrLabelTotalHeJi.Summary.Running = SummaryRunning.Report;
            this.xrLabelTotalHeJi.DataBindings.Add("Text", this.DataSource, "invoiceXO.InvoiceHeji", "{0:0}");

            this.xrLabelTotalTax.Summary.FormatString = "{0:0}";
            this.xrLabelTotalTax.Summary.Func = SummaryFunc.Sum;
            this.xrLabelTotalTax.Summary.IgnoreNullValues = true;
            this.xrLabelTotalTax.Summary.Running = SummaryRunning.Report;


            this.xrLabelTotalTax.DataBindings.Add("Text", this.DataSource, "InvoiceXO.InvoiceTax", "{0:0}");
            //this.xrTableCellCustomName.DataBindings.Add("Text", this.DataSource, "CustomerName");


            this.xrLabelTotalZongJi.Summary.FormatString = "{0:0}";
            this.xrLabelTotalZongJi.Summary.Func = SummaryFunc.Sum;
            this.xrLabelTotalZongJi.Summary.IgnoreNullValues = true;
            this.xrLabelTotalZongJi.Summary.Running = SummaryRunning.Report;
            this.xrLabelTotalZongJi.DataBindings.Add("Text", this.DataSource, "InvoiceXO.InvoiceTotal", "{0:0}");

            //this.xrLabelTotalZSE.Summary.FormatString = "{0:0}";
            //this.xrLabelTotalZSE.Summary.Func = SummaryFunc.Sum;
            //this.xrLabelTotalZSE.Summary.IgnoreNullValues = true;
            //this.xrLabelTotalZSE.Summary.Running = SummaryRunning.Report;
            //this.xrLabelTotalZSE.DataBindings.Add("Text", this.DataSource, Model.InvoiceXS.PROPERTY_INVOICEZSE, "{0:0}");


            this.xrSubreport1.ReportSource = new Q20_1();
        }

        //三参构造
        public Q20(IList<Model.InvoiceXS> invoiceXS, string ProductStart, string ProductEnd)
            : this()
        {
            // this.customerProducts = customerProducts;

            //IList<Model.InvoiceXS> xsdetails = new List<Model.InvoiceXS>();
            //IList<Model.InvoiceXS> xsdetail = new List<Model.InvoiceXS>();

            //foreach (Model.CustomerProducts customerProduct in this.customerProducts)
            //{
            //    xsdetail = invoiceManager.Select(;
            //    if (xsdetail != null)
            //    {
            //        foreach (Model.InvoiceXS xsInvoice in xsdetail)
            //        {
            //            xsdetails.Add(xsInvoice);
            //        }
            //        xsdetail.Clear();
            //    }
            //}
            //foreach (Model.Customer customer in this.customerDetails)
            //{
            //    xsdetail = invoiceManager.Select(customer);
            //    if (xsdetail != null)
            //    {
            //        foreach (Model.InvoiceXS xsInvoice in xsdetail)
            //        {
            //            if (xsInvoice.InvoiceXO != null)
            //                xsdetails.Add(xsInvoice);
            //            // xsInvoice.InvoiceXO.InvoiceTax;

            //        }

            //    }
            //}

            if (invoiceXS == null || invoiceXS.Count <= 0)
            {
                    throw new global::Helper.InvalidValueException("無數據");
            }

            this.bindingSource1.DataSource = invoiceXS;
            this.xrLabelReportName.Text = Properties.Resources.CHDetail;
            //  this.xrLabelDateRange.Text = string.Format(Properties.Resources.DateRange, start.ToString("yyyy/MM/dd"), end.ToString("yyyy/MM/dd"));
            //this.xrTableCellInvoiceId.DataBindings.Add("Text", this.DataSource, Model.Invoice.PROPERTY_INVOICEID);
            //this.xrTableCellInvoiceDate.DataBindings.Add("Text", this.DataSource, Model.Invoice.PROPERTY_INVOICEDATE, "{0:yyyy-MM-dd}");
            //  new  xsdetail().
            // this.xrLabelZongji.DataBindings.Add("Text", this.DataSource, "InvoiceXO.InvoiceTotal", "{0:0}");
            ////this.xrLabelHeji.DataBindings.Add("Text", this.DataSource, "InvoiceXO.InvoiceHeji", "{0:0}");
            //this.xrLabelTax.DataBindings.Add("Text", this.DataSource, "InvoiceXO.InvoiceTax", "{0:0}");

            this.xrLabelTotalHeJi.Summary.FormatString = "{0:0}";
            this.xrLabelTotalHeJi.Summary.Func = SummaryFunc.Sum;
            this.xrLabelTotalHeJi.Summary.IgnoreNullValues = true;
            this.xrLabelTotalHeJi.Summary.Running = SummaryRunning.Report;
            this.xrLabelTotalHeJi.DataBindings.Add("Text", this.DataSource, "InvoiceXO.InvoiceHeji", "{0:0}");


            this.xrLabelTotalTax.Summary.FormatString = "{0:0}";
            this.xrLabelTotalTax.Summary.Func = SummaryFunc.Sum;
            this.xrLabelTotalTax.Summary.IgnoreNullValues = true;
            this.xrLabelTotalTax.Summary.Running = SummaryRunning.Report;
            this.xrLabelTotalTax.DataBindings.Add("Text", this.DataSource, "InvoiceXO.InvoiceTax", "{0:0}");
            //this.xrTableCellCustomName.DataBindings.Add("Text", this.DataSource, "CustomerName");


            this.xrLabelTotalZongJi.Summary.FormatString = "{0:0}";
            this.xrLabelTotalZongJi.Summary.Func = SummaryFunc.Sum;
            this.xrLabelTotalZongJi.Summary.IgnoreNullValues = true;
            this.xrLabelTotalZongJi.Summary.Running = SummaryRunning.Report;
            this.xrLabelTotalZongJi.DataBindings.Add("Text", this.DataSource, "InvoiceXO.InvoiceTotal", "{0:0}");

            //this.xrLabelTotalZSE.Summary.FormatString = "{0:0}";
            //this.xrLabelTotalZSE.Summary.Func = SummaryFunc.Sum;
            //this.xrLabelTotalZSE.Summary.IgnoreNullValues = true;
            //this.xrLabelTotalZSE.Summary.Running = SummaryRunning.Report;
            //this.xrLabelTotalZSE.DataBindings.Add("Text", this.DataSource, Model.InvoiceXS.PROPERTY_INVOICEZSE, "{0:0}");


            this.xrSubreport1.ReportSource = new Q20_1(ProductStart, ProductEnd);
        }

        public Q20(ConditionA condition)
            : this()
        {
            DateTime start = condition.StartDate;
            DateTime end = condition.EndDate;

            System.Collections.Generic.IList<Model.InvoiceXS> list = this.invoiceManager.Select(start, end);

            if (list == null || list.Count <= 0)
            {
                    throw new global::Helper.InvalidValueException("無數據");
            }

            this.bindingSource1.DataSource = list;
            this.xrLabelReportName.Text = Properties.Resources.CHDetail;
            this.xrLabelDateRange.Text = string.Format(Properties.Resources.DateRange, start.ToString("yyyy/MM/dd"), end.ToString("yyyy/MM/dd"));
            //this.xrTableCellInvoiceId.DataBindings.Add("Text", this.DataSource, Model.Invoice.PROPERTY_INVOICEID);
            //this.xrTableCellInvoiceDate.DataBindings.Add("Text", this.DataSource, Model.Invoice.PROPERTY_INVOICEDATE, "{0:yyyy-MM-dd}");
            //this.xrTableCellCustomName.DataBindings.Add("Text", this.DataSource, "Company." + Model.Company.PROPERTY_COMPANYNAME);

            //this.xrLabelZongji.DataBindings.Add("Text", this.DataSource, Model.InvoiceXS.PROPERTY_INVOICEZONGJI, "{0:0}");
            //this.xrLabelHeji.DataBindings.Add("Text", this.DataSource, Model.InvoiceXS.PROPERTY_INVOICEHEJI, "{0:0}");
            //this.xrLabelTax.DataBindings.Add("Text", this.DataSource, Model.InvoiceXS.PROPERTY_INVOICETAX, "{0:0}");
            //this.xrLabelZS.DataBindings.Add("Text", this.DataSource, Model.InvoiceXS.PROPERTY_INVOICEZSE, "{0:0}");


            this.xrLabelTotalHeJi.Summary.FormatString = "{0:0}";
            this.xrLabelTotalHeJi.Summary.Func = SummaryFunc.Sum;
            this.xrLabelTotalHeJi.Summary.IgnoreNullValues = true;
            this.xrLabelTotalHeJi.Summary.Running = SummaryRunning.Report;
            //this.xrLabelTotalHeJi.DataBindings.Add("Text", this.DataSource, Model.InvoiceXS.PROPERTY_INVOICEHEJI, "{0:0}");

            this.xrLabelTotalTax.Summary.FormatString = "{0:0}";
            this.xrLabelTotalTax.Summary.Func = SummaryFunc.Sum;
            this.xrLabelTotalTax.Summary.IgnoreNullValues = true;
            this.xrLabelTotalTax.Summary.Running = SummaryRunning.Report;
            //this.xrLabelTotalTax.DataBindings.Add("Text", this.DataSource, Model.InvoiceXS.PROPERTY_INVOICETAX, "{0:0}");

            this.xrLabelTotalZongJi.Summary.FormatString = "{0:0}";
            this.xrLabelTotalZongJi.Summary.Func = SummaryFunc.Sum;
            this.xrLabelTotalZongJi.Summary.IgnoreNullValues = true;
            this.xrLabelTotalZongJi.Summary.Running = SummaryRunning.Report;
            //this.xrLabelTotalZongJi.DataBindings.Add("Text", this.DataSource, Model.InvoiceXS.PROPERTY_INVOICEZONGJI, "{0:0}");

            //this.xrLabelTotalZSE.Summary.FormatString = "{0:0}";
            //this.xrLabelTotalZSE.Summary.Func = SummaryFunc.Sum;
            //this.xrLabelTotalZSE.Summary.IgnoreNullValues = true;
            //this.xrLabelTotalZSE.Summary.Running = SummaryRunning.Report;
            //this.xrLabelTotalZSE.DataBindings.Add("Text", this.DataSource, Model.InvoiceXS.PROPERTY_INVOICEZSE, "{0:0}");


            this.xrSubreport1.ReportSource = new Q20_1();
        }

        public Q20(ConditionX condition)
            : this()
        {
            DateTime start = condition.StartDate;
            DateTime end = condition.EndDate;

            this.xrLabelReportName.Text = Properties.Resources.CHDetail;
            this.xrLabelDateRange.Text = string.Format(Properties.Resources.DateRange, start.ToString("yyyy-MM-dd"), end.ToString("yyyy/MM/dd"));
            this.bindingSource1.DataSource = this.invoiceManager.SelectDateRangAndWhere(condition.Customer1, condition.Customer2, condition.StartDate, condition.EndDate, condition.Yjri1, condition.Yjri2, condition.CusXOId, condition.Product, condition.XOId1, condition.XOId2, condition.DepotId, condition.HandBookId);

            if (this.bindingSource1.DataSource == null)
            {
                    throw new global::Helper.InvalidValueException("無數據");
            }
            this.xrLabelInvoiceId.DataBindings.Add("Text", this.DataSource, Model.Invoice.PROPERTY_INVOICEID);
            this.xrLabelInvoiceDate.DataBindings.Add("Text", this.DataSource, Model.Invoice.PROPERTY_INVOICEDATE, "{0:yyyy-MM-dd}");
            this.xrLabelSupplier.DataBindings.Add("Text", this.DataSource, "Customer." + Model.Customer.PRO_CustomerFullName);
            this.xrLabelDepot.DataBindings.Add("Text", this.DataSource, "Depot." + Model.Depot.PRO_DepotName);
            this.xrLabelEmp0.DataBindings.Add("Text", this.DataSource, "Employee0." + Model.Employee.PROPERTY_EMPLOYEENAME);
            this.xrLabelEmpCheck.DataBindings.Add("Text", this.DataSource, "Employee1." + Model.Employee.PROPERTY_EMPLOYEENAME);
            this.xrLabelCGDate.DataBindings.Add("Text", this.DataSource, Model.Invoice.PROPERTY_INVOICEDATE, "{0:yyyy-MM-dd}");
            this.xrLabelChuHuoCustomer.DataBindings.Add("Text", this.DataSource, "XSCustomer." + Model.Customer.PRO_CustomerFullName);
            this.xrLabelTotalHeJi.Summary.FormatString = "{0:0}";
            this.xrLabelTotalHeJi.Summary.Func = SummaryFunc.Sum;
            this.xrLabelTotalHeJi.Summary.IgnoreNullValues = true;
            this.xrLabelTotalHeJi.Summary.Running = SummaryRunning.Report;
            this.xrLabelTotalHeJi.DataBindings.Add("Text", this.DataSource, Model.InvoiceXS.PRO_InvoiceHeji, "{0:0}");

            this.xrLabelTotalTax.Summary.FormatString = "{0:0}";
            this.xrLabelTotalTax.Summary.Func = SummaryFunc.Sum;
            this.xrLabelTotalTax.Summary.IgnoreNullValues = true;
            this.xrLabelTotalTax.Summary.Running = SummaryRunning.Report;
            this.xrLabelTotalTax.DataBindings.Add("Text", this.DataSource, Model.InvoiceXS.PRO_InvoiceTax, "{0:0}");

            this.xrLabelTotalZongJi.Summary.FormatString = "{0:0}";
            this.xrLabelTotalZongJi.Summary.Func = SummaryFunc.Sum;
            this.xrLabelTotalZongJi.Summary.IgnoreNullValues = true;
            this.xrLabelTotalZongJi.Summary.Running = SummaryRunning.Report;
            this.xrLabelTotalZongJi.DataBindings.Add("Text", this.DataSource, Model.InvoiceXS.PRO_InvoiceTotal, "{0:0}");

            this.xrSubreport1.ReportSource = new Q20_1(condition);

        }

        /// <summary>
        /// 报表打印前事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void xrSubreport1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            Q20_1 subReport = this.xrSubreport1.ReportSource as Q20_1;
            subReport.Invoice = this.GetCurrentRow() as Model.InvoiceXS;
        }
    }
}
