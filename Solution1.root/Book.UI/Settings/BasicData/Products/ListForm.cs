using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Book.UI.Settings.BasicData.Products
{
    /*----------------------------------------------------------------
   // Copyright (C) 2008 - 2010  咸陽飛馳軟件有限公司
   //                     版權所有 圍著必究
   // 功能描述: 
   // 文 件 名：ListForm
   // 编 码 人: 茍波濤                   完成时间:2009-10-30
   // 修改原因：
   // 修 改 人:                          修改时间:
   // 修改原因：
   // 修 改 人:                          修改时间:
   //----------------------------------------------------------------*/
    public partial class ListForm : Settings.BasicData.BaseListForm
    {
        BL.ProductCategoryManager productCategoryManager = new Book.BL.ProductCategoryManager();
        BL.ProductManager productManager = new Book.BL.ProductManager();
        Model.ProductCategory productCate = new Book.Model.ProductCategory();
        public ListForm()
        {
            InitializeComponent();
            this.manager = new Book.BL.ProductManager();
        }

        private void LisFormt_Load(object sender, EventArgs e)
        {

            this.bindingSourceProductCate.DataSource = this.productCategoryManager.Select();
            this.bindingSource1.DataSource = this.productManager.Select();
       
        }

        #region Overrides

        protected override BaseEditForm GetEditForm()
        {
            return new EditForm(this.productCate);
        }

        protected override BaseEditForm GetEditForm(object[] args)
        {
            Type type = typeof(EditForm);
            return (EditForm)type.Assembly.CreateInstance(type.FullName, false, System.Reflection.BindingFlags.CreateInstance, null, args, null, null);
        }

        #endregion


        private void gridView1_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.ListSourceRowIndex < 0)
                return;

            switch (e.Column.Name)
            {
                case "colProductCategoryId":
                    IList<Model.Product> products = this.bindingSource1.DataSource as IList<Model.Product>;
                    if (products.Count > 0)
                    {
                        e.DisplayText = products[e.ListSourceRowIndex].ProductCategory == null ? "" : products[e.ListSourceRowIndex].ProductCategory.ProductCategoryName;
                    }
                    break;                    
                default:
                    break;
            }
        }

        private void gridView1_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            //switch (e.Column.Name)
            //{
            //    case "colProductCategoryId":
            //        IList<Model.Product> products = this.bindingSource1.DataSource as IList<Model.Product>;
            //        if (e.IsGetData)
            //            e.Value = products[e.ListSourceRowIndex].ProductCategoryId;
            //        break;
            //    default:
            //        break;
            //}
        } 
        private void bindingSourceProductCate_CurrentChanged(object sender, EventArgs e)
        {
            this.productCate = this.bindingSourceProductCate.Current as Model.ProductCategory; 
            this.bindingSource1.DataSource=this.productManager.Select(productCate);
            this.gridControl1.RefreshDataSource();
        }
        public override void gridView1_DoubleClick(object sender, EventArgs e)
        {        
           EditForm._product = this.bindingSource1.Current as Model.Product;        
            this.DialogResult=DialogResult.OK;
        }
   
    }
}