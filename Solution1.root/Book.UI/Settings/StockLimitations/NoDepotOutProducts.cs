using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.Settings.StockLimitations
{
    public partial class NoDepotOutProducts : DevExpress.XtraEditors.XtraForm
    {
        BL.StockManager stockManager = new Book.BL.StockManager();
        public DataTable dt = new DataTable();
        double year = 0;
        public NoDepotOutProducts()
        {
            InitializeComponent();

            //this.gridView1.GroupPanelText = "默認顯示一年以上未出倉的商品";
            //this.dt = stockManager.SelectProductNoDepotout(Convert.ToDouble(1), null);
            //GetLastDepotoutDate(this.dt);
            this.bindingSource1.DataSource = dt;
            this.label1.Text = this.bindingSource1.Count.ToString() + " 項";
            if (BL.Settings.NoDepotOutProducts == "1")
                this.checkEdit1.Checked = true;
            else
                this.checkEdit1.Checked = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.year = 1;
            this.newChooseContorl1.Choose = new Settings.BasicData.ProductCategories.ChooseProductCategories();
        }

        private void btn_Search_Click(object sender, EventArgs e)
        {
            this.dt = stockManager.SelectProductNoDepotout(Convert.ToDouble(this.txt_Years.Text == "" ? null : this.txt_Years.Text), (this.newChooseContorl1.EditValue as Model.ProductCategory) == null ? null : (this.newChooseContorl1.EditValue as Model.ProductCategory).ProductCategoryId);
            //GetLastDepotoutDate(this.dt);
            this.bindingSource1.DataSource = dt;
            this.label1.Text = this.bindingSource1.Count.ToString() + " 項";
            this.year = Convert.ToDouble(this.txt_Years.Text == "" ? null : this.txt_Years.Text);
        }

        private void checkEdit1_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkEdit1.Checked)
                new BL.SettingManager().Update("NoDepotOutProducts", "1");
            else
                new BL.SettingManager().Update("NoDepotOutProducts", "2");
        }

        private void btn_Print_Click(object sender, EventArgs e)
        {
            NoDepotOutProductsReport r = new NoDepotOutProductsReport(this.dt, this.year);
            r.ShowPreviewDialog();
        }

        private void GetLastDepotoutDate(DataTable dt)
        {
            dt.Columns.Add("LastDepotoutDate", typeof(string));
            //foreach (DataRow item in dt.Rows)
            //{
            //    item["LastDepotoutDate"] = this.stockManager.GetLastDepotoutDate(item["productid"].ToString());
            //}
        }
    }
}