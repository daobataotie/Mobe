using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.Settings.BasicData.Products
{
    /*----------------------------------------------------------------
   // Copyright (C) 2008 - 2010  咸陽飛馳軟件有限公司
   //                     版權所有 圍著必究
   // 功能描述: 
   // 文 件 名：List1Form
   // 编 码 人: 茍波濤                   完成时间:2009-10-30
   // 修改原因：
   // 修 改 人:                          修改时间:
   // 修改原因：
   // 修 改 人:                          修改时间:
   //----------------------------------------------------------------*/
    public partial class List1Form : BaseListForm
    {
        DataTable dt = new DataTable();
        public List1Form()
        {

            InitializeComponent();
            // this.barManager1.Bars[0].Visible = false ;

            this.manager = new BL.ProductManager();
        }
        public override void gridView1_DoubleClick(object sender, EventArgs e)
        {
            string id = dt.Rows[this.bindingSource1.IndexOf(this.bindingSource1.Current)][0].ToString();
            EditForm._product = ((BL.ProductManager)this.manager).Get(id);
            //this.DialogResult = DialogResult.OK;
            EditForm f = new EditForm(EditForm._product, "view");
            f.ShowDialog(this);
        }

        private void List1Form_Load(object sender, EventArgs e)
        {


        }
        protected override void RefreshData()
        {
            //this.bindingSource1.DataSource = ((BL.ProductManager)this.manager).Query(" SELECT product.Id,ProductSpecification,ProductName,ProductCategoryName,SupplierFullName ,CustomerProductName,ProductDescription FROM Product left join ProductCategory ca  on ca.ProductCategoryId=Product.ProductCategoryId left join Supplier s on  s.SupplierId=Product.SupplierId  order by  ProductName", 240, "pro").Tables[0];
            //为了查询速度，暂将ProductSpecification，ProductDescription去掉
            this.bindingSource1.DataSource = this.dt = ((BL.ProductManager)this.manager).Query("SELECT p.ProductId,p.Id,p.ProductName,SupplierFullName ,CustomerProductName,ca.ProductCategoryName,isnull(StocksQuantity,0) StocksQuantity FROM Product p left join ProductCategory ca  on ca.ProductCategoryId=p.ProductCategoryId  left join Supplier s on  s.SupplierId=p.SupplierId order by  ProductName", 240, "pro").Tables[0];
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Dispose();
            GC.Collect();
        }
        //protected override BaseEditForm GetEditForm()
        //{
        //    return new EditForm(this.productCate);
        //}

        //protected override BaseEditForm GetEditForm(object[] args)
        //{
        //    Type type = typeof(EditForm);
        //    return (EditForm)type.Assembly.CreateInstance(type.FullName, false, System.Reflection.BindingFlags.CreateInstance, null, args, null, null);
        //}
    }
}