using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Book.UI.Settings.StockLimitations
{
    public partial class DepotPositionAndNumsForm : Form
    {

        private Model.StockCheckDetail _stockCheckDetail = new Book.Model.StockCheckDetail();
        private BL.StockCheckDetailManager _stockCheckDetailManager = new Book.BL.StockCheckDetailManager();
        private IList<Model.StockCheckDetail> stockCheckDetailList = new List<Model.StockCheckDetail>();
        private BL.DepotPositionManager depotPositionManager = new Book.BL.DepotPositionManager();
        private BL.StockManager _stockManager = new Book.BL.StockManager();

        public DepotPositionAndNumsForm()
        {
            InitializeComponent();
        }


        public DepotPositionAndNumsForm(Model.StockCheckDetail stockCheckDetail)
        {
            InitializeComponent();
            this._stockCheckDetail = stockCheckDetail;
        }

        private void DepotPositionAndNums_Load(object sender, EventArgs e)
        {
            IList<Model.DepotPosition> list = depotPositionManager.Select(this._stockCheckDetail.Depot);
            if (list.Count == 0) return;
            foreach (Model.DepotPosition item in list)
            {
                Model.StockCheckDetail checkdetail = new Book.Model.StockCheckDetail();
                checkdetail.StockCheckId = this._stockCheckDetail.StockCheckId;
                checkdetail.StockCheckDetailId = Guid.NewGuid().ToString();
                checkdetail.Depot = this._stockCheckDetail.Depot;
                checkdetail.DepotId = this._stockCheckDetail.DepotId;
                checkdetail.Product = this._stockCheckDetail.Product;
                checkdetail.ProductId = this._stockCheckDetail.ProductId;
                checkdetail.DepotPosition = item;
                checkdetail.DepotPositionId = item.DepotPositionId;
                checkdetail.StockCheckQuantity = null;
                checkdetail.ProductUnitName = this._stockCheckDetail.Product.DepotUnit.CnName;

                if (EditForm.dic.ContainsKey(checkdetail.ProductId + checkdetail.DepotPositionId))
                {
                    Model.StockCheckDetail d = EditForm.dic[checkdetail.ProductId + checkdetail.DepotPositionId];
                    checkdetail.StockCheckQuantity = d.StockCheckQuantity;
                }
                //else
                //{
                //    Model.Stock tempstock = this._stockManager.GetStockByProductIdAndDepotPositionId(checkdetail.ProductId, checkdetail.DepotPositionId);
                //    if (tempstock != null)
                //    {
                //        if (tempstock.StockQuantity1 != null)
                //        {
                //            checkdetail.StockCheckQuantity = tempstock.StockQuantity1.Value;
                //            if (!EditForm.dic.ContainsKey(checkdetail.ProductId + checkdetail.DepotPositionId))
                //                EditForm.dic.Add(checkdetail.ProductId + checkdetail.DepotPositionId, checkdetail);
                //        }
                //    }
                else
                    checkdetail.StockCheckQuantity = null;
                //}

                this.stockCheckDetailList.Add(checkdetail);
            }

            this.bindingSourceStockCheckDetail.DataSource = this.stockCheckDetailList;
            this.gridControl1.RefreshDataSource();
        }

        private void sbtn_Setting_Click(object sender, EventArgs e)
        {
            stockCheckDetailList = this.bindingSourceStockCheckDetail.DataSource as IList<Model.StockCheckDetail>;
            double nums = 0;

            //IList<Model.StockCheckDetail> templist = this._stockCheckDetailManager.SelectByProductId(this._stockCheckDetail.ProductId);

            foreach (Model.StockCheckDetail item in stockCheckDetailList)
            {
                if (item.StockCheckQuantity != null)
                {
                    nums += item.StockCheckQuantity.Value;
                    if (EditForm.dic.ContainsKey(item.ProductId + item.DepotPositionId))
                    {
                        EditForm._stockCheck.ProductPositionNums.Remove(EditForm.dic[item.ProductId + item.DepotPositionId]);
                        EditForm._stockCheck.ProductPositionNums.Add(item);
                        EditForm.dic.Remove(item.ProductId + item.DepotPositionId);
                        EditForm.dic.Add(item.ProductId + item.DepotPositionId, item);
                    }
                    else
                    {
                        EditForm._stockCheck.ProductPositionNums.Add(item);
                        EditForm.dic.Add(item.ProductId + item.DepotPositionId, item);
                    }
                }
                else
                {
                    //if(EditForm.dic.ContainsKey(item.ProductId + item.DepotPositionId))

                    //Model.StockCheckDetail temdetal = this._stockCheckDetailManager.SelectByProductIdAndPositionId(item.DepotPositionId, item.ProductId);
                    //if (temdetal != null && temdetal.StockCheckQuantity != null)
                    //{
                    if (EditForm.dic.ContainsKey(item.ProductId + item.DepotPositionId))
                    {
                        EditForm._stockCheck.ProductPositionNums.Remove(EditForm.dic[item.ProductId + item.DepotPositionId]);
                        EditForm._stockCheck.ProductPositionNums.Add(item);
                        EditForm.dic.Remove(item.ProductId + item.DepotPositionId);
                        EditForm.dic.Add(item.ProductId + item.DepotPositionId, item);
                    }
                    //    else
                    //    {
                    //        EditForm._stockCheck.ProductPositionNums.Add(item);
                    //        EditForm.dic.Add(item.ProductId + item.DepotPositionId, item);
                    //    }
                    //}
                }
            }

            EditForm.SetNums = nums;
            this.DialogResult = DialogResult.OK;
        }

        private void sbtn_Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
