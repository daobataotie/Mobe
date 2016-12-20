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

// 编 码 人:  够波涛             完成时间:2009-4-21
// 修改原因：
// 修 改 人:                          修改时间:
// 修改原因：
// 修 改 人:                          修改时间:
//----------------------------------------------------------------*/
    public partial class ConditionJChooseForm : ConditionAChooseForm
    {
        //货品管理
        private BL.ProductManager productManager = new Book.BL.ProductManager();

        private ConditionJ condition;

        /// <summary>
        /// 无参构造
        /// </summary>
        public ConditionJChooseForm()
        {
            InitializeComponent();
        }

        #region 重写父类方法
        protected override void Init()
        {
            this.dateEditStartDate.DateTime = DateTime.Now.Date;
            this.dateEditEndDate.DateTime = DateTime.Now.Date;
        }
        protected override void OnOK()
        {
            if (this.condition == null)
                this.condition = new ConditionJ();
            this.condition.StartDate = this.dateEditStartDate.DateTime;
            this.condition.EndDate = this.dateEditEndDate.DateTime;
            this.condition.StartId = this.comboBoxEditStartId.Text.Split(new char[] { ' ' })[0];
            this.condition.EndId = this.comboBoxEditEndId.Text.Split(new char[] { ' ' })[0];

        }
        public override Condition Condition
        {
            get
            {
                return this.condition;
            }
            set
            {
                this.condition = value as ConditionJ;
            }
        }
        #endregion


        /// <summary>
        /// 选择货品
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonEditProduct_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            Book.UI.Invoices.ChooseProductForm f = new Book.UI.Invoices.ChooseProductForm();

            if (f.ShowDialog(this) == DialogResult.OK) 
            {
                (sender as DevExpress.XtraEditors.ButtonEdit).EditValue = f.SelectedItem;
            }
        }

        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConditionJChooseForm_Load(object sender, EventArgs e)
        {
            //System.Collections.Generic.IList<Model.Product> products = productManager.Select();
            //foreach (Model.Product product in products)
            //{
            //    this.comboBoxEditStartId.Properties.Items.Add(string.Format("{0} {1}", product.ProductId, product.ProductName));
            //    this.comboBoxEditEndId.Properties.Items.Add(string.Format("{0} {1}", product.ProductId, product.ProductName));
            //}
        }
    }
}