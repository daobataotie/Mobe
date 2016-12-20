using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Book.UI.Query
{
    /*----------------------------------------------------------------
// Copyright (C) 2008 - 2010  咸阳飛馳軟件有限公司
//                     版權所有 圍著必究

// 编 码 人:  够波涛             完成时间:2009-6-8
// 修改原因：
// 修 改 人:                          修改时间:
// 修改原因：
// 修 改 人:                          修改时间:
//----------------------------------------------------------------*/
    public partial class Q25_1 : DevExpress.XtraReports.UI.XtraReport
    {
        private BL.InvoiceZZDetailManager detailManager = new Book.BL.InvoiceZZDetailManager();


        /// <summary>
        /// 无参构造，初始化
        /// </summary>
        public Q25_1()
        {
            InitializeComponent();

            this.xrTableCellNote.DataBindings.Add("Text", this.DataSource, Model.InvoiceZZDetail.PROPERTY_INVOICEZZDETAILNOTE);
            this.xrTableCellProductId.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_Id);
            this.xrTableCellProductName.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_ProductName);
            this.xrTableCellQuantity.DataBindings.Add("Text", this.DataSource, Model.InvoiceZZDetail.PROPERTY_INVOICEZZDETAILQUANTITY, "{0:0}");

            //this.xrLabelTotalQuantity.Summary.Running = SummaryRunning.Report;
            //this.xrLabelTotalQuantity.Summary.IgnoreNullValues = true;
            //this.xrLabelTotalQuantity.Summary.Func = SummaryFunc.Sum;
            //this.xrLabelTotalQuantity.Summary.FormatString = "{0:0}";
            //this.xrLabelTotalQuantity.DataBindings.Add("Text", this.DataSource, Model.InvoiceZZDetail.PROPERTY_INVOICEZZDETAILQUANTITY, "{0:0}");


        }
        private Model.InvoiceZZ invoice;

        public Model.InvoiceZZ Invoice
        {
            get { return invoice; }
            set { invoice = value; }
        }

        private void Q25_1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            this.DataSource = this.detailManager.Select("O", this.invoice);
        }

    }
}
