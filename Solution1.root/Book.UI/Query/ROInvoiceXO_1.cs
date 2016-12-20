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

// 编 码 人:  够波涛             完成时间:2009-6-11
// 修改原因：
// 修 改 人:                          修改时间:
// 修改原因：
// 修 改 人:                          修改时间:
//----------------------------------------------------------------*/
    public partial class ROInvoiceXO_1 : DevExpress.XtraReports.UI.XtraReport
    {
        private BL.InvoiceXODetailManager detailManager = new Book.BL.InvoiceXODetailManager();
        private Model.InvoiceXO invoice = null;
        private Query.ConditionX condition;
        public ROInvoiceXO_1()
        {
            InitializeComponent();
            //this.xrTableCellProductId.DataBindings.Add("Text", this.DataSource, Model.InvoiceXSDetail.PRO_PrimaryKeyId);
            //this.xrTableCellProductName.DataBindings.Add("Text", this.DataSource, "PrimaryKey.CustomerProductName");
            //this.xrTableCellQuantity.DataBindings.Add("Text", this.DataSource, Model.InvoiceXSDetail.PRO_InvoiceXSDetailQuantity);
            //this.xrTableCellUnit.DataBindings.Add("Text", this.DataSource, Model.InvoiceXSDetail.PRO_InvoiceProductUnit);
            //this.xrTableCellGuiGe.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_ProductSpecification);
            //this.xrTableCellPrice.DataBindings.Add("Text", this.DataSource, "InvoiceXODetail.InvoiceXODetailPrice", "{0:0}");

            //  this.xrTableCellPrice.Text=string.IsNullOrEmpty(this.xrTableCellPrice.Text) ? "0" : this.xrTableCellPrice.Text;
            //   this.xrTableCellQuantity.Text = string.IsNullOrEmpty(this.xrTableCellQuantity.Text) ? "0" : this.xrTableCellQuantity.Text;
            //this.xrTableCellTotalMoney.DataBindings.Add("Text", this.DataSource, "xiaoji", "{0:0}");
            // this.xrTableCellTotalMoney.Text = (Convert.ToInt32(this.xrTableCellPrice.Text) * Convert.ToInt32(this.xrTableCellQuantity.Text)).ToString();

        }
        public ROInvoiceXO_1(ConditionX cond)
        {
            InitializeComponent();
            this.condition = cond;
            this.xrTableCellProductId.DataBindings.Add("Text", this.DataSource, Model.InvoiceXODetail.PRO_Inumber);
            this.xrTableCellProductName.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_ProductName);
            this.xrTableCusPro.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_CustomerProductName);
            this.xrTablePrice.DataBindings.Add("Text", this.DataSource, Model.InvoiceXODetail.PRO_InvoiceXODetailPrice, "{0:0.###}");
            this.xrTableHeJi.DataBindings.Add("Text", this.DataSource, Model.InvoiceXODetail.PRO_InvoiceXODetailMoney, "{0:0.###}");
            this.xrTableOrderQuantity.DataBindings.Add("Text", this.DataSource, Model.InvoiceXODetail.PRO_InvoiceXODetailQuantity, "{0:0.###}");
            this.xrTableNoXGQuantity.DataBindings.Add("Text", this.DataSource, Model.InvoiceXODetail.PRO_InvoiceXODetailQuantity0, "{0:0.###}");
            this.xrTableZR.DataBindings.Add("Text", this.DataSource, Model.InvoiceXODetail.PRO_InvoiceAllowance, "{0:0.###}");
            this.xrTableUnit.DataBindings.Add("Text", this.DataSource, Model.InvoiceXODetail.PRO_InvoiceProductUnit);
            this.xrCheckBoxZS.DataBindings.Add("Checked", this.DataSource, Model.InvoiceXODetail.PRO_Islargess);
            //this.xrTableZS.DataBindings.Add("Text", this.DataSource, Model.InvoiceXODetail.PRO_Islargess);
            //this.xrTableTransferQuantity.DataBindings.Add("Text", this.DataSource, Model.InvoiceCGDetail.PRO_ProduceTransferQuantity, "{0:0.###}");
            //this.xrTableTax.DataBindings.Add("Text", this.DataSource, Model.InvoiceXODetail.PRO_InvoiceXODetailTax, "{0:0.###}");
            //this.xrTableTaxTotal.DataBindings.Add("Text", this.DataSource, Model.InvoiceXODetail.PRO_InvoiceXOSDetailTaxMoney, "{0:0.###}");
            //this.xrTableTaxPrice.DataBindings.Add("Text", this.DataSource, Model.InvoiceXODetail.PRO_InvoiceXODetailTaxPrice, "{0:0.###}");
            // this.xrTableCellProductQuantity.DataBindings.Add("Text", this.DataSource, Model.InvoiceXSDetail.PRO_InvoiceXSDetailQuantity);
        }

        public Model.InvoiceXO Invoice
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

        private void ROInvoiceXO_1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            this.bindingSource1.DataSource = this.detailManager.SelectByHeaderProRang(Invoice, condition.Product, condition.Product2, condition.IsClose);
        }

    }
}
