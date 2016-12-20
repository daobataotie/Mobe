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
   // Copyright (C) 2008 - 2010  �����w�Yܛ�����޹�˾
   //                     ������� �����ؾ�
   // �� �� ����ChooseAcademicBackGroundForm
   // �� �� ��: ���޾�                   ���ʱ��:2009-09-09
   // �޸�ԭ��
   // �� �� ��:                          �޸�ʱ��:
   // �޸�ԭ��
   // �� �� ��:                          �޸�ʱ��:
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
        //------------------------�쳣�б����------------------//
        private DataSet _anormalyDS = null;
        private DateTime _anormalyDateTime;
        //------------------------�쳣�б����------------------//
        public AttenreporCrystalForm(Model.Employee Employee, DateTime datetime, DataSet ds)
            : this()
        {
            this._employee = Employee;
            this._date = datetime;
            this._ds = ds;
            this.printclass = 1;
        }
        //�쳣��ӡ
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
                    attenreport.SetParameterValue("Search_DateTime", _date.ToString("yyyy��MM��"));
                    attenreport.SetParameterValue("EmployeeName", "---" + _employee.EmployeeName + "����ӛ�");
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