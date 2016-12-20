using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Book.UI.Query
{
    /*----------------------------------------------------------------
// Copyright (C) 2008 - 2010  咸阳飛馳軟件有限公司
//                     版權所有 圍著必究

// 编 码 人: 裴盾             完成时间:2009-4-11
// 修改原因：
// 修 改 人:                          修改时间:
// 修改原因：
// 修 改 人:                          修改时间:
//----------------------------------------------------------------*/
    public partial class ConditionDChooseForm : ConditionChooseForm
    {
        //商品管理
        private BL.ProductManager productManager = new Book.BL.ProductManager();

        private ConditionD condition;

        public ConditionDChooseForm()
        {
            InitializeComponent();
        }


        #region 重写父类方法
        protected override void OnOK()
        {
            if (condition == null)
                condition = new ConditionD();
            condition.StartId = this.comboBoxEditStartId.Text.Split(new char[] { ' ' })[0];
            condition.EndId = this.comboBoxEditEndId.Text.Split(new char[] { ' ' })[0];

        }
        #endregion

        public override Condition Condition
        {
            get
            {
                return this.condition;
            }
            set
            {
                this.condition = value as ConditionD;
            }
        }


        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConditionDChooseForm_Load(object sender, EventArgs e)
        {
            System.Collections.Generic.IList<Model.Product> products = productManager.Select();
            foreach (Model.Product product in products)
            {
                this.comboBoxEditStartId.Properties.Items.Add(string.Format("{0} {1}", product.ProductId, product.ProductName));
                this.comboBoxEditEndId.Properties.Items.Add(string.Format("{0} {1}", product.ProductId, product.ProductName));
            }
        }
    }
}