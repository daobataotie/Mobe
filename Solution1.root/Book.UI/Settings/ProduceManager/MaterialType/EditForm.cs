using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.Settings.ProduceManager.MaterialType
{
    /*----------------------------------------------------------------
// Copyright (C) 2008 - 2010  咸阳飛馳軟件有限公司
//                     版權所有 圍著必究

// 编 码 人: 马艳军            完成时间:2009-10-20
// 修改原因：
// 修 改 人:                          修改时间:
// 修改原因：
// 修 改 人:                          修改时间:
//----------------------------------------------------------------*/
    public partial class EditForm : Settings.BasicData.BaseEditForm1
    {
        IList<Model.MaterialType> _detail = new List<Model.MaterialType>();
        protected BL.MaterialTypeManager materialTypeManager = new Book.BL.MaterialTypeManager();
      //  private Model.MaterialType materialType = null;

        //无参构造
        public EditForm()
        {
            InitializeComponent();
            this.requireValueExceptions.Add(Model.MaterialType.PROPERTY_MATERIALTYPENAME, new AA(Properties.Resources.RequireDataForNames, this.gridControl1 as Control));
            //this.requireValueExceptions.Add(Model.MaterialType.PROPERTY_ID, new AA(Properties.Resources.RequireDataForId, this.MaterialTypeID));
            this.invalidValueExceptions.Add(Model.MaterialType.PROPERTY_MATERIALTYPENAME, new AA(Properties.Resources.EntityName, this.gridControl1 as Control));
            this.action = "view";
        }

        #region 重写方法
        protected override void Delete()
        {
            if (MessageBox.Show(Properties.Resources.ConfirmToDelete, this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                return;
           // try
            {
                this.materialTypeManager.Delete(this.bindingSource1.Current as Model.MaterialType);
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
                    this.materialTypeManager.Update(this._detail);
                    break;
                case "update":
                    this.materialTypeManager.Update(this._detail);
                    break;
                default:
                    break;
            }
        }
        public override void Refresh()
        {
            this._detail = this.materialTypeManager.Select();
            this.bindingSource1.DataSource = this._detail;
            if (this.action == "insert")
            {
                Model.MaterialType Material = new Book.Model.MaterialType();
                Material.MaterialTypeID = Guid.NewGuid().ToString();
                Material.MaterialTypeName = string.Empty;
                Material.MaterialTypeDescription = string.Empty;
                Material.EndMaterialTypeLogo = false;
                this._detail.Add(Material);
                this.bindingSource1.Position = this.bindingSource1.IndexOf(Material);      
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
            Model.MaterialType Material = new Book.Model.MaterialType();
            Material.MaterialTypeID = Guid.NewGuid().ToString();
            Material.MaterialTypeName = string.Empty;
            Material.MaterialTypeDescription = string.Empty;
            Material.EndMaterialTypeLogo = false;
            this._detail.Add(Material);
            this.bindingSource1.Position = this.bindingSource1.IndexOf(Material);      
        }
        protected override void grid_KeyDelete()
        {
            this._detail.Remove(this.bindingSource1.Current as Model.MaterialType);
        }
        #endregion 


        /// <summary>
        /// girdview列值改变时触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridView1_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            Model.MaterialType materialType = this.gridView1.GetRow(e.RowHandle) as Model.MaterialType;
            switch (e.Column.Name)
            {
                case "gridColumnLevel":
                    materialType.MaterialLevel = Convert.ToInt32(e.Value.ToString().Substring(0,e.Value.ToString().LastIndexOf('.')));
                    break;
                case "gridColumnIsEnd":

                    if ((bool)e.Value)
                    foreach (Model.MaterialType material in this._detail)
                    {
                        material.EndMaterialTypeLogo = false;
                    }

                    materialType.EndMaterialTypeLogo = (bool)e.Value;
                    this.gridControl1.RefreshDataSource();
                    break;
                case "gridColumnName":
                    materialType.MaterialTypeName = e.Value.ToString();
                    break;
                case  "gridColumnDesc":
                    materialType.MaterialTypeDescription = e.Value.ToString();
                    break;

            }

        }


    }
}


      