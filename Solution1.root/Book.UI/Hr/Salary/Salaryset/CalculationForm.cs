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
// Copyright (C) 2008 - 2010  咸阳飛馳軟件有限公司
//                     版權所有 圍著必究

// 编 码 人: 马艳军             完成时间:2009-10-26
// 修改原因：
// 修 改 人:                          修改时间:
// 修改原因：
// 修 改 人:                          修改时间:
//----------------------------------------------------------------*/
    public partial class CalculationForm : DevExpress.XtraEditors.XtraForm
    {
        /// <summary>
        /// 员工管理实例
        /// </summary>
        protected BL.EmployeeManager employeeManager = new Book.BL.EmployeeManager();
        /// <summary>
        /// 部门管理实例
        /// </summary>
        // protected BL.DepartmentManager departmentManager = new Book.BL.DepartmentManager();
        /// <summary>
        /// 当前待处理员工
        /// </summary>


        /// <summary>
        /// 請假管理
        /// </summary>
        BL.OverTimeManager overtimeman = new Book.BL.OverTimeManager();
        ///// <summary>
        ///// 出勤记录
        ///// </summary>
        //private BL.AttenManager attenmanage = new Book.BL.AttenManager();
        /// <summary>
        /// 请假管理
        /// </summary>
        private BL.LeaveManager leavemanage = new Book.BL.LeaveManager();

        private BL.OverTimeManager overtimemanage = new Book.BL.OverTimeManager();
        /// <summary>
        /// 午餐补助管理
        /// </summary>
        private BL.LunchDetailManager lunchdetailmanage = new Book.BL.LunchDetailManager();
        /// <summary>
        /// 借支管理
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
        /// 实际工作天数
        /// </summary>
        //int workcount = 30;
        /// <summary>
        /// 底薪
        /// </summary>
        // decimal monthsum = 0M;
        /// <summary>
        /// 工作奖金
        /// </summary>
        // decimal WorkBonus = 0M;
        /// <summary>
        ///班别津贴
        /// </summary>
        // decimal bunbonus = 0M;
        /// <summary>
        /// 全勤奖
        /// </summary>
        //  decimal Perfectbonus = 0M;
        /// <summary>
        /// 绩效奖金
        /// </summary>
        //decimal jxbonus = 0M;
        /// <summary>
        /// 作业绩效奖金
        /// </summary>
        // decimal zyjsbonus = 0M;
        /// <summary>
        /// 扣薪天数
        /// </summary>
        //int Punishpay = 0;
        /// <summary>
        /// 底薪总计
        /// </summary>
        //decimal dxsum = 0M;
        /// <summary>
        /// 实领工资
        /// </summary>
        //decimal gongzslsum = 0M;
        /// <summary>
        /// 员工
        /// </summary>
        private Book.Model.Employee employee = null;
        /// <summary>
        /// 总日基数
        /// </summary>
        //decimal dayfactorSum = 0;
        ///// <summary>
        ///// 总月基数
        ///// </summary>
        //decimal monthFactorSum = 0;
        ///// <summary>
        ///// 总出勤数
        ///// </summary>
        //int DutyDateCount = 0;
        ///// <summary>
        ///// 加班津贴
        ///// </summary>
        //decimal OverTimeBonus = decimal.Zero;
        ///// <summary>
        ///// 平日加班酬薪
        ///// </summary>
        //decimal NormalFee = decimal.Zero;
        ///// <summary>
        ///// 假日加班酬薪
        ///// </summary>
        //decimal HolidayFee = decimal.Zero;
        /// <summary>
        /// 迟到扣款
        /// </summary>
        // decimal LatePunish = decimal.Zero;

        public CalculationForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 窗体构造函数
        /// </summary>
        /// <param name="employees"></param>
        public CalculationForm(Model.Employee employees)
        {
            this.employee = employees;
            Calulation(employees);
        }

        /// <summary>
        /// 加载
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
                    this.comboBoxEdit1.Properties.Items.Add(strdate.AddMonths(-1).ToString("yyyy年MM月"));
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
        /// 薪资
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

            #region  取考勤记录
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
                    //在职务津贴扣除符合条件假期
                }
                _ms.mNote = attend.Note;
                if (!string.IsNullOrEmpty(_ms.mNote))
                {
                    if (_ms.mNote != "週日休假" && _ms.mNote.IndexOf("公假") < 0 && _ms.mNote.IndexOf("婚假") < 0 && _ms.mNote.IndexOf("喪假") < 0 && _ms.mNote.IndexOf("年假") < 0 && _ms.mNote.IndexOf("出差") < 0 && _ms.mNote.IndexOf("遲到") < 0)
                        _ms.mCount = _ms.mCount + 1;
                    else if (_ms.mNote.Contains("事假") || _ms.mNote.Contains("病假"))
                        _ms.mCount = _ms.mCount + 1;

                }
            }
            #endregion
            #region 取薪资计算
            DataSet dsms = this.monthlySalaryManager.getMonthlySummaryFee(emp.EmployeeId, _ms.mIdentifyDate, hryear, hrmonth);
            if (dsms.Tables[0].Rows.Count > 0)
            {
                DataRow dr = dsms.Tables[0].Rows[0];
                _ms.mLunchFee = mStrToDouble(dr["LunchFee"]);                                 //午餐費
                _ms.mOverTimeFee = mStrToDouble(dr["OverTimeFee"]);                           //加班費
                _ms.mGeneralOverTime = mStrToDouble(dr["GeneralOverTime"]);                   //平日加班(時數)
                _ms.mHolidayOverTime = mStrToDouble(dr["HolidayOverTime"]);                   //假日加班(時數)
                _ms.GeneralOverTimeCountBig = mStrToDouble(dr["GeneralOverTimeCountBig"]);    //平日加班2小时之外(時數)
                _ms.GeneralOverTimeCountSmall = mStrToDouble(dr["GeneralOverTimeCountSmall"]);//平日加班2小时以下(時數)
                _ms.HolidayOverTimeCountBig = mStrToDouble(dr["HolidayOverTimeCountBig"]);    //假日加班2小时之外(時數)
                _ms.HolidayOverTimeCountSmall = mStrToDouble(dr["HolidayOverTimeCountSmall"]);//假日加班2小时以下(時數)
                _ms.mLateCount = mStrToDouble(dr["LateCount"]);                               //遲到次數
                _ms.mTotalLateInMinute = mStrToDouble(dr["TotalLateInMinute"]);               //總遲到（分）
                _ms.mOverTimeBonus = mStrToDouble(dr["OverTimeBonus"]);                       //加班津貼
                _ms.mSpecialBonus = mStrToDouble(dr["SpecialBonus"]);                         //中夜班津貼
                _ms.mDaysFactor = mStrToDouble(dr["DaysFactor"]);                             //總日基數
                _ms.mMonthFactor = mStrToDouble(dr["MonthFactor"]);                           //總月基數
                _ms.mDutyDateCount = mStrToDouble(dr["DutyDateCount"]);                       //總出勤記錄數
                _ms.mLeaveDate = (dr["LeaveDate"] == null || dr["LeaveDate"].ToString() == "") ? global::Helper.DateTimeParse.NullDate : Convert.ToDateTime(dr["LeaveDate"].ToString());                                               //离职日期
                _ms.mPunishLeaveCount = mStrToDouble(dr["PunishLeaveCount"]);                 //倒扣款假總數
                _ms.mLeaveCount = mStrToDouble(dr["LeaveCount"]);                             //請假總數
                _ms.mAbsentCount = mStrToDouble(dr["AbsentCount"]);                           //曠工總數
                _ms.mTotalHoliday = mStrToDouble(dr["TotalHoliday"]);                         //該月總例假數
                _ms.mLoanFee = mStrToDouble(dr["LoanFee"]);
                // int WuXinCount = Int32.Parse(dr["WuXinCount"].ToString());
                //考勤 不满一月  日基数 =月基数-实际假数-扣款请假天数-旷职-无薪假      //矿工待处理  
                if (_ms.mDutyDateCount < totalDay)
                    _ms.mDaysFactor = _ms.mDaysFactor - _ms.mTotalHoliday;
                //    if (_ms.mDutyDateCount < totalDay && _ms.mLeaveDate != global::Helper.DateTimeParse.NullDate && _ms.mLeaveDate.ToString() != "")             //總出勤記錄數
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
            #region 底薪
            DataSet dx_ds = this.monthlySalaryManager.getMonthlySalary(emp.EmployeeId, _ms.mIdentifyDate);
            if (dx_ds.Tables[0].Rows.Count > 0)
            {
                _ms.mMonthFactor = _ms.mMonthFactor;
                DataRow dx_dr = dx_ds.Tables[0].Rows[0];
                _ms.mDailyPay = mStrToDouble(dx_dr["DailyPay"]); //日工资
                _ms.mMonthlyPay = mStrToDouble(dx_dr["MonthlyPay"]); //月工资
                if (VPerson.vipPerson.Contains(emp.EmployeeId))
                {
                    _ms.mDutyPay = this.GetSiSheWuRu(mStrToDouble(dx_dr["DutyPay"]), 0);  //责任津贴
                    _ms.mPostPay = this.GetSiSheWuRu(mStrToDouble(dx_dr["PostPay"]), 0);  //職務津貼
                }
                else
                {
                    _ms.mDutyPay = this.GetSiSheWuRu(mStrToDouble(dx_dr["DutyPay"]) * _ms.mMonthFactor / totalDay, 0);  //责任津贴
                    _ms.mPostPay = this.GetSiSheWuRu(mStrToDouble(dx_dr["PostPay"]) * _ms.mMonthFactor / totalDay, 0);  //職務津貼
                }
                _ms.mGivenDays = mStrToDouble(dx_dr["HolidayBonusGivenDays"]);  //年假(补休)天数
                _ms.mAnnualHolidayFee = _ms.mDailyPay * _ms.mGivenDays;         //年假(补休)金额
                _ms.mInsurance = mStrToDouble(dx_dr["insurance"]); //保险费
                _ms.mTax = mStrToDouble(dx_dr["Tax"]);   //所得税
                _ms.mEffectFactor = mStrToDouble(dx_dr["EffectFactor"]); //績效係數
                _ms.mOtherPay = mStrToDouble(dx_dr["OtherPay"]);  //其它補款
                _ms.mOtherPunish = mStrToDouble(dx_dr["OtherPunish"]); //其它扣款
                _ms.mFieldPay = this.GetSiSheWuRu(mStrToDouble(dx_dr["FieldPay"]) * (totalDay - _ms.mCount) / totalDay, 0);  //职场津贴
                _ms.mBasePay = _ms.mMonthlyPay * _ms.mMonthFactor / totalDay + _ms.mDailyPay * _ms.mDaysFactor;                //底薪
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
            #region 迟到扣款
            int mIcount = 0;  //和小于10分 次数小于3次
            if ((_ms.mTotalLateInMinute > 10 || _ms.mLateCount > 2) && !VPerson.vipPerson.Contains(emp.EmployeeId))
            {
                //临时记录迟到

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
                // '遲到超過三次，或總遲到超過10分鐘
                //'化成小時，以0.5小時為刻度
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
                mIcount = (int)_ms.mLateCount;  // 10分钟内 3次之内 用于全勤奖
            }
            #endregion
            #region 全勤奖
            if (VPerson.vipPerson.Contains(emp.EmployeeId))
            {
                _ms.mAllAttendBonus = _ms.mDailyPay * 3;
            }
            else
            {
                if (_ms.mDutyDateCount == totalDay)
                {
                    //有做滿一個月：日薪三天
                    if (_ms.mMonthFactor == totalDay)
                    {
                        if (_ms.mPunishLeaveCount == 0)
                        {
                            _ms.mAllAttendBonus = _ms.mDailyPay * 3;
                        }
                        //請倒扣款假小於等於一天：日薪二天
                        else if (_ms.mPunishLeaveCount <= 1)
                        {
                            _ms.mAllAttendBonus = _ms.mDailyPay * 2;
                        }
                        //判斷遲到,扣除全勤獎金
                        if (_ms.mLateCount - mIcount > 0)
                        {
                            _ms.mAllAttendBonus = _ms.mAllAttendBonus - _ms.mDailyPay * (_ms.mLateCount - mIcount) >= 0 ? _ms.mAllAttendBonus - _ms.mDailyPay * (_ms.mLateCount - mIcount) : 0;
                        }
                    }
                    //有缺刷卡記錄                 
                    else
                    {
                        _ms.mAllAttendBonus = 0;
                    }
                }
                //未滿一個月（因到職或離職）
                else
                {
                    _ms.mAllAttendBonus = 0;
                }
            }
            #endregion
            #region 離職未做滿一整個月者，取消：「責任津貼」、「職務津貼」、「職場津貼」、「週日休假」
            if (_ms.mDutyDateCount < totalDay && _ms.mLeaveDate != global::Helper.DateTimeParse.NullDate && !VPerson.vipPerson.Contains(emp.EmployeeId))
            {
                _ms.mDutyPay = 0;
                _ms.mPostPay = 0;
                _ms.mFieldPay = 0;
            }
            #endregion
            #region 平日加班 假日加班 酬薪加班津贴 及税额
            #region //弃用
            //_ms.mGeneralOverTimeFee = GetSiSheWuRu((_ms.mDailyPay / 6) * _ms.mGeneralOverTime, 0);                   //平日加班费
            //_ms.mHolidayOverTimeFee = GetSiSheWuRu((((_ms.mDailyPay / 2) * 3) / 8) * _ms.mHolidayOverTime, 0);       //假日加班费
            #endregion
            #region //启用 假日&平日加班算法修改 2013年4月26日15:09:41 陈宁
            //平日加班 0~2小时之类 时薪*hour*1.33  2>  时薪*hour*1.66
            //if (_ms.mGeneralOverTime < 2)
            //{
            //    _ms.mGeneralOverTimeFee = GetSiSheWuRu((_ms.mDailyPay / 8) * _ms.mGeneralOverTime * 1.33, 0);
            //}
            //else
            //{
            //_ms.mGeneralOverTimeFee = GetSiSheWuRu((_ms.mDailyPay / 8) * 2 * 1.33, 0) + GetSiSheWuRu((_ms.mDailyPay / 8) * (_ms.mGeneralOverTime - 2) * 1.66, 0);
            //}
            //平日加班 小于2小时 为 时薪*1.33*加班小时,超出2小时部分 为 时薪*1.66*加班小时
            _ms.mGeneralOverTimeFee = GetSiSheWuRu((_ms.mDailyPay / 8) * _ms.GeneralOverTimeCountSmall * 1.33, 0) + GetSiSheWuRu((_ms.mDailyPay / 8) * _ms.GeneralOverTimeCountBig * 1.66, 0);
            //假日加班 一律 为时薪 两倍.
            _ms.mHolidayOverTimeFee = GetSiSheWuRu(((_ms.mDailyPay / 8) * 2) * _ms.mHolidayOverTime, 0);
            #endregion
            #endregion
            #region 工作奖金,绩效奖金, 作业技术奖金 => 工作奖金=职务津贴＋责任津贴＋班别津贴+全勤奖
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

            //姓名
            this.label_gname.Text = _ms.mEmployeeName;
            //姓名
            this.label_jiname.Text = _ms.mEmployeeName;
            //底薪
            this.label_dx.Text = _ms.mBasePay.ToString();
            //餐费
            this.label_cf.Text = _ms.mLunchFee.ToString();
            //借支
            this.label_jiez.Text = _ms.mLoanFee.ToString();
            //保险 
            this.label_baox.Text = _ms.mInsurance.ToString();
            //职场津贴
            this.label_zhicjt.Text = _ms.mFieldPay.ToString();
            ////作业技术奖金
            //this.label_zyjj.Text = _ms.mTechBonus.ToString();
            //绩效奖金
            //this.label_jxjj.Text = _ms.mEffectBonus.ToString();
            //假日加班费
            //this.label_jrjb.Text = _ms.mHolidayOverTimeFee.ToString();
            //平日加班费
            //this.label_prjb.Text = _ms.mGeneralOverTimeFee.ToString();
            //加班津贴
            //this.label_jbjt.Text = _ms.mOverTimeBonus.ToString();
            //班别津贴
            this.label_banbiejintie.Text = _ms.mSpecialBonus.ToString();
            //职务津贴
            this.label_zhiwjt.Text = _ms.mPostPay.ToString();
            //年假(补休)天数
            this.txt_givenDays.Text = _ms.mGivenDays.ToString();
            //年假(补休)金额
            this.label_AnnualHolidayFee.Text = _ms.mAnnualHolidayFee.ToString();
            //工作奖金(全) 2013年3月29日  工作奖金 带出全勤奖金
            this.label_gj.Text = _ms.mAllAttendBonus.ToString();
            //责任津贴
            this.label_zerenjt.Text = _ms.mDutyPay.ToString();
            //绩效系数
            this.txtEffectFactor.EditValue = _ms.mEffectFactor;
            //迟到扣款
            this.label_cdkk.Text = _ms.mLatePunish.ToString("N0");
            //其它付款 
            this.txtOtherPay.EditValue = _ms.mOtherPay;
            //其它扣款
            this.txtOtherPunish.EditValue = _ms.mOtherPunish;
            //实领奖金
            this.label_BonusTotal.Text = _ms.BonusTotal.ToString();
            //小计
            this.label_xiaoji.Text = _ms.XiaoJI.ToString();
            //加班
            this.label_jiaban.Text = this.GetSiSheWuRu(_ms.JiaBan, 0).ToString();
            //总计
            this.label_SubTotal.Text = this.GetSiSheWuRu(_ms.SubTotal, 0).ToString();
            //实领金额
            this.label_SalaryTotal.Text = this.GetSiSheWuRu(_ms.mSalaryTotal, 0).ToString();

            flag = 0;
        }

        //单击作业技术奖金
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

        //双击查看加班
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

        //单击底薪
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

        //单击查看餐费
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

        //保存
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

        //打印
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

            //------------------------------ From计算方法 ----Start----------------------------------------------------//
            #region  取考勤记录

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
                    //在职务津贴扣除符合条件假期
                }
                _ms.mNote = attend.Note;
                if (!string.IsNullOrEmpty(_ms.mNote))
                {
                    if (_ms.mNote != "週日休假" && _ms.mNote.IndexOf("公假") < 0 && _ms.mNote.IndexOf("婚假") < 0 && _ms.mNote.IndexOf("喪假") < 0 && _ms.mNote.IndexOf("年假") < 0 && _ms.mNote.IndexOf("出差") < 0 && _ms.mNote.IndexOf("遲到") < 0)
                        _ms.mCount = _ms.mCount + 1;
                    else if (_ms.mNote.Contains("事假") || _ms.mNote.Contains("病假"))
                        _ms.mCount = _ms.mCount + 1;
                }
            }
            #endregion
            #region 取薪资计算
            DataSet dsms = this.monthlySalaryManager.getMonthlySummaryFee(emp.EmployeeId, _ms.mIdentifyDate, hryear, hrmonth);
            if (dsms.Tables[0].Rows.Count > 0)
            {
                DataRow dr = dsms.Tables[0].Rows[0];
                _ms.mLunchFee = mStrToDouble(dr["LunchFee"]);                                 //午餐費
                _ms.mOverTimeFee = mStrToDouble(dr["OverTimeFee"]);                           //加班費
                _ms.mGeneralOverTime = mStrToDouble(dr["GeneralOverTime"]);                   //平日加班(時數)
                _ms.mHolidayOverTime = mStrToDouble(dr["HolidayOverTime"]);                   //假日加班(時數)
                _ms.GeneralOverTimeCountBig = mStrToDouble(dr["GeneralOverTimeCountBig"]);    //平日加班2小时之外(時數)
                _ms.GeneralOverTimeCountSmall = mStrToDouble(dr["GeneralOverTimeCountSmall"]);//平日加班2小时以下(時數)
                _ms.HolidayOverTimeCountBig = mStrToDouble(dr["HolidayOverTimeCountBig"]);    //假日加班2小时之外(時數)
                _ms.HolidayOverTimeCountSmall = mStrToDouble(dr["HolidayOverTimeCountSmall"]);//假日加班2小时以下(時數)
                _ms.mLateCount = mStrToDouble(dr["LateCount"]);                               //遲到次數
                _ms.mTotalLateInMinute = mStrToDouble(dr["TotalLateInMinute"]);               //總遲到(分)
                _ms.mOverTimeBonus = mStrToDouble(dr["OverTimeBonus"]);                       //加班津貼
                _ms.mSpecialBonus = mStrToDouble(dr["SpecialBonus"]);                         //中夜班津貼
                _ms.mDaysFactor = mStrToDouble(dr["DaysFactor"]);                             //總日基數
                _ms.mMonthFactor = mStrToDouble(dr["MonthFactor"]);                           //總月基數
                _ms.mDutyDateCount = mStrToDouble(dr["DutyDateCount"]);                       //總出勤記錄數
                _ms.mLeaveDate = (dr["LeaveDate"] == null || dr["LeaveDate"].ToString() == "") ? global::Helper.DateTimeParse.NullDate : Convert.ToDateTime(dr["LeaveDate"].ToString());                                                   //离职日期
                _ms.mPunishLeaveCount = mStrToDouble(dr["PunishLeaveCount"]);                 //倒扣款假總數
                _ms.mLeaveCount = mStrToDouble(dr["LeaveCount"]);                             //請假總數
                _ms.mAbsentCount = mStrToDouble(dr["AbsentCount"]);                           //曠工總數
                _ms.mTotalHoliday = mStrToDouble(dr["TotalHoliday"]);                         //該月總例假數
                _ms.mLoanFee = mStrToDouble(dr["LoanFee"]);
                // int WuXinCount = Int32.Parse(dr["WuXinCount"].ToString());
                //考勤 不满一月  日基数 =月基数-实际假数-扣款请假天数-旷职-无薪假          //矿工待处理  
                if (_ms.mDutyDateCount < totalDay)
                    _ms.mDaysFactor = _ms.mDaysFactor - _ms.mTotalHoliday;
                //    if (_ms.mDutyDateCount < totalDay && _ms.mLeaveDate != global::Helper.DateTimeParse.NullDate && _ms.mLeaveDate.ToString() != "")             //總出勤記錄數
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
            #region 底薪
            DataSet dx_ds = this.monthlySalaryManager.getMonthlySalary(emp.EmployeeId, _ms.mIdentifyDate);
            if (dx_ds.Tables[0].Rows.Count > 0)
            {
                _ms.mMonthFactor = _ms.mMonthFactor;
                DataRow dx_dr = dx_ds.Tables[0].Rows[0];
                _ms.mDailyPay = mStrToDouble(dx_dr["DailyPay"]); //日工资
                _ms.mMonthlyPay = mStrToDouble(dx_dr["MonthlyPay"]); //月工资
                if (VPerson.vipPerson.Contains(emp.EmployeeId))
                {
                    _ms.mDutyPay = this.GetSiSheWuRu(mStrToDouble(dx_dr["DutyPay"]), 0);  //责任津贴
                    _ms.mPostPay = this.GetSiSheWuRu(mStrToDouble(dx_dr["PostPay"]), 0);  //職務津貼
                }
                else
                {
                    _ms.mDutyPay = this.GetSiSheWuRu(mStrToDouble(dx_dr["DutyPay"]) * _ms.mMonthFactor / totalDay, 0);  //责任津贴
                    _ms.mPostPay = this.GetSiSheWuRu(mStrToDouble(dx_dr["PostPay"]) * _ms.mMonthFactor / totalDay, 0);  //職務津貼
                }
                _ms.mGivenDays = mStrToDouble(dx_dr["HolidayBonusGivenDays"]);  //年假(补休)天数
                _ms.mAnnualHolidayFee = _ms.mDailyPay * _ms.mGivenDays;         //年假(补休)金额
                _ms.mInsurance = mStrToDouble(dx_dr["insurance"]); //保险费
                _ms.mTax = mStrToDouble(dx_dr["Tax"]);   //所得税
                _ms.mEffectFactor = mStrToDouble(dx_dr["EffectFactor"]); //績效係數
                _ms.mOtherPay = mStrToDouble(dx_dr["OtherPay"]);  //其它補款
                _ms.mOtherPunish = mStrToDouble(dx_dr["OtherPunish"]); //其它扣款
                _ms.mFieldPay = this.GetSiSheWuRu(mStrToDouble(dx_dr["FieldPay"]) * (totalDay - _ms.mCount) / totalDay, 0);  //职场津贴
                _ms.mBasePay = _ms.mMonthlyPay * _ms.mMonthFactor / totalDay + _ms.mDailyPay * _ms.mDaysFactor;                //底薪
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
            #region 迟到扣款
            int mIcount = 0;  //和小于10分 次数
            if ((_ms.mTotalLateInMinute > 10 || _ms.mLateCount > 2) && !VPerson.vipPerson.Contains(emp.EmployeeId))
            {
                //临时记录迟到

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
                // '遲到超過三次，或總遲到超過10分鐘
                //'化成小時，以0.5小時為刻度
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
                mIcount = (int)_ms.mLateCount;// 10分钟内 次数 用于全勤奖
            }
            #endregion
            #region 全勤奖
            if (VPerson.vipPerson.Contains(emp.EmployeeId))
            {
                _ms.mAllAttendBonus = _ms.mDailyPay * 3;
            }
            else
            {
                if (_ms.mDutyDateCount == totalDay)
                {
                    //有做滿一個月：日薪三天
                    if (_ms.mMonthFactor == totalDay)
                    {
                        if (_ms.mPunishLeaveCount == 0)
                        {
                            _ms.mAllAttendBonus = _ms.mDailyPay * 3;
                        }
                        //請倒扣款假小於等於一天：日薪二天   
                        else if (_ms.mPunishLeaveCount <= 1)
                        {
                            _ms.mAllAttendBonus = _ms.mDailyPay * 2;
                        }
                        //判斷遲到,扣除全勤獎金
                        if (_ms.mLateCount - mIcount > 0)
                        {
                            _ms.mAllAttendBonus = _ms.mAllAttendBonus - _ms.mDailyPay * (_ms.mLateCount - mIcount) >= 0 ? _ms.mAllAttendBonus - _ms.mDailyPay * (_ms.mLateCount - mIcount) : 0;
                        }
                    }
                    //有缺刷卡記錄                 
                    else
                    {
                        _ms.mAllAttendBonus = 0;
                    }
                }
                //未滿一個月（因到職或離職）
                else
                {
                    _ms.mAllAttendBonus = 0;
                }
            }
            #endregion
            #region 離職未做滿一整個月者，取消：「責任津貼」、「職務津貼」、「職場津貼」、「週日休假」
            if (_ms.mDutyDateCount < totalDay && _ms.mLeaveDate != global::Helper.DateTimeParse.NullDate && !VPerson.vipPerson.Contains(emp.EmployeeId))
            {
                _ms.mDutyPay = 0;
                _ms.mPostPay = 0;
                _ms.mFieldPay = 0;
            }
            #endregion
            #region 平日加班 假日加班 酬薪加班津贴 及税额
            #region //弃用
            //_ms.mGeneralOverTimeFee = GetSiSheWuRu((_ms.mDailyPay / 6) * _ms.mGeneralOverTime, 0);                   //平日加班费
            //_ms.mHolidayOverTimeFee = GetSiSheWuRu((((_ms.mDailyPay / 2) * 3) / 8) * _ms.mHolidayOverTime, 0);       //假日加班费
            #endregion
            #region //启用 假日&平日加班算法修改 2013年4月26日15:09:41 陈宁
            //平日加班 0~2小时之类 时薪*hour*1.33  2>  时薪*hour*1.66
            //if (_ms.mGeneralOverTime < 2)
            //{
            //    _ms.mGeneralOverTimeFee = GetSiSheWuRu((_ms.mDailyPay / 8) * _ms.mGeneralOverTime * 1.33, 0);
            //}
            //else
            //{
            //    _ms.mGeneralOverTimeFee = GetSiSheWuRu((_ms.mDailyPay / 8) * 2 * 1.33, 0) + GetSiSheWuRu((_ms.mDailyPay / 8) * (_ms.mGeneralOverTime - 2) * 1.66, 0);
            //}
            _ms.mGeneralOverTimeFee = GetSiSheWuRu((_ms.mDailyPay / 8) * _ms.GeneralOverTimeCountSmall * 1.33, 0) + GetSiSheWuRu((_ms.mDailyPay / 8) * _ms.GeneralOverTimeCountBig * 1.66, 0);
            //假日加班 一律 为时薪 两倍.
            _ms.mHolidayOverTimeFee = GetSiSheWuRu(((_ms.mDailyPay / 8) * 2) * _ms.mHolidayOverTime, 0);
            #endregion
            #endregion
            #region 工作奖金,绩效奖金, 作业技术奖金 => 工作奖金=职务津贴＋责任津贴＋班别津贴+全勤奖
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
            //------------------------------ From计算方法 ----End----------------------------------------------------//

            #region //------------------------------ 原有方法---Start-----------------------------------------------------//
            //#region  取考勤记录

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
            //            //在职务津贴扣除符合条件假期
            //        }
            //        _ms.mNote = drms["Note"].ToString();
            //        if (!string.IsNullOrEmpty(_ms.mNote))
            //        {
            //            if (_ms.mNote != "周日休假" && _ms.mNote.IndexOf("公假") < 0 && _ms.mNote.IndexOf("婚假") < 0 && _ms.mNote.IndexOf("喪假") < 0 && _ms.mNote.IndexOf("年假") < 0 && _ms.mNote.IndexOf("出差") < 0 && _ms.mNote.IndexOf("遲到") < 0)
            //                _ms.mCount = _ms.mCount + 1;

            //        }

            //    }
            //}
            //ds.Clear();
            //ds.Dispose();
            //#endregion
            //#region 取薪资计算
            //DataSet dsms = this.monthlySalaryManager.getMonthlySummaryFee(emp.EmployeeId, _ms.mIdentifyDate, hryear, hrmonth);
            //if (dsms.Tables[0].Rows.Count > 0)
            //{
            //    DataRow dr = dsms.Tables[0].Rows[0];
            //    _ms.mLunchFee = mStrToDouble(dr["LunchFee"]);  // 午餐費
            //    _ms.mOverTimeFee = mStrToDouble(dr["OverTimeFee"]);// 加班費
            //    _ms.mGeneralOverTime = mStrToDouble(dr["GeneralOverTime"]); // 平日加班（時數）
            //    _ms.mHolidayOverTime = mStrToDouble(dr["HolidayOverTime"]); // 假日加班（時數）
            //    _ms.mLateCount = mStrToDouble(dr["LateCount"]); // 遲到次數
            //    _ms.mTotalLateInMinute = mStrToDouble(dr["TotalLateInMinute"]);            // 總遲到（分）
            //    _ms.mOverTimeBonus = mStrToDouble(dr["OverTimeBonus"]);                     // 加班津貼
            //    _ms.mSpecialBonus = mStrToDouble(dr["SpecialBonus"]);                       // 中夜班津貼
            //    _ms.mDaysFactor = mStrToDouble(dr["DaysFactor"]);                           // 總日基數
            //    _ms.mMonthFactor = mStrToDouble(dr["MonthFactor"]);                         // 總月基數
            //    _ms.mDutyDateCount = mStrToDouble(dr["DutyDateCount"]);                     // 總出勤記錄數
            //    _ms.mLeaveDate = (dr["LeaveDate"] == null || dr["LeaveDate"].ToString() == "") ? global::Helper.DateTimeParse.NullDate : Convert.ToDateTime(dr["LeaveDate"].ToString()); // 离职日期
            //    _ms.mPunishLeaveCount = mStrToDouble(dr["PunishLeaveCount"]);               // 倒扣款假總數
            //    _ms.mLeaveCount = mStrToDouble(dr["LeaveCount"]);                           // 請假總數
            //    _ms.mAbsentCount = mStrToDouble(dr["AbsentCount"]);                         // 曠工總數
            //    _ms.mTotalHoliday = mStrToDouble(dr["TotalHoliday"]);                       // 該月總例假數
            //    _ms.mLoanFee = mStrToDouble(dr["LoanFee"]);
            //    //  int WuXinCount = Int32.Parse(dr["WuXinCount"].ToString());
            //    //考勤 不满一月  日基数 =月基数-实际假数-扣款请假天数-旷职-无薪假     //矿工待处理  
            //    if (_ms.mDutyDateCount < totalDay)
            //        _ms.mDaysFactor = _ms.mDaysFactor - _ms.mTotalHoliday;
            //    //    if (_ms.mDutyDateCount < totalDay && _ms.mLeaveDate != global::Helper.DateTimeParse.NullDate && _ms.mLeaveDate.ToString() != "")             //總出勤記錄數
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
            //#region 底薪
            //DataSet dx_ds = this.monthlySalaryManager.getMonthlySalary(emp.EmployeeId, _ms.mIdentifyDate);
            //if (dx_ds.Tables[0].Rows.Count > 0)
            //{
            //    _ms.mMonthFactor = _ms.mMonthFactor;
            //    DataRow dx_dr = dx_ds.Tables[0].Rows[0];
            //    _ms.mDailyPay = mStrToDouble(dx_dr["DailyPay"]);                          //日工资
            //    _ms.mMonthlyPay = mStrToDouble(dx_dr["MonthlyPay"]);                     //月工资
            //    _ms.mDutyPay = this.GetSiSheWuRu(mStrToDouble(dx_dr["DutyPay"]) * _ms.mMonthFactor / totalDay, 0); //责任津贴
            //    _ms.mPostPay = this.GetSiSheWuRu(mStrToDouble(dx_dr["PostPay"]) * _ms.mMonthFactor / totalDay, 0); //職務津貼
            //    _ms.mInsurance = mStrToDouble(dx_dr["insurance"]);                                      //保险费
            //    _ms.mTax = mStrToDouble(dx_dr["Tax"]);                                                  //所得税
            //    _ms.mEffectFactor = mStrToDouble(dx_dr["EffectFactor"]);   //績效係數
            //    _ms.mOtherPay = mStrToDouble(dx_dr["OtherPay"]);           //其它補款
            //    _ms.mOtherPunish = mStrToDouble(dx_dr["OtherPunish"]);     //其它扣款
            //    _ms.mFieldPay = this.GetSiSheWuRu(mStrToDouble(dx_dr["FieldPay"]) * (totalDay - _ms.mCount) / totalDay, 0); //职场津贴
            //    _ms.mBasePay = _ms.mMonthlyPay * _ms.mMonthFactor / totalDay + _ms.mDailyPay * _ms.mDaysFactor;                //底薪
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
            //#region 迟到扣款
            //int mIcount = 0;  //和小于10分 次数
            //if (_ms.mTotalLateInMinute > 10 || _ms.mLateCount > 2)
            //{
            //    //临时记录迟到

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
            //    // '遲到超過三次，或總遲到超過10分鐘
            //    //'化成小時，以0.5小時為刻度
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
            //    mIcount = (int)_ms.mLateCount;// 10分钟内 次数 用于全勤奖
            //}
            //#endregion
            //#region 全勤奖
            //if (_ms.mDutyDateCount == totalDay)
            //{
            //    //有做滿一個月：日薪三天
            //    if (_ms.mMonthFactor == totalDay)
            //    {
            //        if (_ms.mPunishLeaveCount == 0)
            //        {
            //            _ms.mAllAttendBonus = _ms.mDailyPay * 3;
            //        }
            //        //請倒扣款假小於等於一天：日薪二天   
            //        else if (_ms.mPunishLeaveCount <= 1)
            //        {
            //            _ms.mAllAttendBonus = _ms.mDailyPay * 2;
            //        }
            //        //判斷遲到,扣除全勤獎金
            //        if (_ms.mLateCount - mIcount > 0)
            //        {
            //            _ms.mAllAttendBonus = _ms.mAllAttendBonus - _ms.mDailyPay * (_ms.mLateCount - mIcount) >= 0 ? _ms.mAllAttendBonus - _ms.mDailyPay * (_ms.mLateCount - mIcount) : 0;
            //        }
            //    }
            //    //有缺刷卡記錄                 
            //    else
            //    {
            //        _ms.mAllAttendBonus = 0;
            //    }
            //}
            ////未滿一個月（因到職或離職）
            //else
            //{
            //    _ms.mAllAttendBonus = 0;
            //}
            //#endregion
            //#region 離職未做滿一整個月者，取消：「責任津貼」、「職務津貼」、「職場津貼」、「週日休假」
            //if (_ms.mDutyDateCount < totalDay && _ms.mLeaveDate != global::Helper.DateTimeParse.NullDate)
            //{
            //    _ms.mDutyPay = 0;
            //    _ms.mPostPay = 0;
            //    _ms.mFieldPay = 0;
            //}
            //#endregion
            //#region 平日加班 假日加班 酬薪加班津贴 及税额
            //_ms.mGeneralOverTimeFee = GetSiSheWuRu((_ms.mDailyPay / 6) * _ms.mGeneralOverTime, 0);                   //平日加班费
            //_ms.mHolidayOverTimeFee = GetSiSheWuRu((((_ms.mDailyPay / 2) * 3) / 8) * _ms.mHolidayOverTime, 0);       //假日加班费


            //#endregion
            //#region 工作奖金,绩效奖金, 作业技术奖金 => 工作奖金=职务津贴＋责任津贴＋班别津贴+全勤奖
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
            #endregion------------------------------ 原有方法- End-------------------------------------------------------//

            #region 取考勤note (取消不用)
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
            //        //在职务津贴扣除符合条件假期
            //    }
            //    _ms.mNote = attend.Note;
            //    if (!string.IsNullOrEmpty(_ms.mNote))
            //    {
            //        if (_ms.mNote != "周日休假" && _ms.mNote.IndexOf("公假") < 0 && _ms.mNote.IndexOf("婚假") < 0 && _ms.mNote.IndexOf("喪假") < 0 && _ms.mNote.IndexOf("年假") < 0 && _ms.mNote.IndexOf("出差") < 0 && _ms.mNote.IndexOf("遲到") < 0)
            //            _ms.mCount = _ms.mCount + 1;

            //    }
            //}
            #endregion

        }

        //列印总月报表
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
                    //赋值
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
                        _drT["JiaBanDesc"] = "平=" + _ms.GeneralOverTimeCountSmall.ToString() + "H * 1.33 + " + _ms.GeneralOverTimeCountBig.ToString() + "H * 1.66,假=" + _ms.mHolidayOverTime + "H * 2";
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
        /// 四舍五入方法.
        /// </summary>
        /// <param name="objTarget">要操作的double类型数字</param>
        /// <param name="mIndex">欲保留小数位数</param>
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

        //标识是否是手动输入,true 手动输入 需要重新计算保存,flase 不是手动输入 不需要保存
        bool _IsKeyIn;

        //其它补款
        private void txtOtherPay_EditValueChanged(object sender, EventArgs e)
        {
            if (this.txtOtherPay.IsEditorActive)
                _IsKeyIn = true;
            else
                _IsKeyIn = false;
            this.mSaveToChange();
        }

        //其它扣款
        private void txtOtherPunish_EditValueChanged(object sender, EventArgs e)
        {
            if (this.txtOtherPunish.IsEditorActive)
                _IsKeyIn = true;
            else
                _IsKeyIn = false;
            this.mSaveToChange();
        }

        //绩效系数更改
        private void txtEffectFactor_EditValueChanged(object sender, EventArgs e)
        {
            if (this.txtEffectFactor.IsEditorActive)
                _IsKeyIn = true;
            else
                _IsKeyIn = false;
            this.mSaveToChange();
        }

        //年假(补休)天数更改
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
        /// 对象转Double
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


#region ===========注释备用==========
#region @ old Print
//DataRow dr;//内存表
//DataRow row; //monthlySalary薪资表
//DataRow row1;// 薪资统计表中HrAttendStat的行 只有1行
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
//        if (_hrAttendStat != null)   //考勤统计表
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

//        //日基数
//        dr["DaysFactor"] = row1["DaysFactor"] == null || row1["DaysFactor"].ToString() == "" ? 0 : decimal.Parse(row1["DaysFactor"].ToString());
//        //非离职出勤不到满月  排除例假数。请假数，旷职数

//        if (row1["DutyDateCount"] != null && row1["DutyDateCount"].ToString() != "" && decimal.Parse(row1["DutyDateCount"].ToString()) < DateTime.DaysInMonth(hryear, hrmonth))
//        {
//            dr["DaysFactor"] = decimal.Parse(row1["MonthFactor"].ToString()) - (row1["TotalHoliday"] == null || row1["TotalHoliday"].ToString() == "" ? 0 : decimal.Parse(row1["TotalHoliday"].ToString())) - (row1["LeaveCount"] == null || row1["LeaveCount"].ToString() == "" ? 0 : decimal.Parse(row1["LeaveCount"].ToString())) - (row1["AbsentCount"] == null || row1["AbsentCount"].ToString() == "" ? 0 : decimal.Parse(row1["AbsentCount"].ToString()));
//        }

//        //月基数
//        dr["MonthFactor"] = row1["MonthFactor"] == null || row1["MonthFactor"].ToString() == "" ? 0 : decimal.Parse(row1["MonthFactor"].ToString());

//        #region 底薪
//        // 倒扣款例假数


//        PunishLeaveCount = 0;//倒扣款例假總數

//        //小於兩天不扣
//        if (row1["PunishLeaveCount"] != null && row1["PunishLeaveCount"].ToString() != "" && decimal.Parse(row1["PunishLeaveCount"].ToString()) < 2)
//            PunishLeaveCount = 0;
//        //2-2.5天扣1個例假日，3-3.5扣2個例假日，4-4.5天扣3個例假日，5-5.5天扣4個例假日
//        else
//        {

//            row1["TotalHoliday"] = row1["TotalHoliday"] == null || row1["TotalHoliday"].ToString() == "" ? 0 : row1["TotalHoliday"];

//            if (row1["PunishLeaveCount"] != null && row1["PunishLeaveCount"].ToString() != "" && decimal.Parse(row1["PunishLeaveCount"].ToString()) - decimal.Parse("1.05") >= decimal.Parse(row1["TotalHoliday"].ToString()))
//                PunishLeaveCount = decimal.Parse(row1["TotalHoliday"].ToString());
//            else
//                PunishLeaveCount = decimal.Parse(row1["TotalHoliday"].ToString()) - decimal.Parse("1.05");
//        }
//        dr["DaysFactor"] = decimal.Parse(dr["DaysFactor"].ToString()) - PunishLeaveCount;
//        //底薪=月薪/当前月天数*月基数 + 日薪*（日基数-旷职日数-扣例假数）
//        dr["BasePay"] = employees.MonthlyPay.Value / DateTime.DaysInMonth(hryear, hrmonth) * decimal.Parse(dr["MonthFactor"].ToString()) + employees.DailyPay.Value * (decimal.Parse(dr["DaysFactor"].ToString()));

//        #endregion
//        dr["GivenDays"] = row["HolidayBonusGivenDays"] == null || row["HolidayBonusGivenDays"].ToString() == "" ? 0 : decimal.Parse(row["HolidayBonusGivenDays"].ToString());
//        dr["OtherPay"] = row["OtherPay"] == null || row["OtherPay"].ToString() == "" ? 0 : decimal.Parse(row["OtherPay"].ToString());
//        dr["OtherPunish"] = row["OtherPunish"] == null || row["OtherPunish"].ToString() == "" ? 0 : decimal.Parse(row["OtherPunish"].ToString());

//        dr["PostPay"] = employees.PostPay == null ? 0 : employees.PostPay.Value;
//        dr["DutyPay"] = employees.DutyPay == null ? 0 : employees.DutyPay.Value;
//        #region/职场津贴





//        // decimal SpecificHoliday = this.leavemanage.SelectSpecificHolidayMonthEmp(employees, hryear, hrmonth);
//        //HFieldPay = (employees.FieldPay.Value / DateTime.DaysInMonth(hryear, hrmonth) * (dayfactorSum - SpecificHoliday))==0?0:(employees.FieldPay.Value / DateTime.DaysInMonth(hryear, hrmonth) * (dayfactorSum - SpecificHoliday));
//        //如果旷职 扣除周末 职场津贴
//        if (row1["AbsentCount"] != null && row1["AbsentCount"].ToString() != "" && decimal.Parse(row1["AbsentCount"].ToString()) > 0)
//        {
//            dr["FieldPay"] = employees.FieldPay.Value;// / DateTime.DaysInMonth(hryear, hrmonth) * (decimal.Parse(row1["DaysFactor"].ToString()) - this.leavemanage.SelectSpecificHolidayMonthEmp(employees, hryear, hrmonth) - this.leavemanage.SelectTotalHolidayMonthEmp(employees, hryear, hrmonth));
//        }
//        else
//        {
//            dr["FieldPay"] = employees.FieldPay.Value;
//        }
//        #endregion




//        #region    全勤奖
//        // decimal AllAttendBonus = decimal.Zero;

//        if (row1["DutyDateCount"] != null && row1["DutyDateCount"].ToString() != "" && decimal.Parse(row1["DutyDateCount"].ToString()) == DateTime.DaysInMonth(hryear, hrmonth))
//        {

//            //有做滿一個月：日薪三天
//            if (decimal.Parse(dr["MonthFactor"].ToString()) == DateTime.DaysInMonth(hryear, hrmonth))
//            {
//                if (row1["PunishLeaveCount"] != null && row1["PunishLeaveCount"].ToString() != "" && decimal.Parse(row1["PunishLeaveCount"].ToString()) == 0)
//                {
//                    dr["AllAttendBonus"] = employees.DailyPay.Value * 3;
//                }
//                //請倒扣款假小於等於一天：日薪二天   
//                else if (row1["PunishLeaveCount"] != null && row1["PunishLeaveCount"].ToString() != "" && decimal.Parse(row1["PunishLeaveCount"].ToString()) <= 1)
//                {
//                    dr["AllAttendBonus"] = employees.DailyPay.Value * 2;
//                }
//                else
//                    dr["AllAttendBonus"] = 0;

//            }

//             //有缺刷卡記錄                 
//            else
//            {
//                dr["AllAttendBonus"] = 0;
//            }
//        }
//        //未滿一個月（因到職或離職）
//        else
//        {
//            dr["AllAttendBonus"] = 0;
//        }
//        #endregion
//        // dr["WorkBonus"] = 1000;
//        dr["EffectBonus"] = 1000;


//        #region //加班
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
//        #region 判断为空
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
//#region @初始化
////monthsum = 0;

//#endregion
//#region @底薪
//////总出勤数量
////DutyDateCount = this._hrManager.SelectDutyDateCount(hryear, hrmonth, this.employee);
//////日基数
////dayfactorSum = this._hrManager.SelectDayFactorSum(hryear, hrmonth, this.employee);
////monthFactorSum = this._hrManager.SelectDayMonthSum(hryear, hrmonth, this.employee);

////// 倒扣款例假数

////decimal PunishCount = this.leavemanage.SelectPunishByMonthEmp(this.employee, hryear, hrmonth);// 倒扣款假總數
////decimal TotalHoliday = this.leavemanage.SelectTotalHolidayMonthEmp(this.employee, hryear, hrmonth);//出勤记录月份假日和
////decimal PunishLeaveCount = PunishCount;//倒扣款例假總數


////#region 小於兩天不扣
//////if (PunishLeaveCount < 2)
//////    PunishLeaveCount = 0;
////////2-2.5天扣1個例假日，3-3.5扣2個例假日，4-4.5天扣3個例假日，5-5.5天扣4個例假日
//////else
//////{
//////    if (PunishLeaveCount - decimal.Parse("1.05") >= TotalHoliday)
//////        PunishLeaveCount = TotalHoliday;
//////    else
//////        PunishLeaveCount = TotalHoliday - decimal.Parse("1.05");
//////}
////#endregion
//////底薪=月薪/当前月天数*月基数 + 日薪*（日基数-旷职日数-扣例假数）
////this.employee.MonthlyPay = this.employee.MonthlyPay == null ? 0 : this.employee.MonthlyPay;
////this.employee.DailyPay = this.employee.DailyPay == null ? 0 : this.employee.DailyPay;

////monthsum = this.employee.MonthlyPay.Value / DateTime.DaysInMonth(hryear, hrmonth) * this.monthFactorSum + this.employee.DailyPay.Value * (this.dayfactorSum - PunishLeaveCount);


//#endregion
//#region @加班津贴

//#endregion
//#region        @班别津贴

////decimal BusinessHourPay = decimal.Zero;
////BusinessHourPay = this._hrManager.SelectBusinessHourPaySum(hryear, hrmonth, this.employee.EmployeeId);
//#endregion
//#region @平日加班 假日加班 酬薪加班津贴 及税额

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
//#region @迟到扣款
////if (this._hrManager.SelectLateSum(hryear, hrmonth, this.employee) > 10 || this._hrManager.SelectLateCount(hryear, hrmonth, this.employee) > 3)
////{
////    LatePunish = 0;
////    //临时记录迟到
////    int mCount = 0;  //和小于10分 次数
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
////    // '遲到超過三次，或總遲到超過10分鐘
////    //'化成小時，以0.5小時為刻度
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
//#region    @全勤奖
////decimal AllAttendBonus = decimal.Zero;

////if (DutyDateCount == DateTime.DaysInMonth(hryear, hrmonth))
////{

////    //有做滿一個月：日薪三天
////    if (monthFactorSum == DateTime.DaysInMonth(hryear, hrmonth))
////    {
////        if (PunishCount == 0)
////        {
////            AllAttendBonus = this.employee.DailyPay.Value * 3;
////        }
////        //請倒扣款假小於等於一天：日薪二天   
////        else if (PunishCount <= 1)
////        {
////            AllAttendBonus = this.employee.DailyPay.Value * 2;
////        }
////    }

////     //有缺刷卡記錄                 
////    else
////    {
////        AllAttendBonus = 0;
////    }
////}
//////未滿一個月（因到職或離職）
////else
////{
////    AllAttendBonus = 0;
////}



//#endregion
//#region @工作奖金,绩效奖金, 作业技术奖金
//////工作奖金=职务津贴＋责任津贴＋班别津贴+全勤奖
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
//#region @职场津贴
////decimal HFieldPay = decimal.Zero;
////decimal SpecificHoliday = this.leavemanage.SelectSpecificHolidayMonthEmp(this.employee, hryear, hrmonth);
////this.employee.FieldPay = this.employee.FieldPay == null ? 0 : this.employee.FieldPay;
////HFieldPay = this.employee.FieldPay.Value / DateTime.DaysInMonth(hryear, hrmonth) * (dayfactorSum - SpecificHoliday);


//////如果旷职 扣除周末 职场津贴
////if (this._hrManager.SelectAbsentCount(hryear, hrmonth, this.employee) > 0)
////{
////    HFieldPay = this.employee.FieldPay.Value / DateTime.DaysInMonth(hryear, hrmonth) * (dayfactorSum - this.leavemanage.SelectSpecificHolidayMonthEmp(this.employee, hryear, hrmonth) - this.leavemanage.SelectTotalHolidayMonthEmp(this.employee, hryear, hrmonth));
////}
//#endregion
//#region @出勤未做滿一整個月者，取消：  「責任津貼」、「職務津貼」、「職場津貼」、「週日休假」
////if (this._hrManager.SelectDutyDateCount(hryear, hrmonth, this.employee) < DateTime.DaysInMonth(hryear, hrmonth))
////{
////    this.employee.DutyPay = 0;
////    this.employee.PostPay = 0;   //职务津贴
////    HFieldPay = 0; //职场津贴
////}
//#endregion
//#region @绩效奖金
////绩效奖金 = 全勤奖金 
////jxbonus = Punishpay;

////label_jxjj.Text = jxbonus.ToString();
//#endregion
//#region @作业技术奖金
////Model.Department dep = (new BL.DepartmentManager()).Get(employee.DepartmentId);
////if (dep.DepartmentName.Contains("射出"))
////{
////    zyjsbonus = (this.employee.DailyPay.Value) * 3;
////}

////label_zyjj.Text = "0";// zyjsbonus.ToString();
//#endregion
//#region  @绩效系数补款扣款
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