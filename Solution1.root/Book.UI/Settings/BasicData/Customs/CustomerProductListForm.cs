using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.Settings.BasicData.Customs
{

    /*----------------------------------------------------------------
   // Copyright (C) 2008 - 2010  咸陽飛馳軟件有限公司
   //                     版權所有 圍著必究
   // 功能描述: 
   // 文 件 名：CustomerProductListForm
   // 编 码 人: 茍波濤                   完成时间:2009-10-14
   // 修改原因：
   // 修 改 人:                          修改时间:
   // 修改原因：
   // 修 改 人:                          修改时间:
   //----------------------------------------------------------------*/
    public partial class CustomerProductListForm : BaseListForm
    {
        //業務羅傑對象定義
        protected BL.CustomerManager customerManager = new Book.BL.CustomerManager();

        //無慘構造函數
        public CustomerProductListForm()
        {
            InitializeComponent();
            this.manager = new BL.CustomerProductsManager();
        }

        protected override BaseEditForm GetEditForm()
        {
         //  Model.Customer customer = this.bindingSourceCustomers.Current as Model.Customer;            
            return new CustomerProductForm();
        }

        protected override BaseEditForm GetEditForm(object[] args)
        {
            Type type = typeof(CustomerProductForm);
            return (CustomerProductForm)type.Assembly.CreateInstance(type.FullName, false, System.Reflection.BindingFlags.CreateInstance, null, args, null, null);       
        }

        //protected override void RefreshData()
        //{
        //    this.bindingSourceCustomers.DataSource = this.customerManager.Select();

        //    BindData();
        //}

        private void bindingSourceCustomers_CurrentChanged(object sender, EventArgs e)
        {
            //BindData();
        }

        //private void BindData() 
        //{
        //    Model.Customer customer = this.bindingSourceCustomers.Current as Model.Customer;
        //    if (customer == null)
        //        return;
        //    this.bindingSource1.DataSource = (this.manager as BL.CustomerProductsManager).Select(customer);
        //}

        private void gridView1_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            //if (e.ListSourceRowIndex < 0)
            //    return;
            //Model.Product p = null;

            //IList<Model.CustomerProducts> customerProducts = this.bindingSource1.DataSource as IList<Model.CustomerProducts>;

            //if (customerProducts == null || customerProducts.Count <= 0)
            //    return;
            //p = customerProducts[e.ListSourceRowIndex].Product;
            //if (p == null) return;
            //switch (e.Column.Name)
            //{
            //    case "gridColumn2":
            //        e.DisplayText = p.Id;
            //        break;
            //}
        }

        private void treeList1_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            if (e.Node != null)
            { 
            
            }
        }
    }
}