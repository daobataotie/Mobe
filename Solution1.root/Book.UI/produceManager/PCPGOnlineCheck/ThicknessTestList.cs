using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.produceManager.PCPGOnlineCheck
{
    public partial class ThicknessTestList : Settings.BasicData.BaseListForm
    {
        private string _PCPGOnlineCheckDetailId = string.Empty;

        public ThicknessTestList()
        {
            if (!ThicknessTest.isDelete)
                this.CancleDelete();
            InitializeComponent();
            this.manager = new BL.ThicknessTestManager();
        }

        public ThicknessTestList(string PCPGOnlineCheckDetailId)
            : this()
        {
            this._PCPGOnlineCheckDetailId = PCPGOnlineCheckDetailId;
        }

        protected override void RefreshData()
        {
            this.bindingSource1.DataSource = (this.manager as BL.ThicknessTestManager).SelectByDateRage(DateTime.Now.AddDays(-15), global::Helper.DateTimeParse.EndDate, this._PCPGOnlineCheckDetailId);
            this.gridView1.GroupPanelText = "默認顯示半个月内的記錄";
        }

        private void barBtnChangeDate_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Query.ConditionAChooseForm f = new Book.UI.Query.ConditionAChooseForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                Query.ConditionA condition = f.Condition as Query.ConditionA;
                this.bindingSource1.DataSource = (this.manager as BL.ThicknessTestManager).SelectByDateRage(condition.StartDate, condition.EndDate, this._PCPGOnlineCheckDetailId);
                this.gridControl1.RefreshDataSource();
                this.gridView1.GroupPanelText = condition.StartDate.ToShortDateString() + "~" + condition.EndDate.ToShortDateString() + "的記錄";
            }
            f.Dispose();
            GC.Collect();
        }
    }
}