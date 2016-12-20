using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.Settings.BasicData.Supplier
{
    /*----------------------------------------------------------------
   // Copyright (C) 2008 - 2010  咸陽飛馳軟件有限公司
   //                     版權所有 圍著必究
   // 功能描述: 
   // 文 件 名：ChooseSupplierForm
   // 编 码 人: 茍波濤                   完成时间:2009-11-06
   // 修改原因：
   // 修 改 人:                          修改时间:
   // 修改原因：
   // 修 改 人:                          修改时间:
   //----------------------------------------------------------------*/
    public partial class ChooseSupplierForm : BaseChooseForm
    {
        public ChooseSupplierForm()
        {
            InitializeComponent();
            this.manager = new BL.SupplierManager();
        }

        protected override BaseEditForm GetEditForm()
        {
            return new EditForm();
        }

        //private void gridView1_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        //{
        //    if (e.ListSourceRowIndex < 0) return;
        //    IList<Model.Supplier> suppliers = this.bindingSource1.DataSource as IList<Model.Supplier>;
        //    if (suppliers == null || suppliers.Count < 1) return;
        //    Model.Supplier supplier = suppliers[e.ListSourceRowIndex];
        //    switch (e.Column.Name)
        //    {
        //        case "gridColumnSupplierId":
        //            e.DisplayText = string.IsNullOrEmpty(supplier.Id) ? "" : supplier.Id;
        //            break;
        //    }
        //}
    }
}