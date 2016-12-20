using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Book.UI.Settings.PeriodBeginning.ProductCost
{
    /*----------------------------------------------------------------
// Copyright (C) 2008 - 2010  咸阳飛馳軟件有限公司
//                     版權所有 圍著必究

// 编 码 人: 马艳军            完成时间:2009-10-16
// 修改原因：
// 修 改 人:                          修改时间:
// 修改原因：
// 修 改 人:                          修改时间:
//----------------------------------------------------------------*/
    public partial class ListForm : DevExpress.XtraEditors.XtraForm
    {
        protected BL.ProductManager productManager = new Book.BL.ProductManager();

        public ListForm()
        {
            InitializeComponent();
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

            DataTable dt = (DataTable)this.productBindingSource.DataSource;
            int rowCount = dt.Rows.Count;
            if (rowCount > 0)
            {
                for (int i = 0; i < rowCount; i++)
                {
                    DataRow dr = dt.Rows[i];
                    if (dr.RowState == DataRowState.Modified)
                    {
                        dr["ProductStandardCost"] = dr["ProductBeginCost"];
                    }
                }

                this.productManager.UpdateBeginCost(dt);

                this.productBindingSource.DataSource = this.productManager.SelectDataTable();
            }
            else
            {
                MessageBox.Show(Properties.Resources.NoData, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            
        }

        //加载，指定数据源
        private void ListForm_Load(object sender, EventArgs e)
        {
            this.productBindingSource.DataSource = this.productManager.SelectDataTable();
        }
    }
}