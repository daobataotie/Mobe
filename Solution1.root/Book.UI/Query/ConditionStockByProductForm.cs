using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.Query
{
/*----------------------------------------------------------------
// Copyright (C) 2008 - 2010  咸阳飛馳軟件有限公司
//                     版權所有 圍著必究

// 编 码 人: 裴盾              完成时间:2009-5-16
// 修改原因：
// 修 改 人:                          修改时间:
// 修改原因：
// 修 改 人:                          修改时间:
//----------------------------------------------------------------*/
    public partial class ConditionStockByProductForm :ConditionChooseForm
    {
        ConditionStockByProduct condition;

        //无参构造
        public ConditionStockByProductForm()
        {
            InitializeComponent();
        }

        #region 重写父类方法
        public override Condition Condition
        {
            get
            {
                return this.condition;
            }
            set
            {
                this.condition = value as ConditionStockByProduct;
            }
        }
        protected override void OnOK()
        {
            if (condition == null)
            {
                condition = new ConditionStockByProduct();
            }
            condition.Product = EditProduct.EditValue as Model.Product;            
        }
        #endregion
        

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonEdit1_Click(object sender, EventArgs e)
        {
            Book.UI.Invoices.ChooseProductForm f = new Book.UI.Invoices.ChooseProductForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                (sender as ButtonEdit).EditValue = f.SelectedItem;
            }
        }



    }
}