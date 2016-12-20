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

namespace Book.UI.Settings.ProduceManager.ProductEpiboly
{
    /*----------------------------------------------------------------
// Copyright (C) 2008 - 2010  咸阳飛馳軟件有限公司
//                     版權所有 圍著必究

// 编 码 人: 马艳军            完成时间:2009-10-24
// 修改原因：
// 修 改 人:                          修改时间:
// 修改原因：
// 修 改 人:                          修改时间:
//----------------------------------------------------------------*/
    public partial class EditForm : Settings.BasicData.BaseEditForm
    {
        Model.ProductEpiboly productEpiboly;
        BL.ProductEpibolyManager productEpibolyManager = new Book.BL.ProductEpibolyManager();

        #region 构造函数
        public EditForm()
        {
            InitializeComponent();
            this.requireValueExceptions.Add(Model.ProductEpiboly.PROPERTY_PRODUCTID, new AA("請選擇產品", this.newChooseProductId));
            this.requireValueExceptions.Add(Model.ProductEpiboly.PROPERTY_SUPPLIERID, new AA("請選擇供應商", this.newChooseSupplierId));
            this.action = "insert";
        }
        public EditForm(Model.ProductEpiboly productEpiboly)
        {
            this.productEpiboly = productEpiboly;
            this.action = "update";
        }
        public EditForm(Model.ProductEpiboly productEpiboly, string action)
        {
            this.productEpiboly = productEpiboly;
            this.action = action;
        }

        #endregion 
        #region Override
        protected override void AddNew()
        {
            this.productEpiboly = new Model.ProductEpiboly();
        }
        protected override void Save()
        {
            this.productEpiboly.Product = this.newChooseProductId.EditValue as Model.Product;
            if (this.productEpiboly.Product != null)
            {
                this.productEpiboly.ProductId = this.productEpiboly.Product.ProductId;
            }
            this.productEpiboly.Supplier = this.newChooseSupplierId.EditValue as Model.Supplier;
            if (this.productEpiboly.Supplier != null)
            {
                this.productEpiboly.SupplierId = this.productEpiboly.Supplier.SupplierId;
            }
            switch (this.action)
            {
                case "insert":
                    this.productEpibolyManager.Insert(this.productEpiboly);
                    break;
                case "update":
                    this.productEpibolyManager.Update(this.productEpiboly);
                    break;
                default:
                    break;
            }
        }
        public override void Refresh()
        {

            if (this.productEpiboly == null)
            {
                this.productEpiboly = new Book.Model.ProductEpiboly();
                this.action = "insert";
            }
            this.bindingSource1.DataSource = this.productEpibolyManager.Select();
            if (this.productEpiboly.Product != null)
            {
                this.newChooseProductId.EditValue = this.productEpiboly.Product as Model.Product; ;
            }
            if (this.productEpiboly.Supplier != null)
            {
                this.newChooseSupplierId.EditValue = this.productEpiboly.Supplier as Model.Supplier;
            }
            switch (this.action)
            {
                case "insert":
                    this.newChooseProductId.ShowButton = true;
                    this.newChooseProductId.ButtonReadOnly = false;
                    this.newChooseSupplierId.ShowButton = true;
                    this.newChooseSupplierId.ButtonReadOnly = false;

                    break;

                case "update":
                    this.newChooseProductId.ShowButton = true;
                    this.newChooseProductId.ButtonReadOnly = false;
                    this.newChooseSupplierId.ShowButton = true;
                    this.newChooseSupplierId.ButtonReadOnly = false;
                    break;

                case "view":
                    this.newChooseProductId.ShowButton = false;
                    this.newChooseProductId.ButtonReadOnly = true;
                    this.newChooseSupplierId.ShowButton = false;
                    this.newChooseSupplierId.ButtonReadOnly =true;
                    break;
                default:
                    break;
            }
            base.Refresh();
        }
        /// <summary>
        /// 删除
        /// </summary>
        protected override void Delete()
        {
            if (this.productEpiboly == null)
                return;
            if (MessageBox.Show(Properties.Resources.ConfirmToDelete, this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                return;
            try
            {
                this.productEpibolyManager.Delete(this.productEpiboly.ProductEpibolyId);
                this.productEpiboly = this.productEpibolyManager.GetNext(this.productEpiboly);
                if (this.productEpiboly == null)
                {
                    this.productEpiboly = this.productEpibolyManager.GetLast();
                }
            }
            catch
            {
                throw new Exception("");
            }

            return;

        }
        protected override void MoveFirst()
        {
            this.productEpiboly = this.productEpibolyManager.GetFirst();
        }
        protected override void MovePrev()
        {
            Model.ProductEpiboly productEpiboly = this.productEpibolyManager.GetPrev(this.productEpiboly);
            if (productEpiboly == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);

            this.productEpiboly = productEpiboly;
        }
        protected override void MoveLast()
        {
            this.productEpiboly = this.productEpibolyManager.GetLast();
        }
        protected override void MoveNext()
        {
            Model.ProductEpiboly productEpiboly = this.productEpibolyManager.GetNext(this.productEpiboly);
            if (productEpiboly == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);

            this.productEpiboly = productEpiboly;
        }
        protected override bool HasRows()
        {
            return this.productEpibolyManager.HasRows();
        }
        protected override bool HasRowsNext()
        {
            return this.productEpibolyManager.HasRowsAfter(this.productEpiboly);
        }
        protected override bool HasRowsPrev()
        {
            return this.productEpibolyManager.HasRowsBefore(this.productEpiboly);
        }
        protected override void IMECtrl()
        {
            Book.UI.Tools.IMEControl.IMECtrl(new Control[] { this.newChooseSupplierId, this.newChooseProductId, this });
        }
        #endregion


        /// <summary>
        /// gridview单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridView1_Click(object sender, EventArgs e)
        {
            GridView view = sender as GridView;
            GridHitInfo hitInfo = view.CalcHitInfo(view.GridControl.PointToClient(Cursor.Position));
            if (hitInfo.InRow && !view.IsGroupRow(hitInfo.RowHandle))
            {
                Model.ProductEpiboly productEpiboly = this.bindingSource1.Current as Model.ProductEpiboly;
                if (productEpiboly != null)
                {
                    this.productEpiboly = productEpiboly;
                    this.action = "view";
                    this.Refresh();
                }
            }
        }

        /// <summary>
        /// 自定义列显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridView1_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.ListSourceRowIndex < 0) return;
            IList<Model.ProductEpiboly> details = this.bindingSource1.DataSource as IList<Model.ProductEpiboly>;
            if (details == null || details.Count < 1) return;
            Model.Product product = details[e.ListSourceRowIndex].Product;
            Model.Supplier supplier = details[e.ListSourceRowIndex].Supplier;
            if (product == null || supplier == null) return;
            switch (e.Column.Name)
            {
                case "ProductId":
                    e.DisplayText = product.Id;
                    break;
                case "SupplierId":
                    e.DisplayText = supplier.Id;
                    break;
            }
        }
    }
}