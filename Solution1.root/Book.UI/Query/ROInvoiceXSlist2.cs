using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;

namespace Book.UI.Query
{
    public partial class ROInvoiceXSlist2 : DevExpress.XtraReports.UI.XtraReport
    {
        protected BL.InvoiceXSDetailManager detailManager = new Book.BL.InvoiceXSDetailManager();

        public ROInvoiceXSlist2()
        {
            InitializeComponent();
        }

        public ROInvoiceXSlist2(ConditionX condition)
            : this()
        {
            DateTime start = condition.StartDate;
            DateTime end = condition.EndDate;

            this.lblCompanyName.Text = BL.Settings.CompanyChineseName;
            this.lblReportName.Text = Properties.Resources.CHDetail;
            this.lblReportdate.Text += DateTime.Now.ToShortDateString();
            //PrintCondition
            this.lblReprotCondition.Text += "\t單據日期區間:" + condition.StartDate.ToString("yyyy-MM-dd") + "~" + condition.EndDate.AddDays(1).ToString("yyyy-MM-dd");
            if (condition.Yjri1 != Helper.DateTimeParse.NullDate && condition.Yjri2 != Helper.DateTimeParse.EndDate)
                this.lblReprotCondition.Text += "\t預交日期:" + condition.Yjri1.ToString("yyyy-MM-dd") + "~" + condition.Yjri2.AddDays(1).ToString("yyyy-MM-dd");
            if (condition.Customer1 != null && condition.Customer2 != null)
                if (condition.Customer1.CustomerId == condition.Customer2.CustomerId)
                    this.lblReprotCondition.Text += "\t客戶區間:" + condition.Customer1.ToString();
                else
                    this.lblReprotCondition.Text += "\t客戶區間:" + condition.Customer1.ToString() + "~" + condition.Customer2.ToString();
            if (!string.IsNullOrEmpty(condition.XOId1) && !string.IsNullOrEmpty(condition.XOId2))
                this.lblReprotCondition.Text += "\t出貨單編號:" + condition.XOId1 + "~" + condition.XOId1;
            if (!string.IsNullOrEmpty(condition.CusXOId))
                this.lblReprotCondition.Text += "\t客戶訂單編號:" + condition.CusXOId;
            if (condition.Product != null && condition.Product != null)
                if (condition.Product.ProductId == condition.Product2.ProductId)
                    this.lblReprotCondition.Text += "\t商品:" + condition.Product.ToString();
                else
                    this.lblReprotCondition.Text += "\t商品:" + condition.Product.ToString() + "~" + condition.Product2.ToString();

            //bind
            this.DataSource = this.detailManager.SelectbyConditionX(condition.StartDate, condition.EndDate, condition.Yjri1, condition.Yjri2, condition.Customer1, condition.Customer2, condition.XOId1, condition.XOId2, condition.Product, condition.Product2, condition.CusXOId, condition.OrderColumn, condition.OrderType);

            if (this.DataSource == null)
                throw new global::Helper.InvalidValueException("無記錄");

            this.xrTableXSid.DataBindings.Add("Text", this.DataSource, Model.InvoiceXSDetail.PRO_InvoiceId);
            this.xrTableXSDate.DataBindings.Add("Text", this.DataSource, "Invoice." + Model.InvoiceXS.PROPERTY_INVOICEDATE, "{0:yyyy/MM/dd}");
            this.TCXOCustomer.DataBindings.Add("Text", this.DataSource, "Invoice.XSCustomer." + Model.Customer.PRO_CustomerFullName);
            this.TCYJDate.DataBindings.Add("Text", this.DataSource, "InvoiceXO." + Model.InvoiceXO.PRO_InvoiceYjrq, "{0:yyyy/MM/dd}");
            this.xrTableCellProductName.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_ProductName);
            //this.xrTableCellProductGuige.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_ProductSpecification);
            this.TCCustomerProductName.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_CustomerProductName);
            this.xrTableCellProductQuantity.DataBindings.Add("Text", this.DataSource, Model.InvoiceXSDetail.PRO_InvoiceXSDetailQuantity);
            this.xrTablePrice.DataBindings.Add("Text", this.DataSource, Model.InvoiceXSDetail.PRO_InvoiceXSDetailPrice, "{0:0.###}");
            //this.xrTableTaxPrice.DataBindings.Add("Text", this.DataSource, Model.InvoiceXSDetail.PRO_InvoiceXSDetailTaxPrice, "{0:0.###}");
            this.xrTableHeJi.DataBindings.Add("Text", this.DataSource, Model.InvoiceXSDetail.PRO_InvoiceXSDetailMoney, "{0:0.###}");
            //this.xrTableTax.DataBindings.Add("Text", this.DataSource, Model.InvoiceXSDetail.PRO_InvoiceXSDetailTax, "{0:0.###}");
            //this.xrTableTaxTotal.DataBindings.Add("Text", this.DataSource, Model.InvoiceXSDetail.PRO_InvoiceXSDetailTaxMoney, "{0:0.###}");
            //this.xrTableOrderQuantity.DataBindings.Add("Text", this.DataSource, "InvoiceXODetail." + Model.InvoiceXODetail.PRO_InvoiceXODetailQuantity, "{0:0.###}");
            this.xrTableNoXGQuantity.DataBindings.Add("Text", this.DataSource, "InvoiceXODetail." + Model.InvoiceXODetail.PRO_InvoiceXODetailQuantity0, "{0:0.###}");
            //this.xrTableZS.DataBindings.Add("Text", this.DataSource, Model.InvoiceCGDetail.PRO_InvoiceCGDetaiInQuantity, "{0:0.###}");
            //this.xrTableTransferQuantity.DataBindings.Add("Text", this.DataSource, Model.InvoiceCGDetail.PRO_ProduceTransferQuantity, "{0:0.###}");
            //this.xrTableZR.DataBindings.Add("Text", this.DataSource, Model.InvoiceXSDetail.PRO_InvoiceAllowance, "{0:0.###}");
            this.xrTableUnit.DataBindings.Add("Text", this.DataSource, Model.InvoiceXSDetail.PRO_InvoiceProductUnit);
            //this.xrCheckBoxZS.DataBindings.Add("Checked", this.DataSource, Model.InvoiceXSDetail.PRO_Donatetowards);
            this.xrTableCellXOId.DataBindings.Add("Text", this.DataSource, "InvoiceXO." + Model.InvoiceXO.PRO_CustomerInvoiceXOId);
            //this.xrTableInvoiceXSDetailFPQuantity.DataBindings.Add("Text", this.DataSource, Model.InvoiceXSDetail.PRO_InvoiceXSDetailFPQuantity);

            this.xrlblTotalShuliang.Summary.FormatString = "{0:0}";
            this.xrlblTotalShuliang.Summary.Func = SummaryFunc.Sum;
            this.xrlblTotalShuliang.Summary.IgnoreNullValues = true;
            this.xrlblTotalShuliang.Summary.Running = SummaryRunning.Report;
            this.xrlblTotalShuliang.DataBindings.Add("Text", this.DataSource, Model.InvoiceXSDetail.PRO_InvoiceXSDetailQuantity);

            this.xrlblTotalShuiE.Summary.FormatString = "{0:0}";
            this.xrlblTotalShuiE.Summary.Func = SummaryFunc.Sum;
            this.xrlblTotalShuiE.Summary.IgnoreNullValues = true;
            this.xrlblTotalShuiE.Summary.Running = SummaryRunning.Report;
            this.xrlblTotalShuiE.DataBindings.Add("Text", this.DataSource, Model.InvoiceXSDetail.PRO_InvoiceXSDetailTax, "{0:0}");

            this.xrlblTotalJinE.Summary.FormatString = "{0:0}";
            this.xrlblTotalJinE.Summary.Func = SummaryFunc.Sum;
            this.xrlblTotalJinE.Summary.IgnoreNullValues = true;
            this.xrlblTotalJinE.Summary.Running = SummaryRunning.Report;
            this.xrlblTotalJinE.DataBindings.Add("Text", this.DataSource, Model.InvoiceXSDetail.PRO_InvoiceXSDetailMoney, "{0:0}");


            this.xrlblTotalHeJi.Summary.FormatString = "{0:0}";
            this.xrlblTotalHeJi.Summary.Func = SummaryFunc.Sum;
            this.xrlblTotalHeJi.Summary.IgnoreNullValues = true;
            this.xrlblTotalHeJi.Summary.Running = SummaryRunning.Report;
            this.xrlblTotalHeJi.DataBindings.Add("Text", this.DataSource, Model.InvoiceXSDetail.PRO_InvoiceXSDetailTaxMoney, "{0:0}");
        }
    }
}
