using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Book.UI.Settings.StockLimitations
{
    public partial class StockEditorSettingForm : Form
    {
        private IList<Model.StockEditorDetal> stockList = new List<Model.StockEditorDetal>();
        private double nums = 0;
        private BL.DepotPositionManager depotPositionManager = new Book.BL.DepotPositionManager();
        private BL.StockManager _stockManager = new Book.BL.StockManager();
        private BL.StockEditorDetalManager _stockEditorDetailManager = new Book.BL.StockEditorDetalManager();

        private Model.StockEditorDetal _stockEditorDetail;
        private string _depotId;
        public StockEditorSettingForm(Model.StockEditorDetal stockEditordetail, string depotId)
        {
            InitializeComponent();
            this._stockEditorDetail = stockEditordetail;
            this._depotId = depotId;
        }

        private void StockEditorSettingForm_Load(object sender, EventArgs e)
        {
            IList<Model.DepotPosition> list = depotPositionManager.Select(this._depotId);
            if (list.Count == 0) return;
            foreach (Model.DepotPosition item in list)
            {
                Model.StockEditorDetal temp = this._stockEditorDetailManager.SelectByProductIdAndPositionIdAndStockHId(this._stockEditorDetail.ProductId, item.DepotPositionId, this._stockEditorDetail.StockEditorId);
                if (temp == null)
                {
                    temp = new Book.Model.StockEditorDetal();
                    temp.StockEditorDetalId = Guid.NewGuid().ToString();
                    temp.StockEditorId = this._stockEditorDetail.StockEditorId;
                    temp.DepotPositionId = item.DepotPositionId;
                    temp.ProductId = this._stockEditorDetail.ProductId;
                    temp.Product = this._stockEditorDetail.Product;
                    temp.DepotPosition = item;
                    temp.StockQuantity = this._stockManager.GetStockByProductIdAndDepotPositionId(this._stockEditorDetail.ProductId, item.DepotPositionId) == null ? 0 : this._stockManager.GetStockByProductIdAndDepotPositionId(this._stockEditorDetail.ProductId, item.DepotPositionId).StockQuantity1;
                    temp.Directions = this._stockEditorDetail.Directions;
                    temp.ProductUnitName = this._stockEditorDetail.ProductUnitName;
                    temp.StockEditorQuantity = null;
                }
                if (StockEditorForm.dic.ContainsKey(this._stockEditorDetail.ProductId + item.DepotPositionId))
                    temp.StockEditorQuantity = StockEditorForm.dic[this._stockEditorDetail.ProductId + item.DepotPositionId].StockEditorQuantity;
                this.stockList.Add(temp);
            }
            this.bindingSourceStockEditorDetail.DataSource = this.stockList;
            this.gridControl1.RefreshDataSource();
        }

        private void simpleButton_Sure_Click(object sender, EventArgs e)
        {
            this.stockList = this.bindingSourceStockEditorDetail.DataSource as List<Model.StockEditorDetal>;
            double? nums = 0;
            foreach (Model.StockEditorDetal item in this.stockList)
            {
                if (!item.StockEditorQuantity.HasValue)
                {
                    if (StockEditorForm.dic.ContainsKey(item.ProductId + item.DepotPositionId))
                    {
                        StockEditorForm._stockEditor.ProductPositionNums.Remove(StockEditorForm.dic[item.ProductId + item.DepotPositionId]);
                        StockEditorForm.dic.Remove(this._stockEditorDetail.ProductId + item.DepotPositionId);
                    }
                    this._stockEditorDetailManager.Delete(item.StockEditorDetalId);
                }
                else
                {
                    nums += item.StockEditorQuantity.Value;
                    if (StockEditorForm.dic.ContainsKey(item.ProductId + item.DepotPositionId))
                    {
                        if (item.StockEditorQuantity != StockEditorForm.dic[this._stockEditorDetail.ProductId + item.DepotPositionId].StockEditorQuantity)
                        {
                            StockEditorForm._stockEditor.ProductPositionNums.Remove(StockEditorForm.dic[item.ProductId + item.DepotPositionId]);
                            StockEditorForm._stockEditor.ProductPositionNums.Add(item);
                            StockEditorForm.dic.Remove(this._stockEditorDetail.ProductId + item.DepotPositionId);
                            StockEditorForm.dic.Add(this._stockEditorDetail.ProductId + item.DepotPositionId, item);
                        }
                    }
                    else
                    {
                        //if (item.StockEditorQuantity.Value != 0)
                        //{
                        StockEditorForm._stockEditor.ProductPositionNums.Add(item);
                        StockEditorForm.dic.Add(this._stockEditorDetail.ProductId + item.DepotPositionId, item);
                        //}

                    }
                }
            }
            if (nums == 0)
                nums = null;
            StockEditorForm.SetNums = nums;
            this.DialogResult = DialogResult.OK;
        }

        private void simpleButton_Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
