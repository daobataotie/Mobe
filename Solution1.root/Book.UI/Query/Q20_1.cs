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
    public partial class Q20_1 : DevExpress.XtraReports.UI.XtraReport
    {
        private BL.InvoiceXSDetailManager detailManager = new Book.BL.InvoiceXSDetailManager();
        private Model.InvoiceXS invoice = null;
        IList<Model.CustomerProducts> customerProduct = new List<Book.Model.CustomerProducts>();
        IList<Model.InvoiceXSDetail> xsdetail = new List<Model.InvoiceXSDetail>();
        IList<Model.InvoiceXSDetail> xsdetails = new List<Model.InvoiceXSDetail>();
        //private int tag = 0;
        private string productStart = null;
        private string productEnd = null;
        private ConditionX condition;

        //无参构造
        public Q20_1()
        {
            InitializeComponent();
            this.xrTableCellProductId.DataBindings.Add("Text", this.DataSource, Model.InvoiceXSDetail.PRO_PrimaryKeyId);
            this.xrTableCellProductName.DataBindings.Add("Text", this.DataSource, "PrimaryKey.CustomerProductName");
            //this.xrTableCellQuantity.DataBindings.Add("Text", this.DataSource, Model.InvoiceXSDetail.PRO_InvoiceXSDetailQuantity);
            //this.xrTableCellUnit.DataBindings.Add("Text", this.DataSource, Model.InvoiceXSDetail.PRO_InvoiceProductUnit);
            //this.xrTableCellGuiGe.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_ProductSpecification);
            //this.xrTableCellPrice.DataBindings.Add("Text", this.DataSource, "InvoiceXODetail.InvoiceXODetailPrice", "{0:0}");

            //  this.xrTableCellPrice.Text=string.IsNullOrEmpty(this.xrTableCellPrice.Text) ? "0" : this.xrTableCellPrice.Text;
            //   this.xrTableCellQuantity.Text = string.IsNullOrEmpty(this.xrTableCellQuantity.Text) ? "0" : this.xrTableCellQuantity.Text;
            //this.xrTableCellTotalMoney.DataBindings.Add("Text", this.DataSource, "xiaoji", "{0:0}");
            // this.xrTableCellTotalMoney.Text = (Convert.ToInt32(this.xrTableCellPrice.Text) * Convert.ToInt32(this.xrTableCellQuantity.Text)).ToString();

        }


        //两参构造
        public Q20_1(string ProductStart, string ProductEnd)
            : this()
        {

            this.productStart = ProductStart;
            this.productEnd = ProductEnd;

            //xsdetail = this.detailManager.Select(invoice);
            //if (xsdetail != null)
            //{
            //    foreach (Model.InvoiceXSDetail detail in xsdetail)
            //    {
            //        foreach (Model.CustomerProducts product in this.customerProduct)
            //        {
            //            if (detail.PrimaryKeyId == product.PrimaryKeyId)
            //            {
            //                xsdetails.Add(detail);
            //            }
            //        }

            //    }

            //}

        }

        //public Q20_1(IList<Model.CustomerProducts> customerProduct)
        //    : this()
        //{
        //    this.customerProduct = customerProduct;
        // tag = 1;

        //xsdetail = this.detailManager.Select(invoice);
        //if (xsdetail != null)
        //{
        //    foreach (Model.InvoiceXSDetail detail in xsdetail)
        //    {
        //        foreach (Model.CustomerProducts product in this.customerProduct)
        //        {
        //            if (detail.PrimaryKeyId == product.PrimaryKeyId)
        //            {
        //                xsdetails.Add(detail);
        //            }
        //        }

        //    }

        //}

        //}

        public Q20_1(ConditionX cond)
        {
            InitializeComponent();
            this.condition = cond;
            this.xrTableCellProductId.DataBindings.Add("Text", this.DataSource, Model.InvoiceXSDetail.PRO_Inumber);
            this.xrTableCellProductName.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_ProductName);
            this.xrTableCellProductGuige.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_ProductSpecification);
            this.xrTableCellProductQuantity.DataBindings.Add("Text", this.DataSource, Model.InvoiceXSDetail.PRO_InvoiceXSDetailQuantity);

            this.xrTablePrice.DataBindings.Add("Text", this.DataSource, Model.InvoiceXSDetail.PRO_InvoiceXSDetailPrice, "{0:0.###}");
            this.xrTableTaxPrice.DataBindings.Add("Text", this.DataSource, Model.InvoiceXSDetail.PRO_InvoiceXSDetailTaxPrice, "{0:0.###}");
            this.xrTableHeJi.DataBindings.Add("Text", this.DataSource, Model.InvoiceXSDetail.PRO_InvoiceXSDetailMoney, "{0:0.###}");
            this.xrTableTax.DataBindings.Add("Text", this.DataSource, Model.InvoiceXSDetail.PRO_InvoiceXSDetailTax, "{0:0.###}");
            this.xrTableTaxTotal.DataBindings.Add("Text", this.DataSource, Model.InvoiceXSDetail.PRO_InvoiceXSDetailTaxMoney, "{0:0.###}");
            this.xrTableOrderQuantity.DataBindings.Add("Text", this.DataSource, "InvoiceXODetail." + Model.InvoiceXODetail.PRO_InvoiceXODetailQuantity, "{0:0.###}");
            this.xrTableNoXGQuantity.DataBindings.Add("Text", this.DataSource, "InvoiceXODetail." + Model.InvoiceXODetail.PRO_InvoiceXODetailQuantity0, "{0:0.###}");
            this.xrTableZS.DataBindings.Add("Text", this.DataSource, Model.InvoiceCGDetail.PRO_InvoiceCGDetaiInQuantity, "{0:0.###}");
            //this.xrTableTransferQuantity.DataBindings.Add("Text", this.DataSource, Model.InvoiceCGDetail.PRO_ProduceTransferQuantity, "{0:0.###}");
            this.xrTableZR.DataBindings.Add("Text", this.DataSource, Model.InvoiceXSDetail.PRO_InvoiceAllowance, "{0:0.###}");
            this.xrTableUnit.DataBindings.Add("Text", this.DataSource, Model.InvoiceXSDetail.PRO_InvoiceProductUnit);
            this.xrCheckBoxZS.DataBindings.Add("Checked", this.DataSource, Model.InvoiceXSDetail.PRO_Donatetowards);
            this.xrTableCellXOId.DataBindings.Add("Text", this.DataSource, "InvoiceXO" + Model.InvoiceXO.PRO_CustomerInvoiceXOId);
            this.xrTableInvoiceXSDetailFPQuantity.DataBindings.Add("Text", this.DataSource, Model.InvoiceXSDetail.PRO_InvoiceXSDetailFPQuantity);
        }

        private void Q20_1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            this.bindingSource1.DataSource = this.detailManager.Select(this.invoice, condition.Product == null ? null : condition.Product.ProductId, condition.Product == null ? null : condition.Product.ProductId);
        }

        public Model.InvoiceXS Invoice
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
    }
}
