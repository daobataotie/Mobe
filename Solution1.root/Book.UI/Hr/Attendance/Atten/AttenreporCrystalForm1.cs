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
    public partial class AttenreporCrystalForm1 : DevExpress.XtraEditors.XtraForm
    { 
        private HrDailyEmployeeAttendInfoManager hremp = new HrDailyEmployeeAttendInfoManager();
        private Model.Employee _employee;
        private DateTime _date;
        private DataSet ds;
        public AttenreporCrystalForm1()
        {
            InitializeComponent();
        }

        public AttenreporCrystalForm1(DateTime datetime)
            : this()
        {
            this._date = datetime;
            ds = hremp.SelectHrInfoByStateAndDate(_date);
        }

        public AttenreporCrystalForm1(Model.Employee Employee ,DateTime datetime):this()
        {
            this._employee = Employee;
            this._date = datetime;
            ds = hremp.SelectByEmpMonth(_employee, _date);
        }

        private void AttenreporCrystalForm1_Load(object sender, EventArgs e)
        {
            DataTable dt = ds.Tables[0];
            DataTable joindate = hremp.GetemployeeJoinDate(_employee).Tables[0];
            string str = joindate.Rows[0][0].ToString();
            dt.TableName = "attentreport";
            AttenreporCrystal1 attenreport = new AttenreporCrystal1();
            attenreport.SetDataSource(ds);
            attenreport.SetParameterValue("DateTime", _date.ToString("yyyy年MM月")+"---出勤記錄月報表");
            attenreport.SetParameterValue("empName", _employee.EmployeeName);
            attenreport.SetParameterValue("JoinDate", str);
            crystalReportViewer1.ReportSource = attenreport;
        }

      
    }
}