using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;

namespace Book.UI.Settings.StockLimitations
{
    public partial class StockCheckReport : DevExpress.XtraReports.UI.XtraReport
    {
        private BL.StockCheckManager manager = new Book.BL.StockCheckManager();
        private BL.StockCheckDetailManager Detailmanager = new Book.BL.StockCheckDetailManager();
        public StockCheckReport(Model.StockCheck stockCheck)
        {
            InitializeComponent();           
            this.DataSource = stockCheck.Details;
            #region 头信息
            this.xrLabelCompanyInfoName.Text = BL.Settings.CompanyChineseName;
            this.xrLabelTitle.Text="盤點校正單";
            this.xrLabel1stockcheckDate.Text = stockCheck.StockCheckDate.Value.ToShortDateString();
            this.xrLabelDepotName.Text = stockCheck.Depot.DepotName;
            this.xrLabelStockCheckId.Text = stockCheck.StockCheckId;
            if (stockCheck.Employee0 != null)
            this.xrLabel1BusinessMan.Text = stockCheck.Employee0.EmployeeName;
            this.xrLabelDate.Text = this.xrLabelDate.Text + DateTime.Now.ToShortDateString();
            this.xrLabelProCate.Text = (new BL.ProductCategoryManager().Get(stockCheck.ProductCategoryId)) == null ? "" : (new BL.ProductCategoryManager().Get(stockCheck.ProductCategoryId)).ProductCategoryName;
            this.xrLabelDesc.Text=stockCheck.Directions;
            #endregion

            #region 详细信息
            this.xrTableCellProductName.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_ProductName);
            this.xrTableCellUnitName.DataBindings.Add("Text", this.DataSource, Model.StockCheckDetail.PROPERTY_PRODUCTUNITNAME);
            this.xrTableCellCurrentNum.DataBindings.Add("Text", this.DataSource, Model.StockCheckDetail.PROPERTY_STOCKCHECKBOOKQUANTITY);
            this.xrTableCellStockNum.DataBindings.Add("Text", this.DataSource, Model.StockCheckDetail.PROPERTY_STOCKCHECKQUANTITY);
            //this.xrTableCell1Note.DataBindings.Add("Text", this.DataSource, Model.StockCheckDetail.PROPERTY_DIRECTIONS);
            this.xrTableCellChaZhi.DataBindings.Add("Text", this.DataSource, Model.StockCheckDetail.PROPERTY_MINUSSTOCKCHECK);
            this.xrTableCellDepotPosition.DataBindings.Add("Text", this.DataSource, "DepotPosition." + Model.DepotPosition.PROPERTY_ID);
            this.xrTableCusPro.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_CustomerProductName);
            #endregion
        }

    }
}
