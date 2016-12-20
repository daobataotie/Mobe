using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Book.UI.Settings.StockLimitations
{
    /*----------------------------------------------------------------
// Copyright (C) 2008 - 2010  咸阳飛馳軟件有限公司
//                     版權所有 圍著必究

// 编 码 人: 马艳军            完成时间:2009-11-13
// 修改原因：
// 修 改 人:                          修改时间:
// 修改原因：
// 修 改 人:                          修改时间:
//----------------------------------------------------------------*/
    public partial class MainForm : DevExpress.XtraEditors.XtraForm
    {
        protected BL.StockManager stockManager = new Book.BL.StockManager();

        public MainForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 窗体加载，指定数据源
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_Load(object sender, EventArgs e)
        {
            this.DataBind();
        }
        private void DataBind() 
        {
            this.bindingSource1.DataSource = this.stockManager.SelectDataTable();
        }



        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItemSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.gridView1.PostEditor();
            this.gridView1.UpdateCurrentRow();

            DataTable dt = (DataTable)this.bindingSource1.DataSource;
            this.stockManager.UpdateDataTable(dt);
        }
    }
}