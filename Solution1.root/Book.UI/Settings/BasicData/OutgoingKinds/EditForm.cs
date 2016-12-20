using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Book.UI.Settings.BasicData.OutgoingKinds
{
    /*----------------------------------------------------------------
   // Copyright (C) 2008 - 2010  咸陽飛馳軟件有限公司
   //                     版權所有 圍著必究
   // 功能描述: 
   // 文 件 名：EditForm
   // 编 码 人: 茍波濤                   完成时间:2009-10-26
   // 修改原因：
   // 修 改 人:                          修改时间:
   // 修改原因：
   // 修 改 人:                          修改时间:
   //----------------------------------------------------------------*/
    public partial class EditForm : BaseEditForm
    {
        protected BL.OutgoingKindManager outgoingKindManager = new Book.BL.OutgoingKindManager();

        private IList<Model.OutgoingKind> _detail = new List<Model.OutgoingKind>();  
        public EditForm()
        {
            InitializeComponent();

            this.requireValueExceptions.Add(Model.OutgoingKind.PROPERTY_ID, new AA(Properties.Resources.RequireDataForId, this.gridControl1 as Control));      
           this.invalidValueExceptions.Add(Model.OutgoingKind.PROPERTY_ID, new AA(Properties.Resources.EntityExists, this.gridControl1 as Control));
            
            this.action = "view";
        }
        private void EditForm_Load(object sender, EventArgs e)
        {
            this.Visibles();
        }

        #region Override
        protected override void Delete()
        {
          
            if (MessageBox.Show(Properties.Resources.ConfirmToDelete, this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                return;
            this.outgoingKindManager.Delete(this.bindingSource1.Current as Model.OutgoingKind);
     
        }

        protected override void Save()
        {         
            switch (this.action)
            {
                case "insert":
                    this.outgoingKindManager.Update(this._detail);//.Insert(this.outgoingKind);
                    break;
                case "update":               
                    this.outgoingKindManager.Update(this._detail);
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
            this._detail=this.outgoingKindManager.Select();
            this.bindingSource1.DataSource = this._detail;
            if (this.action == "insert")
            {
                Model.OutgoingKind ok = new Book.Model.OutgoingKind();       
                ok.OutgoingKindId=Guid.NewGuid().ToString();
                this._detail.Add(ok);
               // this.bindingSource1.DataSource = this._detail;
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
                    this.gridView1.OptionsBehavior.Editable = false;
                    break;
                default:
                    break;
            }
            base.Refresh();
        }

        protected override bool HasRows()
        {
            return this.outgoingKindManager.HasRows();
        }

        private void gridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.action == "insert" || this.action == "update")
            {
                if (e.KeyData == Keys.Enter)
                {
                    Model.OutgoingKind ok = new Book.Model.OutgoingKind();    
                    ok.OutgoingKindId=Guid.NewGuid().ToString();
                    this._detail.Add(ok);
                    this.bindingSource1.Position = this.bindingSource1.IndexOf(ok);
                }
                if (e.KeyData == Keys.Delete)
                {
                    this._detail.Remove(this.bindingSource1.Current as Model.OutgoingKind);
                }
                this.gridControl1.RefreshDataSource();
            }
        }

        private void gridView1_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            Model.OutgoingKind ok = this.gridView1.GetRow(e.RowHandle) as Model.OutgoingKind;
            if (e.Value == null) return;
            switch (e.Column.Name)
            {
                case "gridColumnId":
                    ok.Id = e.Value.ToString();
                    break;
                case "gridColumnName":
                    ok.OutgoingKindName = e.Value.ToString();
                    break;
                case "gridColumnDesc":
                    ok.OutgoingKindDescription = e.Value.ToString();
                    break;
            }        
        } 
    }
}