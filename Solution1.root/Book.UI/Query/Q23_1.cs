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

// 编 码 人:  够波涛             完成时间:2009-6-16
// 修改原因：
// 修 改 人:                          修改时间:
// 修改原因：
// 修 改 人:                          修改时间:
//----------------------------------------------------------------*/

    public partial class Q23_1 : DevExpress.XtraReports.UI.XtraReport
    {
        private BL.InvoiceXTDetailManager detailManager = new Book.BL.InvoiceXTDetailManager();
        private BL.InvoiceXTManager invoiceManager = new Book.BL.InvoiceXTManager();

        private Model.InvoiceXT invoice = null;

        public Model.InvoiceXT Invoice
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


        /// <summary>
        /// 无参构造
        /// </summary>
        public Q23_1()
        {
            InitializeComponent();

        
            this.xrTableCellProductName.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_ProductName);
            this.xrTableCellQuantity.DataBindings.Add("Text", this.DataSource, Model.InvoiceXTDetail.PRO_InvoiceXTDetailQuantity);
            this.xrTableCellUnit.DataBindings.Add("Text", this.DataSource, Model.InvoiceXTDetail.PRO_InvoiceProductUnit);
            this.xrTableCellGuiGe.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_ProductSpecification);
            this.xrTableCellPrice.DataBindings.Add("Text", this.DataSource, Model.InvoiceXTDetail.PRO_InvoiceXTDetailPrice, "{0:0.000}");
            this.xrTableCellTotalMoney.DataBindings.Add("Text", this.DataSource, Model.InvoiceXTDetail.PRO_InvoiceXTDetailMoney0, "{0:0.000}");

        }

        private void Q23_1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            this.bindingSource1.DataSource = this.detailManager.Select(invoice);
        }

    }
}
