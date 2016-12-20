using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.produceManager.MouldCategory
{
    public partial class ProductsMouldTestList : Settings.BasicData.BaseListForm
    {
        public ProductsMouldTestList()
        {
            InitializeComponent();
            this.manager = new BL.ProductMouldTestManager();
        }

        protected override void RefreshData()
        {
            this.bindingSource1.DataSource = (this.manager as BL.ProductMouldTestManager).SelectByDateRage(DateTime.Now.AddDays(-15), global::Helper.DateTimeParse.EndDate);
            this.gridView1.GroupPanelText = "默認顯示半個月內記錄";
        }

        private void barBtn_Changetime_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Query.ConditionAChooseForm f = new Query.ConditionAChooseForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                Query.ConditionA condtion = f.Condition as Query.ConditionA;
                this.bindingSource1.DataSource = (this.manager as BL.ProductMouldTestManager).SelectByDateRage(condtion.StartDate, condtion.EndDate);
                this.gridControl1.RefreshDataSource();
                this.gridView1.GroupPanelText = condtion.StartDate.ToShortDateString() + "~" + condtion.EndDate.ToShortDateString() + "的記錄";
            }
            f.Dispose();
            GC.Collect();
        }

    }
}