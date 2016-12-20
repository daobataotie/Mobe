using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;
using System.Text;

namespace Book.UI.Hr.Attendance.OverTime
{
    public partial class OverTimeList : DevExpress.XtraReports.UI.XtraReport
    {
        private BL.OverTimeManager _overtimeManager = new Book.BL.OverTimeManager();

        public OverTimeList()
        {
            InitializeComponent();
        }

        public OverTimeList(IList<OverTimeListModel> otListModel, string reportdate)
            : this()
        {

            this.DataSource = otListModel;

            this.lblCompanyName.Text = BL.Settings.CompanyChineseName;
            this.lblReportName.Text = reportdate + Properties.Resources.OverTimeListPrint;
            this.lblPrintDate.Text = "列印日期：" + DateTime.Now.ToShortDateString();

            //明细
            this.TCDateList.DataBindings.Add("Text", this.DataSource, OverTimeListModel.PRO_DateList);
            this.TCEmployeeID.DataBindings.Add("Text", this.DataSource, OverTimeListModel.PRO_EmpId);
            this.TCEmpName.DataBindings.Add("Text", this.DataSource, OverTimeListModel.PRO_EmpName);
            this.TCtotalHour.DataBindings.Add("Text", this.DataSource, OverTimeListModel.PRO_TotalHour);
        }
    }
}


#region 注释
//DateTime startdate = DateTime.Parse(yearAndMonth + "/01");
//DateTime enddate = startdate.Date.AddMonths(1).AddDays(-1);

//IList<Model.OverTime> Details = this._overtimeManager.SelectOverTimeList(startdate, enddate);

//IList<OverTimeListModel> otListModel = new List<OverTimeListModel>();

//var query = from listModel in Details
//            group listModel by listModel.EmployeeId;

//foreach (IGrouping<string, Model.OverTime> overtime in query)
//{
//    OverTimeListModel otlm = new OverTimeListModel();
//    otlm.EmpId = overtime.First<Model.OverTime>().Employee.IDNo;
//    otlm.EmpName = overtime.First<Model.OverTime>().Employee.EmployeeName;
//    double a = 0;
//    StringBuilder sb = new StringBuilder();
//    foreach (Model.OverTime ot in overtime)
//    {
//        a += ot.EoverTime;
//        sb.Append(ot.DueDate.ToString("yyy-MM-dd") + ",");
//    }
//    otlm.TotalHour = a.ToString();
//    otlm.DateList = sb.ToString();

//    otListModel.Add(otlm);
//}

//if (otListModel == null || otListModel.Count == 0)
//    return;
#endregion
