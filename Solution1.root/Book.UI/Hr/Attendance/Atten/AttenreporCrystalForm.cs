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
   // Copyright (C) 2008 - 2010  西安飛馳軟件有限公司
   //                     版權所有 圍著必究
   // 文 件 名：ChooseAcademicBackGroundForm
   // 编 码 人: 马艳军                   完成时间:2009-09-09
   // 修改原因：
   // 修 改 人:                          修改时间:
   // 修改原因：
   // 修 改 人:                          修改时间:
   //----------------------------------------------------------------*/
    public partial class AttenreporCrystalForm : DevExpress.XtraEditors.XtraForm
    {
        public AttenreporCrystalForm()
        {
            InitializeComponent();

        }
        private int printclass = 0;
        private HrDailyEmployeeAttendInfoManager hremp = new HrDailyEmployeeAttendInfoManager();
        private Model.Employee _employee;
        private DateTime _date;
        private DataSet _ds = null;
        //------------------------异常列表参数------------------//
        private DataSet _anormalyDS = null;
        private DateTime _anormalyDateTime;
        //------------------------异常列表参数------------------//
        public AttenreporCrystalForm(Model.Employee Employee, DateTime datetime, DataSet ds)
            : this()
        {
            this._employee = Employee;
            this._date = datetime;
            this._ds = ds;
            this.printclass = 1;
        }
        //异常列印
        public AttenreporCrystalForm(DataSet pritntView, DateTime _dutyTime)
            : this()
        {
            this._anormalyDS = pritntView;
            this._anormalyDateTime = _dutyTime;
            this.printclass = 2;
        }
        private void AttenreporCrystalForm_Load(object sender, EventArgs e)
        {
            switch (this.printclass)
            {
                case 1:
                    DataTable dt = _ds.Tables[0];
                    dt.TableName = "attentreport";
                    AttenreporCrystal attenreport = new AttenreporCrystal();
                    attenreport.SetDataSource(_ds);
                    attenreport.SetParameterValue("Search_DateTime", _date.ToString("yyyy年MM月"));
                    attenreport.SetParameterValue("EmployeeName", "---" + _employee.EmployeeName + "出勤記錄");
                    crv_attenreport.ReportSource = attenreport;
                    break;
                case 2:
                    AnormalySalaryCrystal anormalyCry = new AnormalySalaryCrystal();
                    anormalyCry.SetDataSource(this._anormalyDS.Tables[0]);
                    anormalyCry.SetParameterValue("AnormalyReportDate", this._anormalyDateTime.ToShortDateString());
                    anormalyCry.SetParameterValue("PrintDate", DateTime.Now.ToString("yyyy-MM-dd"));
                    crv_attenreport.ReportSource = anormalyCry;
                    break;
            }

        }
    }
}