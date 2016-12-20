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
    public partial class StockEditorChooseForm : DevExpress.XtraEditors.XtraForm
    {
        BL.StockEditorManager stockEditorManager = new Book.BL.StockEditorManager();
        #region Properties

        public Object SelectedItem
        {
            get
            {
                return this.bindingSource1.Current;
            }
        }

        #endregion
        public StockEditorChooseForm()
        {
            InitializeComponent();
            this.bindingSource1.DataSource = stockEditorManager.SelectNoStockCheck();
          
        }

        private void simpleButtonCancle_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void simpleButtonOK_Click(object sender, EventArgs e)
        {
            if (this.bindingSource1.Current == null)
            {
                return;
            }
            this.DialogResult = DialogResult.OK;
        }

        private void gridView1_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.ListSourceRowIndex < 0) return;
            IList<Model.StockEditor> details = this.bindingSource1.DataSource as IList<Model.StockEditor>;
            if (details == null || details.Count < 1) return;
             string productCategoryId = details[e.ListSourceRowIndex].ProductCategoryId;
             Model .ProductCategory  productCategory = new Book.BL.ProductCategoryManager().Get (productCategoryId);
            if (productCategory == null) return;
            switch (e.Column.Name)
            {
                case "ProductCategoryId":
                    e.DisplayText = productCategory.ProductCategoryName;
                    break;
            }
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {

            if (this.bindingSource1.Current == null)
            {
                return;
            }
            this.DialogResult = DialogResult.OK;
        }
    }
}