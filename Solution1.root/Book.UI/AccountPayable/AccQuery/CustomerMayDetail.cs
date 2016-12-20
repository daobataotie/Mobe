using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
namespace Book.UI.AccountPayable.AccQuery
{
    public partial class CustomerMayDetail : Query.BaseReport
    {
        BL.AcInvoiceXOBillManager xobillmanager = new Book.BL.AcInvoiceXOBillManager();

        public CustomerMayDetail()
        {
            InitializeComponent();
        }

        public CustomerMayDetail(CustomerMayChoose conn)
            : this()
        {
            this.xrLabelReportName.Text = "‘ªÊÕÙ~¿îÃ÷¼šÙ~";
            DataSet ds = this.xobillmanager.SelectMayShou(conn.Customer1, conn.Customer2, conn.Employee1, conn.Employee2, conn.StartDate, conn.EndDate);
            if (ds == null || ds.Tables.Count == 0)
                throw new global::Helper.InvalidValueException();

            this.DataSource = ds.Tables[0];
            this.xrLabelDateRange.Text = conn.StartDate.ToString("yyyy-MM-dd") + " ~ " + conn.EndDate.ToString("yyyy-MM-dd");
            // this.xrTableEmp.DataBindings.Add("Text", this.DataSource, "CusomerName");
            this.xrTableEmp.DataBindings.Add("Text", this.DataSource, "Employee1Name");

            this.xrTableSourceInvoiceDate.DataBindings.Add("Text", this.DataSource, "InvoiceDate", "{0:yyyy-MM-dd}");
            this.xrTableInvoiceIds.DataBindings.Add("Text", this.DataSource, "InvoiceId");
            this.xrTableMoney.DataBindings.Add("Text", this.DataSource, "Total",  global::Helper.DateTimeParse.GetFormatA(BL.V.SetDataFormat.XSZJXiao.Value));
            this.xrTableHasMoneys.DataBindings.Add("Text", this.DataSource, "HasMoney",  global::Helper.DateTimeParse.GetFormatA(BL.V.SetDataFormat.XSZJXiao.Value));
            this.xrTableNoMoneys.DataBindings.Add("Text", this.DataSource, "NoHeXiao",  global::Helper.DateTimeParse.GetFormatA(BL.V.SetDataFormat.XSZJXiao.Value));

            this.xrTableSourceNames.DataBindings.Add("Text", this.DataSource, "SourceName");
            this.xrTableCustomer.DataBindings.Add("Text", this.DataSource, "CusomerName");
        }
    }
}
