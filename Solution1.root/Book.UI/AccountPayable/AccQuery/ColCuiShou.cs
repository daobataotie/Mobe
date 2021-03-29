using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
namespace Book.UI.AccountPayable.AccQuery
{
    public partial class ColCuiShou:Book.UI.Query.BaseReport
    {
        BL.AcInvoiceXOBillManager xobillmanager = new Book.BL.AcInvoiceXOBillManager();
        public ColCuiShou()
        {           
            InitializeComponent();
        }
        public ColCuiShou(ChooseColCuiShou conn):this()
        {
            DataSet ds = this.xobillmanager.SelectCuiShou(conn.Customer1, conn.Customer2, conn.Employee1, conn.Employee2, conn.YJDate);
             if (ds == null || ds.Tables.Count == 0)
                 throw new global::Helper.InvalidValueException("無數據");
            this.DataSource = ds.Tables[0];
            this.xrLabelReportName.Text = "應收賬款超期跟催表";
            
            this.xrTableYSDate.DataBindings.Add("Text", this.DataSource, "YSDate", "{0:yyyy-MM-dd}");
            this.xrTableDays.DataBindings.Add("Text", this.DataSource, "CQDays");
            this.xrTableInvoiceDate.DataBindings.Add("Text", this.DataSource, "InvoiceDate", "{0:yyyy-MM-dd}");
            this.xrTableInvoiceId.DataBindings.Add("Text", this.DataSource, global::Helper.DateTimeParse.GetFormatA(BL.V.SetDataFormat.XSZJXiao.Value));
            this.xrTableMayMoney.DataBindings.Add("Text", this.DataSource, "Total", global::Helper.DateTimeParse.GetFormatA(BL.V.SetDataFormat.XSZJXiao.Value));
            this.xrTableHasMoney.DataBindings.Add("Text", this.DataSource, "HasMoney", global::Helper.DateTimeParse.GetFormatA(BL.V.SetDataFormat.XSZJXiao.Value)); ;
            this.xrTableNoMoney.DataBindings.Add("Text", this.DataSource, "NoHeXiao", global::Helper.DateTimeParse.GetFormatA(BL.V.SetDataFormat.XSZJXiao.Value));
           // this.xrTableZheRang1.DataBindings.Add("Text", this.DataSource, "ZheRang", "{0:0}");
            this.xrTableSourceName.DataBindings.Add("Text", this.DataSource, "SourceName");
                 this.xrTableCustomer.DataBindings.Add("Text", this.DataSource, "CusomerName");

            
        }

    }
}
