using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.Hr.Salary.Salaryset
{
    /*----------------------------------------------------------------
// Copyright (C) 2008 - 2010  �����w�Yܛ�����޹�˾
//                     ������� �����ؾ�

// �� �� ��: ���޾�              ���ʱ��:2009-10-25
// �޸�ԭ��
// �� �� ��:                          �޸�ʱ��:
// �޸�ԭ��
// �� �� ��:                          �޸�ʱ��:
//----------------------------------------------------------------*/
    public partial class CalculationListForm : DevExpress.XtraEditors.XtraForm
    {
        private BL.HrDailyEmployeeAttendInfoManager _hrDailyManger = new BL.HrDailyEmployeeAttendInfoManager();

        private MonthSalaryClass _ms;

        public CalculationListForm()
        {
            InitializeComponent();
        }

        public CalculationListForm(MonthSalaryClass ms)
            : this()
        {
            this._ms = ms;
        }

        private void CalculationListForm_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            InitDailyInfo();
            InitControlValue();
        }

        //���ݵ�ǰԱ�������ڽ��в�ѯ
        protected void InitDailyInfo()
        {
            DataSet _dailyInfo = this._hrDailyManger.SelectDailyInfoByEmployee(this._ms.mEmployeeId, this._ms.mIdentifyDate, string.Empty);
            this.bindingSourceDailyInfo.DataSource = _dailyInfo.Tables[0];
        }

        //Ϊ�����ؼ���ֵ
        protected void InitControlValue()
        {
            this.txtLateCount.Text = this._ms.mLateCount.ToString();                //�ٵ�����
            this.txtLateMinute.Text = this._ms.mLateInMinute.ToString();            //�ٵ���ʱ��(��)
            this.txtBusinessPay.EditValue = this._ms.mSpecialBonus;      //������
            this.txtDayCount.Text = this._ms.mDaysFactor.ToString();                //�ջ���
            this.txtMonthCount.Text = this._ms.mMonthFactor.ToString();             //�»���
            this.txtSpecialHoliday.EditValue = this._ms.mTotalHoliday;   //�����м�����
            this.txtDaySalary.EditValue = this._ms.mDailyPay;            //��н
            this.txtMonthSalary.EditValue = this._ms.mMonthlyPay;        //��н
            this.txtLeaveCount.EditValue = this._ms.mLeaveCount;         //�����������
            this.txtTruancy.Text = this._ms.mAbsentCount.ToString();                //��ְ����
        }

        private void gridView1_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            switch (e.Column.Name)
            {
                case "LateInMinute":
                case "BusinessHours":
                case "DayFactor":
                case "MonthFactor":
                case "gridColumn1":
                    try
                    {
                        if (int.Parse(e.DisplayText) == 0)
                            e.DisplayText = string.Empty;
                        break;
                    }
                    catch
                    {
                        break;
                    }
            }
        }
    }
}