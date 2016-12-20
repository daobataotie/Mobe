using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Book.UI.Hr.Attendance.Leave
{
    public partial class ROLeaveList : DevExpress.XtraReports.UI.XtraReport
    {
        public ROLeaveList()
        {
            InitializeComponent();
        }

        public ROLeaveList(IList<LeaveListModel> leaveList, string reportdate)
            : this()
        {
            this.DataSource = leaveList;

            this.lblCompanyName.Text = BL.Settings.CompanyChineseName;
            this.lblReportName.Text = reportdate + Properties.Resources.LeaveListPrint;
            this.lblReportDate.Text = "列印日期：" + DateTime.Now.ToShortDateString();

            //明细
            this.TCEmpId.DataBindings.Add("Text", this.DataSource, LeaveListModel.PRO_EmpId);
            this.TCEmpName.DataBindings.Add("Text", this.DataSource, LeaveListModel.PRO_EmpName);
            this.TCLeaveDateList.DataBindings.Add("Text", this.DataSource, LeaveListModel.PRO_LeaveDateList);
            this.TCQuantity.DataBindings.Add("Text", this.DataSource, LeaveListModel.PRO_LeaveQuantity);
        }

    }
}
