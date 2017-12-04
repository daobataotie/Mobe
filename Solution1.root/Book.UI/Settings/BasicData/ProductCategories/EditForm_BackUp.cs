using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Book.UI.Settings.BasicData.ProductCategories
{
    /*----------------------------------------------------------------
   // Copyright (C) 2008 - 2010  咸陽飛馳軟件有限公司
   //                     版權所有 圍著必究
   // 功能描述: 
   // 文 件 名：EditForm1
   // 编 码 人: 茍波濤                   完成时间:2009-10-30
   // 修改原因：
   // 修 改 人:                          修改时间:
   // 修改原因：
   // 修 改 人:                          修改时间:
   //----------------------------------------------------------------*/
    public partial class EditForm_BackUp : BaseEditForm
    {
        private BL.ProductCategoryManager productCategoryManager = new Book.BL.ProductCategoryManager();
      // private Model.ProductCategory productCategory = null;
        private IList<Model.ProductCategory> _detail = new List<Model.ProductCategory>();
        #region Constructors

        public EditForm_BackUp()
        {
            InitializeComponent();
            
          this.requireValueExceptions.Add(Model.ProductCategory.PROPERTY_ID,   new AA(Properties.Resources.RequireIdName, this.gridControl1 as Control ));
          this.invalidValueExceptions.Add(Model.ProductCategory.PROPERTY_ID, new AA(Properties.Resources.EntityExists,this.gridControl1 as Control));
          this.invalidValueExceptions.Add(Model.ProductCategory.PROPERTY_PRODUCTCATEGORYNAME, new AA(Properties.Resources.EntityName, this.gridControl1 as Control));
            this.action = "view";
        }
        #endregion

        #region Form Events

        private void EditForm_Load(object sender, EventArgs e)
        {
            this.Visibles();           
        }

        #endregion

        #region Overrieded   
        protected override void Delete()
        {           
            
            if (MessageBox.Show(Properties.Resources.ConfirmToDelete, this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                return;          
              this.productCategoryManager.Delete(this.bindingSource1.Current as Model.ProductCategory);         
     

        }
        protected override void Save()
        {
            if(this.action!="view")
            this.productCategoryManager.Update(this._detail);      
        }

        #endregion

        protected override void AddNew()
        {
            this.action = "insert";        
        } 
        public override void Refresh()
        {
       
            this._detail = productCategoryManager.Select();
            this.bindingSource1.DataSource = this._detail;          
            if (this.action == "insert")
            {
                Model.ProductCategory pc = new Book.Model.ProductCategory();
                pc.ProductCategoryId = Guid.NewGuid().ToString();
                this._detail.Add(pc);
                this.bindingSource1.DataSource = this._detail;
                this.bindingSource1.Position = this.bindingSource1.IndexOf(pc);
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
        protected override bool HasRows()
        {
            return this.productCategoryManager.HasRows();
        }
        protected override void IMECtrl()
        {
            Book.UI.Tools.IMEControl.IMECtrl(new Control[] {this.gridControl1, this });
        }
        private void gridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.action == "insert" || this.action == "update")
            {
                if (e.KeyData == Keys.Enter)
                {
                    Model.ProductCategory pc = new Book.Model.ProductCategory();
                    pc.ProductCategoryId = Guid.NewGuid().ToString();
                    this._detail.Add(pc);
                    this.bindingSource1.Position = this.bindingSource1.IndexOf(pc);             
                }
                if (e.KeyData == Keys.Delete)
                {
                    this._detail.Remove(this.bindingSource1.Current as Model.ProductCategory);       
                }
                this.gridControl1.RefreshDataSource();
            }
        }
        private void gridView1_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            Model.ProductCategory pc = this.gridView1.GetRow(e.RowHandle) as Model.ProductCategory;
            if(e.Value==null) return;
            switch(e.Column.Name)
            {
                case "gridColumn1":
                    pc.Id=e.Value.ToString();
                    break;
                case "gridColumn2":
                    pc.ProductCategoryName=e.Value.ToString();
                    break;
            }        
        }
    }
}