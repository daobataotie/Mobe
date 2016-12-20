using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI
{
    public partial class StockPrompt : DevExpress.XtraEditors.XtraForm
    {
        public IList<Model.Product> productList = new List<Model.Product>();
        BL.ProductManager manager = new Book.BL.ProductManager();
        public StockPrompt()
        {
            InitializeComponent();
            this.productList = manager.StockPrompt();
            this.bindingSource1.DataSource = this.productList;
            if (BL.Settings.StockPromptFlag == "1")
                this.checkEdit1.Checked = true;
            else
                this.checkEdit1.Checked = false;
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_Print_Click(object sender, EventArgs e)
        {
            ROStockPrompt ro = new ROStockPrompt(this.productList);
            ro.ShowPreviewDialog();
        }

        private void checkEdit1_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkEdit1.Checked)
                new BL.SettingManager().Update("StockPromptFlag", "1");
            else
                new BL.SettingManager().Update("StockPromptFlag", "2");
        }
    }
}