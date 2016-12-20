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
    public partial class ROInvoiceCO_1 : DevExpress.XtraReports.UI.XtraReport
    {
        private BL.InvoiceCODetailManager detailManager = new Book.BL.InvoiceCODetailManager();
        private Model.InvoiceCO invoice = null;
        private Query.ConditionCO condition;

        public ROInvoiceCO_1()
        {
            InitializeComponent();
            //this.TCProductId.DataBindings.Add("Text", this.DataSource, Model.InvoiceXSDetail.PRO_PrimaryKeyId);
            //this.TCProductName.DataBindings.Add("Text", this.DataSource, "PrimaryKey.CustomerProductName");
            //this.xrTableCellQuantity.DataBindings.Add("Text", this.DataSource, Model.InvoiceXSDetail.PRO_InvoiceXSDetailQuantity);
            //this.xrTableCellUnit.DataBindings.Add("Text", this.DataSource, Model.InvoiceXSDetail.PRO_InvoiceProductUnit);
            //this.xrTableCellGuiGe.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_ProductSpecification);
            //this.xrTableCellPrice.DataBindings.Add("Text", this.DataSource, "InvoiceXODetail.InvoiceXODetailPrice", "{0:0}");

            //  this.xrTableCellPrice.Text=string.IsNullOrEmpty(this.xrTableCellPrice.Text) ? "0" : this.xrTableCellPrice.Text;
            //   this.xrTableCellQuantity.Text = string.IsNullOrEmpty(this.xrTableCellQuantity.Text) ? "0" : this.xrTableCellQuantity.Text;
            //this.xrTableCellTotalMoney.DataBindings.Add("Text", this.DataSource, "xiaoji", "{0:0}");
            // this.xrTableCellTotalMoney.Text = (Convert.ToInt32(this.xrTableCellPrice.Text) * Convert.ToInt32(this.xrTableCellQuantity.Text)).ToString();

        }
        
        public ROInvoiceCO_1(ConditionCO cond)
        {
            InitializeComponent();
            this.condition = cond;
            this.TCProductId.DataBindings.Add("Text", this.DataSource, Model.InvoiceCODetail.PRO_Inumber);
            this.TCProductName.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_ProductName);
            this.TCInvoiceProductUnit.DataBindings.Add("Text", this.DataSource, Model.InvoiceCODetail.PRO_InvoiceProductUnit);
            this.TCOrderQuantity.DataBindings.Add("Text", this.DataSource, Model.InvoiceCODetail.PRO_OrderQuantity, "{0:0.###}");
            this.TCArrivalQuantity.DataBindings.Add("Text", this.DataSource, Model.InvoiceCODetail.PRO_ArrivalQuantity, "{0:0.###}");
            this.TCInvoiceCODetailPrice.DataBindings.Add("Text", this.DataSource, Model.InvoiceCODetail.PRO_InvoiceCODetailPrice, "{0:0.###}");
            this.TCInvoiceCODetailMoney.DataBindings.Add("Text", this.DataSource, Model.InvoiceCODetail.PRO_InvoiceCODetailMoney, "{0:0.###}");
            this.TCNetWeight.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_NetWeight, "{0:0.###}");
            this.TCGrossWeight.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_GrossWeight, "{0:0.###}");
            this.TCVolume.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_Volume, "{0:0.###}");
            this.TCProductDesc.DataBindings.Add("Rtf", this.DataSource, "Product." + Model.Product.PRO_ProductDescription);
            this.TCInvoiceCODetailNote.DataBindings.Add("Text", this.DataSource, Model.InvoiceCODetail.PRO_InvoiceCODetailNote);
        }

        public Model.InvoiceCO Invoice
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
            this.bindingSource1.DataSource = this.detailManager.SelectByHeaderProRang(invoice.InvoiceId, condition.ProductStart, condition.ProductEnd);
        }
    }
}
