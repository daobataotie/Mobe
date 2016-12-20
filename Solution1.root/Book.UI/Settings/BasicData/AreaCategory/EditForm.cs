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

namespace Book.UI.Settings.BasicData.AreaCategory
{
    /*----------------------------------------------------------------
   // Copyright (C) 2008 - 2010  咸陽飛馳軟件有限公司
   //                     版權所有 圍著必究
   // 文 件 名：EditForm
   // 编 码 人: 马艳军                   完成时间:2009-09-09
   // 修改原因：
   // 修 改 人:                          修改时间:
   // 修改原因：
   // 修 改 人:                          修改时间:
   //----------------------------------------------------------------*/

    public partial class EditForm : BaseEditForm
    {
        //實體對象  ---myj
        private Model.AreaCategory _areaCategory;
        //業務對象   ---myj
        private BL.AreaCategoryManager areaCategoryManager = new Book.BL.AreaCategoryManager();
        //無慘構造函數   ---myj
        public EditForm()
        {
            InitializeComponent();

            this.requireValueExceptions.Add(Model.AreaCategory.PROPERTY_ID, new AA("請輸入地區類別編號", this.textEditAreaCategoryId));
            this.requireValueExceptions.Add(Model.AreaCategory.PROPERTY_AREACATEGORYNAME, new AA("請輸入地區類別名稱", this.textEditAreaCategoryName));

            this.invalidValueExceptions.Add(Model.AreaCategory.PROPERTY_ID, new AA(Properties.Resources.EntityExists, this.textEditAreaCategoryId));
            this.invalidValueExceptions.Add("DeleteError", new AA(Properties.Resources.DeleteError, this));
           
        }

        #region Override

        protected override void AddNew()
        {
            this._areaCategory = new Model.AreaCategory();            
        }

        protected override void Delete()
        {
            if (this._areaCategory == null)
                return;
            if (MessageBox.Show(Properties.Resources.ConfirmToDelete, this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                return;
            this.areaCategoryManager.Delete(this._areaCategory.AreaCategoryId);
            this._areaCategory = this.areaCategoryManager.GetNext(this._areaCategory);
            if (this._areaCategory == null)
            {
                this._areaCategory = this.areaCategoryManager.GetLast();
            }
        }

        protected override bool HasRows()
        {
            return this.areaCategoryManager.HasRows();
        }

        protected override bool HasRowsNext()
        {
            return this.areaCategoryManager.HasRowsAfter(this._areaCategory);
        }

        protected override bool HasRowsPrev()
        {
            return this.areaCategoryManager.HasRowsBefore(this._areaCategory);
        }

        protected override void IMECtrl()
        {
            Book.UI.Tools.IMEControl.IMECtrl(new Control[] { this, this.textEditAreaCategoryId, this.textEditAreaCategoryName,this.memoEditDescription });
        }

        protected override void MoveFirst()
        {
            this._areaCategory = this.areaCategoryManager.GetFirst();
        }

        protected override void MoveLast()
        {
            this._areaCategory = this.areaCategoryManager.GetLast();
        }

        protected override void MoveNext()
        {
            Model.AreaCategory unitGroup = this.areaCategoryManager.GetNext(this._areaCategory);
            if (unitGroup == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);

            this._areaCategory = unitGroup;
        }

        protected override void MovePrev()
        {
            Model.AreaCategory unitGroup = this.areaCategoryManager.GetPrev(this._areaCategory);
            if (unitGroup == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);

            this._areaCategory = unitGroup;
        }


        public override void Refresh()
        {
            if (this._areaCategory == null)
            {
                this._areaCategory = new Book.Model.AreaCategory();
                this.action = "insert";
            }

            this.bindingSourceAreaCategory.DataSource = this.areaCategoryManager.Select();

            this.textEditAreaCategoryId.EditValue = string.IsNullOrEmpty(this._areaCategory.Id) ? this._areaCategory.AreaCategoryId : this._areaCategory.Id;
            this.textEditAreaCategoryName.EditValue = this._areaCategory.AreaCategoryName;      

            this.memoEditDescription.Text = this._areaCategory.Description;

            switch (this.action)
            {
                case "insert":
                    this.textEditAreaCategoryId.Properties.ReadOnly = false;
                    this.textEditAreaCategoryName.Properties.ReadOnly = false;
                    this.memoEditDescription.Properties.ReadOnly = false;
                    break;

                case "update":                   
                    this.textEditAreaCategoryId.Properties.ReadOnly = false;
                    this.textEditAreaCategoryName.Properties.ReadOnly = false;
                    this.memoEditDescription.Properties.ReadOnly = false;
                    break;

                case "view":
                    this.textEditAreaCategoryId.Properties.ReadOnly = true;
                    this.textEditAreaCategoryName.Properties.ReadOnly = true;
                    this.memoEditDescription.Properties.ReadOnly = true;
                    break;
                default:
                    break;
            }
            base.Refresh();
        }

        protected override void Save()
        {
            this._areaCategory.Id = this.textEditAreaCategoryId.Text;
            this._areaCategory.AreaCategoryName = this.textEditAreaCategoryName.Text;
            this._areaCategory.Description = this.memoEditDescription.Text;
            switch (this.action)
            {
                case "insert":
                    this.areaCategoryManager.Insert(this._areaCategory);
                    break;
                case "update":
                    //this._areaCategory.AreaCategoryId = oldId;
                    this.areaCategoryManager.Update(this._areaCategory);
                    break;
                default:
                    break;
            }
        }

        #endregion

        private void EditForm_Load(object sender, EventArgs e)
        {

        }

        //gridview的click事件 ---myj
        private void gridView1_Click(object sender, EventArgs e)
        {
            GridView view = sender as GridView;
            GridHitInfo hitInfo = view.CalcHitInfo(view.GridControl.PointToClient(Cursor.Position));
            if (hitInfo.InRow && !view.IsGroupRow(hitInfo.RowHandle))
            {
                Model.AreaCategory area = this.bindingSourceAreaCategory.Current as Model.AreaCategory;
                if (area != null)
                {
                    this._areaCategory = area;
                    this.action = "view";
                    this.Refresh();
                }
            }
        }
    }
}