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
    public partial class SendMailList : Settings.BasicData.BaseListForm
    {
        public SendMailList()
        {
            if (!EditForm.isDelete)
                this.CancleDelete();
            InitializeComponent();
            this.manager = new BL.ExportSendMailManager();
        }

        protected override void RefreshData()
        {
            this.bindingSource1.DataSource = (this.manager as BL.ExportSendMailManager).SelectByDateRage(DateTime.Now.AddDays(-7), global::Helper.DateTimeParse.EndDate);
            this.gridView1.GroupPanelText = "默認顯示一周内的記錄";
        }

        private void barBtnChangeDate_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Query.ConditionAChooseForm f = new Book.UI.Query.ConditionAChooseForm();

            if (f.ShowDialog(this) == DialogResult.OK)
            {
                Query.ConditionA condition = f.Condition as Query.ConditionA;
                this.bindingSource1.DataSource = (this.manager as BL.ExportSendMailManager).SelectByDateRage(condition.StartDate, condition.EndDate);
                this.gridControl1.RefreshDataSource();
            }
        }
    }
}