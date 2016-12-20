using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Book.UI.Settings.ProfitMargin
{
    /*----------------------------------------------------------------
// Copyright (C) 2008 - 2010  咸阳飛馳軟件有限公司
//                     版權所有 圍著必究

// 编 码 人: 马艳军            完成时间:2009-11-16
// 修改原因：
// 修 改 人:                          修改时间:
// 修改原因：
// 修 改 人:                          修改时间:
//----------------------------------------------------------------*/
    public partial class MainForm : DevExpress.XtraEditors.XtraForm
    {
        protected BL.CompanyLevelManager companyLevelManager = new Book.BL.CompanyLevelManager();
        protected BL.SpecialProfitMarginManager specialProfitMarginManager = new Book.BL.SpecialProfitMarginManager();
        protected BL.ProductManager productManager = new Book.BL.ProductManager();

        private System.Data.DataTable dt;
        DataView dv = null;

        public MainForm()
        {
            InitializeComponent();
        }
        #region Form_Load

        private void MainForm_Load(object sender, EventArgs e)
        {   
            this.companyLevelBindingSource.DataSource = this.companyLevelManager.SelectDateTable();

            dt = this.specialProfitMarginManager.SelectDataTable();

            dv = new DataView(dt);

            this.productBindingSource .DataSource= this.productManager.SelectProduct();
            
            this.gridControl3.DataSource = dv;

        }

        #endregion 

        #region Save

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (!this.gridView1.PostEditor() || !this.gridView1.UpdateCurrentRow())
                return;

            // 基础获利率
            System.Data.DataTable table = (DataTable)this.companyLevelBindingSource.DataSource;
            this.companyLevelManager.UpdateDataTable(table);
            
            // 特殊获利率
            this.companyLevelBindingSource.DataSource = this.companyLevelManager.SelectDateTable();
            this.specialProfitMarginManager.UpdateDataTable(dt);

            MessageBox.Show(Properties.Resources.SuccessfullySaved, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        #endregion 

        #region Choose

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            Invoices.ChooseProductForm f = new Book.UI.Invoices.ChooseProductForm();
            if (f.ShowDialog(this) == DialogResult.OK) 
            {
                Model.Product prod = f.SelectedItem as Model.Product;
                List<Model.Product> list = this.productBindingSource.DataSource as List<Model.Product>;
                bool flag = true;
                foreach (Model.Product product in list)
                {
                    if (product.ProductId == prod.ProductId) 
                    {
                        flag = false;
                    }
                }
                if (flag)
                {
                    list.Add(f.SelectedItem as Model.Product);
                    this.gridControl2.RefreshDataSource();

                    DataRow dr = null;
                    foreach (Model.CompanyLevel level in this.companyLevelManager.Select())
                    {
                        dr = dt.NewRow();
                        dr["CompanyLevelName"] = level.CompanyLevelName;
                        dr["SpecialProfitMarginValue"] = 0;
                        dr["productId"] = prod.ProductId;
                        dr["CompanyLevelId"] = level.CompanyLevelId;
                        dt.Rows.Add(dr);
                    }                    
                }
            }

        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            List<Model.Product> list = this.productBindingSource.DataSource as List<Model.Product>;

            Model.Product p = this.productBindingSource.Current as Model.Product;
            list.Remove(p);

            this.gridControl2.RefreshDataSource();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];

                if (dr["ProductId"].ToString() == p.ProductId)
                {
                    dr.Delete();
                }
            }
        }

        #endregion

        #region Event

        private void productBindingSource_CurrentChanged(object sender, EventArgs e)
        {
            dv.RowFilter = "productId = '" + (this.productBindingSource.Current as Model.Product).ProductId + "'";

            this.gridControl3.DataSource = dv;
        }

        #endregion 


    }
}