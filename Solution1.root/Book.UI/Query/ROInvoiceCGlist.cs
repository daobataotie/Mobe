using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;

namespace Book.UI.Query
{
    public partial class ROInvoiceCGlist : BaseReport
    {
        private BL.InvoiceCGDetailManager invoicecgmanager = new Book.BL.InvoiceCGDetailManager();

        public ROInvoiceCGlist()
        {
            InitializeComponent();
        }

        public ROInvoiceCGlist(ConditionCO condition)
            : this()
        {
            DateTime start = condition.StartDate;
            DateTime end = condition.EndDate;

            this.xrLabelReportName.Text = Properties.Resources.JHDetail;
            this.xrLabelDateRange.Text = string.Format(Properties.Resources.DateRange, start.ToString("yyyy-MM-dd"), end.ToString("yyyy-MM-dd"));
            if (condition.SupplierStart != null && condition.SupplierEnd != null)
            {
                this.lblSupplierRange.Text += condition.SupplierStart.ToString() + "~" + condition.SupplierEnd.ToString();
            }

            IList<Model.InvoiceCGDetail> Details = this.invoicecgmanager.SelectByConditionCO(condition.StartDate, condition.EndDate, condition.StartJHDate, condition.EndJHDate, condition.StartFKDate, condition.EndFKDate, condition.SupplierStart, condition.SupplierEnd, condition.ProductStart, condition.ProductEnd, condition.COStartId, condition.COEndId, condition.CusXOId, condition.EmpStart, condition.EmpEnd);

            if (Details == null || Details.Count == 0)
                throw new Helper.InvalidValueException("Ó›ä›žé¿Õ");

            this.DataSource = Details;

            this.TCInvoiceCGDetailMoney.DataBindings.Add("Text", this.DataSource, Model.InvoiceCGDetail.PRO_InvoiceCGDetailMoney, global::Helper.DateTimeParse.GetFormatA(BL.V.SetDataFormat.CGJEXiao.Value));
            this.TCInvoiceCGDetailPrice.DataBindings.Add("Text", this.DataSource, Model.InvoiceCGDetail.PRO_InvoiceCGDetailPrice, global::Helper.DateTimeParse.GetFormatA(BL.V.SetDataFormat.CGDJXiao.Value));
            this.TCInvoiceCOId.DataBindings.Add("Text", this.DataSource, Model.InvoiceCGDetail.PRO_InvoiceCOId);
            //this.TCInvoiceDate.DataBindings.Add("Text", this.DataSource, "Invoice." + Model.InvoiceCG.PROPERTY_INVOICEDATE, "{0:yyyy-MM-dd}");
            this.TCInvoicePaymentDate.DataBindings.Add("Text", this.DataSource, "Invoice." + Model.InvoiceCG.PRO_InvoiceHisDate, "{0:yyyy-MM-dd}");

            this.TCInvoiceId.DataBindings.Add("Text", this.DataSource, Model.InvoiceCGDetail.PRO_InvoiceId);
            this.TCInvoiceProductUnit.DataBindings.Add("Text", this.DataSource, Model.InvoiceCGDetail.PRO_InvoiceProductUnit);
            this.TCNoCGQuantity.DataBindings.Add("Text", this.DataSource, "InvoiceCODetail." + Model.InvoiceCODetail.PRO_NoArrivalQuantity);
            this.TCOrderQuantity.DataBindings.Add("Text", this.DataSource, "InvoiceCODetail." + Model.InvoiceCODetail.PRO_OrderQuantity);
            this.TCProduct.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_ProductName);
            this.TCProductQuantity.DataBindings.Add("Text", this.DataSource, Model.InvoiceCGDetail.PRO_InvoiceCGDetailQuantity);

            this.xrlblTotalShuliang.Summary.FormatString = "{0:0}";
            this.xrlblTotalShuliang.Summary.Func = SummaryFunc.Sum;
            this.xrlblTotalShuliang.Summary.IgnoreNullValues = true;
            this.xrlblTotalShuliang.Summary.Running = SummaryRunning.Report;
            this.xrlblTotalShuliang.DataBindings.Add("Text", this.DataSource, Model.InvoiceCGDetail.PRO_InvoiceCGDetailQuantity);

            this.xrlblTotalShuiE.Summary.FormatString = "{0:0}";
            this.xrlblTotalShuiE.Summary.Func = SummaryFunc.Sum;
            this.xrlblTotalShuiE.Summary.IgnoreNullValues = true;
            this.xrlblTotalShuiE.Summary.Running = SummaryRunning.Report;
            this.xrlblTotalShuiE.DataBindings.Add("Text", this.DataSource, Model.InvoiceCGDetail.PRO_InvoiceCGDetailTax);

            this.xrlblTotalHeJi.Summary.FormatString = "{0:0}";
            this.xrlblTotalHeJi.Summary.Func = SummaryFunc.Sum;
            this.xrlblTotalHeJi.Summary.IgnoreNullValues = true;
            this.xrlblTotalHeJi.Summary.Running = SummaryRunning.Report;
            this.xrlblTotalHeJi.DataBindings.Add("Text", this.DataSource, Model.InvoiceCGDetail.PRO_InvoiceCGDetailMoney);

            this.xrlblTotalJinE.Summary.FormatString = "{0:0}";
            this.xrlblTotalJinE.Summary.Func = SummaryFunc.Sum;
            this.xrlblTotalJinE.Summary.IgnoreNullValues = true;
            this.xrlblTotalJinE.Summary.Running = SummaryRunning.Report;
            this.xrlblTotalJinE.DataBindings.Add("Text", this.DataSource, Model.InvoiceCGDetail.PRO_InvoiceCGDetailTaxMoney);

        }
    }
}
