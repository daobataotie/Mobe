using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Windows.Forms;

namespace Book.UI.Query
{
    public partial class ROMRSDetailsList : BaseReport
    {
        private BL.MRSdetailsManager mrsdManager = new Book.BL.MRSdetailsManager();
        private BL.InvoiceXOManager invoicexoManager = new Book.BL.InvoiceXOManager();

        public ROMRSDetailsList()
        {

            InitializeComponent();
        }

        public ROMRSDetailsList(ConditionMRS condition)
            : this()
        {
            DateTime start = condition.StartDate;
            DateTime end = condition.EndDate;

            this.xrLabelReportName.Text = Properties.Resources.MRSDetails;

            this.xrLabelDateRange.Text = string.Format(Properties.Resources.DateRange, start.ToString("yyyy-MM-dd"), end.ToString("yyyy/MM/dd"));
            IList<Model.MRSdetails> Details = mrsdManager.SelectbyCondition(condition.MrsStart, condition.MrsEnd, condition.CustomerStart, condition.CustomerEnd, condition.StartDate, condition.EndDate, condition.SourceType, condition.Id1, condition.Id2, condition.Cusxoid, condition.Product, condition.OrderColumn, condition.OrderType, condition.ProductCategory);

            if (Details == null || Details.Count == 0)
                return;


            //foreach (Model.MRSdetails mrsd in Details)
            //{
            //    mrsd.MRSHeader.CustomerInvoiceXOId = invoicexoManager.SelectMrsIsClose(mrsd.MRSHeader, 0).CustomerInvoiceXOId;
            //}

            this.DataSource = Details;

            //Ã÷Ï¸°ó¶¨
            this.TCbianhao.DataBindings.Add("Text", this.DataSource, Model.MRSHeader.PRO_MRSHeaderId);
            this.TCcs.DataBindings.Add("Text", this.DataSource, "SupplierId");
            this.TCdw.DataBindings.Add("Text", this.DataSource, Model.MRSdetails.PRO_ProductUnit);
            this.TCjq.DataBindings.Add("Text", this.DataSource, Model.MRSdetails.PRO_JiaoHuoDate, "{0:yyyy-MM-dd}");
            this.TCkh.DataBindings.Add("Text", this.DataSource, "CustomerName");
            this.TCkhddbh.DataBindings.Add("Text", this.DataSource, "CustomerInvoiceXOId");
            this.TCkhxh.DataBindings.Add("Text", this.DataSource, "CustomerProductName");
            this.TCpdms.DataBindings.Add("Text", this.DataSource, Model.MRSdetails.PRO_ArrangeDesc);
            this.TCrq.DataBindings.Add("Text", this.DataSource, Model.MRSHeader.PRO_MRSstartdate, "{0:yyyy-MM-dd}");
            this.TCscjh.DataBindings.Add("Text", this.DataSource, Model.MRSdetails.PRO_MPSheaderId);
            this.TCsl.DataBindings.Add("Text", this.DataSource, Model.MRSdetails.PRO_MRSdetailssum);
            this.TCspmc.DataBindings.Add("Text", this.DataSource, "ProductName");
            this.TCxqly.DataBindings.Add("Text", this.DataSource, "SourceTypeName");
            this.TCsplb.DataBindings.Add("Text", this.DataSource, "ProductCategoryName");
            this.TCkucun.DataBindings.Add("Text", this.DataSource, "StocksQuantity");
            this.TCjhsl.DataBindings.Add("Text", this.DataSource, Model.MRSdetails.PRO_MRSdetailsQuantity);
            this.xrTableFenPei.DataBindings.Add("Text", this.DataSource, "ProduceDistributioned");
            this.xrTableOrderWay.DataBindings.Add("Text", this.DataSource, "OrderOnWayQuantity");


            this.xrlblTotalShuliang.Summary.FormatString = "{0:0}";
            this.xrlblTotalShuliang.Summary.Func = SummaryFunc.Sum;
            this.xrlblTotalShuliang.Summary.IgnoreNullValues = true;
            this.xrlblTotalShuliang.Summary.Running = SummaryRunning.Report;
            this.xrlblTotalShuliang.DataBindings.Add("Text", this.DataSource, Model.MRSdetails.PRO_MRSdetailssum);
        }
    }
}
