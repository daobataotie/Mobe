using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Book.BL;

namespace Book.UI.Hr.Attendance.Atten
{
    /*----------------------------------------------------------------
// Copyright (C) 2008 - 2010  咸阳飛馳軟件有限公司
//                     版權所有 圍著必究

// 编 码 人:  徐炎飞             完成时间:2010-02-20
// 修改原因：
// 修 改 人:                          修改时间:
// 修改原因：
// 修 改 人:                          修改时间:
//----------------------------------------------------------------*/
    public partial class AttenreportForm2 : DevExpress.XtraEditors.XtraForm
    {
        private HrDailyEmployeeAttendInfoManager hremp = new HrDailyEmployeeAttendInfoManager();
        private DateTime _date;
        DataSet ds;

        public AttenreportForm2()
        {
            InitializeComponent();
        }

        public AttenreportForm2(DateTime date)
            : this()
        {
            this._date = date;
        }

        private void AttenreportForm2_Load(object sender, EventArgs e)
        {
            ds = hremp.SelectByEmpMonth(_date);
            DataTable dt = ds.Tables[0];
            //DateTime bgdate = Convert.ToDateTime(dt.Rows[0]["ShouldCheckIn"].ToString());
            //DateTime endate = Convert.ToDateTime(dt.Rows[0]["ShouldCheckOut"].ToString());

            dt.TableName = "attentreport";
            AttenreportCrystal2 attenreport = new AttenreportCrystal2();
            attenreport.SetDataSource(ds);
            CrystalDecisions.Shared.ParameterValues employeename = new CrystalDecisions.Shared.ParameterValues();
            CrystalDecisions.Shared.ParameterDiscreteValue PDEmployeeId = new CrystalDecisions.Shared.ParameterDiscreteValue();

            //attenreport.SetParameterValue("beginTime", bgdate.ToString("HH:mm"));
            //attenreport.SetParameterValue("toTime", endate.ToString("HH:mm"));

            //CrystalDecisions.Shared.ParameterValues EmployeeName = new CrystalDecisions.Shared.ParameterValues();
            //CrystalDecisions.Shared.ParameterDiscreteValue PDEmployeeID = new CrystalDecisions.Shared.ParameterDiscreteValue();
            //PDEmployeeID.Value = "EmployeeName";
            //EmployeeName.Add(PDEmployeeID);
            crystalReportViewer1.ReportSource = attenreport;
        }
    }
}