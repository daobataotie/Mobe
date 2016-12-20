using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.produceManager.ExportSendMail
{
    public partial class ChooseExport : Settings.BasicData.BaseListForm
    {
        public ChooseExport()
        {
            InitializeComponent();
            this.manager = new BL.PCExportReportANSIManager();
        }

        protected override void RefreshData()
        {
            this.bindingSource1.DataSource = (this.manager as BL.PCExportReportANSIManager).SelectForChooseExport(DateTime.Now.AddDays(-7), global::Helper.DateTimeParse.EndDate, null, null, "", "");
            this.gridView1.GroupPanelText = "默認顯示半个月内的記錄,外銷報告類型不限制";
        }

        private void barBtnChangeSearch_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ChooseConditionExport f = new ChooseConditionExport();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                ConditionExport condition = f.Condition as ConditionExport;
                this.bindingSource1.DataSource = (this.manager as BL.PCExportReportANSIManager).SelectForChooseExport(condition.StartDate, condition.EndDate, condition.Product, condition.Customer, condition.CusXOId, condition.ExpType);
                this.gridControl1.RefreshDataSource();
            }
        }
    }
}