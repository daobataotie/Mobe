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
namespace Book.UI.Accounting.CurrencyCategory
{
    public partial class CategoryEditForm : BaseEditForm
    {
        Model.AtCurrencyCategory AtCurrencyCategory;
        BL.AtCurrencyCategoryManager AtCurrencyCategoryManager = new Book.BL.AtCurrencyCategoryManager();
        public CategoryEditForm()
        {
            InitializeComponent();
            this.requireValueExceptions.Add(Model.AtCurrencyCategory.PRO_Id, new AA("請輸入種類編號", this.textEditId));
            this.requireValueExceptions.Add(Model.AtCurrencyCategory.PRO_AtCurrencyName, new AA("請輸入種類名稱", this.textEditName));
        }
        #region Override
        protected override void AddNew()
        {
            this.AtCurrencyCategory = new Model.AtCurrencyCategory();
        }
        protected override DevExpress.XtraReports.UI.XtraReport GetReport()
        {
            return new Accounting.CurrencyCategory.XO();
        }
        protected override void  Save()
        {
            this.AtCurrencyCategory.Id = this.textEditId.Text; 
            this.AtCurrencyCategory.AtCurrencyName = this.textEditName.Text;
            this.AtCurrencyCategory.EnglishName = this.textEditEnglishName.Text;
         
            switch (this.action)
            {
                case "insert":
                    this.AtCurrencyCategoryManager.Insert(this.AtCurrencyCategory);
                    break;
                case "update":
                    this.AtCurrencyCategoryManager.Update(this.AtCurrencyCategory);
                    break;
                default:
                    break;
            }
        }
        public override void Refresh()
        {

            if (this.AtCurrencyCategory == null)
            {
                this.AtCurrencyCategory = new Book.Model.AtCurrencyCategory();
                this.action = "insert";
            }
            this.bindingSource1.DataSource = this.AtCurrencyCategoryManager.Select();
            this.textEditId.Text = this.AtCurrencyCategory.Id;
            this.textEditName.Text = this.AtCurrencyCategory.AtCurrencyName;
            this.textEditEnglishName.Text = this.AtCurrencyCategory.EnglishName;
          
            switch (this.action)
            {
                case "insert":
                    this.textEditId.Properties.ReadOnly = false;
                    this.textEditName.Properties.ReadOnly = false;
                    this.textEditEnglishName.Properties.ReadOnly = false;
                  
                    break;

                case "update":
                    this.textEditId.Properties.ReadOnly = false;
                    this.textEditName.Properties.ReadOnly = false;
                    this.textEditEnglishName.Properties.ReadOnly = false;
                   
                    break;

                case "view":
                    this.textEditId.Properties.ReadOnly = true;
                    this.textEditName.Properties.ReadOnly = true;
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
            if (this.AtCurrencyCategory == null)
                return;
            if (MessageBox.Show(Properties.Resources.ConfirmToDelete, this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                return;
            try
            {
                this.AtCurrencyCategoryManager.Delete(this.AtCurrencyCategory.AtCurrencyCategoryId);
            }
            catch
            {
                throw new Exception("");
            }

            return;

        }
        protected override void IMECtrl()
        {
            Book.UI.Tools.IMEControl.IMECtrl(new Control[] { this.textEditId, this });
        }
        #endregion


        private void gridView1_Click_1(object sender, EventArgs e)
        {
            GridView view = sender as GridView;
            GridHitInfo hitInfo = view.CalcHitInfo(view.GridControl.PointToClient(Cursor.Position));
            if (hitInfo.InRow && !view.IsGroupRow(hitInfo.RowHandle))
            {
                Model.AtCurrencyCategory productEpiboly = this.bindingSource1.Current as Model.AtCurrencyCategory;
                if (productEpiboly != null)
                {
                    this.AtCurrencyCategory = productEpiboly;
                    this.action = "view";
                    this.Refresh();
                }
            }
        }
    }
}