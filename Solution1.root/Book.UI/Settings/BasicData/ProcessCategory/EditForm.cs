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

namespace Book.UI.Settings.BasicData.ProcessCategory
{
    /*----------------------------------------------------------------
   // Copyright (C) 2008 - 2010  咸陽飛馳軟件有限公司
   //                     版權所有 圍著必究
   // 功能描述: 
   // 文 件 名：EditForm
   // 编 码 人: 茍波濤                   完成时间:2009-10-29
   // 修改原因：
   // 修 改 人:                          修改时间:
   // 修改原因：
   // 修 改 人:                          修改时间:
   //----------------------------------------------------------------*/
    public partial class EditForm : BaseEditForm1
    {
        IList<Model.ProcessCategory> _detail = new List<Model.ProcessCategory>();
        
        public Model.ProcessCategory SelectItem = new Book.Model.ProcessCategory();

        private BL.ProcessCategoryManager processCategoryManager = new Book.BL.ProcessCategoryManager();

        public EditForm()
        {
            InitializeComponent();

            this.requireValueExceptions.Add(Model.ProcessCategory.PROPERTY_ID, new AA("請填寫編號！", this.gridControl1 as Control));
            this.requireValueExceptions.Add(Model.ProcessCategory.PROPERTY_PROCESSCATEGORYNAME, new AA(Properties.Resources.RequireDataForNames, this.gridControl1 as Control));


            this.invalidValueExceptions.Add(Model.ProcessCategory.PROPERTY_ID, new AA(Properties.Resources.EntityExists, this.gridControl1 as Control));

            this.invalidValueExceptions.Add(Model.ProcessCategory.PROPERTY_PROCESSCATEGORYNAME, new AA(Properties.Resources.EntityName, this.gridControl1 as Control));
            this.action = "view";
          this.invalidValueExceptions.Add("DeleteError", new AA(Properties.Resources.DeleteError, this));
        }

        protected override void Delete()
        {
            if (MessageBox.Show(Properties.Resources.ConfirmToDelete, this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                return;
           // try
            {
                this.processCategoryManager.Delete(this.bindingSource1.Current as Model.ProcessCategory);
            }
            //catch
            //{
            //    MessageBox.Show(Properties.Resources.DeleteError, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }

        protected override void Save()
        {
            switch (this.action)
            {
                case "insert":
                    this.processCategoryManager.Update(this._detail);
                    break;
                case "update":
                    this.processCategoryManager.Update(this._detail);
                    break;
                default:
                    break;
            }
        }
        
        public override void Refresh()
        {
            this._detail = this.processCategoryManager.Select();
            this.bindingSource1.DataSource = this._detail;
            if (this.action == "insert")
            {
                Model.ProcessCategory cate = new Book.Model.ProcessCategory();
                cate.ProcessCategoryId = Guid.NewGuid().ToString();
                cate.ProcessCategoryName = string.Empty;
                //cate.Description = string.Empty;
                cate.Id = string.Empty;
                this._detail.Add(cate);
                this.bindingSource1.Position = this.bindingSource1.IndexOf(cate);
                this.gridControl1.RefreshDataSource();
            }
            switch (this.action)
            {
                case "insert":
                    this.gridView1.OptionsBehavior.Editable = true;
                    break;
                case "update":
                    this.gridView1.OptionsBehavior.Editable = true;
                    break;
                case "view":
                    this.gridView1.OptionsBehavior.Editable = false;
                    break;
                default:
                    break;
            }
            base.Refresh();
        }
        
        protected override void grid_keyDpwn()
        {
            Model.ProcessCategory cate = new Book.Model.ProcessCategory();
            cate.ProcessCategoryId = Guid.NewGuid().ToString();
            cate.ProcessCategoryName = string.Empty;
            cate.Id = string.Empty;
            this._detail.Add(cate);
            this.bindingSource1.Position = this.bindingSource1.IndexOf(cate);      
        }
        
        protected override void grid_KeyDelete()
        {
            this._detail.Remove(this.bindingSource1.Current as Model.ProcessCategory);
        }

        private void gridView1_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            Model.ProcessCategory cate = this.gridView1.GetRow(e.RowHandle) as Model.ProcessCategory;
            if (e.Value == null) return;
            switch (e.Column.Name)
            {
                case "gridColumn1":
                    cate.Id = e.Value.ToString();
                    break;
                case "gridColumn2":
                    cate.ProcessCategoryName = e.Value.ToString();
                    break;
                
            }
        }

        private void bindingSource1_CurrentChanged(object sender, EventArgs e)
        {
            this.SelectItem = this.bindingSource1.Current as Model.ProcessCategory;
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
    }
}

