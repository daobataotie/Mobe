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
    public partial class OpticsTestList : Settings.BasicData.BaseListForm
    {
        private string _PCPGOnlineCheckDetailId = string.Empty;
        private string _PCFinishCheckId = string.Empty;
        int _FromPcFinishCheck = 0;

        public OpticsTestList()
        {
            if (!OpticsTest.isDelete)
                this.CancleDelete();
            InitializeComponent();
            this.manager = new BL.OpticsTestManager();
        }

        public OpticsTestList(string PCPGOnlineCheckDetailId)
            : this()
        {
            this._PCPGOnlineCheckDetailId = PCPGOnlineCheckDetailId;
        }

        public OpticsTestList(string PCFinishCheckId, int i)
            : this()
        {
            this._PCFinishCheckId = PCFinishCheckId;
            this._FromPcFinishCheck = i;
        }

        protected override void RefreshData()
        {
            if (this._FromPcFinishCheck == 0)
            {
                this.bindingSource1.DataSource = (this.manager as BL.OpticsTestManager).SelectByDateRage(DateTime.Now.AddDays(-15), global::Helper.DateTimeParse.EndDate, this._PCPGOnlineCheckDetailId);
            }
            else
            {
                this.bindingSource1.DataSource = (this.manager as BL.OpticsTestManager).FSelectByDateRage(DateTime.Now.AddDays(-15), global::Helper.DateTimeParse.EndDate, this._PCFinishCheckId);
            }

            this.gridView1.GroupPanelText = "默認顯示半个月内的記錄";
        }

        private void barBtnChangeDateSearch_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Query.ConditionAChooseForm f = new Book.UI.Query.ConditionAChooseForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                Query.ConditionA condition = f.Condition as Query.ConditionA;
                if (this._FromPcFinishCheck == 0)
                {
                    this.bindingSource1.DataSource = (this.manager as BL.OpticsTestManager).SelectByDateRage(condition.StartDate, condition.EndDate, this._PCPGOnlineCheckDetailId);
                }
                else
                {
                    this.bindingSource1.DataSource = (this.manager as BL.OpticsTestManager).FSelectByDateRage(condition.StartDate, condition.EndDate, this._PCFinishCheckId);
                }
                this.gridControl1.RefreshDataSource();
                this.gridView1.GroupPanelText = condition.StartDate.ToShortDateString() + "~" + condition.EndDate.ToShortDateString() + "的記錄";
            }
            f.Dispose();
            GC.Collect();
        }
    }
}