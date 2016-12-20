using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
namespace Book.UI.AccountPayable.AccQuery
{
    public partial class SupplierMayDetail : Query.BaseReport
    {
        BL.AcInvoiceCOBillManager cCOBillManager = new Book.BL.AcInvoiceCOBillManager();

        public SupplierMayDetail()
        {
            InitializeComponent();
        }

        public SupplierMayDetail(SupplierMayChoose conn)
            : this()
        {
            this.xrLabelReportName.Text = "‘ª¸¶Ù~¿îÃ÷¼šÙ~";
            DataSet ds = this.cCOBillManager.SelectMayShou(conn.Supplier1, conn.Supplier2, conn.Employee1, conn.Employee2, conn.StartDate, conn.EndDate);
            if (ds == null || ds.Tables[0].Rows.Count == 0)
                throw new Helper.InvalidValueException("Ó›ä›žé¿Õ");
            this.DataSource = ds.Tables[0];
            this.xrLabelDateRange.Text = conn.StartDate.ToString("yyyy-MM-dd") + "-" + conn.EndDate.ToString("yyyy-MM-dd");
            //this.xrTableEmp.DataBindings.Add("Text", this.DataSource, "CusomerName");
            this.xrTableEmp.DataBindings.Add("Text", this.DataSource, "Employee1Name");
            this.xrTableSourceInvoiceDate.DataBindings.Add("Text", this.DataSource, "InvoiceDate", "{0:yyyy-MM-dd}");
            this.xrTableInvoiceIds.DataBindings.Add("Text", this.DataSource, "InvoiceId");
            this.xrTableMoney.DataBindings.Add("Text", this.DataSource, "Total",  global::Helper.DateTimeParse.GetFormatA(BL.V.SetDataFormat.XSZJXiao.Value));
            this.xrTableHasMoneys.DataBindings.Add("Text", this.DataSource, "HasMoney",  global::Helper.DateTimeParse.GetFormatA(BL.V.SetDataFormat.XSZJXiao.Value));
            this.xrTableNoMoneys.DataBindings.Add("Text", this.DataSource, "NoHeXiao",  global::Helper.DateTimeParse.GetFormatA(BL.V.SetDataFormat.XSZJXiao.Value));
            this.xrTableSourceNames.DataBindings.Add("Text", this.DataSource, "SourceName");
            this.xrTableSupplier.DataBindings.Add("Text", this.DataSource, "SupplierName");


            this.lbl_heji.Summary.FormatString =  global::Helper.DateTimeParse.GetFormat(BL.V.SetDataFormat.XSZJXiao.Value);
            this.lbl_heji.Summary.Func = SummaryFunc.Sum;
            this.lbl_heji.Summary.IgnoreNullValues = true;
            this.lbl_heji.Summary.Running = SummaryRunning.Report;
            this.lbl_heji.DataBindings.Add("Text", this.DataSource, "JinE");

            this.lbl_shuie.Summary.FormatString =  global::Helper.DateTimeParse.GetFormat(BL.V.SetDataFormat.XSZJXiao.Value);
            this.lbl_shuie.Summary.Func = SummaryFunc.Sum;
            this.lbl_shuie.Summary.IgnoreNullValues = true;
            this.lbl_shuie.Summary.Running = SummaryRunning.Report;
            this.lbl_shuie.DataBindings.Add("Text", this.DataSource, "ShuiE");

            this.lbl_zongji.Summary.FormatString =  global::Helper.DateTimeParse.GetFormat(BL.V.SetDataFormat.XSZJXiao.Value);
            this.lbl_zongji.Summary.Func = SummaryFunc.Sum;
            this.lbl_zongji.Summary.IgnoreNullValues = true;
            this.lbl_zongji.Summary.Running = SummaryRunning.Report;
            this.lbl_zongji.DataBindings.Add("Text", this.DataSource, "Total");
        }
    }
}
