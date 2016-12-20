using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Collections.Generic;
using DevExpress.XtraReports.UI;

namespace Book.UI.Query
{
    public partial class ROInvoiceXOlist : BaseReport
    {
        private BL.InvoiceXODetailManager invoicexoDetailManager = new Book.BL.InvoiceXODetailManager();

        public ROInvoiceXOlist()
        {
            InitializeComponent();
        }

        public ROInvoiceXOlist(ConditionX condition)
            : this()
        {
            DateTime start = condition.StartDate;
            DateTime end = condition.EndDate;

            this.xrLabelReportName.Text = Properties.Resources.InvoiceXODetail;

            this.xrLabelDateRange.Text = string.Format(Properties.Resources.DateRange, start.ToString("yyyy-MM-dd"), end.ToString("yyyy/MM/dd"));

            IList<Model.InvoiceXODetail> Details = invoicexoDetailManager.Select(condition.Customer1, condition.Customer2, condition.StartDate, condition.EndDate, condition.Yjri1, condition.Yjri2, condition.Employee1, condition.Employee2, condition.XOId1, condition.XOId2, condition.CusXOId, condition.Product, condition.Product2, condition.IsClose, false, condition.OrderColumn, condition.OrderType, condition.DetailFlag);

            if (Details == null || Details.Count == 0)
                throw new Helper.InvalidValueException("üo”õ‰õ");

            this.DataSource = Details;

            //this.TCchkh.DataBindings.Add("Text", this.DataSource, "Invoice.xocustomer." + Model.Customer.PRO_CustomerShortName);
            //this.TCcpxh.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_ProductName);
            //this.TCdanjia.DataBindings.Add("Text", this.DataSource, Model.InvoiceXODetail.PRO_InvoiceXODetailPrice, "{0:000}");
            //this.TCdingdanbianhao.DataBindings.Add("Text", this.DataSource, "Invoice." + Model.InvoiceXO.PROPERTY_INVOICEID);
            //this.TCjinge.DataBindings.Add("Text", this.DataSource, Model.InvoiceXODetail.PRO_InvoiceXODetailMoney, "{0:0}");
            //this.TCkh.DataBindings.Add("Text", this.DataSource, "Invoice.Customer." + Model.Customer.PRO_CustomerShortName);
            //this.TCkhddbh.DataBindings.Add("Text", this.DataSource, "Invoice." + Model.InvoiceXO.PRO_CustomerInvoiceXOId);
            //this.TCkhxh.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_CustomerProductName);
            //this.TCriqi.DataBindings.Add("Text", this.DataSource, "Invoice." + Model.InvoiceXO.PROPERTY_INVOICEDATE, "{0:yyyy/MM/dd}");
            //this.TCshuliang.DataBindings.Add("Text", this.DataSource, Model.InvoiceXODetail.PRO_InvoiceXODetailQuantity);
            //this.TCyjrq.DataBindings.Add("Text", this.DataSource, "Invoice." + Model.InvoiceXO.PRO_InvoiceYjrq, "{0:yyyy/MM/dd}");
            //this.TCInvoiceXODetailBeenQuantity.DataBindings.Add("Text", this.DataSource, Model.InvoiceXODetail.PRO_InvoiceXODetailBeenQuantity);
            //this.TCInvoiceXTDetailQuantity.DataBindings.Add("Text", this.DataSource, Model.InvoiceXODetail.PRO_InvoiceXTDetailQuantity);

            this.TCdingdanbianhao.DataBindings.Add("Text", this.DataSource, Model.InvoiceXODetail.PRO_InvoiceId);
            this.TCkhddbh.DataBindings.Add("Text", this.DataSource, Model.InvoiceXODetail.PRO_CustomerInvoiceXOId);
            this.TCdanjia.DataBindings.Add("Text", this.DataSource, Model.InvoiceXODetail.PRO_InvoiceXODetailPrice, "{0:000}");
            this.TCjinge.DataBindings.Add("Text", this.DataSource, Model.InvoiceXODetail.PRO_InvoiceXODetailMoney, "{0:0}");
            this.TCkh.DataBindings.Add("Text", this.DataSource, Model.InvoiceXODetail.PRO_CustomerName);
            this.TCchkh.DataBindings.Add("Text", this.DataSource, Model.InvoiceXODetail.PRO_XOCustomerName);
            this.TCcpxh.DataBindings.Add("Text", this.DataSource, Model.InvoiceXODetail.PRO_ProductName);
            this.TCkhxh.DataBindings.Add("Text", this.DataSource, Model.InvoiceXODetail.PRO_CustomerProductName);
            this.TCriqi.DataBindings.Add("Text", this.DataSource, Model.InvoiceXODetail.PRO_InvoiceDate, "{0:yyyy/MM/dd}");
            this.TCyjrq.DataBindings.Add("Text", this.DataSource, Model.InvoiceXODetail.PRO_InvoiceYjrq, "{0:yyyy/MM/dd}");
            this.TCshuliang.DataBindings.Add("Text", this.DataSource, Model.InvoiceXODetail.PRO_InvoiceXODetailQuantity);
            this.TCInvoiceXODetailBeenQuantity.DataBindings.Add("Text", this.DataSource, Model.InvoiceXODetail.PRO_InvoiceXODetailBeenQuantity);
            this.TCInvoiceXTDetailQuantity.DataBindings.Add("Text", this.DataSource, Model.InvoiceXODetail.PRO_InvoiceXTDetailQuantity);


            this.xrlblTotalShuliang.Summary.FormatString = "{0:0}";
            this.xrlblTotalShuliang.Summary.Func = SummaryFunc.Sum;
            this.xrlblTotalShuliang.Summary.IgnoreNullValues = true;
            this.xrlblTotalShuliang.Summary.Running = SummaryRunning.Report;
            this.xrlblTotalShuliang.DataBindings.Add("Text", this.DataSource, Model.InvoiceXODetail.PRO_InvoiceXODetailQuantity);

            this.xrlblTotalJinE.Summary.FormatString = "{0:0}";
            this.xrlblTotalJinE.Summary.Func = SummaryFunc.Sum;
            this.xrlblTotalJinE.Summary.IgnoreNullValues = true;
            this.xrlblTotalJinE.Summary.Running = SummaryRunning.Report;
            this.xrlblTotalJinE.DataBindings.Add("Text", this.DataSource, Model.InvoiceXODetail.PRO_InvoiceXODetailMoney);
        }
    }
}
