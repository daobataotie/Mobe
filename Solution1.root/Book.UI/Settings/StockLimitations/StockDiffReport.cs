using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Book.UI.Settings.StockLimitations
{
    public partial class StockDiffReport : DevExpress.XtraReports.UI.XtraReport
    {
        private BL.StockEditorDetalManager stockManger = new Book.BL.StockEditorDetalManager();

        public StockDiffReport(Query.ConditionA condition)
        {
            DateTime start = condition.StartDate;
            DateTime end = condition.EndDate;
            InitializeComponent();
            this.xrLabelCompany.Text = BL.Settings.CompanyChineseName;
            this.xrLabelTitle.Text = "庫存盤點差異詳細";
            this.xrLabeDateCon.Text = start.ToShortDateString() + " ~ " + end.ToShortDateString();

            IList<Model.StockEditorDetal> list = this.stockManger.SelectStockEditorDiff(start, end);
            this.DataSource = list;
            this.xrLabelDate.Text = this.xrLabelDate.Text + DateTime.Now.ToShortDateString();
            this.xrTableCellStockCheckDate.DataBindings.Add("Text", this.DataSource, "StockEditor." + Model.StockEditor.PROPERTY_STOCKEDITORDATE, "{0:yyyy/MM/dd}");
            this.xrTableCellDepotName.DataBindings.Add("Text", this.DataSource, "DepotPosition.Depot." + Model.Depot.PRO_DepotName);
            this.xrTableCellPosition.DataBindings.Add("Text", this.DataSource, "DepotPosition." + Model.DepotPosition.PROPERTY_ID);
            this.xrTableCell1ProductId.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_Id);
            this.xrTableCellProductName.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_ProductName);
            this.xrTableCellCustomerPro.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_CustomerProductName);
            this.xrTableCell1MinusNum.DataBindings.Add("Text", this.DataSource, Model.StockEditorDetal.PROPERTY_GETCHAZHI.ToString(), "{0:0.####}");
            this.xrTableCell1FaceNum.DataBindings.Add("Text", this.DataSource, Model.StockEditorDetal.PROPERTY_STOCKEDITORQUANTITY, "{0:0.####}");
            this.xrTableCell1StockNum.DataBindings.Add("Text", this.DataSource, Model.StockEditorDetal.PROPERTY_STOCKQUANTITY, "{0:0.####}");
            //this.xrTableCell1MinusNum.DataBindings.Add("Text", this.DataSource, "Stock." + Model.Stock.PROPERTY_stockQuantityDiff);

        }

    }
}
