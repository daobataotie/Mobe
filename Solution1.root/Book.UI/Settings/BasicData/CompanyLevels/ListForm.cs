using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Book.UI.Settings.BasicData.CompanyLevels
{

    /*----------------------------------------------------------------
   // Copyright (C) 2008 - 2010  咸陽飛馳軟件有限公司
   //                     版權所有 圍著必究
   // 功能描述: 客戶級別設置
   // 文 件 名：ListForm
   // 编 码 人: 马艳军                   完成时间:2009-09-27
   // 修改原因：
   // 修 改 人:                          修改时间:
   // 修改原因：
   // 修 改 人:                          修改时间:
   //----------------------------------------------------------------*/
    public partial class ListForm : DevExpress.XtraEditors.XtraForm
    {
        #region myj---業務對象定義
        protected BL.CompanyLevelManager companyLevelManager = new Book.BL.CompanyLevelManager();
        #endregion

        #region myj---無慘構造函數
        public ListForm()
        {
            InitializeComponent();
        }
        #endregion

        #region myj---窗體加載事件(綁定默認數據)
        private void ListForm_Load(object sender, EventArgs e)
        {
            this.companyLevelBindingSource.DataSource = this.companyLevelManager.SelectDateTable();
        }
        #endregion

        #region myj---barButtonItem點擊事件
        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (!this.gridView1.PostEditor() || !this.gridView1.UpdateCurrentRow())
                return;

            // 基础获利率
            System.Data.DataTable table = (DataTable)this.companyLevelBindingSource.DataSource;
            this.companyLevelManager.UpdateDataTable(table);
        }
        #endregion

    }
}