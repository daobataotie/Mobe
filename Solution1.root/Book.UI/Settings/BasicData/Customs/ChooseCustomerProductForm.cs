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
   // 文 件 名：ChooseCustomerProductForm
   // 编 码 人: 马艳军                   完成时间:2009-10-10
   // 修改原因：
   // 修 改 人:                          修改时间:
   // 修改原因：
   // 修 改 人:                          修改时间:
   //----------------------------------------------------------------*/
    public partial class ChooseCustomerProductForm : BaseChooseForm
    {

        #region 變量對象定義
        private Model.Customer _customer;
        IList<Model.CustomerProducts> customerProductDetails = new List<Model.CustomerProducts>();
        private IList<Model.Customer> customerDetail = new List<Model.Customer>();
        private IList<Model.Customer> customerDetails = new List<Model.Customer>();
        private int tag = 0;
        #endregion

        #region 構造函數
        /// <summary>
        /// 無慘
        /// </summary>
        public ChooseCustomerProductForm()
        {
            InitializeComponent();
            this.manager = new BL.CustomerProductsManager();
        }
        /// <param name="customer">model對象</param>
        public ChooseCustomerProductForm(Model.Customer customer)
            : this()
        {

            this._customer = customer;

        }
        /// <param name="customerStart">customerStart</param>
        /// <param name="customerEnd">customerEnd</param>
        public ChooseCustomerProductForm(string customerStart, string customerEnd)
            : this()
        {

            customerDetail = new BL.CustomerManager().Select(customerStart, customerEnd, Convert.ToDateTime("1900-01-01"), Convert.ToDateTime("3000-01-01"));

            tag = 1;


        }
        #endregion

        #region 重載父類的方法
        protected override BaseEditForm GetEditForm()
        {
            return new CustomerProductForm(_customer);
        }
        #endregion

        #region gridview 的事件
        private void gridView1_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.ListSourceRowIndex < 0) return;
            IList<Model.CustomerProducts> details = this.bindingSource1.DataSource as IList<Model.CustomerProducts>;
            if (details == null || details.Count < 1) return;
            Model.Product product = details[e.ListSourceRowIndex].Product;
            if (product == null) return;
            switch (e.Column.Name)
            {
                case "gridColumn2":
                    e.DisplayText = product.Id;
                    break;
            }
        }
        #endregion

        #region 重載父類的加載方法
        protected override void LoadData()
        {
            customerProductDetails.Clear();
            if (tag == 1)
            {
                if (this.customerDetail.Count != 0)
                {
                    foreach (Model.Customer customer in this.customerDetail)
                    {

                        foreach (Model.CustomerProducts customerProducts in (this.manager as BL.CustomerProductsManager).Select(customer))
                        {
                            customerProductDetails.Add(customerProducts);
                        }
                    }
                }

                this.bindingSource1.DataSource = customerProductDetails;
            }
            else
            {
                this.bindingSource1.DataSource = (this.manager as BL.CustomerProductsManager).Select(_customer);
            }

        }
        #endregion
    }
}