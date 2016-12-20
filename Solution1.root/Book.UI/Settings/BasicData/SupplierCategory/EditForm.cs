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

namespace Book.UI.Settings.BasicData.SupplierCategory
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
        private Model.SupplierCategory _supplierCategory;

        private BL.SupplierCategoryManager supplierCategoryManager = new Book.BL.SupplierCategoryManager();  
        public EditForm()
        {
            InitializeComponent();

            this.requireValueExceptions.Add(Model.SupplierCategory.PROPERTY_ID, new AA(Properties.Resources.RequireDataForId, this.textEditId));
            this.requireValueExceptions.Add(Model.SupplierCategory.PROPERTY_SUPPLIERCATEGORYNAME, new AA(Properties.Resources.RequireDataForDepotName, this.textEditSupplierCategoryName));

            this.invalidValueExceptions.Add(Model.SupplierCategory.PROPERTY_ID, new AA(Properties.Resources.EntityExists, this.textEditId));
            this.invalidValueExceptions.Add("DeleteError", new AA(Properties.Resources.DeleteError, this));
        }

        #region Override

        protected override void AddNew()
        {
            this._supplierCategory = new Model.SupplierCategory();
            //this._supplierCategory.SupplierCategoryId = this.supplierCategoryManager.GetId();
        }

        protected override void Delete()
        {
            if (this._supplierCategory == null)
                return;
            if (MessageBox.Show(Properties.Resources.ConfirmToDelete, this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                return;
            this.supplierCategoryManager.Delete(this._supplierCategory.SupplierCategoryId);
            this._supplierCategory = this.supplierCategoryManager.GetNext(this._supplierCategory);
            if (this._supplierCategory == null)
            {
                this._supplierCategory = this.supplierCategoryManager.GetLast();
            }
        }

        protected override bool HasRows()
        {
            return this.supplierCategoryManager.HasRows();
        }

        protected override bool HasRowsNext()
        {
            return this.supplierCategoryManager.HasRowsAfter(this._supplierCategory);
        }

        protected override bool HasRowsPrev()
        {
            return this.supplierCategoryManager.HasRowsBefore(this._supplierCategory);
        }

        protected override void IMECtrl()
        {
            Book.UI.Tools.IMEControl.IMECtrl(new Control[] { this, this.textEditId, this.textEditSupplierCategoryName, this.memoEditSupplierCategoryDescription });
        }

        protected override void MoveFirst()
        {
            this._supplierCategory = this.supplierCategoryManager.GetFirst();
        }

        protected override void MoveLast()
        {
            this._supplierCategory = this.supplierCategoryManager.GetLast();
        }

        protected override void MoveNext()
        {
            Model.SupplierCategory unitGroup = this.supplierCategoryManager.GetNext(this._supplierCategory);
            if (unitGroup == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);

            this._supplierCategory = unitGroup;
        }

        protected override void MovePrev()
        {
            Model.SupplierCategory unitGroup = this.supplierCategoryManager.GetPrev(this._supplierCategory);
            if (unitGroup == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);

            this._supplierCategory = unitGroup;
        }


        public override void Refresh()
        {
            if (this._supplierCategory == null)
            {
                this._supplierCategory = new Book.Model.SupplierCategory();
                this.action = "insert";
            }

            this.bindingSourceSupplierCategory.DataSource = this.supplierCategoryManager.Select();
            this.textEditId.Text =(string.IsNullOrEmpty( this._supplierCategory.Id)?this._supplierCategory.SupplierCategoryId:this._supplierCategory.Id);
            this.textEditSupplierCategoryName.EditValue = this._supplierCategory.SupplierCategoryName;
            this.memoEditSupplierCategoryDescription.Text = this._supplierCategory.SupplierCategoryDescription;

            switch (this.action)
            {
                case "insert":
                    this.textEditId.Properties.ReadOnly = false;
                    this.textEditSupplierCategoryName.Properties.ReadOnly = false;
                    this.memoEditSupplierCategoryDescription.Properties.ReadOnly = false;
                    break;

                case "update":             
                    this.textEditId.Properties.ReadOnly = false ;
                    this.textEditSupplierCategoryName.Properties.ReadOnly = false;
                    this.memoEditSupplierCategoryDescription.Properties.ReadOnly = false;
                    break;

                case "view":
                    this.textEditId.Properties.ReadOnly = true;
                    this.textEditSupplierCategoryName.Properties.ReadOnly = true;
                    this.memoEditSupplierCategoryDescription.Properties.ReadOnly = true;
                    break;
                default:
                    break;
            }
            base.Refresh();
        }

        protected override void Save()
        {
            this._supplierCategory.Id = this.textEditId.Text;         
            this._supplierCategory.SupplierCategoryName = this.textEditSupplierCategoryName.Text;
            this._supplierCategory.SupplierCategoryDescription = this.memoEditSupplierCategoryDescription.Text;
            switch (this.action)
            {
                case "insert":
                    this.supplierCategoryManager.Insert(this._supplierCategory);
                    break;
                case "update":
                    this.supplierCategoryManager.Update(this._supplierCategory);
                    break;
                default:
                    break;                    
            }
        }

        #endregion

        private void EditForm_Load(object sender, EventArgs e)
        {

        }

        private void gridView1_Click(object sender, EventArgs e)
        {
            GridView view = sender as GridView;
            GridHitInfo hitInfo = view.CalcHitInfo(view.GridControl.PointToClient(Cursor.Position));
            if (hitInfo.InRow && !view.IsGroupRow(hitInfo.RowHandle))
            {
                Model.SupplierCategory supplier = this.bindingSourceSupplierCategory.Current as Model.SupplierCategory;
                if (supplier != null)
                {
                    this._supplierCategory = supplier;
                    this.action = "view";
                    this.Refresh();
                }
            }
        }
    }
}