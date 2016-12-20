using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Book.UI.Query
{
    public partial class ROInvoiceCOlist : BaseReport
    {
        private BL.InvoiceCODetailManager invoicecomanager = new Book.BL.InvoiceCODetailManager();

        public ROInvoiceCOlist()
        {
            InitializeComponent();
        }

        public ROInvoiceCOlist(ConditionCO condition)
            : this()
        {
            DateTime start = condition.StartDate;
            DateTime end = condition.EndDate;

            this.xrLabelReportName.Text = Properties.Resources.InvoiceCODetail;
            this.xrLabelDateRange.Text = string.Format(Properties.Resources.DateRange, start.ToString("yyyy-MM-dd"), end.ToString("yyyy/MM/dd"));

            IList<Model.InvoiceCODetail> Details = this.invoicecomanager.Select(condition.COStartId, condition.COEndId, condition.SupplierStart, condition.SupplierEnd, condition.StartDate, condition.EndDate, condition.ProductStart, condition.ProductEnd, condition.CusXOId, condition.StartJHDate, condition.EndJHDate, condition.InvoiceFlag, condition.EmpStart, condition.EmpEnd);

            if (Details == null || Details.Count == 0)
                throw new Helper.InvalidValueException();

            this.DataSource = Details;

            this.TCdanjia.DataBindings.Add("Text", this.DataSource, Model.InvoiceCODetail.PRO_InvoiceCODetailPrice, "{0:0.###}");
            this.TCdanjubianhao.DataBindings.Add("Text", this.DataSource, Model.InvoiceCODetail.PRO_InvoiceId);
            this.TCdanwei.DataBindings.Add("Text", this.DataSource, Model.InvoiceCODetail.PRO_InvoiceProductUnit);
            this.TCgongyingshang.DataBindings.Add("Text", this.DataSource, "Invoice.Supplier." + Model.Supplier.PROPERTY_SUPPLIERFULLNAME);
            this.TCjine.DataBindings.Add("Text", this.DataSource, Model.InvoiceCODetail.PRO_InvoiceCODetailMoney, "{0:0}");
            this.TCkehu.DataBindings.Add("Text", this.DataSource, "Invoice.Customer." + Model.Customer.PRO_CustomerFullName);
            this.TCkehudingdanhao.DataBindings.Add("Text", this.DataSource, "Invoice." + Model.InvoiceCO.PRO_InvoiceCustomXOId);
            this.TCriqi.DataBindings.Add("Text", this.DataSource, "Invoice." + Model.InvoiceCO.PROPERTY_INVOICEDATE, "{0:yyyy-MM-dd}");
            this.TCshangpingbianhao.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_Id);
            this.TCshangpingmingcheng.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_ProductName);
            this.TCshuliang.DataBindings.Add("Text", this.DataSource, Model.InvoiceCODetail.PRO_OrderQuantity);
            this.TCyujiaoriqi.DataBindings.Add("Text", this.DataSource, "Invoice." + Model.InvoiceCO.PRO_InvoiceYjrq, "{0:yyyy-MM-dd}");
            this.TCArrivalQuantity.DataBindings.Add("Text", this.DataSource, Model.InvoiceCODetail.PRO_ArrivalQuantity);
            this.TCInvoiceCTQuantity.DataBindings.Add("Text", this.DataSource, Model.InvoiceCODetail.PRO_InvoiceCTQuantity);

            this.xrlblTotalShuliang.Summary.FormatString = "{0:0}";
            this.xrlblTotalShuliang.Summary.Func = SummaryFunc.Sum;
            this.xrlblTotalShuliang.Summary.IgnoreNullValues = true;
            this.xrlblTotalShuliang.Summary.Running = SummaryRunning.Report;
            this.xrlblTotalShuliang.DataBindings.Add("Text", this.DataSource, Model.InvoiceCODetail.PRO_OrderQuantity);

            this.xrlblTotalJinE.Summary.FormatString = "{0:0}";
            this.xrlblTotalJinE.Summary.Func = SummaryFunc.Sum;
            this.xrlblTotalJinE.Summary.IgnoreNullValues = true;
            this.xrlblTotalJinE.Summary.Running = SummaryRunning.Report;
            this.xrlblTotalJinE.DataBindings.Add("Text", this.DataSource, Model.InvoiceCODetail.PRO_InvoiceCODetailMoney);

        }
    }
}
