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

// 编 码 人:  够波涛             完成时间:2009-6-12
// 修改原因：
// 修 改 人:                          修改时间:
// 修改原因：
// 修 改 人:                          修改时间:
//----------------------------------------------------------------*/
    public partial class Q28_1 : DevExpress.XtraReports.UI.XtraReport
    {
        private BL.InvoiceCGDetailManager cgDetailManager = new Book.BL.InvoiceCGDetailManager();

        private Model.InvoiceCG invoice;

        //索引器
        public Model.InvoiceCG Invoice
        {
            get { return invoice; }
            set { invoice = value; }
        }


        /// <summary>
        /// 无参构造
        /// </summary>
        public Q28_1()
        {
            InitializeComponent();

            this.xrTableCellProductId.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_Id);
            this.xrTableCellProductName.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_ProductName);
            this.xrTableCellQuantity.DataBindings.Add("Text", this.DataSource, Model.InvoiceCGDetail.PRO_InvoiceCGDetailQuantity);
            this.xrTableCellUnit.DataBindings.Add("Text", this.DataSource, Model.InvoiceCGDetail.PRO_InvoiceProductUnit);
            //this.xrTableCellUnitPrice.DataBindings.Add("Text", this.DataSource, Model.InvoiceCGDetail.PROPERTY_INVOICECGDETAILPRICE, "{0:0}");
            //this.xrTableCell1TotalMoney.DataBindings.Add("Text", this.DataSource, Model.InvoiceCGDetail.PROPERTY_INVOICECGDETAILMONEY0, "{0:0}");

            this.xrTableCellTotalHeji.Summary.Running = SummaryRunning.Report;
            this.xrTableCellTotalHeji.Summary.IgnoreNullValues = true;
            this.xrTableCellTotalHeji.Summary.Func = SummaryFunc.Sum;
            this.xrTableCellTotalHeji.Summary.FormatString = "{0:0}";
            //this.xrTableCellTotalHeji.DataBindings.Add("Text", this.DataSource, Model.InvoiceCGDetail.PROPERTY_INVOICECGDETAILMONEY0, "{0:0}");
        
        }

        private void Q28_1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            this.bindingSource1.DataSource = this.cgDetailManager.Select(this.invoice);
        }

    }
}
