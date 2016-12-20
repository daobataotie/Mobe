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
    public partial class ProductMouldSizeList : Settings.BasicData.BaseListForm
    {
        public ProductMouldSizeList()
        {
            if (!ProductMouldSize.isDelete)
                this.CancleDelete();
            InitializeComponent();

            this.manager = new BL.ProductMouldSizeManager();
        }

        protected override void RefreshData()
        {
            //默认显示一个月记录
            this.bindingSource1.DataSource = (this.manager as BL.ProductMouldSizeManager).SelectByDateRage(DateTime.Now.AddDays(-30), DateTime.Now);
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Query.ConditionAChooseForm f = new Book.UI.Query.ConditionAChooseForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                Query.ConditionA condtion = f.Condition as Query.ConditionA;
                this.bindingSource1.DataSource = (this.manager as BL.ProductMouldSizeManager).SelectByDateRage(condtion.StartDate, condtion.EndDate);
                this.gridControl1.RefreshDataSource();
            }
            f.Dispose();
            GC.Collect();
        }


    }
}