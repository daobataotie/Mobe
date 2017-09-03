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
    public partial class ListProductClassify : Settings.BasicData.BaseListForm
    {
        BL.ProductClassifyManager productClassifyManager = new Book.BL.ProductClassifyManager();
        Model.ProductClassify _productClassify = new Book.Model.ProductClassify();
        IList<Model.ProductClassify> list = new List<Model.ProductClassify>();
        public ListProductClassify()
        {
            InitializeComponent();
            this.manager = new Book.BL.ProductManager();
            this.gridView1.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(gridView1_FocusedRowChanged);
        }

        void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            this._productClassify = this.bindingSource1.Current as Model.ProductClassify;
        }

        #region Overrides

        protected override void RefreshData()
        {
        }

        protected override BaseEditForm GetEditForm()
        {
            return new ProductClassifyForm(this._productClassify);
        }

        protected override BaseEditForm GetEditForm(object[] args)
        {
            Type type = typeof(ProductClassifyForm);
            return (ProductClassifyForm)type.Assembly.CreateInstance(type.FullName, false, System.Reflection.BindingFlags.CreateInstance, null, args, null, null);
        }

        #endregion

        public override void gridView1_DoubleClick(object sender, EventArgs e)
        {
            Form f = GetEditForm(new object[] { this.bindingSource1.Current as Model.ProductClassify });
            if (f != null)
                f.ShowDialog();
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ProductClassifyConditionForm f = new ProductClassifyConditionForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                this.list = productClassifyManager.SelectCondtidion(f.StartDate, f.EndDate, f.KeyWord);
                this.bindingSource1.DataSource = this.list;
                this.gridControl1.RefreshDataSource();
            }
        }

    }
}