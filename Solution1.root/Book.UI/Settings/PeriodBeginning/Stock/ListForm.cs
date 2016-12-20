using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;

namespace Book.UI.Settings.PeriodBeginning.Stock
{
    /*----------------------------------------------------------------
// Copyright (C) 2008 - 2010   咸阳飛馳軟件有限公司
//                     版權所有 圍著必究

// 编 码 人: 马艳军            完成时间:2009-10-15
// 修改原因：
// 修 改 人:                          修改时间:
// 修改原因：
// 修 改 人:                          修改时间:
//----------------------------------------------------------------*/
    public partial class ListForm : DevExpress.XtraEditors.XtraForm
    {

        #region 变量对象的定义
        protected BL.StockManager stockManager = new Book.BL.StockManager();
        private BL.ProductManager _productManager = new Book.BL.ProductManager();
        private BL.ProductUnitManager _productUnitManager = new Book.BL.ProductUnitManager();
        private BL.DepotManager _depotManager = new Book.BL.DepotManager();
        private BL.ProductCategoryManager _productCategoryManger = new BL.ProductCategoryManager();
        public static double SetNums = 0;
        public static IList<Model.Stock> stocks = new List<Model.Stock>();
        public static IList<Model.Stock> _stocklist = new List<Model.Stock>();
        IList<Model.Product> products = new List<Model.Product>();
        public static Dictionary<string, Model.Stock> dic = new Dictionary<string, Book.Model.Stock>();
        #endregion

        public ListForm()
        {
            InitializeComponent();
            IList<Model.Depot> depots = this._depotManager.Select();
            this.bindingSourceDepot.DataSource = depots;
            this.lookUpEditProductCatogry.Properties.DataSource = this._productCategoryManger.Select();
            if (depots.Count > 0)
                this.lookUpEditDepot.EditValue = depots[0].DepotId;
        }

        private void ListForm_Load(object sender, EventArgs e)
        {

        }

        //指定数据源
        private void DataBind()
        {
            this.bindingSourceStock.DataSource = this.stockManager.SelectDataTable();
        }

        //保存方法
        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (!this.gridView1.PostEditor() || !this.gridView1.UpdateCurrentRow())
                return;


            if (this.dateEdit1.EditValue != null)
                _stocklist.ToList<Model.Stock>().ForEach(a => a.Stock0Date = this.dateEdit1.DateTime);
            this.stockManager.Insert(_stocklist);
            this.gridControl1.RefreshDataSource();
            MessageBox.Show(Properties.Resources.SuccessfullySaved, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            _stocklist.Clear();
            //DataTable dt = (DataTable)this.bindingSourceStock.DataSource;
            //int rowCount = dt.Rows.Count;
            //if (rowCount > 0)
            //{
            //    for (int i = 0; i < rowCount; i++)
            //    {
            //        DataRow dr = dt.Rows[i];
            //        if (dr.RowState == DataRowState.Modified)
            //        {
            //            dr["StockQuantity1"] = dr["StockQuantity0"];
            //            dr["StockCurrentJR"] = dr["StockBeginJR"];
            //            dr["StockCurrentJC"] = dr["StockBeginJC"];
            //        }
            //    }
            //    this.stockManager.UpdateDataTable(dt);

            //    //this.DataBind();
            //}
            this.barStaticItemCount.Caption = "共" + this.gridView1.RowCount + "項";
        }

        private void gridView1_ShowingEditor(object sender, CancelEventArgs e)
        {
            if (this.gridView1.FocusedColumn.Name == "gridColumnProductUnit")
            {
                if (this.gridView1.FocusedColumn.ColumnEdit is DevExpress.XtraEditors.Repository.RepositoryItemComboBox)
                {
                    Model.Product p = (this.gridView1.GetRow(this.gridView1.FocusedRowHandle) as Model.Stock).Product;

                    this.repositoryItemComboBox1.Items.Clear();

                    if (!string.IsNullOrEmpty(p.BasedUnitGroupId))
                    {
                        IList<Model.ProductUnit> unitList = this._productUnitManager.Select(p.BasedUnitGroupId);
                        foreach (Model.ProductUnit item in unitList)
                        {
                            this.repositoryItemComboBox1.Items.Add(item.CnName);
                        }
                    }
                }
            }
        }

        private void repositoryItemHyperLinkEdit1_Click(object sender, EventArgs e)
        {
            if (this.dateEdit1.EditValue == null)
            {
                MessageBox.Show("请先选择期初日期");
                return;
            }
            if (lookUpEditDepot.EditValue == null)
            {
                MessageBox.Show(Properties.Resources.deptNotNull, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                lookUpEditDepot.Focus();
                return;
            }

            Model.Stock sto = this.bindingSourceStock.Current as Model.Stock;

            DepotPositionAndNumsForm depotPN = new DepotPositionAndNumsForm(sto.Product, this._depotManager.Get(this.lookUpEditDepot.EditValue.ToString()));
            if (depotPN.ShowDialog(this) == DialogResult.OK)
            {
                if (SetNums == 0)
                    sto.StockQuantity0 = 0;
                else
                    sto.StockQuantity0 = SetNums;
            }
            sto.Stock0Date = this.dateEdit1.DateTime;
            this.gridControl1.RefreshDataSource();
        }

        private void lookUpEditDepot_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            this.bindingSourceDepot.DataSource = this._depotManager.Select();
        }

        private void gridView1_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.ListSourceRowIndex < 0) return;
            IList<Model.Stock> stockDetail = this.bindingSourceStock.DataSource as IList<Model.Stock>;
            if (stockDetail == null || stockDetail.Count < 1) return;
            Model.Product product = stockDetail[e.ListSourceRowIndex].Product;
            if (product == null) return;
            switch (e.Column.Name)
            {
                case "gridColumnProductUnit":
                    e.DisplayText = product.DepotUnit == null ? string.Empty : product.DepotUnit.CnName;
                    break;
                case "gridColumnProductSpecification":
                    e.DisplayText = product.ProductSpecification;
                    break;
                case "gridColumnProductId":
                    e.DisplayText = product.Id;
                    break;
                case "gridColumn":
                    e.DisplayText = product.ProductDescription;
                    break;
                case "gridColumnDepotName":
                    e.DisplayText = this.lookUpEditDepot.Text;

                    break;
            }
        }

        private void simpleButton_Search_Click(object sender, EventArgs e)
        {
            //if (lookUpEditDepot.EditValue == null)
            //{
            //    MessageBox.Show(Properties.Resources.deptNotNull, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    lookUpEditDepot.Focus();
            //    return;
            //}
            Model.ProductCategory temp = this.lookUpEditProductCatogry.EditValue as Model.ProductCategory;
            products = this._productManager.GetProductByCondition(temp == null ? null : temp.ProductCategoryId, this.textEditProductNameOrId.Text, this.lookUpEditDepot.EditValue.ToString());

            stocks = (from p in products
                      select new Model.Stock()
                      {
                          StockId = Guid.NewGuid().ToString(),
                          Product = p,
                          ProductId = p.ProductId,
                          StockQuantity0 = stockManager.GetTheCount0OfProductByProductId(p, this._depotManager.Get(this.lookUpEditDepot.EditValue.ToString())),
                          StockQuantity1 = stockManager.GetTheCountByProduct(p),
                          DepotStockQuantity = stockManager.GetTheCount1OfProductByProductId(p, this._depotManager.Get(this.lookUpEditDepot.EditValue.ToString())),
                          ProductCategory = p.ProductCategory,
                          GetDescription = p.ProductDescription,
                          CustomerProductName = p.CustomerProductName,
                          Stock0Date = stockManager.Get0DateByProduct(p.ProductId)
                      }).ToList<Model.Stock>();


            this.bindingSourceStock.DataSource = stocks;
            this.gridControl1.RefreshDataSource();
            barStaticItemCount.Caption = "共" + stocks.Count + "項";
        }

        private void simpleButton_Empty_Click(object sender, EventArgs e)
        {
            this.lookUpEditProductCatogry.EditValue = null;
        }

        private void lookUpEditProductCatogry_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            switch (e.Button.Index)
            {
                case 0:
                    break;
                case 1:
                    this.lookUpEditProductCatogry.EditValue = null;
                    break;
            }
        }

        //private void simpleButton1_Click(object sender, EventArgs e)
        //{


        //    if (dateEdit1.EditValue == null || stocks == null || stocks.Count < 0) return;
        //    if (MessageBox.Show("确认修改下面商品的期初日期", this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
        //        return;
        //    stocks.ToList<Model.Stock>().ForEach(a => a.Stock0Date = dateEdit1.DateTime);
        //    this.gridControl1.RefreshDataSource();

        //}
    }
}