using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;

namespace Book.UI.Settings.BasicData.TradeCategory
{
    /*----------------------------------------------------------------
   // Copyright (C) 2008 - 2010  咸陽飛馳軟件有限公司
   //                     版權所有 圍著必究
   // 功能描述: 
   // 文 件 名：EditForm
   // 编 码 人: 茍波濤                   完成时间:2009-11-07
   // 修改原因：
   // 修 改 人:                          修改时间:
   // 修改原因：
   // 修 改 人:                          修改时间:
   //----------------------------------------------------------------*/
    public partial class EditForm : BaseEditForm
    {

        #region gbt---定義變量對象
        private Model.TradeCategory _tradeCategory;
        private BL.TradeCategoryManager tradeCategoryManager = new Book.BL.TradeCategoryManager();
        #endregion

        #region gbt---無慘構造函數
        public EditForm()
        {
            InitializeComponent();

            this.requireValueExceptions.Add(Model.TradeCategory.PROPERTY_ID, new AA(Properties.Resources.RequireDataForId, this.textEditId));
            this.requireValueExceptions.Add(Model.TradeCategory.PROPERTY_TRADECATEGORYNAME, new AA(Properties.Resources.RequireDataForDepotName, this.textEditTradeCategoryName));            

            this.invalidValueExceptions.Add(Model.TradeCategory.PROPERTY_ID, new AA(Properties.Resources.EntityExists, this.textEditId));
            this.invalidValueExceptions.Add("DeleteError", new AA(Properties.Resources.DeleteError, this));
        }
        #endregion

        #region gbt---重載基類的方法

        protected override void AddNew()
        {
            this._tradeCategory = new Model.TradeCategory();
            //this._tradeCategory.TradeCategoryId = this.tradeCategoryManager.GetId();
        }

        protected override void Delete()
        {
            if (this._tradeCategory == null)
                return;
            if (MessageBox.Show(Properties.Resources.ConfirmToDelete, this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                return;
            this.tradeCategoryManager.Delete(this._tradeCategory.TradeCategoryId);
            this._tradeCategory = this.tradeCategoryManager.GetNext(this._tradeCategory);
            if (this._tradeCategory == null)
            {
                this._tradeCategory = this.tradeCategoryManager.GetLast();
            }
        }

        protected override bool HasRows()
        {
            return this.tradeCategoryManager.HasRows();
        }

        protected override bool HasRowsNext()
        {
            return this.tradeCategoryManager.HasRowsAfter(this._tradeCategory);
        }

        protected override bool HasRowsPrev()
        {
            return this.tradeCategoryManager.HasRowsBefore(this._tradeCategory);
        }

        protected override void IMECtrl()
        {
            Book.UI.Tools.IMEControl.IMECtrl(new Control[] { this, this.textEditId, this.textEditTradeCategoryName,this.memoEditDescription });
        }

        protected override void MoveFirst()
        {
            this._tradeCategory = this.tradeCategoryManager.GetFirst();
        }

        protected override void MoveLast()
        {
            this._tradeCategory = this.tradeCategoryManager.GetLast();
        }

        protected override void MoveNext()
        {
            Model.TradeCategory unitGroup = this.tradeCategoryManager.GetNext(this._tradeCategory);
            if (unitGroup == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);

            this._tradeCategory = unitGroup;
        }

        protected override void MovePrev()
        {
            Model.TradeCategory unitGroup = this.tradeCategoryManager.GetPrev(this._tradeCategory);
            if (unitGroup == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);

            this._tradeCategory = unitGroup;
        }


        public override void Refresh()
        {
            if (this._tradeCategory == null)
            {
                this._tradeCategory = new Book.Model.TradeCategory();
                this.action = "insert";
            }

            this.bindingSourceTradeCategory.DataSource = this.tradeCategoryManager.Select();

            this.textEditId.EditValue = string.IsNullOrEmpty(this._tradeCategory.Id)?this._tradeCategory.TradeCategoryId:this._tradeCategory.Id;
            this.textEditTradeCategoryName.EditValue = this._tradeCategory.TradeCategoryName;
            this.memoEditDescription.Text = this._tradeCategory.Description;

            switch (this.action)
            {
                case "insert":
                    this.textEditId.Properties.ReadOnly = false;
                    this.textEditTradeCategoryName.Properties.ReadOnly = false;
                    this.memoEditDescription.Properties.ReadOnly = false;
                    break;

                case "update":
                   // oldId = this.textEditId.EditValue.ToString();
                    this.textEditId.Properties.ReadOnly = false;
                    this.textEditTradeCategoryName.Properties.ReadOnly = false;
                    this.memoEditDescription.Properties.ReadOnly = false;
                    break;

                case "view":
                    this.textEditId.Properties.ReadOnly = true;
                    this.textEditTradeCategoryName.Properties.ReadOnly = true;
                    this.memoEditDescription.Properties.ReadOnly = true;
                    break;
                default:
                    break;
            }
            base.Refresh();
        }

        protected override void Save()
        {
            this._tradeCategory.Id = this.textEditId.Text;
            this._tradeCategory.TradeCategoryName = this.textEditTradeCategoryName.Text;
            this._tradeCategory.Description = this.memoEditDescription.Text;
            switch (this.action)
            {
                case "insert":
                    this.tradeCategoryManager.Insert(this._tradeCategory);
                    break;
                case "update":                    
                    this.tradeCategoryManager.Update(this._tradeCategory);
                    break;
                default:
                    break;
            }
        }

        #endregion

        private void EditForm_Load(object sender, EventArgs e)
        {

        }

        #region gbt---gridview 的點擊事件
        private void gridView1_Click(object sender, EventArgs e)
        {
            GridView view = sender as GridView;
            GridHitInfo hitInfo = view.CalcHitInfo(view.GridControl.PointToClient(Cursor.Position));
            if (hitInfo.InRow && !view.IsGroupRow(hitInfo.RowHandle))
            {
                Model.TradeCategory trade = this.bindingSourceTradeCategory.Current as Model.TradeCategory;
                if (trade != null)
                {
                    this._tradeCategory = trade;
                    this.action = "view";
                    this.Refresh();
                }
            }
        }
        #endregion

    }
}