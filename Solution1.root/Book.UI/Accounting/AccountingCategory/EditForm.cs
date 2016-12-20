using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data; 
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Book.UI.Settings.BasicData;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
namespace Book.UI.Accounting.AccountingCategory
{
    public partial class EditForm : BaseEditForm
    {
        Model.AtAccountingCategory AtAccountingCategory;
        BL.AtAccountingCategoryManager AtAccountingCategoryManager = new Book.BL.AtAccountingCategoryManager();
        public EditForm()
        {
            InitializeComponent();
            this.requireValueExceptions.Add(Model.AtAccountingCategory.PRO_Id, new AA("請輸入類別編號", this.textEditAccountingCategoryId));
            this.requireValueExceptions.Add(Model.AtAccountingCategory.PRO_AccountingCategoryName, new AA("請輸入類別名稱", this.textEditAccountingCategoryName));
            this.requireValueExceptions.Add(Model.AtAccountingCategory.PRO_AccountingCategoriesId, new AA("請選擇總類", this.newChooseContorlAccountingCategoriesId));
            this.newChooseContorlAccountingCategoriesId.Choose = new Accounting.AccountingCategories.ChooseAccountCategories();
        }
        #region Override
        protected override void AddNew()
        {
            this.AtAccountingCategory = new Model.AtAccountingCategory();
        }
        protected override void Save()
        {
            this.AtAccountingCategory.AccountingCategories= this.newChooseContorlAccountingCategoriesId.EditValue as Model.AtAccountingCategories;
            if (this.AtAccountingCategory.AccountingCategories != null)
            {
                this.AtAccountingCategory.AccountingCategoriesId = this.AtAccountingCategory.AccountingCategories.AccountingCategoriesId;
            }
            this.AtAccountingCategory.Id = this.textEditAccountingCategoryId.Text;
            this.AtAccountingCategory.AccountingCategoryName = this.textEditAccountingCategoryName.Text;
            this.AtAccountingCategory.EnglishName = this.textEditEnglishName.Text;
            switch (this.action)
            {
                case "insert":
                    this.AtAccountingCategoryManager.Insert(this.AtAccountingCategory);
                    break;
                case "update":
                    this.AtAccountingCategoryManager.Update(this.AtAccountingCategory);
                    break;
                default:
                    break;
            }
        }
        public override void Refresh()
        {

            if (this.AtAccountingCategory == null)
            {
                this.AtAccountingCategory = new Book.Model.AtAccountingCategory();
                this.action = "insert";
            }
            this.bindingSource1.DataSource = this.AtAccountingCategoryManager.Select();
            this.newChooseContorlAccountingCategoriesId.EditValue = this.AtAccountingCategory.AccountingCategories;
            this.textEditAccountingCategoryId.Text = this.AtAccountingCategory.Id;
            this.textEditAccountingCategoryName.Text = this.AtAccountingCategory.AccountingCategoryName;
            this.textEditEnglishName.Text = this.AtAccountingCategory.EnglishName;
            switch (this.action)
            {
                case "insert":
                    this.newChooseContorlAccountingCategoriesId.ShowButton = true;
                    this.newChooseContorlAccountingCategoriesId.ButtonReadOnly = false;
                    this.textEditAccountingCategoryId.Properties.ReadOnly = false;
                    this.textEditAccountingCategoryName.Properties.ReadOnly = false;
                    this.textEditEnglishName.Properties.ReadOnly = false;
                    break;

                case "update":
                    this.newChooseContorlAccountingCategoriesId.ShowButton = true;
                    this.newChooseContorlAccountingCategoriesId.ButtonReadOnly = false;
                    this.textEditAccountingCategoryId.Properties.ReadOnly = false;
                    this.textEditAccountingCategoryName.Properties.ReadOnly = false;
                    this.textEditEnglishName.Properties.ReadOnly = false;
                    break;

                case "view":
                    this.newChooseContorlAccountingCategoriesId.ShowButton = false;
                    this.newChooseContorlAccountingCategoriesId.ButtonReadOnly = true;
                    this.textEditAccountingCategoryId.Properties.ReadOnly = true;
                    this.textEditAccountingCategoryName.Properties.ReadOnly = true;
                    this.textEditEnglishName.Properties.ReadOnly = true;
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
            if (this.AtAccountingCategory == null)
                return;
            if (MessageBox.Show(Properties.Resources.ConfirmToDelete, this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                return;
            try
            {
                this.AtAccountingCategoryManager.Delete(this.AtAccountingCategory.AccountingCategoryId);
                this.AtAccountingCategory = this.AtAccountingCategoryManager.GetNext(this.AtAccountingCategory);
                if (this.AtAccountingCategory == null)
                {
                    this.AtAccountingCategory = this.AtAccountingCategoryManager.GetLast();
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
            this.AtAccountingCategory = this.AtAccountingCategoryManager.GetFirst();
        }
        protected override void MovePrev()
        {
            Model.AtAccountingCategory AtAccountingCategory = this.AtAccountingCategoryManager.GetPrev(this.AtAccountingCategory);
            if (AtAccountingCategory == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);

            this.AtAccountingCategory = AtAccountingCategory;
        }
        protected override void MoveLast()
        {
            this.AtAccountingCategory = this.AtAccountingCategoryManager.GetLast();
        }
        protected override void MoveNext()
        {
            Model.AtAccountingCategory AtAccountingCategory = this.AtAccountingCategoryManager.GetNext(this.AtAccountingCategory);
            if (AtAccountingCategory == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);

            this.AtAccountingCategory = AtAccountingCategory;
        }
        protected override bool HasRows()
        {
            return this.AtAccountingCategoryManager.HasRows();
        }
        protected override bool HasRowsNext()
        {
            return this.AtAccountingCategoryManager.HasRowsAfter(this.AtAccountingCategory);
        }
        protected override bool HasRowsPrev()
        {
            return this.AtAccountingCategoryManager.HasRowsBefore(this.AtAccountingCategory);
        }
        protected override void IMECtrl()
        {
            Book.UI.Tools.IMEControl.IMECtrl(new Control[] { this.textEditAccountingCategoryId, this.newChooseContorlAccountingCategoriesId,this.textEditAccountingCategoryName,this.textEditEnglishName, this });
        }
        #endregion

        private void gridView1_Click(object sender, EventArgs e)
        {
            GridView view = sender as GridView;
            GridHitInfo hitInfo = view.CalcHitInfo(view.GridControl.PointToClient(Cursor.Position));
            if (hitInfo.InRow && !view.IsGroupRow(hitInfo.RowHandle))
            {
                Model.AtAccountingCategory productEpiboly = this.bindingSource1.Current as Model.AtAccountingCategory;
                if (productEpiboly != null)
                {
                    this.AtAccountingCategory = productEpiboly;
                    this.action = "view";
                    this.Refresh();
                }
            }
        }

    }
}