using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.produceManager.MouldCategory
{
    public partial class ProductCategory : Settings.BasicData.BaseEditForm
    {
        IList<Model.ProductMouldCategory> _productMouldCategoryList = new List<Model.ProductMouldCategory>();
        Model.ProductMouldCategory _productMouldCategory;
        BL.ProductMouldCategoryManager _manage = new Book.BL.ProductMouldCategoryManager();

        public ProductCategory()
        {
            InitializeComponent();

            this.invalidValueExceptions.Add(Model.ProductMouldCategory.PRO_Id, new AA(Properties.Resources.NumsIsNotNull, this.txt_Id));
            this.invalidValueExceptions.Add(Model.ProductMouldCategory.PRO_CategoryName, new AA(Properties.Resources.NameIsNotNull, this.txt_Name));

            this.action = "view";
        }

        int sign = 0;
        public ProductCategory(string id, string name)
            : this()
        {
            this.txt_Id.Text = id;
            this.txt_Name.Text = name;
            this.action = "insert";
        }

        protected override void AddNew()
        {
            this._productMouldCategory = new Book.Model.ProductMouldCategory();
            this._productMouldCategory.ProductMouldCategoryId = Guid.NewGuid().ToString();
            this.action = "insert";
        }

        protected override void MoveNext()
        {
            Model.ProductMouldCategory model = this._manage.GetNext(this._productMouldCategory);
            if (model == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);
            this._productMouldCategory = model;
        }

        protected override void MovePrev()
        {
            Model.ProductMouldCategory model = this._manage.GetPrev(this._productMouldCategory);
            if (model == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);
            this._productMouldCategory = model;
        }

        protected override void MoveFirst()
        {
            this._productMouldCategory = this._manage.GetFirst();
        }

        protected override void MoveLast()
        {
            if (this.action == "view")
                this._productMouldCategory = this._manage.GetLast();
        }

        protected override bool HasRows()
        {
            return this._manage.HasRows();
        }

        protected override bool HasRowsNext()
        {
            return this._manage.HasRowsAfter(this._productMouldCategory);
        }

        protected override bool HasRowsPrev()
        {
            return this._manage.HasRowsBefore(this._productMouldCategory);
        }

        protected override void Delete()
        {
            if (this._productMouldCategory == null)
                return;
            if (MessageBox.Show(Properties.Resources.ConfirmToDelete, this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                return;
            this._manage.Delete(this._productMouldCategory.ProductMouldCategoryId);
            this._productMouldCategory = this._manage.GetNext(this._productMouldCategory);
            if (this._productMouldCategory == null)
                this._productMouldCategory = this._manage.GetLast();
        }

        protected override void Save()
        {
            this._productMouldCategory.Id = this.txt_Id.Text;
            this._productMouldCategory.CategoryName = this.txt_Name.Text;
            this._productMouldCategory.CategoryDes = this.memoEditDes.EditValue == null ? null : this.memoEditDes.EditValue.ToString();

            switch (this.action)
            {
                case "insert":
                    this._manage.Insert(this._productMouldCategory);
                    break;
                case "update":
                    this._manage.Update(this._productMouldCategory);
                    break;
            }
        }

        public override void Refresh()
        {
            if (this.action == "view")
            {
                if (this._productMouldCategory == null)
                    this.AddNew();

                this.txt_Id.EditValue = this._productMouldCategory.Id;
                this.txt_Name.EditValue = this._productMouldCategory.CategoryName;
                this.memoEditDes.EditValue = this._productMouldCategory.CategoryDes;
            }
            this.bindingSource1.DataSource = this._manage.Select();
            this.gridControl1.RefreshDataSource();

            base.Refresh();
        }

        private void gridView1_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            this._productMouldCategory = this.bindingSource1.Current as Model.ProductMouldCategory;
            if (this._productMouldCategory != null)
            {
                this.action = "view";
                this.txt_Id.Text = this._productMouldCategory.Id;
                this.txt_Name.Text = this._productMouldCategory.CategoryName;
                this.memoEditDes.Text = this._productMouldCategory.CategoryDes;
                this.Refresh();
            }
        }

    }
}