using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

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
    public partial class DepotPositionAndNumsForm : Form
    {

        private Model.Stock _stock = new Book.Model.Stock();
        private IList<Model.Stock> stockList = new List<Model.Stock>();
        private double nums = 0;
        private BL.DepotPositionManager depotPositionManager = new Book.BL.DepotPositionManager();
        private BL.StockManager _stockManager = new Book.BL.StockManager();

        public DepotPositionAndNumsForm()
        {
            InitializeComponent();
        }

        private Model.Product _product;
        private Model.Depot _depot;
        public DepotPositionAndNumsForm(Model.Product pro, Model.Depot dep)
        {
            InitializeComponent();
            this._product = pro;
            this._depot = dep;
        }

        private void DepotPositionAndNumsForm_Load(object sender, EventArgs e)
        {


            IList<Model.DepotPosition> list = depotPositionManager.Select(this._depot);
            if (list.Count == 0) return;
            foreach (Model.DepotPosition item in list)
            {
                Model.Stock stock = new Book.Model.Stock();
                stock.StockId = Guid.NewGuid().ToString();
                stock.DepotId = this._depot.DepotId;
                stock.DepotPosition = item;
                stock.DepotPositionId = item.DepotPositionId;
                stock.Product = this._product;
                stock.ProductId = stock.Product.ProductId;


                if (ListForm.dic.ContainsKey(this._product.ProductId + stock.DepotPositionId))
                {
                    stock.StockQuantity0 = ListForm.dic[this._product.ProductId + stock.DepotPositionId].StockQuantity0;
                    stock.OldStock = stock.StockQuantity0;
                    stock.Stock0Date = ListForm.dic[this._product.ProductId + stock.DepotPositionId].Stock0Date;
                }
                else
                {
                    Model.Stock sk = this._stockManager.GetStockByProductIdAndDepotPositionId(this._product.ProductId, item.DepotPositionId);
                    if (sk != null)
                    {
                        stock.StockQuantity0 = sk.StockQuantity0;
                        stock.OldStock = stock.StockQuantity0;
                        stock.Stock0Date = sk.Stock0Date;
                    }
                    else
                    {
                        stock.StockQuantity0 = 0;
                        stock.OldStock = 0;
                    }
                }
                this.stockList.Add(stock);
            }

            foreach (Model.Stock item in this.stockList)
            {
                if (item.StockQuantity0 != 0&& item.StockQuantity0 != null)
                    nums += item.StockQuantity0.Value;

            }
            this.bindingSourceStock.DataSource = this.stockList;

            this.gridControl1.RefreshDataSource();
        }

        private void simpleButton_Sure_Click(object sender, EventArgs e)
        {
            //this.stockList = this.bindingSourceStock.DataSource as List<Model.Stock>;
            //double nums = 0;
            //foreach (Model.Stock item in this.stockList)
            //{

            //    if (ListForm.dic.ContainsKey(this._product.ProductId + item.DepotPositionId))
            //    {

            //        //if (item.StockQuantity0 != 0)
            //        //{
            //            nums += item.StockQuantity0.Value;
            //            item.StockQuantity1 = item.StockQuantity0;
            //            ListForm.dic.Remove(this._product.ProductId + item.DepotPositionId);
            //            ListForm.dic.Add(this._product.ProductId + item.DepotPositionId, item);
            //            ListForm._stocklist.Remove(ListForm.dic[this._product.ProductId + item.DepotPositionId]);
            //            ListForm._stocklist.Add(item);

            //        //}
            //        //else
            //        //{
            //        //    item.StockQuantity1 = item.StockQuantity0;
            //        //    ListForm._stocklist.Remove(ListForm.dic[this._product.ProductId + item.DepotPositionId]);
            //        //    ListForm.dic.Remove(this._product.ProductId + item.DepotPositionId);
            //        //    //ListForm.dic.Add(this._product.ProductId + item.DepotPositionId, item);

            //        //    //ListForm._stocklist.Add(item);
            //        //}
            //    }
            //    else
            //    {
            //        nums += item.StockQuantity0 == null ? 0 : item.StockQuantity0.Value;
            //        //if (item.StockQuantity0 != null && item.StockQuantity0 != 0)
            //        //{
            //            item.StockQuantity1 = item.StockQuantity0;
            //            ListForm._stocklist.Add(item);
            //            ListForm.dic.Add(this._product.ProductId + item.DepotPositionId, item);
            //        //}
            //    }
            //}

            ListForm.SetNums = nums;
            this.DialogResult = DialogResult.OK;
        }

        private void gridView1_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.ListSourceRowIndex < 0) return;
            IList<Model.Stock> stock = this.bindingSourceStock.DataSource as IList<Model.Stock>;
            if (stock == null || stock.Count < 1) return;
            Model.DepotPosition depotposition = stock[e.ListSourceRowIndex].DepotPosition;
            if (depotposition == null) return;
            switch (e.Column.Name)
            {
                case "gridColumn1":
                    e.DisplayText = depotposition.Id;
                    break;
            }
        }

        private void simpleButton_Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {

            if (e.Column.Name == this.gridColumnQuantity0.Name || e.Column.Name == this.gridColumn2.Name)
            {
                Model.Stock detail = this.gridView1.GetRow(e.RowHandle) as Model.Stock;
                if (detail != null)
                {
                
                    if (ListForm.dic.ContainsKey(detail.ProductId + detail.DepotPositionId))
                    {

                        //if (item.StockQuantity0 != 0)
                        //{
                        // nums -= ListForm.dic[detail.ProductId + detail.DepotPositionId].StockQuantity0.Value;
                        // nums += Convert.ToDouble(e.Value.ToString()) - detail.OldStock==null?0:detail.OldStock.Value;
                        //detail.StockQuantity0.Value;
                        detail.StockQuantity1 = detail.StockQuantity0;// = Convert.ToDouble(e.Value.ToString());
                        ListForm.dic.Remove(detail.ProductId + detail.DepotPositionId);
                        ListForm.dic.Add(detail.ProductId + detail.DepotPositionId, detail);
                        ListForm._stocklist.Remove(ListForm.dic[detail.ProductId + detail.DepotPositionId]);
                        ListForm._stocklist.Add(detail);

                        //}
                        //else
                        //{
                        //    item.StockQuantity1 = item.StockQuantity0;
                        //    ListForm._stocklist.Remove(ListForm.dic[this._product.ProductId + item.DepotPositionId]);
                        //    ListForm.dic.Remove(this._product.ProductId + item.DepotPositionId);
                        //    //ListForm.dic.Add(this._product.ProductId + item.DepotPositionId, item);

                        //    //ListForm._stocklist.Add(item);
                        //}
                    }
                    else
                    {
                        //nums -= ListForm.dic[detail.ProductId + detail.DepotPositionId].StockQuantity0.Value;

                        //if (item.StockQuantity0 != null && item.StockQuantity0 != 0)
                        //{
                        detail.StockQuantity1 = detail.StockQuantity0 ;//= Convert.ToDouble(e.Value.ToString());
                        ListForm._stocklist.Add(detail);
                        ListForm.dic.Add(detail.ProductId + detail.DepotPositionId, detail);
                        //}
                    }
                    nums +=( detail.StockQuantity0.Value - (detail.OldStock == null ? 0 : detail.OldStock.Value));
                    detail.OldStock = detail.StockQuantity0;
                }

            }
        }
    }
}
