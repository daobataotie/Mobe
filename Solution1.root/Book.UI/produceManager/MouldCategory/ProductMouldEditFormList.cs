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
    public partial class ProductMouldEditFormList : Settings.BasicData.BaseListForm
    {
        public ProductMouldEditFormList()
        {
            if (!ProductMouldEditForm.isDelete)
                this.CancleDelete();
            InitializeComponent();

            this.manager = new BL.ProductMouldManager();
        }

        protected override void RefreshData()
        {
            //默认显示一个月的记录
            this.bindingSource1.DataSource = (this.manager as BL.ProductMouldManager).SelectByDateRage(DateTime.Now.AddDays(-30), DateTime.Now, null, null, null);
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Query.ConditionProductMould f = new Book.UI.Query.ConditionProductMould();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                this.bindingSource1.DataSource = (this.manager as BL.ProductMouldManager).SelectByDateRage(f.ConditionPM.StartDate, f.ConditionPM.EndDate, f.ConditionPM.MouldId, f.ConditionPM.MouldName, f.ConditionPM.MouldCategory);
                this.gridControl1.RefreshDataSource();
            }
            f.Dispose();
            GC.Collect();
        }
    }
}