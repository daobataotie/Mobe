using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;

namespace Book.UI.Query
{
    public partial class ConditionStockCheckReport : DevExpress.XtraReports.UI.XtraReport
    {

        private BL.StockCheckDetailManager manager = new Book.BL.StockCheckDetailManager();

        public ConditionStockCheckReport(ConditionStockCheck condition)
        {
            InitializeComponent();
            this.xrLabel1.Text = BL.Settings.CompanyChineseName;
            this.xrLabelTitle.Text = condition.StartDate + "--" + condition.EndDate + "盘点信息";
            IList<Model.StockCheckDetail> list = manager.GetStockCheckDetailByDate(condition.StartDate, condition.EndDate);
            this.DataSource = list;
            this.xrTableCellStockCheckDate.DataBindings.Add("Text", this.DataSource, "StockCheck." + Model.StockCheck.PROPERTY_STOCKCHECKDATE);
            this.xrTableCellDepotName.DataBindings.Add("Text", this.DataSource, "StockCheck.Depot." + Model.Depot.PRO_DepotName);
            this.xrTableCellStockCheckId.DataBindings.Add("Text", this.DataSource, Model.StockCheckDetail.PROPERTY_STOCKCHECKID);
            this.xrTableCell1ProductName.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_ProductName);
            this.xrTableCell1UnitName.DataBindings.Add("Text", this.DataSource, Model.StockCheckDetail.PROPERTY_PRODUCTUNITNAME);
            this.xrTableCell1FaceNum.DataBindings.Add("Text", this.DataSource, Model.StockCheckDetail.PROPERTY_STOCKCHECKQUANTITY);
            this.xrTableCell1StockNum.DataBindings.Add("Text", this.DataSource, Model.StockCheckDetail.PROPERTY_STOCKCHECKQUANTITYOLD);
            this.xrTableCell1MinusNum.DataBindings.Add("Text", this.DataSource, Model.StockCheckDetail.PROPERTY_MINUSSTOCKCHECK);
        }

    }
}
