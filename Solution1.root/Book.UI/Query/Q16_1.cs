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

// 编 码 人: 裴盾 够波涛             完成时间:2009-6-4
// 修改原因：
// 修 改 人:                          修改时间:
// 修改原因：
// 修 改 人:                          修改时间:
//----------------------------------------------------------------*/

    /// <summary>
    /// 进货明细表，商品明细子报表
    /// </summary>
    public partial class Q16_1 : DevExpress.XtraReports.UI.XtraReport
    {
        private BL.InvoiceCGDetailManager detailManager = new Book.BL.InvoiceCGDetailManager();
        private BL.InvoiceCGManager invoiceManager = new Book.BL.InvoiceCGManager();

        private Model.InvoiceCG invoice = null;

        private ConditionCO condition;
        public Model.InvoiceCG Invoice
        {
            get
            {
                return this.invoice;
            }
            set
            {
                this.invoice = value;
            }
        }

        //两参构造
        public Q16_1(ConditionCO condition)
        {
            InitializeComponent();
            this.condition = condition;
            this.xrTableCellProductId.DataBindings.Add("Text", this.DataSource, Model.InvoiceCGDetail.PRO_Inumber);
            this.xrTableCellProductName.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_ProductName);
            this.xrTableCellProductGuige.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_ProductSpecification);
            this.xrTableCellProductQuantity.DataBindings.Add("Text", this.DataSource, Model.InvoiceCGDetail.PRO_InvoiceCGDetailQuantity);

            this.xrTablePrice.DataBindings.Add("Text", this.DataSource, Model.InvoiceCGDetail.PRO_InvoiceCGDetailPrice, "{0:0.###}");
            this.xrTableTaxPrice.DataBindings.Add("Text", this.DataSource, Model.InvoiceCGDetail.PRO_InvoiceCGDetailTaxPrice, "{0:0.###}");
            this.xrTableHeJi.DataBindings.Add("Text", this.DataSource, Model.InvoiceCGDetail.PRO_InvoiceCGDetailMoney, "{0:0.###}");
            this.xrTableTax.DataBindings.Add("Text", this.DataSource, Model.InvoiceCGDetail.PRO_InvoiceCGDetailTax, "{0:0.###}");
            this.xrTableTaxTotal.DataBindings.Add("Text", this.DataSource, Model.InvoiceCGDetail.PRO_InvoiceCGDetailTaxMoney, "{0:0.###}");
            this.xrTableOrderQuantity.DataBindings.Add("Text", this.DataSource, "InvoiceCODetail." + Model.InvoiceCODetail.PRO_OrderQuantity, "{0:0.###}");
            this.xrTableNoCGQuantity.DataBindings.Add("Text", this.DataSource, "InvoiceCODetail." + Model.InvoiceCODetail.PRO_NoArrivalQuantity, "{0:0.###}");
            this.xrTableInQuantity.DataBindings.Add("Text", this.DataSource, Model.InvoiceCGDetail.PRO_InvoiceCGDetaiInQuantity, "{0:0.###}");
            this.xrTableTransferQuantity.DataBindings.Add("Text", this.DataSource, Model.InvoiceCGDetail.PRO_ProduceTransferQuantity, "{0:0.###}");
            this.xrTableZR.DataBindings.Add("Text", this.DataSource, Model.InvoiceCGDetail.PRO_InvoiceAllowance, "{0:0.###}");
            this.xrTableUnit.DataBindings.Add("Text", this.DataSource, Model.InvoiceCGDetail.PRO_InvoiceProductUnit);
            this.xrCheckBoxZS.DataBindings.Add("Checked", this.DataSource, Model.InvoiceCGDetail.PRO_Donatetowards);
            this.xrTableCellCOId.DataBindings.Add("Text", this.DataSource, Model.InvoiceCGDetail.PRO_InvoiceCOId);
            this.xrTableWorkHouse.DataBindings.Add("Text", this.DataSource, "WorkHouse." + Model.WorkHouse.PROPERTY_WORKHOUSENAME);
           
           
        }

        private void Q16_1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

            this.bindingSource1.DataSource = this.detailManager.Select(invoice, condition.ProductStart, condition.ProductEnd);
        }

    }
}
