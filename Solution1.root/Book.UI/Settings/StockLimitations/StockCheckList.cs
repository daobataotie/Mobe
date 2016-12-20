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
    public partial class StockCheckList : Settings.BasicData.BaseListForm
    {
        public StockCheckList()
        {
            if (!StockCheckForm.isDelete)
                this.CancleDelete();
            InitializeComponent();
            this.manager = new BL.StockCheckManager();
        }

        protected override void RefreshData()
        {
            IList<Model.StockCheck> mStockChecklist = (this.manager as BL.StockCheckManager).SelectByDateRage(DateTime.Now.AddMonths(-1), global::Helper.DateTimeParse.EndDate);
            foreach (Model.StockCheck sc in mStockChecklist)
            {
                Model.ProductCategory _pc = new BL.ProductCategoryManager().Get(sc.ProductCategoryId);
                sc.ProductCategoryName = _pc == null ? "" : _pc.ProductCategoryName;
            }
            this.bindingSource1.DataSource = mStockChecklist;
            this.gridView1.GroupPanelText = "默認顯示一个月内的記錄";
        }

        private void barBtn_ChangeDateSearch_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Query.ConditionAChooseForm f = new Book.UI.Query.ConditionAChooseForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                Query.ConditionA condition = f.Condition as Query.ConditionA;

                IList<Model.StockCheck> mStockChecklist = (this.manager as BL.StockCheckManager).SelectByDateRage(condition.StartDate, condition.EndDate);
                foreach (Model.StockCheck sc in mStockChecklist)
                {
                    Model.ProductCategory _pc = new BL.ProductCategoryManager().Get(sc.ProductCategoryId);
                    sc.ProductCategoryName = _pc == null ? "" : _pc.ProductCategoryName;
                }
                this.bindingSource1.DataSource = mStockChecklist;
                this.gridView1.GroupPanelText = condition.StartDate.ToString("yyyy-MM-dd") + "到" + condition.EndDate.ToString("yyyy-MM-dd") + "的記錄";
            }
        }
    }
}