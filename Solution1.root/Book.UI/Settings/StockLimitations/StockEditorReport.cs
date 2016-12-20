using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Book.UI.Settings.StockLimitations
{
    public partial class StockEditorReport : DevExpress.XtraReports.UI.XtraReport
    {

        private BL.StockEditorDetalManager stockEditorDetailManager = new BL.StockEditorDetalManager();
        private BL.ProductCategoryManager productCategoryManager = new BL.ProductCategoryManager();

        public StockEditorReport(Model.StockEditor stockEditor)
        {
            InitializeComponent();
            this.xrLabelReportName.Text = BL.Settings.CompanyChineseName;
            this.xrLabelTitleName.Text = Properties.Resources.StockEditor;
            this.xrLabelStockEditorId.Text = stockEditor.StockEditorId;
            this.xrLabelStockEditorDate.Text = stockEditor.StockEditorDate.Value.ToString("yyyy-MM-dd");
            this.xrLabelProductCategoryId.Text = this.productCategoryManager.Get(stockEditor.ProductCategoryId).ProductCategoryName;
            this.xrLabelEmployeeId.Text = stockEditor.Employee == null ? "" : stockEditor.Employee.EmployeeName;
            this.xrLabelEmployee0Id.Text = stockEditor.Employee0 == null ? "" : stockEditor.Employee0.EmployeeName;
            this.xrLabelDirections.Text = stockEditor.Directions;
            this.xrLabelDepotId.Text = stockEditor.Depot==null?"":stockEditor.Depot.ToString();
            System.Collections.Generic.IList<Model.StockEditorDetal> list = this.stockEditorDetailManager.SelectByStockEditorId(stockEditor.StockEditorId);
            this.DataSource = list;
            this.xrTableCellCustomerProductName.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_CustomerProductName);
            this.xrTableCellDepotPositionId.DataBindings.Add("Text", this.DataSource, "DepotPosition." + Model.DepotPosition.PROPERTY_ID);
            this.xrTableCellDirections.DataBindings.Add("Text", this.DataSource, Model.StockEditorDetal.PROPERTY_DIRECTIONS);
            this.xrTableCellProductId.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_Id);
            this.xrTableCellProductName.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_ProductName);
            this.xrTableCellProductUnitName.DataBindings.Add("Text", this.DataSource, Model.StockEditorDetal.PROPERTY_PRODUCTUNITNAME);
            this.xrTableCellStockEditorQuantity.DataBindings.Add("Text", this.DataSource, Model.StockEditorDetal.PROPERTY_STOCKEDITORQUANTITY);
        }
    }
}
