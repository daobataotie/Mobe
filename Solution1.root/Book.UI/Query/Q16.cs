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

// 编 码 人:  够波涛             完成时间:2009-6-5
// 修改原因：
// 修 改 人:                          修改时间:
// 修改原因：
// 修 改 人:                          修改时间:
//----------------------------------------------------------------*/
    public partial class Q16 : BaseReport
    {

        protected BL.InvoiceCGManager invoiceManager = new Book.BL.InvoiceCGManager();
        protected BL.InvoiceCGDetailManager detailManager = new Book.BL.InvoiceCGDetailManager();

        //无参构造
        public Q16()
        {
            InitializeComponent();
        }
        //三参构造
        public Q16(ConditionCO condition)
            : this()
        {

           this.xrLabelReportName.Text = Properties.Resources.JHDetail;
           this.xrLabelDateRange.Text = string.Format(Properties.Resources.DateRange, condition.StartDate.ToString("yyyy/MM/dd"), condition.EndDate.ToString("yyyy/MM/dd"));         
           IList<Model.InvoiceCG>  Details = new BL.InvoiceCGManager().Select(condition.COStartId,condition.COEndId,condition.SupplierStart, condition.SupplierEnd, condition.StartDate, condition.EndDate, condition.ProductStart,condition.ProductEnd,condition.CusXOId,condition.StartJHDate,condition.EndJHDate);
            //this.supplierDetails = supplierDetails;
            //IList<Model.InvoiceCG> cgdetails = new List<Model.InvoiceCG>();
            //IList<Model.InvoiceCG> cgdetail = new List<Model.InvoiceCG>();

            //foreach (Model.Supplier supplier in this.supplierDetails)
            //{
            //    cgdetail = invoiceManager.Select(supplier);
            //    if (cgdetail != null)
            //    {
            //        foreach (Model.InvoiceCG cgInvoice in cgdetail)
            //        {
            //            cgdetails.Add(cgInvoice);
            //        }
            //        cgdetail.Clear();
            //    }
            //}
            if (Details == null || Details.Count <= 0)
            {
                throw new global::Helper.InvalidValueException("無數據");
            }

            this.bindingSource1.DataSource = Details;

            // this.xrLabelDateRange.Text = string.Format(Properties.Resources.DateRange, start.ToString("yyyy/MM/dd"), end.ToString("yyyy/MM/dd"));
            this.xrLabelInvoiceId.DataBindings.Add("Text", this.DataSource, Model.Invoice.PROPERTY_INVOICEID);
            this.xrLabelInvoiceDate.DataBindings.Add("Text", this.DataSource, Model.Invoice.PROPERTY_INVOICEDATE, "{0:yyyy-MM-dd}");
            this.xrLabelSupplier.DataBindings.Add("Text", this.DataSource, "Supplier."+Model.Supplier.PROPERTY_SUPPLIERSHORTNAME);
            //this.xrLabelCOId.DataBindings.Add("Text", this.DataSource,"InvoiceCO."+ Model.Invoice.PROPERTY_INVOICEID);          
            this.xrLabelDepot.DataBindings.Add("Text", this.DataSource, "Depot." + Model.Depot.PRO_DepotName);

            this.xrLabelEmp0.DataBindings.Add("Text", this.DataSource, "Employee0." + Model.Employee.PROPERTY_EMPLOYEENAME);
            this.xrLabelEmp1.DataBindings.Add("Text", this.DataSource, "Employee1." + Model.Employee.PROPERTY_EMPLOYEENAME);
            this.xrLabelCGDate.DataBindings.Add("Text", this.DataSource, Model.InvoiceCG.PRO_InvoiceHisDate, "{0:yyyy-MM-dd}");
            this.xrLabelTaxCaluType.DataBindings.Add("Text", this.DataSource, Model.InvoiceCG.PROPERTY_TaxCaluTypeName);

            //this.xrLabelZongji.DataBindings.Add("Text", this.DataSource, Model.InvoiceCG.PROPERTY_INVOICEZONGJI, "{0:0}");
            this.xrLabelTaxrate.DataBindings.Add("Text", this.DataSource, Model.InvoiceCG.PRO_InvoiceTaxrate, "{0:0.00}");
           this.xrLabelHeji.DataBindings.Add("Text", this.DataSource,"InvoiceHeji", "{0:0}");
           this.xrLabelTax.DataBindings.Add("Text", this.DataSource,"InvoiceTax", "{0:0}");
           this.xrLabelZongji.DataBindings.Add("Text", this.DataSource, "InvoiceTotal", "{0:0}");
            //new Model.InvoiceCG().InvoiceCO.InvoiceTotal
            //xrTableRow2.Table.Rows.Count;
            //    xrTableRow2.Table.Rows
                    

          //  this.xrLabelHeji.Text=this.Detail.s


            this.xrLabelTotalHeJi.Summary.FormatString = "{0:0}";
            this.xrLabelTotalHeJi.Summary.Func = SummaryFunc.Sum;
            this.xrLabelTotalHeJi.Summary.IgnoreNullValues = true;
            this.xrLabelTotalHeJi.Summary.Running = SummaryRunning.Report;
            this.xrLabelTotalHeJi.DataBindings.Add("Text", this.DataSource, "InvoiceHeji", "{0:0}");


            this.xrLabelTotalTax.Summary.FormatString = "{0:0}";
            this.xrLabelTotalTax.Summary.Func = SummaryFunc.Sum;
            this.xrLabelTotalTax.Summary.IgnoreNullValues = true;
            this.xrLabelTotalTax.Summary.Running = SummaryRunning.Report;
            this.xrLabelTotalTax.DataBindings.Add("Text", this.DataSource, "InvoiceTax", "{0:0}");

            this.xrLabelTotalZongJi.Summary.FormatString = "{0:0}";
            this.xrLabelTotalZongJi.Summary.Func = SummaryFunc.Sum;
            this.xrLabelTotalZongJi.Summary.IgnoreNullValues = true;
            this.xrLabelTotalZongJi.Summary.Running = SummaryRunning.Report;
            this.xrLabelTotalZongJi.DataBindings.Add("Text", this.DataSource, "InvoiceTotal", "{0:0}");

            //this.xrLabelTotalZSE.Summary.FormatString = "{0:0}";
            //this.xrLabelTotalZSE.Summary.Func = SummaryFunc.Sum;
            //this.xrLabelTotalZSE.Summary.IgnoreNullValues = true;
            //this.xrLabelTotalZSE.Summary.Running = SummaryRunning.Report;
            //this.xrLabelTotalZSE.DataBindings.Add("Text", this.DataSource, Model.InvoiceCG.PROPERTY_INVOICEZSE, "{0:0}");

            this.xrSubreport1.ReportSource = new Q16_1( condition);
        }
        public Q16(ConditionA condition)
            : this()
        {
            DateTime start = condition.StartDate;
            DateTime end = condition.EndDate;         
            System.Collections.Generic.IList<Model.InvoiceCG> list = this.invoiceManager.Select(start, end);
            if (list == null || list.Count <= 0)
            {
                    throw new global::Helper.InvalidValueException("無數據");
            }
            this.bindingSource1.DataSource = list;
            this.xrLabelDateRange.Text = string.Format(Properties.Resources.DateRange, start.ToString("yyyy/MM/dd"), end.ToString("yyyy/MM/dd"));
            this.xrLabelInvoiceId.DataBindings.Add("Text", this.DataSource, Model.Invoice.PROPERTY_INVOICEID);
            this.xrLabelInvoiceDate.DataBindings.Add("Text", this.DataSource, Model.Invoice.PROPERTY_INVOICEDATE, "{0:yyyy-MM-dd}");
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
            //this.xrLabelTotalZongJi.DataBindings.Add("Text", this.DataSource, Model.InvoiceCG.PROPERTY_INVOICEZONGJI, "{0:0}");

            //this.xrLabelTotalZSE.Summary.FormatString = "{0:0}";
            //this.xrLabelTotalZSE.Summary.Func = SummaryFunc.Sum;
            //this.xrLabelTotalZSE.Summary.IgnoreNullValues = true;
            //this.xrLabelTotalZSE.Summary.Running = SummaryRunning.Report;
            //this.xrLabelTotalZSE.DataBindings.Add("Text", this.DataSource, Model.InvoiceCG.PROPERTY_INVOICEZSE, "{0:0}");

            //this.xrSubreport1.ReportSource = new Q16_1();
        }

        private void xrSubreport1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            Q16_1 reportProducts = this.xrSubreport1.ReportSource as Q16_1;
            reportProducts.Invoice = this.GetCurrentRow() as Model.InvoiceCG;
        }
    }
}
