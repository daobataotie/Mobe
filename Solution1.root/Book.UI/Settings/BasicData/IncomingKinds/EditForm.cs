using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Book.UI.Settings.BasicData.IncomingKinds
{
    /*----------------------------------------------------------------
   // Copyright (C) 2008 - 2010  咸陽飛馳軟件有限公司
   //                     版權所有 圍著必究
   // 功能描述: 
   // 文 件 名：EditForm
   // 编 码 人: 茍波濤                   完成时间:2009-10-18
   // 修改原因：
   // 修 改 人:                          修改时间:
   // 修改原因：
   // 修 改 人:                          修改时间:
   //----------------------------------------------------------------*/
    public partial class EditForm : BaseEditForm1
    {
        protected BL.IncomingKindManager incomingKindManager = new Book.BL.IncomingKindManager();

        protected IList<Model.IncomingKind> _detail = new List<Model.IncomingKind>();
        public EditForm()
        {
            InitializeComponent();
            this.requireValueExceptions.Add(Model.IncomingKind.PROPERTY_ID, new AA(Properties.Resources.RequireDataForId, this.gridControl1 as Control));           
            this.invalidValueExceptions.Add(Model.IncomingKind.PROPERTY_ID, new AA(Properties.Resources.EntityExists, this.gridControl1 as Control));            
            this.action = "view";
        }
        #region Override
        protected override void Delete()
        {      
            if (MessageBox.Show(Properties.Resources.ConfirmToDelete, this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                return;
            this.incomingKindManager.Delete(this.bindingSource1.Current as Model.IncomingKind );       
        }
        protected override void Save()
        {   
            switch (this.action)
            {
                case "insert":
                    this.incomingKindManager.Update(this._detail);
                    break;
                case "update":
                    this.incomingKindManager.Update(this._detail);
                    break;
                default:
                    break;
            }
        }
        #endregion

        protected override void AddNew()
        {
            this.action = "insert";        
        }
        public override void Refresh()
        {
            this._detail = this.incomingKindManager.Select();
            this.bindingSource1.DataSource = this._detail;        
            if (this.action == "insert")
            {
                Model.IncomingKind ok = new Book.Model.IncomingKind();
                ok.IncomingKindId = Guid.NewGuid().ToString();
                this._detail.Add(ok);             
                this.bindingSource1.Position = this.bindingSource1.IndexOf(ok);
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
                    this.gridView1.OptionsBehavior.Editable = false ;
                    break;
                default:
                    break;
            }
            base.Refresh();
        }
        protected override void grid_keyDpwn()
        {
            Model.IncomingKind ik = new Book.Model.IncomingKind();
            ik.IncomingKindId = Guid.NewGuid().ToString();
            this._detail.Add(ik);
            this.bindingSource1.Position = this.bindingSource1.IndexOf(ik);
        }
        protected override void grid_KeyDelete()
        {
            this._detail.Remove(this.bindingSource1.Current as Model.IncomingKind);    
        }

        private void gridView1_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {

            Model.IncomingKind incomingKind = this.gridView1.GetRow(e.RowHandle) as Model.IncomingKind;
            if (e.Value == null) return;
            switch (e.Column.Name)
            {
                case "gridColumnId":
                    incomingKind.Id = e.Value.ToString();
                    break;
                case "gridColumnName":
                    incomingKind.IncomingKindName = e.Value.ToString();
                    break;
                case "gridColumnDesc":
                    incomingKind.IncomingKindDescription = e.Value.ToString();
                    break;
            }        
        } 

    }
}