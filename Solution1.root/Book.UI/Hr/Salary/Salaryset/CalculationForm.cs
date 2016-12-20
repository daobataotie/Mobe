using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Data.SqlClient;

namespace Book.UI.Hr.Salary.Salaryset
{
    /*----------------------------------------------------------------
// Copyright (C) 2008 - 2010  �����w�Yܛ�����޹�˾
//                     ������� �����ؾ�

// �� �� ��: ���޾�             ���ʱ��:2009-10-26
// �޸�ԭ��
// �� �� ��:                          �޸�ʱ��:
// �޸�ԭ��
// �� �� ��:                          �޸�ʱ��:
//----------------------------------------------------------------*/
    public partial class CalculationForm : DevExpress.XtraEditors.XtraForm
    {
        /// <summary>
        /// Ա������ʵ��
        /// </summary>
        protected BL.EmployeeManager employeeManager = new Book.BL.EmployeeManager();
        /// <summary>
        /// ���Ź���ʵ��
        /// </summary>
        // protected BL.DepartmentManager departmentManager = new Book.BL.DepartmentManager();
        /// <summary>
        /// ��ǰ������Ա��
        /// </summary>


        /// <summary>
        /// Ո�ٹ���
        /// </summary>
        BL.OverTimeManager overtimeman = new Book.BL.OverTimeManager();
        ///// <summary>
        ///// ���ڼ�¼
        ///// </summary>
        //private BL.AttenManager attenmanage = new Book.BL.AttenManager();
        /// <summary>
        /// ��ٹ���
        /// </summary>
        private BL.LeaveManager leavemanage = new Book.BL.LeaveManager();

        private BL.OverTimeManager overtimemanage = new Book.BL.OverTimeManager();
        /// <summary>
        /// ��Ͳ�������
        /// </summary>
        private BL.LunchDetailManager lunchdetailmanage = new Book.BL.LunchDetailManager();
        /// <summary>
        /// ��֧����
        /// </summary>
        private BL.LoanDetailManager loandetailManage = new Book.BL.LoanDetailManager();
        BL.MonthlySalaryManager monthlySalaryManager = new Book.BL.MonthlySalaryManager();
        private BL.HrDailyEmployeeAttendInfoManager _hrManager = new Book.BL.HrDailyEmployeeAttendInfoManager();
        // Model.HrAttendStat _hrAttendStat;
        private MonthSalaryClass _ms;
        private Model.MonthlySalary _monthlySalary = new Book.Model.MonthlySalary();
        private BL.HrAttendStatManager hrAttendManager = new Book.BL.HrAttendStatManager();
        private IList<Model.Employee> _emplist = new List<Model.Employee>();
        private int hryear = 0;
        private int hrmonth = 0;

        /// <summary>
        /// ʵ�ʹ�������
        /// </summary>
        //int workcount = 30;
        /// <summary>
        /// ��н
        /// </summary>
        // decimal monthsum = 0M;
        /// <summary>
        /// ��������
        /// </summary>
        // decimal WorkBonus = 0M;
        /// <summary>
        ///������
        /// </summary>
        // decimal bunbonus = 0M;
        /// <summary>
        /// ȫ�ڽ�
        /// </summary>
        //  decimal Perfectbonus = 0M;
        /// <summary>
        /// ��Ч����
        /// </summary>
        //decimal jxbonus = 0M;
        /// <summary>
        /// ��ҵ��Ч����
        /// </summary>
        // decimal zyjsbonus = 0M;
        /// <summary>
        /// ��н����
        /// </summary>
        //int Punishpay = 0;
        /// <summary>
        /// ��н�ܼ�
        /// </summary>
        //decimal dxsum = 0M;
        /// <summary>
        /// ʵ�칤��
        /// </summary>
        //decimal gongzslsum = 0M;
        /// <summary>
        /// Ա��
        /// </summary>
        private Book.Model.Employee employee = null;
        /// <summary>
        /// ���ջ���
        /// </summary>
        //decimal dayfactorSum = 0;
        ///// <summary>
        ///// ���»���
        ///// </summary>
        //decimal monthFactorSum = 0;
        ///// <summary>
        ///// �ܳ�����
        ///// </summary>
        //int DutyDateCount = 0;
        ///// <summary>
        ///// �Ӱ����
        ///// </summary>
        //decimal OverTimeBonus = decimal.Zero;
        ///// <summary>
        ///// ƽ�ռӰ��н
        ///// </summary>
        //decimal NormalFee = decimal.Zero;
        ///// <summary>
        ///// ���ռӰ��н
        ///// </summary>
        //decimal HolidayFee = decimal.Zero;
        /// <summary>
        /// �ٵ��ۿ�
        /// </summary>
        // decimal LatePunish = decimal.Zero;

        public CalculationForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// ���幹�캯��
        /// </summary>
        /// <param name="employees"></param>
        public CalculationForm(Model.Employee employees)
        {
            this.employee = employees;
            Calulation(employees);
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CalculationForm_Load(object sender, EventArgs e)
        {
            //this.SetBounds((Screen.GetBounds(this).Width / 2) - (this.Width / 2), (Screen.GetBounds(this).Height / 2) - (this.Height / 2), this.Width, this.Height, BoundsSpecified.Location);
            //this.StartPosition = FormStartPosition.CenterScreen;

            DateTime date = this.monthlySalaryManager.get_MaxIdentifyDateMonth();
            if (date.Year != 1)
            {
                DateTime strdate = date.AddMonths(1);

                for (int i = 1; i < 10; i++)
                {
                    this.comboBoxEdit1.Properties.Items.Add(strdate.AddMonths(-1).ToString("yyyy��MM��"));
                    strdate = strdate.AddMonths(-1);
                }
                this.comboBoxEdit1.SelectedIndex = 0;
                this.hryear = Int32.Parse(this.comboBoxEdit1.Text.Substring(0, 4));
                this.hrmonth = Int32.Parse(this.comboBoxEdit1.Text.Substring(5, 2));
                _emplist = this.employeeManager.SelectHrDailyAttend(new DateTime(hryear, hrmonth, 1));
                this.bindingSourceEmployee.DataSource = _emplist;
            }
        }

        /// <summary>
        /// н��
        /// </summary>
        private int flag = 0;

        private void Calulation(Model.Employee emp)
        {
            int mTemp = 0;
            int mHicount = 0;
            double mLateHalfCount = 0;
            StringBuilder strBU = new StringBuilder();
            int totalDay = DateTime.DaysInMonth(hryear, hrmonth);
            //////////////////////////////////////////////////////////////////
            _ms = new MonthSalaryClass();
            _ms.mIdentifyDate = new DateTime(hryear, hrmonth, 1);
            _ms.mEmployeeId = emp.EmployeeId;
            _ms.mEmployeeName = emp.EmployeeName;
            _ms.mIDNo = emp.IDNo;
            _ms.mDepartmetName = emp.Department.DepartmentName;

            #region  ȡ���ڼ�¼
            //  DataSet ds = this.monthlySalaryManager.getAttendInfoList(emp.EmployeeId, hryear, hrmonth); 
            foreach (Model.HrDailyEmployeeAttendInfo attend in this._hrManager.SelectByEmpMonth(emp, hryear, hrmonth))
            {
                if (attend.LateInMinute.HasValue && attend.LateInMinute.Value != 0)
                {
                    strBU.Append(attend.LateInMinute.ToString() + "|");
                    mTemp = mTemp + attend.LateInMinute.Value;
                    //if (mTemp <= 10)
                    //{
                    //    mCount = mCount + 1;
                    //}
                    if (attend.LateInMinute.Value > 30)
                    {
                        mHicount = mHicount + 1;
                        if ((attend.LateInMinute.Value + 30) % 60 > 30)
                        {
                            mLateHalfCount = mLateHalfCount + (attend.LateInMinute.Value + 30) / 60 + 0.5;
                        }
                        else
                        {
                            mLateHalfCount = mLateHalfCount + (attend.LateInMinute.Value + 30) / 60;
                        }
                    }
                    //��ְ������۳�������������
                }
                _ms.mNote = attend.Note;
                if (!string.IsNullOrEmpty(_ms.mNote))
                {
                    if (_ms.mNote != "�L���ݼ�" && _ms.mNote.IndexOf("����") < 0 && _ms.mNote.IndexOf("���") < 0 && _ms.mNote.IndexOf("�ʼ�") < 0 && _ms.mNote.IndexOf("���") < 0 && _ms.mNote.IndexOf("����") < 0 && _ms.mNote.IndexOf("�t��") < 0)
                        _ms.mCount = _ms.mCount + 1;
                    else if (_ms.mNote.Contains("�¼�") || _ms.mNote.Contains("����"))
                        _ms.mCount = _ms.mCount + 1;

                }
            }
            #endregion
            #region ȡн�ʼ���
            DataSet dsms = this.monthlySalaryManager.getMonthlySummaryFee(emp.EmployeeId, _ms.mIdentifyDate, hryear, hrmonth);
            if (dsms.Tables[0].Rows.Count > 0)
            {
                DataRow dr = dsms.Tables[0].Rows[0];
                _ms.mLunchFee = mStrToDouble(dr["LunchFee"]);                                 //����M
                _ms.mOverTimeFee = mStrToDouble(dr["OverTimeFee"]);                           //�Ӱ��M
                _ms.mGeneralOverTime = mStrToDouble(dr["GeneralOverTime"]);                   //ƽ�ռӰ�(�r��)
                _ms.mHolidayOverTime = mStrToDouble(dr["HolidayOverTime"]);                   //���ռӰ�(�r��)
                _ms.GeneralOverTimeCountBig = mStrToDouble(dr["GeneralOverTimeCountBig"]);    //ƽ�ռӰ�2Сʱ֮��(�r��)
                _ms.GeneralOverTimeCountSmall = mStrToDouble(dr["GeneralOverTimeCountSmall"]);//ƽ�ռӰ�2Сʱ����(�r��)
                _ms.HolidayOverTimeCountBig = mStrToDouble(dr["HolidayOverTimeCountBig"]);    //���ռӰ�2Сʱ֮��(�r��)
                _ms.HolidayOverTimeCountSmall = mStrToDouble(dr["HolidayOverTimeCountSmall"]);//���ռӰ�2Сʱ����(�r��)
                _ms.mLateCount = mStrToDouble(dr["LateCount"]);                               //�t���Δ�
                _ms.mTotalLateInMinute = mStrToDouble(dr["TotalLateInMinute"]);               //���t�����֣�
                _ms.mOverTimeBonus = mStrToDouble(dr["OverTimeBonus"]);                       //�Ӱ���N
                _ms.mSpecialBonus = mStrToDouble(dr["SpecialBonus"]);                         //��ҹ����N
                _ms.mDaysFactor = mStrToDouble(dr["DaysFactor"]);                             //���ջ���
                _ms.mMonthFactor = mStrToDouble(dr["MonthFactor"]);                           //���»���
                _ms.mDutyDateCount = mStrToDouble(dr["DutyDateCount"]);                       //������ӛ䛔�
                _ms.mLeaveDate = (dr["LeaveDate"] == null || dr["LeaveDate"].ToString() == "") ? global::Helper.DateTimeParse.NullDate : Convert.ToDateTime(dr["LeaveDate"].ToString());                                               //��ְ����
                _ms.mPunishLeaveCount = mStrToDouble(dr["PunishLeaveCount"]);                 //���ۿ�ٿ���
                _ms.mLeaveCount = mStrToDouble(dr["LeaveCount"]);                             //Ո�ٿ���
                _ms.mAbsentCount = mStrToDouble(dr["AbsentCount"]);                           //�繤����
                _ms.mTotalHoliday = mStrToDouble(dr["TotalHoliday"]);                         //ԓ�¿����ٔ�
                _ms.mLoanFee = mStrToDouble(dr["LoanFee"]);
                // int WuXinCount = Int32.Parse(dr["WuXinCount"].ToString());
                //���� ����һ��  �ջ��� =�»���-ʵ�ʼ���-�ۿ��������-��ְ-��н��      //�󹤴�����  
                if (_ms.mDutyDateCount < totalDay)
                    _ms.mDaysFactor = _ms.mDaysFactor - _ms.mTotalHoliday;
                //    if (_ms.mDutyDateCount < totalDay && _ms.mLeaveDate != global::Helper.DateTimeParse.NullDate && _ms.mLeaveDate.ToString() != "")             //������ӛ䛔�
                //        _ms.mDaysFactor = _ms.mMonthFactor - _ms.mTotalHoliday - _ms.mPunishLeaveCount - _ms.mAbsentCount - WuXinCount;
                //    else if ((_ms.mLeaveDate == global::Helper.DateTimeParse.NullDate || _ms.mLeaveDate.ToString() == "") && _ms.mDutyDateCount < totalDay)
                //        _ms.mDaysFactor = _ms.mMonthFactor - _ms.mTotalHoliday - _ms.mPunishLeaveCount - _ms.mAbsentCount - WuXinCount;
            }
            else
            {
                _ms.mLoanFee = 0;
                _ms.mLunchFee = 0;
                _ms.mOverTimeFee = 0;
                _ms.mGeneralOverTime = 0;
                _ms.mHolidayOverTime = 0;
                _ms.mLateCount = 0;
                _ms.mTotalLateInMinute = 0;
                _ms.mOverTimeBonus = 0;
                _ms.mSpecialBonus = 0;
                _ms.mDaysFactor = 0;
                _ms.mMonthFactor = 0;
                _ms.mDutyDateCount = 0;
                _ms.mPunishLeaveCount = 0;
                _ms.mLeaveCount = 0;
                _ms.mTotalHoliday = 0;
            }
            dsms.Clear();
            #endregion
            #region ��н
            DataSet dx_ds = this.monthlySalaryManager.getMonthlySalary(emp.EmployeeId, _ms.mIdentifyDate);
            if (dx_ds.Tables[0].Rows.Count > 0)
            {
                _ms.mMonthFactor = _ms.mMonthFactor;
                DataRow dx_dr = dx_ds.Tables[0].Rows[0];
                _ms.mDailyPay = mStrToDouble(dx_dr["DailyPay"]); //�չ���
                _ms.mMonthlyPay = mStrToDouble(dx_dr["MonthlyPay"]); //�¹���
                if (VPerson.vipPerson.Contains(emp.EmployeeId))
                {
                    _ms.mDutyPay = this.GetSiSheWuRu(mStrToDouble(dx_dr["DutyPay"]), 0);  //���ν���
                    _ms.mPostPay = this.GetSiSheWuRu(mStrToDouble(dx_dr["PostPay"]), 0);  //�ս��N
                }
                else
                {
                    _ms.mDutyPay = this.GetSiSheWuRu(mStrToDouble(dx_dr["DutyPay"]) * _ms.mMonthFactor / totalDay, 0);  //���ν���
                    _ms.mPostPay = this.GetSiSheWuRu(mStrToDouble(dx_dr["PostPay"]) * _ms.mMonthFactor / totalDay, 0);  //�ս��N
                }
                _ms.mGivenDays = mStrToDouble(dx_dr["HolidayBonusGivenDays"]);  //���(����)����
                _ms.mAnnualHolidayFee = _ms.mDailyPay * _ms.mGivenDays;         //���(����)���
                _ms.mInsurance = mStrToDouble(dx_dr["insurance"]); //���շ�
                _ms.mTax = mStrToDouble(dx_dr["Tax"]);   //����˰
                _ms.mEffectFactor = mStrToDouble(dx_dr["EffectFactor"]); //��Ч�S��
                _ms.mOtherPay = mStrToDouble(dx_dr["OtherPay"]);  //�����a��
                _ms.mOtherPunish = mStrToDouble(dx_dr["OtherPunish"]); //�����ۿ�
                _ms.mFieldPay = this.GetSiSheWuRu(mStrToDouble(dx_dr["FieldPay"]) * (totalDay - _ms.mCount) / totalDay, 0);  //ְ������
                _ms.mBasePay = _ms.mMonthlyPay * _ms.mMonthFactor / totalDay + _ms.mDailyPay * _ms.mDaysFactor;                //��н
            }
            else
            {
                _ms.mDailyPay = 0;
                _ms.mMonthlyPay = 0;
                _ms.mDutyPay = 0;
                _ms.mPostPay = 0;
                _ms.mGivenDays = 0;
                _ms.mAnnualHolidayFee = 0;
                _ms.mInsurance = 0;
                _ms.mTax = 0;
                _ms.mEffectFactor = 0;
                _ms.mOtherPay = 0;
                _ms.mOtherPunish = 0;
                _ms.mFieldPay = 0;
                _ms.mBasePay = 0;
            }
            dx_ds.Clear();
            #endregion
            #region �ٵ��ۿ�
            int mIcount = 0;  //��С��10�� ����С��3��
            if ((_ms.mTotalLateInMinute > 10 || _ms.mLateCount > 2) && !VPerson.vipPerson.Contains(emp.EmployeeId))
            {
                //��ʱ��¼�ٵ�

                //StringBuilder strBU = new StringBuilder();
                //foreach (Model.HrDailyEmployeeAttendInfo attend in this._hrManager.SelectByEmpMonth(this.employee, hryear, hrmonth))
                //{
                //    if (attend.LateInMinute.Value != 0)
                //    {
                //        strBU.Append(attend.LateInMinute.ToString() + "|");
                //        mTemp = mTemp + attend.LateInMinute.Value;
                //        if (mTemp <= 10)
                //        {
                //            mCount = mCount + 1;
                //        }
                //        if (attend.LateInMinute.Value > 30)
                //        {
                //            mHicount = mHicount + 1;
                //        }
                //    }
                //}
                // '�t�����^���Σ����t�����^10���
                //'����С�r����0.5С�r��̶�
                string[] strs = new string[31];
                IList<int> a = new List<int>();

                if (strBU.Length > 0)
                    strs = strBU.ToString(0, strBU.Length - 1).Split('|');
                for (int i = 0; i < strs.Length; i++)
                {
                    if (strs[i] != null)
                    {
                        if (strs[i] == "0")
                            continue;
                        else
                            a.Add(Int32.Parse(strs[i]));
                    }
                }

                int temp = 0;
                for (int i = 0; i < a.Count; i++)
                {
                    for (int j = i + 1; j < a.Count; j++)
                    {
                        if (a[i] > a[j])
                        {
                            temp = a[i];
                            a[i] = a[j];
                            a[j] = temp;
                        }

                    }
                }
                int sum = 0;
                int m;
                for (m = 0; m < a.Count; m++)
                {
                    sum = sum + a[m];
                    if (sum > 10)
                        break;
                }
                if (m > 2)
                {
                    m = 2;
                }
                mIcount = m;
                _ms.mTotalLateInHour = (_ms.mLateCount - mIcount - mHicount) * 0.5 + mLateHalfCount;
                _ms.mLatePunish = _ms.mTotalLateInHour * double.Parse(_ms.mDailyPay.ToString()) / 8;
            }
            else
            {
                _ms.mTotalLateInHour = 0;
                _ms.mLatePunish = 0;
                mIcount = (int)_ms.mLateCount;  // 10������ 3��֮�� ����ȫ�ڽ�
            }
            #endregion
            #region ȫ�ڽ�
            if (VPerson.vipPerson.Contains(emp.EmployeeId))
            {
                _ms.mAllAttendBonus = _ms.mDailyPay * 3;
            }
            else
            {
                if (_ms.mDutyDateCount == totalDay)
                {
                    //�����Mһ���£���н����
                    if (_ms.mMonthFactor == totalDay)
                    {
                        if (_ms.mPunishLeaveCount == 0)
                        {
                            _ms.mAllAttendBonus = _ms.mDailyPay * 3;
                        }
                        //Ո���ۿ��С춵��һ�죺��н����
                        else if (_ms.mPunishLeaveCount <= 1)
                        {
                            _ms.mAllAttendBonus = _ms.mDailyPay * 2;
                        }
                        //�Д��t��,�۳�ȫ�ڪ���
                        if (_ms.mLateCount - mIcount > 0)
                        {
                            _ms.mAllAttendBonus = _ms.mAllAttendBonus - _ms.mDailyPay * (_ms.mLateCount - mIcount) >= 0 ? _ms.mAllAttendBonus - _ms.mDailyPay * (_ms.mLateCount - mIcount) : 0;
                        }
                    }
                    //��ȱˢ��ӛ�                 
                    else
                    {
                        _ms.mAllAttendBonus = 0;
                    }
                }
                //δ�Mһ���£������x��
                else
                {
                    _ms.mAllAttendBonus = 0;
                }
            }
            #endregion
            #region �xδ���Mһ�������ߣ�ȡ������؟�ν��N�������ս��N�����������N�������L���ݼ١�
            if (_ms.mDutyDateCount < totalDay && _ms.mLeaveDate != global::Helper.DateTimeParse.NullDate && !VPerson.vipPerson.Contains(emp.EmployeeId))
            {
                _ms.mDutyPay = 0;
                _ms.mPostPay = 0;
                _ms.mFieldPay = 0;
            }
            #endregion
            #region ƽ�ռӰ� ���ռӰ� ��н�Ӱ���� ��˰��
            #region //����
            //_ms.mGeneralOverTimeFee = GetSiSheWuRu((_ms.mDailyPay / 6) * _ms.mGeneralOverTime, 0);                   //ƽ�ռӰ��
            //_ms.mHolidayOverTimeFee = GetSiSheWuRu((((_ms.mDailyPay / 2) * 3) / 8) * _ms.mHolidayOverTime, 0);       //���ռӰ��
            #endregion
            #region //���� ����&ƽ�ռӰ��㷨�޸� 2013��4��26��15:09:41 ����
            //ƽ�ռӰ� 0~2Сʱ֮�� ʱн*hour*1.33  2>  ʱн*hour*1.66
            //if (_ms.mGeneralOverTime < 2)
            //{
            //    _ms.mGeneralOverTimeFee = GetSiSheWuRu((_ms.mDailyPay / 8) * _ms.mGeneralOverTime * 1.33, 0);
            //}
            //else
            //{
            //_ms.mGeneralOverTimeFee = GetSiSheWuRu((_ms.mDailyPay / 8) * 2 * 1.33, 0) + GetSiSheWuRu((_ms.mDailyPay / 8) * (_ms.mGeneralOverTime - 2) * 1.66, 0);
            //}
            //ƽ�ռӰ� С��2Сʱ Ϊ ʱн*1.33*�Ӱ�Сʱ,����2Сʱ���� Ϊ ʱн*1.66*�Ӱ�Сʱ
            _ms.mGeneralOverTimeFee = GetSiSheWuRu((_ms.mDailyPay / 8) * _ms.GeneralOverTimeCountSmall * 1.33, 0) + GetSiSheWuRu((_ms.mDailyPay / 8) * _ms.GeneralOverTimeCountBig * 1.66, 0);
            //���ռӰ� һ�� Ϊʱн ����.
            _ms.mHolidayOverTimeFee = GetSiSheWuRu(((_ms.mDailyPay / 8) * 2) * _ms.mHolidayOverTime, 0);
            #endregion
            #endregion
            #region ��������,��Ч����, ��ҵ�������� => ��������=ְ����������ν�����������+ȫ�ڽ�
            int[] months = { 1, 2, 3, 1, 2, 3, 1, 2, 3, 1, 2, 3 };
            switch (months[hrmonth - 1])
            {
                case 1:
                    _ms.mWorkBonus = _ms.mPostPay + _ms.mDutyPay + _ms.mSpecialBonus + _ms.mAllAttendBonus;
                    label_gj.Text = _ms.mWorkBonus.ToString();
                    //label_jxjj.Text = "0";
                    //label_zyjj.Text = "0";
                    break;
                case 2:
                    _ms.mEffectBonus = _ms.mPostPay + _ms.mDutyPay + _ms.mSpecialBonus + _ms.mAllAttendBonus;
                    //label_jxjj.Text = _ms.mEffectBonus.ToString();
                    label_gj.Text = "0";
                    //label_zyjj.Text = "0";
                    break;
                case 3:
                    _ms.mTechBonus = _ms.mPostPay + _ms.mDutyPay + _ms.mSpecialBonus + _ms.mAllAttendBonus;
                    //label_zyjj.Text = _ms.mTechBonus.ToString();
                    //label_jxjj.Text = "0";
                    label_gj.Text = "0";
                    break;
            }

            #endregion
            ////////////////////////////////////////////////////////////////
            flag = 1;

            //����
            this.label_gname.Text = _ms.mEmployeeName;
            //����
            this.label_jiname.Text = _ms.mEmployeeName;
            //��н
            this.label_dx.Text = _ms.mBasePay.ToString();
            //�ͷ�
            this.label_cf.Text = _ms.mLunchFee.ToString();
            //��֧
            this.label_jiez.Text = _ms.mLoanFee.ToString();
            //���� 
            this.label_baox.Text = _ms.mInsurance.ToString();
            //ְ������
            this.label_zhicjt.Text = _ms.mFieldPay.ToString();
            ////��ҵ��������
            //this.label_zyjj.Text = _ms.mTechBonus.ToString();
            //��Ч����
            //this.label_jxjj.Text = _ms.mEffectBonus.ToString();
            //���ռӰ��
            //this.label_jrjb.Text = _ms.mHolidayOverTimeFee.ToString();
            //ƽ�ռӰ��
            //this.label_prjb.Text = _ms.mGeneralOverTimeFee.ToString();
            //�Ӱ����
            //this.label_jbjt.Text = _ms.mOverTimeBonus.ToString();
            //������
            this.label_banbiejintie.Text = _ms.mSpecialBonus.ToString();
            //ְ�����
            this.label_zhiwjt.Text = _ms.mPostPay.ToString();
            //���(����)����
            this.txt_givenDays.Text = _ms.mGivenDays.ToString();
            //���(����)���
            this.label_AnnualHolidayFee.Text = _ms.mAnnualHolidayFee.ToString();
            //��������(ȫ) 2013��3��29��  �������� ����ȫ�ڽ���
            this.label_gj.Text = _ms.mAllAttendBonus.ToString();
            //���ν���
            this.label_zerenjt.Text = _ms.mDutyPay.ToString();
            //��Чϵ��
            this.txtEffectFactor.EditValue = _ms.mEffectFactor;
            //�ٵ��ۿ�
            this.label_cdkk.Text = _ms.mLatePunish.ToString("N0");
            //�������� 
            this.txtOtherPay.EditValue = _ms.mOtherPay;
            //�����ۿ�
            this.txtOtherPunish.EditValue = _ms.mOtherPunish;
            //ʵ�콱��
            this.label_BonusTotal.Text = _ms.BonusTotal.ToString();
            //С��
            this.label_xiaoji.Text = _ms.XiaoJI.ToString();
            //�Ӱ�
            this.label_jiaban.Text = this.GetSiSheWuRu(_ms.JiaBan, 0).ToString();
            //�ܼ�
            this.label_SubTotal.Text = this.GetSiSheWuRu(_ms.SubTotal, 0).ToString();
            //ʵ����
            this.label_SalaryTotal.Text = this.GetSiSheWuRu(_ms.mSalaryTotal, 0).ToString();

            flag = 0;
        }

        //������ҵ��������
        private void label_gj_Click(object sender, EventArgs e)
        {
            if (this._ms == null) return;
            Form f = new WordBonusList();
            if (IsShowForm(f.GetType().FullName))
            {
                if (this._ms == null) return;
                f = new WordBonusList(this._ms);
                f.MdiParent = this.MdiParent;
                f.Show();
            }
        }

        //˫���鿴�Ӱ�
        private void label_jrjb_Click(object sender, EventArgs e)
        {
            if (this._ms == null) return;
            Form f = new OverTimeForm();
            if (IsShowForm(f.GetType().FullName))
            {
                if (this._ms == null) return;
                f = new OverTimeForm(this._ms, this.employee);
                f.MdiParent = this.MdiParent;
                f.Show();
            }
        }

        //������н
        private void label_dx_Click(object sender, EventArgs e)
        {
            if (this._ms == null) return;
            Form f = new CalculationListForm();
            if (IsShowForm(f.GetType().FullName))
            {
                if (this.employee == null) return;
                f = new CalculationListForm(this._ms);
                f.MdiParent = this.MdiParent;
                f.WindowState = FormWindowState.Maximized;
                f.Show();
            }
        }

        private bool IsShowForm(string frName)
        {
            foreach (Form frm in this.ParentForm.MdiChildren)
            {
                if (frm.GetType().FullName.EndsWith(frName))
                {
                    frm.BringToFront();
                    return false;
                }
            }
            return true;
        }

        private void gridView2_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.ListSourceRowIndex < 0) return;
            Model.Employee Employee = _emplist[e.ListSourceRowIndex];
            if (e.Column.Name == this.gridColumnBusinessTime.Name && Employee.BusinessHours != null)
                e.DisplayText = Convert.ToDateTime(Employee.BusinessHours.Fromtime).ToString("HH:mm") + "~" + Convert.ToDateTime(Employee.BusinessHours.ToTime).ToString("HH:mm");
        }

        private void repositoryItemHyperLinkEdit1_Click(object sender, EventArgs e)
        {
            //this._IsKeyIn = false;
            labelControl14.Text = comboBoxEdit1.Text + Properties.Resources.JJInformation;
            labelControl15.Text = comboBoxEdit1.Text + Properties.Resources.MoneyDetail;
            this.employee = this.bindingSourceEmployee.Current as Model.Employee;
            if (this.employee == null) return;
            this.Calulation(this.employee);
        }

        //�����鿴�ͷ�
        private void label_cf_Click(object sender, EventArgs e)
        {
            if (this._ms == null) return;
            Form f = new LunchDetailForm();
            if (IsShowForm(f.GetType().FullName))
            {
                if (this.employee == null) return;
                f = new LunchDetailForm(this._ms);
                f.MdiParent = this.MdiParent;
                f.BringToFront();
                f.Show();
            }
        }

        #region //private void spinEditEffectFactor_EditValueChanged(object sender, EventArgs e)
        //{
        //    if (this.flag != 1)
        //    {
        //        double a = 0;
        //        double.TryParse(this.txtEffectFactor.Text, out a);
        //        _ms.mEffectFactor = a;
        //        this.label_BonusTotal.Text = _ms.BonusTotal.ToString();
        //    }
        //}
        #endregion

        //����
        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (this.employee == null) return;
                Model.MonthlySalaryStruct monthlySalaryStruct = new Book.Model.MonthlySalaryStruct();
                monthlySalaryStruct.MonthlySalaryId = Guid.NewGuid().ToString();
                monthlySalaryStruct.EmployeeId = this.employee.EmployeeId;

                decimal.TryParse(this.txtEffectFactor.Text, out monthlySalaryStruct.EffectFactor);
                decimal.TryParse(this.txtOtherPay.Text, out monthlySalaryStruct.OtherPay);
                decimal.TryParse(this.txtOtherPunish.Text, out monthlySalaryStruct.OtherPunish);
                float.TryParse(this.txt_givenDays.Text, out monthlySalaryStruct.HolidayBonusGivenDays);

                monthlySalaryStruct.IdentifyDate = this._ms.mIdentifyDate;//new DateTime(Int32.Parse(this.comboBoxEdit1.Text.Substring(0, 4)), Int32.Parse(this.comboBoxEdit1.Text.Substring(5, 2)), 1);
                this.monthlySalaryManager.UpdateDataSet(monthlySalaryStruct);
                MessageBox.Show(Properties.Resources.SuccessfullySaved, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //��ӡ
        private MonthSalaryClass CalulationNoText(Model.Employee emp)
        {
            int mTemp = 0;
            int mHicount = 0;
            double mLateHalfCount = 0;
            StringBuilder strBU = new StringBuilder();
            int totalDay = DateTime.DaysInMonth(hryear, hrmonth);
            //////////////////////////////////////////////////////////////////
            MonthSalaryClass _ms = new MonthSalaryClass();
            _ms.mIdentifyDate = new DateTime(hryear, hrmonth, 1);
            _ms.mEmployeeId = emp.EmployeeId;
            _ms.mEmployeeName = emp.EmployeeName;
            _ms.mIDNo = emp.IDNo;
            _ms.mDepartmetName = emp.Department.DepartmentName;

            //------------------------------ From���㷽�� ----Start----------------------------------------------------//
            #region  ȡ���ڼ�¼

            //  DataSet ds = this.monthlySalaryManager.getAttendInfoList(emp.EmployeeId, hryear, hrmonth);                   

            foreach (Model.HrDailyEmployeeAttendInfo attend in this._hrManager.SelectByEmpMonth(emp, hryear, hrmonth))
            {
                if (attend.LateInMinute.HasValue && attend.LateInMinute.Value != 0)
                {
                    strBU.Append(attend.LateInMinute.ToString() + "|");
                    mTemp = mTemp + attend.LateInMinute.Value;
                    //if (mTemp <= 10)
                    //{
                    //    mCount = mCount + 1;
                    //}
                    if (attend.LateInMinute.Value > 30)
                    {
                        mHicount = mHicount + 1;
                        if ((attend.LateInMinute.Value + 30) % 60 > 30)
                        {
                            mLateHalfCount = mLateHalfCount + (attend.LateInMinute.Value + 30) / 60 + 0.5;
                        }
                        else
                        {
                            mLateHalfCount = mLateHalfCount + (attend.LateInMinute.Value + 30) / 60;
                        }
                    }
                    //��ְ������۳�������������
                }
                _ms.mNote = attend.Note;
                if (!string.IsNullOrEmpty(_ms.mNote))
                {
                    if (_ms.mNote != "�L���ݼ�" && _ms.mNote.IndexOf("����") < 0 && _ms.mNote.IndexOf("���") < 0 && _ms.mNote.IndexOf("�ʼ�") < 0 && _ms.mNote.IndexOf("���") < 0 && _ms.mNote.IndexOf("����") < 0 && _ms.mNote.IndexOf("�t��") < 0)
                        _ms.mCount = _ms.mCount + 1;
                    else if (_ms.mNote.Contains("�¼�") || _ms.mNote.Contains("����"))
                        _ms.mCount = _ms.mCount + 1;
                }
            }
            #endregion
            #region ȡн�ʼ���
            DataSet dsms = this.monthlySalaryManager.getMonthlySummaryFee(emp.EmployeeId, _ms.mIdentifyDate, hryear, hrmonth);
            if (dsms.Tables[0].Rows.Count > 0)
            {
                DataRow dr = dsms.Tables[0].Rows[0];
                _ms.mLunchFee = mStrToDouble(dr["LunchFee"]);                                 //����M
                _ms.mOverTimeFee = mStrToDouble(dr["OverTimeFee"]);                           //�Ӱ��M
                _ms.mGeneralOverTime = mStrToDouble(dr["GeneralOverTime"]);                   //ƽ�ռӰ�(�r��)
                _ms.mHolidayOverTime = mStrToDouble(dr["HolidayOverTime"]);                   //���ռӰ�(�r��)
                _ms.GeneralOverTimeCountBig = mStrToDouble(dr["GeneralOverTimeCountBig"]);    //ƽ�ռӰ�2Сʱ֮��(�r��)
                _ms.GeneralOverTimeCountSmall = mStrToDouble(dr["GeneralOverTimeCountSmall"]);//ƽ�ռӰ�2Сʱ����(�r��)
                _ms.HolidayOverTimeCountBig = mStrToDouble(dr["HolidayOverTimeCountBig"]);    //���ռӰ�2Сʱ֮��(�r��)
                _ms.HolidayOverTimeCountSmall = mStrToDouble(dr["HolidayOverTimeCountSmall"]);//���ռӰ�2Сʱ����(�r��)
                _ms.mLateCount = mStrToDouble(dr["LateCount"]);                               //�t���Δ�
                _ms.mTotalLateInMinute = mStrToDouble(dr["TotalLateInMinute"]);               //���t��(��)
                _ms.mOverTimeBonus = mStrToDouble(dr["OverTimeBonus"]);                       //�Ӱ���N
                _ms.mSpecialBonus = mStrToDouble(dr["SpecialBonus"]);                         //��ҹ����N
                _ms.mDaysFactor = mStrToDouble(dr["DaysFactor"]);                             //���ջ���
                _ms.mMonthFactor = mStrToDouble(dr["MonthFactor"]);                           //���»���
                _ms.mDutyDateCount = mStrToDouble(dr["DutyDateCount"]);                       //������ӛ䛔�
                _ms.mLeaveDate = (dr["LeaveDate"] == null || dr["LeaveDate"].ToString() == "") ? global::Helper.DateTimeParse.NullDate : Convert.ToDateTime(dr["LeaveDate"].ToString());                                                   //��ְ����
                _ms.mPunishLeaveCount = mStrToDouble(dr["PunishLeaveCount"]);                 //���ۿ�ٿ���
                _ms.mLeaveCount = mStrToDouble(dr["LeaveCount"]);                             //Ո�ٿ���
                _ms.mAbsentCount = mStrToDouble(dr["AbsentCount"]);                           //�繤����
                _ms.mTotalHoliday = mStrToDouble(dr["TotalHoliday"]);                         //ԓ�¿����ٔ�
                _ms.mLoanFee = mStrToDouble(dr["LoanFee"]);
                // int WuXinCount = Int32.Parse(dr["WuXinCount"].ToString());
                //���� ����һ��  �ջ��� =�»���-ʵ�ʼ���-�ۿ��������-��ְ-��н��          //�󹤴�����  
                if (_ms.mDutyDateCount < totalDay)
                    _ms.mDaysFactor = _ms.mDaysFactor - _ms.mTotalHoliday;
                //    if (_ms.mDutyDateCount < totalDay && _ms.mLeaveDate != global::Helper.DateTimeParse.NullDate && _ms.mLeaveDate.ToString() != "")             //������ӛ䛔�
                //        _ms.mDaysFactor = _ms.mMonthFactor - _ms.mTotalHoliday - _ms.mPunishLeaveCount - _ms.mAbsentCount - WuXinCount;
                //    else if ((_ms.mLeaveDate == global::Helper.DateTimeParse.NullDate || _ms.mLeaveDate.ToString() == "") && _ms.mDutyDateCount < totalDay)
                //        _ms.mDaysFactor = _ms.mMonthFactor - _ms.mTotalHoliday - _ms.mPunishLeaveCount - _ms.mAbsentCount - WuXinCount;
            }
            else
            {
                _ms.mLoanFee = 0;
                _ms.mLunchFee = 0;
                _ms.mOverTimeFee = 0;
                _ms.mGeneralOverTime = 0;
                _ms.mHolidayOverTime = 0;
                _ms.mLateCount = 0;
                _ms.mTotalLateInMinute = 0;
                _ms.mOverTimeBonus = 0;
                _ms.mSpecialBonus = 0;
                _ms.mDaysFactor = 0;
                _ms.mMonthFactor = 0;
                _ms.mDutyDateCount = 0;
                _ms.mPunishLeaveCount = 0;
                _ms.mLeaveCount = 0;
                _ms.mTotalHoliday = 0;
            }
            dsms.Clear();
            #endregion
            #region ��н
            DataSet dx_ds = this.monthlySalaryManager.getMonthlySalary(emp.EmployeeId, _ms.mIdentifyDate);
            if (dx_ds.Tables[0].Rows.Count > 0)
            {
                _ms.mMonthFactor = _ms.mMonthFactor;
                DataRow dx_dr = dx_ds.Tables[0].Rows[0];
                _ms.mDailyPay = mStrToDouble(dx_dr["DailyPay"]); //�չ���
                _ms.mMonthlyPay = mStrToDouble(dx_dr["MonthlyPay"]); //�¹���
                if (VPerson.vipPerson.Contains(emp.EmployeeId))
                {
                    _ms.mDutyPay = this.GetSiSheWuRu(mStrToDouble(dx_dr["DutyPay"]), 0);  //���ν���
                    _ms.mPostPay = this.GetSiSheWuRu(mStrToDouble(dx_dr["PostPay"]), 0);  //�ս��N
                }
                else
                {
                    _ms.mDutyPay = this.GetSiSheWuRu(mStrToDouble(dx_dr["DutyPay"]) * _ms.mMonthFactor / totalDay, 0);  //���ν���
                    _ms.mPostPay = this.GetSiSheWuRu(mStrToDouble(dx_dr["PostPay"]) * _ms.mMonthFactor / totalDay, 0);  //�ս��N
                }
                _ms.mGivenDays = mStrToDouble(dx_dr["HolidayBonusGivenDays"]);  //���(����)����
                _ms.mAnnualHolidayFee = _ms.mDailyPay * _ms.mGivenDays;         //���(����)���
                _ms.mInsurance = mStrToDouble(dx_dr["insurance"]); //���շ�
                _ms.mTax = mStrToDouble(dx_dr["Tax"]);   //����˰
                _ms.mEffectFactor = mStrToDouble(dx_dr["EffectFactor"]); //��Ч�S��
                _ms.mOtherPay = mStrToDouble(dx_dr["OtherPay"]);  //�����a��
                _ms.mOtherPunish = mStrToDouble(dx_dr["OtherPunish"]); //�����ۿ�
                _ms.mFieldPay = this.GetSiSheWuRu(mStrToDouble(dx_dr["FieldPay"]) * (totalDay - _ms.mCount) / totalDay, 0);  //ְ������
                _ms.mBasePay = _ms.mMonthlyPay * _ms.mMonthFactor / totalDay + _ms.mDailyPay * _ms.mDaysFactor;                //��н
            }
            else
            {
                _ms.mDailyPay = 0;
                _ms.mMonthlyPay = 0;
                _ms.mDutyPay = 0;
                _ms.mPostPay = 0;
                _ms.mGivenDays = 0;
                _ms.mAnnualHolidayFee = 0;
                _ms.mInsurance = 0;
                _ms.mTax = 0;
                _ms.mEffectFactor = 0;
                _ms.mOtherPay = 0;
                _ms.mOtherPunish = 0;
                _ms.mFieldPay = 0;
                _ms.mBasePay = 0;
            }
            dx_ds.Clear();
            #endregion
            #region �ٵ��ۿ�
            int mIcount = 0;  //��С��10�� ����
            if ((_ms.mTotalLateInMinute > 10 || _ms.mLateCount > 2) && !VPerson.vipPerson.Contains(emp.EmployeeId))
            {
                //��ʱ��¼�ٵ�

                //StringBuilder strBU = new StringBuilder();
                //foreach (Model.HrDailyEmployeeAttendInfo attend in this._hrManager.SelectByEmpMonth(this.employee, hryear, hrmonth))
                //{
                //    if (attend.LateInMinute.Value != 0)
                //    {
                //        strBU.Append(attend.LateInMinute.ToString() + "|");
                //        mTemp = mTemp + attend.LateInMinute.Value;
                //        if (mTemp <= 10)
                //        {
                //            mCount = mCount + 1;
                //        }
                //        if (attend.LateInMinute.Value > 30)
                //        {
                //            mHicount = mHicount + 1;
                //        }
                //    }
                //}
                // '�t�����^���Σ����t�����^10���
                //'����С�r����0.5С�r��̶�
                string[] strs = new string[31];
                IList<int> a = new List<int>();

                if (strBU.Length > 0)
                    strs = strBU.ToString(0, strBU.Length - 1).Split('|');
                for (int i = 0; i < strs.Length; i++)
                {
                    if (strs[i] != null)
                    {
                        if (strs[i] == "0")
                            continue;
                        else
                            a.Add(Int32.Parse(strs[i]));
                    }
                }

                int temp = 0;
                for (int i = 0; i < a.Count; i++)
                {
                    for (int j = i + 1; j < a.Count; j++)
                    {
                        if (a[i] > a[j])
                        {
                            temp = a[i];
                            a[i] = a[j];
                            a[j] = temp;
                        }

                    }
                }
                int sum = 0;
                int m;
                for (m = 0; m < a.Count; m++)
                {
                    sum = sum + a[m];
                    if (sum > 10)
                        break;
                }
                if (m > 2)
                {
                    m = 2;
                }
                mIcount = m;
                _ms.mTotalLateInHour = (_ms.mLateCount - mIcount - mHicount) * 0.5 + mLateHalfCount;
                _ms.mLatePunish = _ms.mTotalLateInHour * double.Parse(_ms.mDailyPay.ToString()) / 8;
            }
            else
            {
                _ms.mTotalLateInHour = 0;
                _ms.mLatePunish = 0;
                mIcount = (int)_ms.mLateCount;// 10������ ���� ����ȫ�ڽ�
            }
            #endregion
            #region ȫ�ڽ�
            if (VPerson.vipPerson.Contains(emp.EmployeeId))
            {
                _ms.mAllAttendBonus = _ms.mDailyPay * 3;
            }
            else
            {
                if (_ms.mDutyDateCount == totalDay)
                {
                    //�����Mһ���£���н����
                    if (_ms.mMonthFactor == totalDay)
                    {
                        if (_ms.mPunishLeaveCount == 0)
                        {
                            _ms.mAllAttendBonus = _ms.mDailyPay * 3;
                        }
                        //Ո���ۿ��С춵��һ�죺��н����   
                        else if (_ms.mPunishLeaveCount <= 1)
                        {
                            _ms.mAllAttendBonus = _ms.mDailyPay * 2;
                        }
                        //�Д��t��,�۳�ȫ�ڪ���
                        if (_ms.mLateCount - mIcount > 0)
                        {
                            _ms.mAllAttendBonus = _ms.mAllAttendBonus - _ms.mDailyPay * (_ms.mLateCount - mIcount) >= 0 ? _ms.mAllAttendBonus - _ms.mDailyPay * (_ms.mLateCount - mIcount) : 0;
                        }
                    }
                    //��ȱˢ��ӛ�                 
                    else
                    {
                        _ms.mAllAttendBonus = 0;
                    }
                }
                //δ�Mһ���£������x��
                else
                {
                    _ms.mAllAttendBonus = 0;
                }
            }
            #endregion
            #region �xδ���Mһ�������ߣ�ȡ������؟�ν��N�������ս��N�����������N�������L���ݼ١�
            if (_ms.mDutyDateCount < totalDay && _ms.mLeaveDate != global::Helper.DateTimeParse.NullDate && !VPerson.vipPerson.Contains(emp.EmployeeId))
            {
                _ms.mDutyPay = 0;
                _ms.mPostPay = 0;
                _ms.mFieldPay = 0;
            }
            #endregion
            #region ƽ�ռӰ� ���ռӰ� ��н�Ӱ���� ��˰��
            #region //����
            //_ms.mGeneralOverTimeFee = GetSiSheWuRu((_ms.mDailyPay / 6) * _ms.mGeneralOverTime, 0);                   //ƽ�ռӰ��
            //_ms.mHolidayOverTimeFee = GetSiSheWuRu((((_ms.mDailyPay / 2) * 3) / 8) * _ms.mHolidayOverTime, 0);       //���ռӰ��
            #endregion
            #region //���� ����&ƽ�ռӰ��㷨�޸� 2013��4��26��15:09:41 ����
            //ƽ�ռӰ� 0~2Сʱ֮�� ʱн*hour*1.33  2>  ʱн*hour*1.66
            //if (_ms.mGeneralOverTime < 2)
            //{
            //    _ms.mGeneralOverTimeFee = GetSiSheWuRu((_ms.mDailyPay / 8) * _ms.mGeneralOverTime * 1.33, 0);
            //}
            //else
            //{
            //    _ms.mGeneralOverTimeFee = GetSiSheWuRu((_ms.mDailyPay / 8) * 2 * 1.33, 0) + GetSiSheWuRu((_ms.mDailyPay / 8) * (_ms.mGeneralOverTime - 2) * 1.66, 0);
            //}
            _ms.mGeneralOverTimeFee = GetSiSheWuRu((_ms.mDailyPay / 8) * _ms.GeneralOverTimeCountSmall * 1.33, 0) + GetSiSheWuRu((_ms.mDailyPay / 8) * _ms.GeneralOverTimeCountBig * 1.66, 0);
            //���ռӰ� һ�� Ϊʱн ����.
            _ms.mHolidayOverTimeFee = GetSiSheWuRu(((_ms.mDailyPay / 8) * 2) * _ms.mHolidayOverTime, 0);
            #endregion
            #endregion
            #region ��������,��Ч����, ��ҵ�������� => ��������=ְ����������ν�����������+ȫ�ڽ�
            int[] months = { 1, 2, 3, 1, 2, 3, 1, 2, 3, 1, 2, 3 };
            switch (months[hrmonth - 1])
            {
                case 1:
                    _ms.mWorkBonus = _ms.mPostPay + _ms.mDutyPay + _ms.mSpecialBonus + _ms.mAllAttendBonus;
                    break;
                case 2:
                    _ms.mEffectBonus = _ms.mPostPay + _ms.mDutyPay + _ms.mSpecialBonus + _ms.mAllAttendBonus;
                    break;
                case 3:
                    _ms.mTechBonus = _ms.mPostPay + _ms.mDutyPay + _ms.mSpecialBonus + _ms.mAllAttendBonus;
                    break;
            }
            #endregion

            return _ms;
            //------------------------------ From���㷽�� ----End----------------------------------------------------//

            #region //------------------------------ ԭ�з���---Start-----------------------------------------------------//
            //#region  ȡ���ڼ�¼

            //DataRow drms;
            //int lateInMinute = 0;
            //DataSet ds = this.monthlySalaryManager.getAttendInfoList(emp.EmployeeId, hryear, hrmonth);
            //{
            //    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            //    {
            //        drms = ds.Tables[0].Rows[i];


            //        if (drms["LateInMinute"] != null && int.Parse(drms["LateInMinute"].ToString()) != 0)
            //        {
            //            lateInMinute = int.Parse(drms["LateInMinute"].ToString());
            //            strBU.Append(drms["LateInMinute"].ToString() + "|");
            //            mTemp = mTemp + lateInMinute;
            //            //if (mTemp <= 10)
            //            //{
            //            //    mCount = mCount + 1;
            //            //}
            //            if (lateInMinute > 30)
            //            {
            //                mHicount = mHicount + 1;
            //                if ((lateInMinute + 30) % 60 > 30)
            //                {
            //                    mLateHalfCount = mLateHalfCount + (lateInMinute + 30) / 60 + 0.5;
            //                }
            //                else
            //                {
            //                    mLateHalfCount = mLateHalfCount + (lateInMinute + 30) / 60;
            //                }
            //            }
            //            //��ְ������۳�������������
            //        }
            //        _ms.mNote = drms["Note"].ToString();
            //        if (!string.IsNullOrEmpty(_ms.mNote))
            //        {
            //            if (_ms.mNote != "�����ݼ�" && _ms.mNote.IndexOf("����") < 0 && _ms.mNote.IndexOf("���") < 0 && _ms.mNote.IndexOf("�ʼ�") < 0 && _ms.mNote.IndexOf("���") < 0 && _ms.mNote.IndexOf("����") < 0 && _ms.mNote.IndexOf("�t��") < 0)
            //                _ms.mCount = _ms.mCount + 1;

            //        }

            //    }
            //}
            //ds.Clear();
            //ds.Dispose();
            //#endregion
            //#region ȡн�ʼ���
            //DataSet dsms = this.monthlySalaryManager.getMonthlySummaryFee(emp.EmployeeId, _ms.mIdentifyDate, hryear, hrmonth);
            //if (dsms.Tables[0].Rows.Count > 0)
            //{
            //    DataRow dr = dsms.Tables[0].Rows[0];
            //    _ms.mLunchFee = mStrToDouble(dr["LunchFee"]);  // ����M
            //    _ms.mOverTimeFee = mStrToDouble(dr["OverTimeFee"]);// �Ӱ��M
            //    _ms.mGeneralOverTime = mStrToDouble(dr["GeneralOverTime"]); // ƽ�ռӰࣨ�r����
            //    _ms.mHolidayOverTime = mStrToDouble(dr["HolidayOverTime"]); // ���ռӰࣨ�r����
            //    _ms.mLateCount = mStrToDouble(dr["LateCount"]); // �t���Δ�
            //    _ms.mTotalLateInMinute = mStrToDouble(dr["TotalLateInMinute"]);            // ���t�����֣�
            //    _ms.mOverTimeBonus = mStrToDouble(dr["OverTimeBonus"]);                     // �Ӱ���N
            //    _ms.mSpecialBonus = mStrToDouble(dr["SpecialBonus"]);                       // ��ҹ����N
            //    _ms.mDaysFactor = mStrToDouble(dr["DaysFactor"]);                           // ���ջ���
            //    _ms.mMonthFactor = mStrToDouble(dr["MonthFactor"]);                         // ���»���
            //    _ms.mDutyDateCount = mStrToDouble(dr["DutyDateCount"]);                     // ������ӛ䛔�
            //    _ms.mLeaveDate = (dr["LeaveDate"] == null || dr["LeaveDate"].ToString() == "") ? global::Helper.DateTimeParse.NullDate : Convert.ToDateTime(dr["LeaveDate"].ToString()); // ��ְ����
            //    _ms.mPunishLeaveCount = mStrToDouble(dr["PunishLeaveCount"]);               // ���ۿ�ٿ���
            //    _ms.mLeaveCount = mStrToDouble(dr["LeaveCount"]);                           // Ո�ٿ���
            //    _ms.mAbsentCount = mStrToDouble(dr["AbsentCount"]);                         // �繤����
            //    _ms.mTotalHoliday = mStrToDouble(dr["TotalHoliday"]);                       // ԓ�¿����ٔ�
            //    _ms.mLoanFee = mStrToDouble(dr["LoanFee"]);
            //    //  int WuXinCount = Int32.Parse(dr["WuXinCount"].ToString());
            //    //���� ����һ��  �ջ��� =�»���-ʵ�ʼ���-�ۿ��������-��ְ-��н��     //�󹤴�����  
            //    if (_ms.mDutyDateCount < totalDay)
            //        _ms.mDaysFactor = _ms.mDaysFactor - _ms.mTotalHoliday;
            //    //    if (_ms.mDutyDateCount < totalDay && _ms.mLeaveDate != global::Helper.DateTimeParse.NullDate && _ms.mLeaveDate.ToString() != "")             //������ӛ䛔�
            //    //        _ms.mDaysFactor = _ms.mMonthFactor - _ms.mTotalHoliday - _ms.mPunishLeaveCount - _ms.mAbsentCount - WuXinCount;
            //    //    else if ((_ms.mLeaveDate == global::Helper.DateTimeParse.NullDate || _ms.mLeaveDate.ToString() == "") && _ms.mDutyDateCount < totalDay)
            //    //        _ms.mDaysFactor = _ms.mMonthFactor - _ms.mTotalHoliday - _ms.mPunishLeaveCount - _ms.mAbsentCount - WuXinCount;
            //}
            //else
            //{
            //    _ms.mLoanFee = 0;
            //    _ms.mLunchFee = 0;
            //    _ms.mOverTimeFee = 0;
            //    _ms.mGeneralOverTime = 0;
            //    _ms.mHolidayOverTime = 0;
            //    _ms.mLateCount = 0;
            //    _ms.mTotalLateInMinute = 0;
            //    _ms.mOverTimeBonus = 0;
            //    _ms.mSpecialBonus = 0;
            //    _ms.mDaysFactor = 0;
            //    _ms.mMonthFactor = 0;
            //    _ms.mDutyDateCount = 0;
            //    _ms.mPunishLeaveCount = 0;
            //    _ms.mLeaveCount = 0;
            //    _ms.mTotalHoliday = 0;
            //}
            //dsms.Clear();
            //dsms.Dispose();
            //#endregion
            //#region ��н
            //DataSet dx_ds = this.monthlySalaryManager.getMonthlySalary(emp.EmployeeId, _ms.mIdentifyDate);
            //if (dx_ds.Tables[0].Rows.Count > 0)
            //{
            //    _ms.mMonthFactor = _ms.mMonthFactor;
            //    DataRow dx_dr = dx_ds.Tables[0].Rows[0];
            //    _ms.mDailyPay = mStrToDouble(dx_dr["DailyPay"]);                          //�չ���
            //    _ms.mMonthlyPay = mStrToDouble(dx_dr["MonthlyPay"]);                     //�¹���
            //    _ms.mDutyPay = this.GetSiSheWuRu(mStrToDouble(dx_dr["DutyPay"]) * _ms.mMonthFactor / totalDay, 0); //���ν���
            //    _ms.mPostPay = this.GetSiSheWuRu(mStrToDouble(dx_dr["PostPay"]) * _ms.mMonthFactor / totalDay, 0); //�ս��N
            //    _ms.mInsurance = mStrToDouble(dx_dr["insurance"]);                                      //���շ�
            //    _ms.mTax = mStrToDouble(dx_dr["Tax"]);                                                  //����˰
            //    _ms.mEffectFactor = mStrToDouble(dx_dr["EffectFactor"]);   //��Ч�S��
            //    _ms.mOtherPay = mStrToDouble(dx_dr["OtherPay"]);           //�����a��
            //    _ms.mOtherPunish = mStrToDouble(dx_dr["OtherPunish"]);     //�����ۿ�
            //    _ms.mFieldPay = this.GetSiSheWuRu(mStrToDouble(dx_dr["FieldPay"]) * (totalDay - _ms.mCount) / totalDay, 0); //ְ������
            //    _ms.mBasePay = _ms.mMonthlyPay * _ms.mMonthFactor / totalDay + _ms.mDailyPay * _ms.mDaysFactor;                //��н
            //}
            //else
            //{
            //    _ms.mDailyPay = 0;
            //    _ms.mMonthlyPay = 0;
            //    _ms.mDutyPay = 0;
            //    _ms.mPostPay = 0;
            //    _ms.mInsurance = 0;
            //    _ms.mTax = 0;
            //    _ms.mEffectFactor = 0;
            //    _ms.mOtherPay = 0;
            //    _ms.mOtherPunish = 0;
            //    _ms.mFieldPay = 0;
            //    _ms.mBasePay = 0;
            //}
            //dx_ds.Clear();
            //dx_ds.Dispose();
            //#endregion
            //#region �ٵ��ۿ�
            //int mIcount = 0;  //��С��10�� ����
            //if (_ms.mTotalLateInMinute > 10 || _ms.mLateCount > 2)
            //{
            //    //��ʱ��¼�ٵ�

            //    //StringBuilder strBU = new StringBuilder();
            //    //foreach (Model.HrDailyEmployeeAttendInfo attend in this._hrManager.SelectByEmpMonth(this.employee, hryear, hrmonth))
            //    //{
            //    //    if (attend.LateInMinute.Value != 0)
            //    //    {
            //    //        strBU.Append(attend.LateInMinute.ToString() + "|");
            //    //        mTemp = mTemp + attend.LateInMinute.Value;
            //    //        if (mTemp <= 10)
            //    //        {
            //    //            mCount = mCount + 1;
            //    //        }
            //    //        if (attend.LateInMinute.Value > 30)
            //    //        {
            //    //            mHicount = mHicount + 1;
            //    //        }
            //    //    }
            //    //}
            //    // '�t�����^���Σ����t�����^10���
            //    //'����С�r����0.5С�r��̶�
            //    string[] strs = new string[31];
            //    IList<int> a = new List<int>();

            //    if (strBU.Length > 0)
            //        strs = strBU.ToString(0, strBU.Length - 1).Split('|');
            //    for (int i = 0; i < strs.Length; i++)
            //    {
            //        if (strs[i] != null)
            //        {
            //            if (strs[i] == "0")
            //                continue;
            //            else
            //                a.Add(Int32.Parse(strs[i]));
            //        }
            //    }

            //    int temp = 0;
            //    for (int i = 0; i < a.Count; i++)
            //    {
            //        for (int j = i + 1; j < a.Count; j++)
            //        {
            //            if (a[i] > a[j])
            //            {
            //                temp = a[i];
            //                a[i] = a[j];
            //                a[j] = temp;
            //            }

            //        }
            //    }
            //    int sum = 0;
            //    int m;
            //    for (m = 0; m < a.Count; m++)
            //    {
            //        sum = sum + a[m];
            //        if (sum > 10)
            //            break;
            //    }
            //    if (m > 2)
            //    {
            //        m = 2;
            //    }
            //    mIcount = m;
            //    _ms.mTotalLateInHour = (_ms.mLateCount - mIcount - mHicount) * 0.5 + mLateHalfCount;
            //    _ms.mLatePunish = _ms.mTotalLateInHour * double.Parse(_ms.mDailyPay.ToString()) / 8;
            //}
            //else
            //{
            //    _ms.mTotalLateInHour = 0;
            //    _ms.mLatePunish = 0;
            //    mIcount = (int)_ms.mLateCount;// 10������ ���� ����ȫ�ڽ�
            //}
            //#endregion
            //#region ȫ�ڽ�
            //if (_ms.mDutyDateCount == totalDay)
            //{
            //    //�����Mһ���£���н����
            //    if (_ms.mMonthFactor == totalDay)
            //    {
            //        if (_ms.mPunishLeaveCount == 0)
            //        {
            //            _ms.mAllAttendBonus = _ms.mDailyPay * 3;
            //        }
            //        //Ո���ۿ��С춵��һ�죺��н����   
            //        else if (_ms.mPunishLeaveCount <= 1)
            //        {
            //            _ms.mAllAttendBonus = _ms.mDailyPay * 2;
            //        }
            //        //�Д��t��,�۳�ȫ�ڪ���
            //        if (_ms.mLateCount - mIcount > 0)
            //        {
            //            _ms.mAllAttendBonus = _ms.mAllAttendBonus - _ms.mDailyPay * (_ms.mLateCount - mIcount) >= 0 ? _ms.mAllAttendBonus - _ms.mDailyPay * (_ms.mLateCount - mIcount) : 0;
            //        }
            //    }
            //    //��ȱˢ��ӛ�                 
            //    else
            //    {
            //        _ms.mAllAttendBonus = 0;
            //    }
            //}
            ////δ�Mһ���£������x��
            //else
            //{
            //    _ms.mAllAttendBonus = 0;
            //}
            //#endregion
            //#region �xδ���Mһ�������ߣ�ȡ������؟�ν��N�������ս��N�����������N�������L���ݼ١�
            //if (_ms.mDutyDateCount < totalDay && _ms.mLeaveDate != global::Helper.DateTimeParse.NullDate)
            //{
            //    _ms.mDutyPay = 0;
            //    _ms.mPostPay = 0;
            //    _ms.mFieldPay = 0;
            //}
            //#endregion
            //#region ƽ�ռӰ� ���ռӰ� ��н�Ӱ���� ��˰��
            //_ms.mGeneralOverTimeFee = GetSiSheWuRu((_ms.mDailyPay / 6) * _ms.mGeneralOverTime, 0);                   //ƽ�ռӰ��
            //_ms.mHolidayOverTimeFee = GetSiSheWuRu((((_ms.mDailyPay / 2) * 3) / 8) * _ms.mHolidayOverTime, 0);       //���ռӰ��


            //#endregion
            //#region ��������,��Ч����, ��ҵ�������� => ��������=ְ����������ν�����������+ȫ�ڽ�
            //int[] months = { 1, 2, 3, 1, 2, 3, 1, 2, 3, 1, 2, 3 };
            //switch (months[hrmonth - 1])
            //{
            //    case 1:
            //        _ms.mWorkBonus = _ms.mPostPay + _ms.mDutyPay + _ms.mSpecialBonus + _ms.mAllAttendBonus;
            //        break;
            //    case 2:
            //        _ms.mEffectBonus = _ms.mPostPay + _ms.mDutyPay + _ms.mSpecialBonus + _ms.mAllAttendBonus;
            //        break;
            //    case 3:
            //        _ms.mTechBonus = _ms.mPostPay + _ms.mDutyPay + _ms.mSpecialBonus + _ms.mAllAttendBonus;
            //        break;
            //}

            //return _ms;
            //#endregion
            #endregion------------------------------ ԭ�з���- End-------------------------------------------------------//

            #region ȡ����note (ȡ������)
            //foreach (Model.HrDailyEmployeeAttendInfo attend in this._hrManager.SelectByEmpMonth(emp, hryear, hrmonth))
            //{
            //    if (attend.LateInMinute.HasValue && attend.LateInMinute.Value != 0)
            //    {
            //        strBU.Append(attend.LateInMinute.ToString() + "|");
            //        mTemp = mTemp + attend.LateInMinute.Value;
            //        //if (mTemp <= 10)
            //        //{
            //        //    mCount = mCount + 1;
            //        //}
            //        if (attend.LateInMinute.Value > 30)
            //        {
            //            mHicount = mHicount + 1;
            //            if ((attend.LateInMinute.Value + 30) % 60 > 30)
            //            {
            //                mLateHalfCount = mLateHalfCount + (attend.LateInMinute.Value + 30) / 60 + 0.5;
            //            }
            //            else
            //            {
            //                mLateHalfCount = mLateHalfCount + (attend.LateInMinute.Value + 30) / 60;
            //            }
            //        }
            //        //��ְ������۳�������������
            //    }
            //    _ms.mNote = attend.Note;
            //    if (!string.IsNullOrEmpty(_ms.mNote))
            //    {
            //        if (_ms.mNote != "�����ݼ�" && _ms.mNote.IndexOf("����") < 0 && _ms.mNote.IndexOf("���") < 0 && _ms.mNote.IndexOf("�ʼ�") < 0 && _ms.mNote.IndexOf("���") < 0 && _ms.mNote.IndexOf("����") < 0 && _ms.mNote.IndexOf("�t��") < 0)
            //            _ms.mCount = _ms.mCount + 1;

            //    }
            //}
            #endregion

        }

        //��ӡ���±���
        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Model.MonthlySalary monthlySalarys = new Book.Model.MonthlySalary();
            DataSet1.MonthlySalarysDataTable table = new DataSet1.MonthlySalarysDataTable();
            DataRow _drT;
            //  DataSet dx_ds = this.monthlySalaryManager.GetMonthlySummaryByMonth(hryear, hrmonth);
            if (_emplist != null && _emplist.Count > 0)// (dx_ds.Tables[0].Rows.Count > 0)
            {
                foreach (Model.Employee emp in _emplist) // for (int i = 0; i < dx_ds.Tables[0].Rows.Count; i++)
                {
                    MonthSalaryClass _ms = this.CalulationNoText(emp);
                    //��ֵ
                    _drT = table.NewRow();
                    _drT["EmployeeId"] = _ms.mEmployeeId;
                    _drT["DailyPay"] = _ms.mDailyPay;
                    _drT["IDNo"] = _ms.mIDNo;
                    _drT["DepartmentName"] = _ms.mDepartmetName;
                    _drT["CompanyName"] = emp.Company == null ? "" : emp.Company.ToString();
                    _drT["MonthlyPay"] = _ms.mMonthlyPay;
                    _drT["BasePay"] = _ms.mBasePay;
                    _drT["FieldPay"] = _ms.mFieldPay;
                    _drT["SubTotal"] = GetSiSheWuRu(_ms.SubTotal, 0);
                    _drT["LunchFee"] = _ms.mLunchFee;
                    _drT["Insurance"] = _ms.mInsurance;
                    _drT["LoanFee"] = _ms.mLoanFee;
                    _drT["Tax"] = _ms.mTax;
                    _drT["SalaryTotal"] = _ms.mSalaryTotal;
                    _drT["DutyPay"] = _ms.mDutyPay;
                    _drT["PostPay"] = _ms.mPostPay;
                    _drT["AllAttendBonus"] = _ms.mAllAttendBonus;
                    _drT["SpecialBonus"] = _ms.mSpecialBonus;
                    _drT["WorkBonus"] = _ms.mWorkBonus;
                    _drT["EffectBonus"] = _ms.mEffectBonus;
                    _drT["TechBonus"] = _ms.mTechBonus;
                    _drT["EffectFactor"] = _ms.mEffectFactor;
                    _drT["GeneralOverTime"] = _ms.mGeneralOverTime;
                    //_drT["GeneralOverTime"] = _ms.OverTimeCountBig + _ms.OverTimeCountSmall;
                    _drT["HolidayOverTime"] = _ms.mHolidayOverTime;
                    _drT["GeneralOverTimeFee"] = _ms.mGeneralOverTimeFee;
                    //_drT["GeneralOverTimeFee"] = GetSiSheWuRu(_ms.JiaBan, 0);
                    _drT["HolidayOverTimeFee"] = _ms.mHolidayOverTimeFee;
                    _drT["OverTimeFee"] = _ms.JiaBan;
                    _drT["OverTimeBonus"] = _ms.mOverTimeBonus;
                    _drT["GivenDays"] = _ms.mGivenDays;
                    _drT["AnnualHolidayFee"] = GetSiSheWuRu(_ms.mAnnualHolidayFee, 0);
                    _drT["OtherPay"] = _ms.mOtherPay;
                    _drT["OtherPunish"] = _ms.mOtherPunish;
                    _drT["BonusTotal"] = _ms.BonusTotal;
                    _drT["ShouldPay"] = _ms.mShouldPay;
                    _drT["LatePunish"] = _ms.mLatePunish;
                    _drT["LateCount"] = _ms.mLateCount;
                    _drT["TotalLateInMinute"] = _ms.mTotalLateInMinute;
                    _drT["TotalLateInHour"] = _ms.mTotalLateInHour;
                    _drT["PunishCount"] = _ms.mPunishCount;
                    _drT["EmployeeName"] = _ms.mEmployeeName;
                    _drT["MonthFactor"] = _ms.mMonthFactor;
                    _drT["DaysFactor"] = _ms.mDaysFactor;
                    //_drT["AnnualPay"] = _ms.mActualPay;
                    _drT["AnnualPay"] = _ms.mSalaryTotal + _ms.BonusTotal;
                    //_drT["XiaoJI"] = GetSiSheWuRu(_ms.XiaoJI, 0);
                    _drT["XiaoJI"] = GetSiSheWuRu(_ms.XiaoJI, 0);
                    _drT["JiaBan"] = GetSiSheWuRu(_ms.JiaBan, 0);
                    if (Convert.ToInt16(_drT["JiaBan"]) == 0)
                        _drT["JiaBanDesc"] = string.Empty;
                    else
                        //_drT["JiaBanDesc"] = _ms.OverTimeCountSmall.ToString() + "H * 1.33 + " + _ms.OverTimeCountBig.ToString() + "H * 1.66";
                        _drT["JiaBanDesc"] = "ƽ=" + _ms.GeneralOverTimeCountSmall.ToString() + "H * 1.33 + " + _ms.GeneralOverTimeCountBig.ToString() + "H * 1.66,��=" + _ms.mHolidayOverTime + "H * 2";
                    table.Rows.Add(_drT);
                }
                CalCrystalReportForm f = new CalCrystalReportForm(table, hryear, hrmonth);
                //table.Clear();
                //table.Dispose();
                f.Show();
            }
        }

        private void comboBoxEdit1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.hryear = Int32.Parse(this.comboBoxEdit1.Text.Substring(0, 4));
            this.hrmonth = Int32.Parse(this.comboBoxEdit1.Text.Substring(5, 2));
            this._emplist = this.employeeManager.SelectHrDailyAttend(new DateTime(hryear, hrmonth, 1));
            this.bindingSourceEmployee.DataSource = _emplist;
        }

        /// <summary>
        /// �������뷽��.
        /// </summary>
        /// <param name="objTarget">Ҫ������double��������</param>
        /// <param name="mIndex">������С��λ��</param>
        /// <returns></returns>
        public double GetSiSheWuRu(double objTarget, int mIndex)
        {
            double a1 = Math.Pow(10, mIndex);
            double a2 = Math.Pow(10, mIndex + 1);
            double b1 = Math.Truncate(objTarget * a1);
            double b2 = Math.Truncate(objTarget * a2);
            if (b2 % 10 >= 5 || b2 % 10 <= -5)
            {
                return objTarget > 0 ? (b1 + 1) / a1 : (b1 - 1) / a1;
            }
            else
            {
                return b1 / a1;
            }
        }

        //��ʶ�Ƿ����ֶ�����,true �ֶ����� ��Ҫ���¼��㱣��,flase �����ֶ����� ����Ҫ����
        bool _IsKeyIn;

        //��������
        private void txtOtherPay_EditValueChanged(object sender, EventArgs e)
        {
            if (this.txtOtherPay.IsEditorActive)
                _IsKeyIn = true;
            else
                _IsKeyIn = false;
            this.mSaveToChange();
        }

        //�����ۿ�
        private void txtOtherPunish_EditValueChanged(object sender, EventArgs e)
        {
            if (this.txtOtherPunish.IsEditorActive)
                _IsKeyIn = true;
            else
                _IsKeyIn = false;
            this.mSaveToChange();
        }

        //��Чϵ������
        private void txtEffectFactor_EditValueChanged(object sender, EventArgs e)
        {
            if (this.txtEffectFactor.IsEditorActive)
                _IsKeyIn = true;
            else
                _IsKeyIn = false;
            this.mSaveToChange();
        }

        //���(����)��������
        private void txt_givenDays_EditValueChanged(object sender, EventArgs e)
        {
            if (this.txt_givenDays.IsEditorActive)
                _IsKeyIn = true;
            else
                _IsKeyIn = false;

            this.mSaveToChange();
        }

        private void mSaveToChange()
        {
            try
            {
                if (_IsKeyIn)
                {
                    if (this.employee == null) return;
                    Model.MonthlySalaryStruct monthlySalaryStruct = new Book.Model.MonthlySalaryStruct();
                    monthlySalaryStruct.MonthlySalaryId = Guid.NewGuid().ToString();
                    monthlySalaryStruct.EmployeeId = this.employee.EmployeeId;

                    decimal.TryParse(this.txtEffectFactor.Text, out monthlySalaryStruct.EffectFactor);
                    decimal.TryParse(this.txtOtherPay.Text, out monthlySalaryStruct.OtherPay);
                    decimal.TryParse(this.txtOtherPunish.Text, out monthlySalaryStruct.OtherPunish);
                    float.TryParse(this.txt_givenDays.Text, out monthlySalaryStruct.HolidayBonusGivenDays);

                    monthlySalaryStruct.IdentifyDate = this._ms.mIdentifyDate;
                    this.monthlySalaryManager.UpdateDataSet(monthlySalaryStruct);
                }
                this.Calulation(this.employee);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// ����תDouble
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        private double mStrToDouble(object o)
        {
            return double.Parse(string.IsNullOrEmpty(o.ToString()) ? "0" : o.ToString());
        }
    }

    public static class VPerson
    {
        public static readonly IList<string> vipPerson = new List<string> { "B212C2AD-E291-41C1-ABBC-79C7496BFC55" };
    }
}


#region ===========ע�ͱ���==========
#region @ old Print
//DataRow dr;//�ڴ��
//DataRow row; //monthlySalaryн�ʱ�
//DataRow row1;// н��ͳ�Ʊ���HrAttendStat���� ֻ��1��
//DataSet dataset = this.monthlySalaryManager.GetMonthlySummaryByMonth(hryear, hrmonth);
//Model.Employee employees;

//decimal PunishLeaveCount = 0;

//if (dataset.Tables[0].Rows.Count > 0)
//{
//    for (int j = 0; j < dataset.Tables[0].Rows.Count; j++)
//    {
//        row = dataset.Tables[0].Rows[j];
//        employees = this.employeeManager.Get(row["EmployeeId"].ToString());

//        _hrAttendStat = this.hrAttendManager.SelectHrAttendStatByEmpidAndYearMonth(employees, hryear, hrmonth);
//        row1 = this.monthlySalaryManager.GetMonthlySummaryFee(employees.EmployeeId, hryear, hrmonth).Tables[0].Rows[0];

//        //monthFactor = this._hrManager.SelectDayMonthSum(hryear, hrmonth, employees);
//        dr = table.NewRow();
//        if (_hrAttendStat != null)   //����ͳ�Ʊ�
//        {
//            dr["LoanFee"] = this._hrAttendStat.LoanFee == null ? 0 : this._hrAttendStat.LoanFee.Value;

//            dr["LunchFee"] = this._hrAttendStat.LunchFee == null ? 0 : this._hrAttendStat.LunchFee.Value;

//            dr["OverTimeFee"] = this._hrAttendStat.OverTimeFee == null ? 0 : this._hrAttendStat.OverTimeFee.Value;
//            //row1["OverTimeFee"] == null || row1["OverTimeFee"].ToString() == "" ? 0 : decimal.Parse(row1["OverTimeFee"].ToString());
//            dr["OverTimeBonus"] = this._hrAttendStat.OverTimeBonus == null ? 0 : this._hrAttendStat.OverTimeBonus.Value;//row1["OverTimeBonus"] == null || row1["OverTimeBonus"].ToString() == "" ? 0 : decimal.Parse(row1["OverTimeBonus"].ToString()); 
//            dr["SpecialBonus"] = this._hrAttendStat.SpecialBonus == null ? 0 : this._hrAttendStat.SpecialBonus.Value; //row1["SpecialBonus"] == null || row1["SpecialBonus"].ToString() == "" ? 0 : decimal.Parse(row1["SpecialBonus"].ToString());
//            row1["DutyDateCount"] = this._hrAttendStat.DutyDateCount == null ? 0 : this._hrAttendStat.DutyDateCount.Value;
//            row1["TotalHoliday"] = this._hrAttendStat.TotalHoliday == null ? 0 : this._hrAttendStat.TotalHoliday.Value;
//            row1["AbsentCount"] = this._hrAttendStat.AbsentCount == null ? 0 : this._hrAttendStat.AbsentCount.Value;
//        }

//        dr["CompanyName"] = BL.Settings.CompanyChineseName;
//        dr["EmployeeName"] = employees.EmployeeName;
//        dr["IDNo"] = employees.IDNo;

//        dr["EffectFactor"] = row["EffectFactor"] == null || row["EffectFactor"].ToString() == "" ? 0 : decimal.Parse(row["EffectFactor"].ToString());

//        //�ջ���
//        dr["DaysFactor"] = row1["DaysFactor"] == null || row1["DaysFactor"].ToString() == "" ? 0 : decimal.Parse(row1["DaysFactor"].ToString());
//        //����ְ���ڲ�������  �ų������������������ְ��

//        if (row1["DutyDateCount"] != null && row1["DutyDateCount"].ToString() != "" && decimal.Parse(row1["DutyDateCount"].ToString()) < DateTime.DaysInMonth(hryear, hrmonth))
//        {
//            dr["DaysFactor"] = decimal.Parse(row1["MonthFactor"].ToString()) - (row1["TotalHoliday"] == null || row1["TotalHoliday"].ToString() == "" ? 0 : decimal.Parse(row1["TotalHoliday"].ToString())) - (row1["LeaveCount"] == null || row1["LeaveCount"].ToString() == "" ? 0 : decimal.Parse(row1["LeaveCount"].ToString())) - (row1["AbsentCount"] == null || row1["AbsentCount"].ToString() == "" ? 0 : decimal.Parse(row1["AbsentCount"].ToString()));
//        }

//        //�»���
//        dr["MonthFactor"] = row1["MonthFactor"] == null || row1["MonthFactor"].ToString() == "" ? 0 : decimal.Parse(row1["MonthFactor"].ToString());

//        #region ��н
//        // ���ۿ�������


//        PunishLeaveCount = 0;//���ۿ����ٿ���

//        //С춃��첻��
//        if (row1["PunishLeaveCount"] != null && row1["PunishLeaveCount"].ToString() != "" && decimal.Parse(row1["PunishLeaveCount"].ToString()) < 2)
//            PunishLeaveCount = 0;
//        //2-2.5���1�������գ�3-3.5��2�������գ�4-4.5���3�������գ�5-5.5���4��������
//        else
//        {

//            row1["TotalHoliday"] = row1["TotalHoliday"] == null || row1["TotalHoliday"].ToString() == "" ? 0 : row1["TotalHoliday"];

//            if (row1["PunishLeaveCount"] != null && row1["PunishLeaveCount"].ToString() != "" && decimal.Parse(row1["PunishLeaveCount"].ToString()) - decimal.Parse("1.05") >= decimal.Parse(row1["TotalHoliday"].ToString()))
//                PunishLeaveCount = decimal.Parse(row1["TotalHoliday"].ToString());
//            else
//                PunishLeaveCount = decimal.Parse(row1["TotalHoliday"].ToString()) - decimal.Parse("1.05");
//        }
//        dr["DaysFactor"] = decimal.Parse(dr["DaysFactor"].ToString()) - PunishLeaveCount;
//        //��н=��н/��ǰ������*�»��� + ��н*���ջ���-��ְ����-����������
//        dr["BasePay"] = employees.MonthlyPay.Value / DateTime.DaysInMonth(hryear, hrmonth) * decimal.Parse(dr["MonthFactor"].ToString()) + employees.DailyPay.Value * (decimal.Parse(dr["DaysFactor"].ToString()));

//        #endregion
//        dr["GivenDays"] = row["HolidayBonusGivenDays"] == null || row["HolidayBonusGivenDays"].ToString() == "" ? 0 : decimal.Parse(row["HolidayBonusGivenDays"].ToString());
//        dr["OtherPay"] = row["OtherPay"] == null || row["OtherPay"].ToString() == "" ? 0 : decimal.Parse(row["OtherPay"].ToString());
//        dr["OtherPunish"] = row["OtherPunish"] == null || row["OtherPunish"].ToString() == "" ? 0 : decimal.Parse(row["OtherPunish"].ToString());

//        dr["PostPay"] = employees.PostPay == null ? 0 : employees.PostPay.Value;
//        dr["DutyPay"] = employees.DutyPay == null ? 0 : employees.DutyPay.Value;
//        #region/ְ������





//        // decimal SpecificHoliday = this.leavemanage.SelectSpecificHolidayMonthEmp(employees, hryear, hrmonth);
//        //HFieldPay = (employees.FieldPay.Value / DateTime.DaysInMonth(hryear, hrmonth) * (dayfactorSum - SpecificHoliday))==0?0:(employees.FieldPay.Value / DateTime.DaysInMonth(hryear, hrmonth) * (dayfactorSum - SpecificHoliday));
//        //�����ְ �۳���ĩ ְ������
//        if (row1["AbsentCount"] != null && row1["AbsentCount"].ToString() != "" && decimal.Parse(row1["AbsentCount"].ToString()) > 0)
//        {
//            dr["FieldPay"] = employees.FieldPay.Value;// / DateTime.DaysInMonth(hryear, hrmonth) * (decimal.Parse(row1["DaysFactor"].ToString()) - this.leavemanage.SelectSpecificHolidayMonthEmp(employees, hryear, hrmonth) - this.leavemanage.SelectTotalHolidayMonthEmp(employees, hryear, hrmonth));
//        }
//        else
//        {
//            dr["FieldPay"] = employees.FieldPay.Value;
//        }
//        #endregion




//        #region    ȫ�ڽ�
//        // decimal AllAttendBonus = decimal.Zero;

//        if (row1["DutyDateCount"] != null && row1["DutyDateCount"].ToString() != "" && decimal.Parse(row1["DutyDateCount"].ToString()) == DateTime.DaysInMonth(hryear, hrmonth))
//        {

//            //�����Mһ���£���н����
//            if (decimal.Parse(dr["MonthFactor"].ToString()) == DateTime.DaysInMonth(hryear, hrmonth))
//            {
//                if (row1["PunishLeaveCount"] != null && row1["PunishLeaveCount"].ToString() != "" && decimal.Parse(row1["PunishLeaveCount"].ToString()) == 0)
//                {
//                    dr["AllAttendBonus"] = employees.DailyPay.Value * 3;
//                }
//                //Ո���ۿ��С춵��һ�죺��н����   
//                else if (row1["PunishLeaveCount"] != null && row1["PunishLeaveCount"].ToString() != "" && decimal.Parse(row1["PunishLeaveCount"].ToString()) <= 1)
//                {
//                    dr["AllAttendBonus"] = employees.DailyPay.Value * 2;
//                }
//                else
//                    dr["AllAttendBonus"] = 0;

//            }

//             //��ȱˢ��ӛ�                 
//            else
//            {
//                dr["AllAttendBonus"] = 0;
//            }
//        }
//        //δ�Mһ���£������x��
//        else
//        {
//            dr["AllAttendBonus"] = 0;
//        }
//        #endregion
//        // dr["WorkBonus"] = 1000;
//        dr["EffectBonus"] = 1000;


//        #region //�Ӱ�
//        decimal OverTimeFee = decimal.Zero;
//        decimal OverTimeBonus = decimal.Zero;
//        DataSet overtimeData = this.overtimemanage.SelectOverTimeInfoByEmployeeId(employees.EmployeeId, new DateTime(hryear, hrmonth, 01));
//        for (int i = 0; i < overtimeData.Tables[0].Rows.Count; i++)
//        {
//            OverTimeFee += decimal.Parse(overtimeData.Tables[0].Rows[i][Model.OverTime.PROPERTY_OVERTIMEFEE].ToString());
//            OverTimeBonus += decimal.Parse(overtimeData.Tables[0].Rows[i][Model.OverTime.PROPERTY_OVERTIMEBONUS].ToString());
//        }
//        dr["OverTimeFee"] = OverTimeFee;
//        dr["OverTimeBonus"] = OverTimeBonus;

//        #endregion
//        dr["AnnualHolidayFee"] = 0;


//        dr["LatePunish"] = 0;
//        dr["DailyPay"] = employees.DailyPay == null ? 0 : employees.DailyPay.Value;
//        dr["Insurance"] = employees.Insurance == null ? 0 : employees.Insurance.Value;
//        // dr["LoanFee"] = row1["LoanFee"] == null || row1["LoanFee"].ToString() == "" ? 0 : decimal.Parse(row1["LoanFee"].ToString()); // this.loandetailManage.SelectFeeSum(employees, hryear, hrmonth);
//        // dr["LunchFee"] = row1["LunchFee"] == null || row1["LunchFee"].ToString() == "" ? 0 : decimal.Parse(row1["LunchFee"].ToString());// this.lunchdetailmanage.SelectFeeSum(employees, hryear, hrmonth);
//        #region �ж�Ϊ��
//        dr["Tax"] = employees.Tax == null ? 0 : employees.Tax.Value;

//        dr["BasePay"] = dr["BasePay"] == null || dr["BasePay"].ToString() == "" ? 0 : dr["BasePay"];
//        dr["AllAttendBonus"] = dr["AllAttendBonus"] == null || dr["AllAttendBonus"].ToString() == "" ? 0 : dr["AllAttendBonus"];
//        dr["DutyPay"] = dr["DutyPay"] == null || dr["DutyPay"].ToString() == "" ? 0 : dr["DutyPay"];
//        dr["PostPay"] = dr["PostPay"] == null || dr["PostPay"].ToString() == "" ? 0 : dr["PostPay"];
//        dr["FieldPay"] = dr["FieldPay"] == null || dr["FieldPay"].ToString() == "" ? 0 : dr["FieldPay"];
//        dr["OverTimeFee"] = dr["OverTimeFee"] == null || dr["OverTimeFee"].ToString() == "" ? 0 : dr["OverTimeFee"];
//        dr["OverTimeBonus"] = dr["OverTimeBonus"] == null || dr["OverTimeBonus"].ToString() == "" ? 0 : dr["OverTimeBonus"];
//        dr["SpecialBonus"] = dr["SpecialBonus"] == null || dr["SpecialBonus"].ToString() == "" ? 0 : dr["SpecialBonus"];
//        dr["EffectFactor"] = dr["EffectFactor"] == null || dr["EffectFactor"].ToString() == "" ? 0 : dr["EffectFactor"];

//        dr["AnnualHolidayFee"] = dr["AnnualHolidayFee"] == null || dr["AnnualHolidayFee"].ToString() == "" ? 0 : dr["AnnualHolidayFee"];
//        dr["OtherPay"] = dr["OtherPay"] == null || dr["OtherPay"].ToString() == "" ? 0 : dr["OtherPay"];
//        dr["OtherPunish"] = dr["OtherPunish"] == null || dr["OtherPunish"].ToString() == "" ? 0 : dr["OtherPunish"];
//        dr["LatePunish"] = dr["LatePunish"] == null || dr["LatePunish"].ToString() == "" ? 0 : dr["LatePunish"];


//        dr["ShouldPay"] = dr["ShouldPay"] == null || dr["ShouldPay"].ToString() == "" ? 0 : dr["ShouldPay"];

//        dr["Insurance"] = dr["Insurance"] == null || dr["Insurance"].ToString() == "" ? 0 : dr["Insurance"];
//        dr["LunchFee"] = dr["LunchFee"] == null || dr["LunchFee"].ToString() == "" ? 0 : dr["LunchFee"];
//        dr["LoanFee"] = dr["LoanFee"] == null || dr["LoanFee"].ToString() == "" ? 0 : dr["LoanFee"];
//        #endregion

//        dr["ShouldPay"] = decimal.Parse(dr["BasePay"].ToString()) + decimal.Parse(dr["AllAttendBonus"].ToString()) + decimal.Parse(dr["DutyPay"].ToString()) + decimal.Parse(dr["PostPay"].ToString()) + decimal.Parse(dr["FieldPay"].ToString()) + decimal.Parse(dr["OverTimeFee"].ToString()) + decimal.Parse(dr["OverTimeBonus"].ToString()) + decimal.Parse(dr["SpecialBonus"].ToString()) + decimal.Parse(dr["EffectFactor"].ToString()) + decimal.Parse(dr["AnnualHolidayFee"].ToString()) + decimal.Parse(dr["OtherPay"].ToString()) - decimal.Parse(dr["OtherPunish"].ToString()) - decimal.Parse(dr["LatePunish"].ToString());

//        dr["BonusTotal"] = decimal.Parse(dr["ShouldPay"].ToString()) - decimal.Parse(dr["Insurance"].ToString()) - decimal.Parse(dr["LunchFee"].ToString()) - decimal.Parse(dr["LoanFee"].ToString()) - decimal.Parse(dr["Tax"].ToString());
//        //dr["BonusTotal"] = 2222;
//        table.Rows.Add(dr);
//    }

//    CalCrystalReportForm f = new CalCrystalReportForm(table, hryear, hrmonth);
//    f.Show();
//}
#endregion
//#region @��ʼ��
////monthsum = 0;

//#endregion
//#region @��н
//////�ܳ�������
////DutyDateCount = this._hrManager.SelectDutyDateCount(hryear, hrmonth, this.employee);
//////�ջ���
////dayfactorSum = this._hrManager.SelectDayFactorSum(hryear, hrmonth, this.employee);
////monthFactorSum = this._hrManager.SelectDayMonthSum(hryear, hrmonth, this.employee);

////// ���ۿ�������

////decimal PunishCount = this.leavemanage.SelectPunishByMonthEmp(this.employee, hryear, hrmonth);// ���ۿ�ٿ���
////decimal TotalHoliday = this.leavemanage.SelectTotalHolidayMonthEmp(this.employee, hryear, hrmonth);//���ڼ�¼�·ݼ��պ�
////decimal PunishLeaveCount = PunishCount;//���ۿ����ٿ���


////#region С춃��첻��
//////if (PunishLeaveCount < 2)
//////    PunishLeaveCount = 0;
////////2-2.5���1�������գ�3-3.5��2�������գ�4-4.5���3�������գ�5-5.5���4��������
//////else
//////{
//////    if (PunishLeaveCount - decimal.Parse("1.05") >= TotalHoliday)
//////        PunishLeaveCount = TotalHoliday;
//////    else
//////        PunishLeaveCount = TotalHoliday - decimal.Parse("1.05");
//////}
////#endregion
//////��н=��н/��ǰ������*�»��� + ��н*���ջ���-��ְ����-����������
////this.employee.MonthlyPay = this.employee.MonthlyPay == null ? 0 : this.employee.MonthlyPay;
////this.employee.DailyPay = this.employee.DailyPay == null ? 0 : this.employee.DailyPay;

////monthsum = this.employee.MonthlyPay.Value / DateTime.DaysInMonth(hryear, hrmonth) * this.monthFactorSum + this.employee.DailyPay.Value * (this.dayfactorSum - PunishLeaveCount);


//#endregion
//#region @�Ӱ����

//#endregion
//#region        @������

////decimal BusinessHourPay = decimal.Zero;
////BusinessHourPay = this._hrManager.SelectBusinessHourPaySum(hryear, hrmonth, this.employee.EmployeeId);
//#endregion
//#region @ƽ�ռӰ� ���ռӰ� ��н�Ӱ���� ��˰��

////DataSet overtimeData = this.overtimemanage.SelectOverTimeInfoByEmployeeId(this.employee.EmployeeId, new DateTime(hryear, hrmonth, 01));
////for (int i = 0; i < overtimeData.Tables[0].Rows.Count; i++)
////{

////    if ((bool)overtimeData.Tables[0].Rows[i][Model.OverTime.PROPERTY_ISHOLIDAY])
////    {
////        HolidayFee += decimal.Parse(overtimeData.Tables[0].Rows[i][Model.OverTime.PROPERTY_OVERTIMEFEE].ToString());

////    }
////    else
////    {

////        NormalFee += decimal.Parse(overtimeData.Tables[0].Rows[i][Model.OverTime.PROPERTY_OVERTIMEFEE].ToString());
////    }
////    OverTimeBonus += decimal.Parse(overtimeData.Tables[0].Rows[i][Model.OverTime.PROPERTY_OVERTIMEBONUS].ToString());

////}

////this.label_prjb.Text = NormalFee.ToString("N0");
////this.label_jrjb.Text = HolidayFee.ToString("N0");
////this.label_jbjt.Text = OverTimeBonus.ToString("N0");
////this.employee.Tax = this.employee.Tax == null ? 0 : this.employee.Tax;
////this.labelTax.Text = this.employee.Tax.Value.ToString("N0");
//#endregion
//#region @�ٵ��ۿ�
////if (this._hrManager.SelectLateSum(hryear, hrmonth, this.employee) > 10 || this._hrManager.SelectLateCount(hryear, hrmonth, this.employee) > 3)
////{
////    LatePunish = 0;
////    //��ʱ��¼�ٵ�
////    int mCount = 0;  //��С��10�� ����
////    //StringBuilder strBU = new StringBuilder();
////    foreach (Model.HrDailyEmployeeAttendInfo attend in this._hrManager.SelectByEmpMonth(this.employee, hryear, hrmonth))
////    {
////        if (attend.LateInMinute.Value != 0)
////        {
////            strBU.Append(attend.LateInMinute.ToString() + "|");
////            mTemp = mTemp + attend.LateInMinute.Value;
////            if (mTemp <= 10)
////            {
////                mCount = mCount + 1;
////            }
////            if (attend.LateInMinute.Value > 30)
////            {
////                mHicount = mHicount + 1;
////            }
////        }
////    }
////    // '�t�����^���Σ����t�����^10���
////    //'����С�r����0.5С�r��̶�
////    string[] strs = new string[31];
////    IList<int> a = new List<int>();

////    if (strBU.Length > 0)
////        strs = strBU.ToString(0, strBU.Length - 1).Split('|');
////    for (int i = 0; i < strs.Length; i++)
////    {
////        if (strs[i] != null)
////        {
////            if (strs[i] == "0")
////                continue;
////            else
////                a.Add(Int32.Parse(strs[i]));
////        }
////    }

////    int temp = 0;
////    for (int i = 0; i < a.Count; i++)
////    {
////        for (int j = i + 1; j < a.Count; j++)
////        {
////            if (a[i] > a[j])
////            {
////                temp = a[i];
////                a[i] = a[j];
////                a[j] = temp;
////            }

////        }
////    }
////    int sum = 0;
////    int m;
////    for (m = 0; m < a.Count; m++)
////    {
////        sum = sum + a[m];
////        if (sum > 10)
////            break;
////    }
////    if (m > 3)
////    {
////        m = 3;
////    }
////    mCount = m;
////    decimal TotalLateInHour = decimal.Parse(((this._hrManager.SelectLateCount(hryear, hrmonth, this.employee) - mCount - mHicount) * 0.5 + mHicount).ToString());
////    LatePunish = TotalLateInHour * this.employee.DailyPay.Value / 8;

////}
////this.label_cdkk.Text = LatePunish.ToString("N0");


//#endregion
//#region    @ȫ�ڽ�
////decimal AllAttendBonus = decimal.Zero;

////if (DutyDateCount == DateTime.DaysInMonth(hryear, hrmonth))
////{

////    //�����Mһ���£���н����
////    if (monthFactorSum == DateTime.DaysInMonth(hryear, hrmonth))
////    {
////        if (PunishCount == 0)
////        {
////            AllAttendBonus = this.employee.DailyPay.Value * 3;
////        }
////        //Ո���ۿ��С춵��һ�죺��н����   
////        else if (PunishCount <= 1)
////        {
////            AllAttendBonus = this.employee.DailyPay.Value * 2;
////        }
////    }

////     //��ȱˢ��ӛ�                 
////    else
////    {
////        AllAttendBonus = 0;
////    }
////}
//////δ�Mһ���£������x��
////else
////{
////    AllAttendBonus = 0;
////}



//#endregion
//#region @��������,��Ч����, ��ҵ��������
//////��������=ְ����������ν�����������+ȫ�ڽ�
////this.employee.DutyPay = this.employee.DutyPay == null ? 0 : this.employee.DutyPay;
////this.employee.PostPay = this.employee.PostPay == null ? 0 : this.employee.PostPay;
////WorkBonus = this.employee.PostPay.Value + this.employee.DutyPay.Value + BusinessHourPay + AllAttendBonus;



////int[] months = { 1, 2, 3, 1, 2, 3, 1, 2, 3, 1, 2, 3 };
////switch (months[hrmonth - 1])
////{
////    case 1:
////        label_gj.Text = WorkBonus.ToString("N0");
////        label_jxjj.Text = "0";
////        label_zyjj.Text = "0";
////        break;
////    case 2:
////        label_jxjj.Text = WorkBonus.ToString("N0");
////        label_gj.Text = "0";
////        label_zyjj.Text = "0";
////        break;
////    case 3:
////        label_zyjj.Text = WorkBonus.ToString("N0");
////        label_jxjj.Text = "0";
////        label_gj.Text = "0";
////        break;
////}

//#endregion
//#region @ְ������
////decimal HFieldPay = decimal.Zero;
////decimal SpecificHoliday = this.leavemanage.SelectSpecificHolidayMonthEmp(this.employee, hryear, hrmonth);
////this.employee.FieldPay = this.employee.FieldPay == null ? 0 : this.employee.FieldPay;
////HFieldPay = this.employee.FieldPay.Value / DateTime.DaysInMonth(hryear, hrmonth) * (dayfactorSum - SpecificHoliday);


//////�����ְ �۳���ĩ ְ������
////if (this._hrManager.SelectAbsentCount(hryear, hrmonth, this.employee) > 0)
////{
////    HFieldPay = this.employee.FieldPay.Value / DateTime.DaysInMonth(hryear, hrmonth) * (dayfactorSum - this.leavemanage.SelectSpecificHolidayMonthEmp(this.employee, hryear, hrmonth) - this.leavemanage.SelectTotalHolidayMonthEmp(this.employee, hryear, hrmonth));
////}
//#endregion
//#region @����δ���Mһ�������ߣ�ȡ����  ��؟�ν��N�������ս��N�����������N�������L���ݼ١�
////if (this._hrManager.SelectDutyDateCount(hryear, hrmonth, this.employee) < DateTime.DaysInMonth(hryear, hrmonth))
////{
////    this.employee.DutyPay = 0;
////    this.employee.PostPay = 0;   //ְ�����
////    HFieldPay = 0; //ְ������
////}
//#endregion
//#region @��Ч����
////��Ч���� = ȫ�ڽ��� 
////jxbonus = Punishpay;

////label_jxjj.Text = jxbonus.ToString();
//#endregion
//#region @��ҵ��������
////Model.Department dep = (new BL.DepartmentManager()).Get(employee.DepartmentId);
////if (dep.DepartmentName.Contains("���"))
////{
////    zyjsbonus = (this.employee.DailyPay.Value) * 3;
////}

////label_zyjj.Text = "0";// zyjsbonus.ToString();
//#endregion
//#region  @��Чϵ������ۿ�
////this._monthlySalary = this.monthlySalaryManager.GetByeEmpIdMonth(this.employee, hryear, hrmonth);

////if (this._monthlySalary != null)
////{
////    this.spinEditEffectFactor.Value = this._monthlySalary.EffectFactor == null ? 0 : this._monthlySalary.EffectFactor.Value;
////    this.calcEditOtherPay.Value = this._monthlySalary.OtherPay == null ? 0 : this._monthlySalary.OtherPay.Value;
////    this.calcEditOtherPunish.Value = this._monthlySalary.OtherPunish == null ? 0 : this._monthlySalary.OtherPunish.Value;
////}
////else
////{
////    this.spinEditEffectFactor.Value = 0;
////    this.calcEditOtherPay.Value = 0;
////    this.calcEditOtherPunish.Value = 0;

////}
//#endregion
#endregion