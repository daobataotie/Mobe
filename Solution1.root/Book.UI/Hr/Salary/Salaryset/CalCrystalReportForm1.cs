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
    public partial class CalCrystalReportForm1 : DevExpress.XtraEditors.XtraForm
    {
        Model.HrAttendStat _hrAttendStat;
        private Model.MonthlySalary _monthlySalary = new Book.Model.MonthlySalary();
        protected BL.EmployeeManager employeeManager = new Book.BL.EmployeeManager();
        BL.MonthlySalaryManager monthlySalaryManager = new Book.BL.MonthlySalaryManager();
        private BL.HrAttendStatManager hrAttendManager = new Book.BL.HrAttendStatManager();
        private BL.OverTimeManager OverTimeManger = new BL.OverTimeManager();
        private BL.HrDailyEmployeeAttendInfoManager _hrManager = new Book.BL.HrDailyEmployeeAttendInfoManager();

        private DataSet1.MonthlySalarysDataTable table = new DataSet1.MonthlySalarysDataTable();

        public CalCrystalReportForm1()
        {
            InitializeComponent();
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {
            int hryear = DateTime.Now.Date.AddMonths(-1).Date.Year;
            int hrmonth = DateTime.Now.Date.AddMonths(-1).Date.Month;
            CreateDataSet();
            //CrystalReport1 cr = new CrystalReport1();
            CrystalReport3 cr = new CrystalReport3();
            DataTable tables = table;
            cr.SetDataSource(tables);
            cr.SetParameterValue("BoundTitle", hryear.ToString() + "年" + hrmonth.ToString() + "月獎金明細表");
            cr.SetParameterValue("SalaryTitle", hryear.ToString() + "年" + hrmonth.ToString() + "月薪資單");

            this.crystalReportViewer1.ReportSource = cr;
        }

        private void CreateDataSet()
        {
            int hryear = DateTime.Now.Date.AddMonths(-1).Date.Year;
            int hrmonth = DateTime.Now.Date.AddMonths(-1).Date.Month;
            Model.MonthlySalary monthlySalarys = new Book.Model.MonthlySalary();

            DataRow _drT;
            Model.Employee emp = new Book.Model.Employee();
            DataSet dx_ds = this.monthlySalaryManager.GetMonthlySummaryByMonth(hryear, hrmonth);
            if (dx_ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < dx_ds.Tables[0].Rows.Count; i++)
                {
                    emp = this.employeeManager.Get(dx_ds.Tables[0].Rows[i]["EmployeeId"].ToString());
                    MonthSalaryClass _ms = this.Calulation(emp);
                    //赋值
                    _drT = table.NewRow();
                    _drT["EmployeeId"] = emp.EmployeeId;
                    _drT["DailyPay"] = _ms.mDailyPay;
                    _drT["IDNo"] = emp.IDNo;
                    _drT["DepartmentName"] = emp.Department == null ? "" : emp.Department.DepartmentName;
                    _drT["CompanyName"] = emp.Company == null ? "" : emp.Company.CompanyName;
                    _drT["MonthlyPay"] = _ms.mMonthlyPay;
                    _drT["BasePay"] = _ms.mBasePay;
                    _drT["FieldPay"] = _ms.mFieldPay;
                    _drT["SubTotal"] = this.GetSiSheWuRu(_ms.SubTotal, 0);
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
                    _drT["HolidayOverTime"] = _ms.mHolidayOverTime;
                    _drT["GeneralOverTimeFee"] = _ms.mGeneralOverTimeFee;
                    _drT["HolidayOverTimeFee"] = _ms.mHolidayOverTimeFee;
                    _drT["OverTimeFee"] = _ms.mOverTimeFee;
                    _drT["OverTimeBonus"] = _ms.mOverTimeBonus;
                    _drT["GivenDays"] = _ms.mGivenDays;
                    _drT["AnnualHolidayFee"] = _ms.mAnnualHolidayFee;
                    _drT["OtherPay"] = _ms.mOtherPay;
                    _drT["OtherPunish"] = _ms.mOtherPunish;
                    _drT["BonusTotal"] = _ms.BonusTotal;
                    _drT["ShouldPay"] = _ms.mShouldPay;
                    _drT["LatePunish"] = _ms.mLatePunish;
                    _drT["LateCount"] = _ms.mLateCount;
                    _drT["TotalLateInMinute"] = _ms.mTotalLateInMinute;
                    _drT["TotalLateInHour"] = _ms.mTotalLateInHour;
                    _drT["PunishCount"] = _ms.mPunishCount;
                    _drT["EmployeeName"] = emp.EmployeeName;
                    _drT["MonthFactor"] = _ms.mMonthFactor;
                    _drT["DaysFactor"] = _ms.mDaysFactor;
                    _drT["BusinessHoursName"] = emp.BusinessHours == null ? "" : emp.BusinessHours.BusinessHoursName;
                    _drT["XiaoJI"] = _ms.XiaoJI;
                    _drT["JiaBan"] = GetSiSheWuRu(_ms.JiaBan, 0);
                    if (Convert.ToInt16(_drT["JiaBan"]) == 0)
                        _drT["JiaBanDesc"] = string.Empty;
                    else
                        //_drT["JiaBanDesc"] = _ms.OverTimeCountSmall.ToString() + "H * 1.33 + " + _ms.OverTimeCountBig.ToString() + "H * 1.66";
                        _drT["JiaBanDesc"] = "平=" + _ms.GeneralOverTimeCountSmall.ToString() + "H * 1.33 + " + _ms.GeneralOverTimeCountBig.ToString() + "H * 1.66,假=" + _ms.mHolidayOverTime + "H * 2";
                    table.Rows.Add(_drT);
                }
            }
        }

        /// <summary>
        /// 计算取值
        /// </summary>
        /// <param name="emp"></param>
        private MonthSalaryClass Calulation(Model.Employee emp)
        {
            int mTemp = 0;
            int mHicount = 0;
            double mLateHalfCount = 0;
            StringBuilder strBU = new StringBuilder();
            int hryear = DateTime.Now.Date.AddMonths(-1).Date.Year;
            int hrmonth = DateTime.Now.Date.AddMonths(-1).Date.Month;
            //int hryear = 2011;// DateTime.Now.Year;
            //int hrmonth = 12;// DateTime.Now.Month - 1;
            int totalDay = DateTime.DaysInMonth(hryear, hrmonth);
            //////////////////////////////////////////////////////////////////
            MonthSalaryClass _ms = new MonthSalaryClass();
            _ms.mIdentifyDate = new DateTime(hryear, hrmonth, 1);
            //_ms.mEmployeeId = emp.EmployeeId;
            //_ms.mEmployeeName = emp.EmployeeName;
            //_ms.mIDNo = emp.IDNo;
            //_ms.mDepartmetName = emp.Department.DepartmentName;

            //------------------------------ From计算方法 ----Start----------------------------------------------------//
            #region  取考勤记录
            //DataSet ds = this.monthlySalaryManager.getAttendInfoList(emp.EmployeeId, hryear, hrmonth);                   
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
                }
            }
            #endregion
            #region 取薪资计算
            DataSet dsms = this.monthlySalaryManager.getMonthlySummaryFee(emp.EmployeeId, _ms.mIdentifyDate, hryear, hrmonth);
            if (dsms.Tables[0].Rows.Count > 0)
            {
                DataRow dr = dsms.Tables[0].Rows[0];
                _ms.mLunchFee = mStrToDouble(dr["LunchFee"]);                                //午餐費
                _ms.mOverTimeFee = mStrToDouble(dr["OverTimeFee"]);                          //加班費
                _ms.mGeneralOverTime = mStrToDouble(dr["GeneralOverTime"]);                  //平日加班（時數）
                _ms.mHolidayOverTime = mStrToDouble(dr["HolidayOverTime"]);                  //假日加班（時數）
                _ms.GeneralOverTimeCountBig = mStrToDouble(dr["GeneralOverTimeCountBig"]);    //平日加班2小时之外(時數)
                _ms.GeneralOverTimeCountSmall = mStrToDouble(dr["GeneralOverTimeCountSmall"]);//平日加班2小时以下(時數)
                _ms.HolidayOverTimeCountBig = mStrToDouble(dr["HolidayOverTimeCountBig"]);    //假日加班2小时之外(時數)
                _ms.HolidayOverTimeCountSmall = mStrToDouble(dr["HolidayOverTimeCountSmall"]);//假日加班2小时以下(時數)
                _ms.mLateCount = mStrToDouble(dr["LateCount"]);                              //遲到次數
                _ms.mTotalLateInMinute = mStrToDouble(dr["TotalLateInMinute"]);              //總遲到（分）
                _ms.mOverTimeBonus = mStrToDouble(dr["OverTimeBonus"]);                      //加班津貼
                _ms.mSpecialBonus = mStrToDouble(dr["SpecialBonus"]);                        //中夜班津貼
                _ms.mDaysFactor = mStrToDouble(dr["DaysFactor"]);                            //總日基數
                _ms.mMonthFactor = mStrToDouble(dr["MonthFactor"]);                          //總月基數
                _ms.mDutyDateCount = mStrToDouble(dr["DutyDateCount"]);                      //總出勤記錄數
                _ms.mLeaveDate = (dr["LeaveDate"] == null || dr["LeaveDate"].ToString() == "") ? global::Helper.DateTimeParse.NullDate : Convert.ToDateTime(dr["LeaveDate"].ToString());                                                  //离职日期
                _ms.mPunishLeaveCount = mStrToDouble(dr["PunishLeaveCount"]);                //倒扣款假總數
                _ms.mLeaveCount = mStrToDouble(dr["LeaveCount"]);                            //請假總數
                _ms.mAbsentCount = mStrToDouble(dr["AbsentCount"]);                          //曠工總數
                _ms.mTotalHoliday = mStrToDouble(dr["TotalHoliday"]);                        //該月總例假數
                _ms.mLoanFee = mStrToDouble(dr["LoanFee"]);                                  //借支
                // int WuXinCount = Int32.Parse(dr["WuXinCount"].ToString());
                //考勤 不满一月  日基数 =月基数-实际假数-扣款请假天数-旷职-无薪假         //矿工待处理  
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

            #region //------------------------------备注-----------------------------------------------------------//
            //#region  取考勤记录
            //// DataSet ds = this.monthlySalaryManager.getAttendInfoList(employee.EmployeeId, hryear, hrmonth);
            ////foreach (Model.HrDailyEmployeeAttendInfo attend in this._hrManager.SelectByEmpMonth(emp, hryear, hrmonth))
            ////{
            ////    if (attend.LateInMinute.HasValue && attend.LateInMinute.Value != 0)
            ////    {

            ////        strBU.Append(attend.LateInMinute.ToString() + "|");
            ////        mTemp = mTemp + attend.LateInMinute.Value;
            ////        //if (mTemp <= 10)
            ////        //{
            ////        //    mCount = mCount + 1;
            ////        //}
            ////        if (attend.LateInMinute.Value > 30)
            ////        {
            ////            mHicount = mHicount + 1;
            ////            if ((attend.LateInMinute.Value + 30) % 60 > 30)
            ////            {
            ////                mLateHalfCount = mLateHalfCount + (attend.LateInMinute.Value + 30) / 60 + 0.5;
            ////            }
            ////            else
            ////            {
            ////                mLateHalfCount = mLateHalfCount + (attend.LateInMinute.Value + 30) / 60;
            ////            }
            ////        }

            ////        //在职务津贴扣除符合条件假期

            ////    }
            ////    _ms.mNote = attend.Note;
            ////    if (!string.IsNullOrEmpty(_ms.mNote))
            ////    {
            ////        if (_ms.mNote != "周日休假" && _ms.mNote.IndexOf("公假") < 0 && _ms.mNote.IndexOf("婚假") < 0 && _ms.mNote.IndexOf("喪假") < 0 && _ms.mNote.IndexOf("年假") < 0 && _ms.mNote.IndexOf("出差") < 0 && _ms.mNote.IndexOf("遲到") < 0)
            ////            _ms.mCount = _ms.mCount + 1;

            ////    }
            ////}
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
            //    _ms.mLunchFee = double.Parse(dr["LunchFee"].ToString());                                 // 午餐費
            //    _ms.mOverTimeFee = double.Parse(dr["OverTimeFee"].ToString());                           // 加班費
            //    _ms.mGeneralOverTime = double.Parse(dr["GeneralOverTime"].ToString());                   // 平日加班（時數）
            //    _ms.mHolidayOverTime = double.Parse(dr["HolidayOverTime"].ToString());                   // 假日加班（時數）
            //    _ms.mLateCount = double.Parse(dr["LateCount"].ToString());                               // 遲到次數
            //    _ms.mTotalLateInMinute = double.Parse(dr["TotalLateInMinute"].ToString());               // 總遲到（分）
            //    _ms.mOverTimeBonus = double.Parse(dr["OverTimeBonus"].ToString());                       // 加班津貼
            //    _ms.mSpecialBonus = double.Parse(dr["SpecialBonus"].ToString());                         // 中夜班津貼
            //    _ms.mDaysFactor = double.Parse(dr["DaysFactor"].ToString());                             // 總日基數
            //    _ms.mMonthFactor = double.Parse(dr["MonthFactor"].ToString());                           // 總月基數
            //    _ms.mDutyDateCount = double.Parse(dr["DutyDateCount"].ToString());                       // 總出勤記錄數
            //    _ms.mLeaveDate = (dr["LeaveDate"] == null || dr["LeaveDate"].ToString() == "") ? global::Helper.DateTimeParse.NullDate : Convert.ToDateTime(dr["LeaveDate"].ToString()); // 离职日期
            //    _ms.mPunishLeaveCount = double.Parse(dr["PunishLeaveCount"].ToString());                 // 倒扣款假總數
            //    _ms.mLeaveCount = double.Parse(dr["LeaveCount"].ToString());                             // 請假總數
            //    _ms.mAbsentCount = double.Parse(dr["AbsentCount"].ToString());                           // 曠工總數
            //    _ms.mTotalHoliday = double.Parse(dr["TotalHoliday"].ToString());                         // 該月總例假數
            //    _ms.mLoanFee = double.Parse(dr["LoanFee"].ToString());
            //    // int WuXinCount = Int32.Parse(dr["WuXinCount"].ToString());
            //    //考勤 不满一月  日基数 =月基数-实际假数-扣款请假天数-旷职-无薪假     //矿工待处理  
            //    if (_ms.mDutyDateCount < totalDay)
            //        _ms.mDaysFactor = _ms.mDaysFactor - _ms.mTotalHoliday;
            //    //if (_ms.mDutyDateCount < totalDay && _ms.mLeaveDate != global::Helper.DateTimeParse.NullDate && _ms.mLeaveDate.ToString() != "")             //總出勤記錄數
            //    //    _ms.mDaysFactor = _ms.mMonthFactor - _ms.mTotalHoliday - _ms.mPunishLeaveCount - _ms.mAbsentCount - WuXinCount;
            //    //else if ((_ms.mLeaveDate == global::Helper.DateTimeParse.NullDate || _ms.mLeaveDate.ToString() == "") && _ms.mDutyDateCount < totalDay)
            //    //    _ms.mDaysFactor = _ms.mMonthFactor - _ms.mTotalHoliday - _ms.mPunishLeaveCount - _ms.mAbsentCount - WuXinCount;


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

            //#endregion
            //#region 底薪
            //DataSet dx_ds = this.monthlySalaryManager.getMonthlySalary(emp.EmployeeId, _ms.mIdentifyDate);
            //if (dx_ds.Tables[0].Rows.Count > 0)
            //{
            //    _ms.mMonthFactor = _ms.mMonthFactor;
            //    DataRow dx_dr = dx_ds.Tables[0].Rows[0];
            //    _ms.mDailyPay = double.Parse(dx_dr["DailyPay"].ToString());                                         //日工资
            //    _ms.mMonthlyPay = double.Parse(dx_dr["MonthlyPay"].ToString());                                     //月工资
            //    _ms.mDutyPay = double.Parse(dx_dr["DutyPay"].ToString()) * _ms.mMonthFactor / totalDay;                        //责任津贴
            //    _ms.mPostPay = double.Parse(dx_dr["PostPay"].ToString()) * _ms.mMonthFactor / totalDay;                        //職務津貼
            //    _ms.mInsurance = double.Parse(dx_dr["insurance"].ToString());                                       //保险费
            //    _ms.mTax = double.Parse(dx_dr["Tax"].ToString());                                                   //所得税
            //    _ms.mEffectFactor = double.Parse(dx_dr["EffectFactor"] == null || dx_dr["EffectFactor"].ToString() == "" ? "0" : dx_dr["EffectFactor"].ToString());                                 //績效係數
            //    _ms.mOtherPay = double.Parse(dx_dr["OtherPay"] == null || dx_dr["OtherPay"].ToString() == "" ? "0" : dx_dr["OtherPay"].ToString());                                         //其它補款
            //    _ms.mOtherPunish = double.Parse(dx_dr["OtherPunish"] == null || dx_dr["OtherPunish"].ToString() == "" ? "0" : dx_dr["OtherPunish"].ToString());                                   //其它扣款
            //    _ms.mFieldPay = double.Parse(dx_dr["FieldPay"] == null || dx_dr["FieldPay"].ToString() == "" ? "0" : dx_dr["FieldPay"].ToString()) * (totalDay - _ms.mCount) / totalDay;    //职场津贴
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

            //#endregion
            #endregion //------------------------------备注-----------------------------------------------------------//
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
}

#region =========注释备用=======
//     DataRow dr;//内存表
//     DataRow row; //monthlySalary薪资表
//     DataRow row1;// 薪资统计表中的行 只有1行
//    DataSet  dataset = this.monthlySalaryManager.GetMonthlySummaryByMonth(hryear, hrmonth);

//    Model.Employee employees;

//    decimal PunishLeaveCount = 0;

//    if (dataset.Tables[0].Rows.Count>0)
//    {
//        for(int j=0;j<dataset.Tables[0].Rows.Count;j++)
//        {
//            row = dataset.Tables[0].Rows[j];
//            employees = this.employeeManager.Get(row["EmployeeId"].ToString());

//            _hrAttendStat = this.hrAttendManager.SelectHrAttendStatByEmpidAndYearMonth(employees, hryear, hrmonth);//考勤统计表
//            row1 = this.monthlySalaryManager.GetMonthlySummaryFee(employees.EmployeeId, hryear, hrmonth).Tables[0].Rows[0] ;

//         //   monthFactor = this._hrManager.SelectDayMonthSum(hryear, hrmonth, employees);


//            dr = table.NewRow();
//            if (_hrAttendStat != null)   //考勤统计表
//            {
//                dr["LoanFee"] = this._hrAttendStat.LoanFee == null ? 0 : this._hrAttendStat.LoanFee.Value;

//                dr["LunchFee"] = this._hrAttendStat.LunchFee == null ? 0 : this._hrAttendStat.LunchFee.Value;

//                dr["OverTimeFee"] = this._hrAttendStat.OverTimeFee == null ? 0 : this._hrAttendStat.OverTimeFee.Value;
//                //row1["OverTimeFee"] == null || row1["OverTimeFee"].ToString() == "" ? 0 : decimal.Parse(row1["OverTimeFee"].ToString());
//                dr["OverTimeBonus"] = this._hrAttendStat.OverTimeBonus == null ? 0 : this._hrAttendStat.OverTimeBonus.Value;//row1["OverTimeBonus"] == null || row1["OverTimeBonus"].ToString() == "" ? 0 : decimal.Parse(row1["OverTimeBonus"].ToString()); 
//                dr["SpecialBonus"] = this._hrAttendStat.SpecialBonus == null ? 0 : this._hrAttendStat.SpecialBonus.Value; //row1["SpecialBonus"] == null || row1["SpecialBonus"].ToString() == "" ? 0 : decimal.Parse(row1["SpecialBonus"].ToString());
//                row1["DutyDateCount"] = this._hrAttendStat.DutyDateCount == null ? 0 : this._hrAttendStat.DutyDateCount.Value;
//                row1["TotalHoliday"] = this._hrAttendStat.TotalHoliday == null ? 0 : this._hrAttendStat.TotalHoliday.Value;
//                row1["AbsentCount"] = this._hrAttendStat.AbsentCount == null ? 0 : this._hrAttendStat.AbsentCount.Value;
//            }

//            dr["BoundTitle"] = hryear + "年" + hrmonth + "月獎金明細表";
//            dr["SalaryTitle"] = hryear + "年" + hrmonth + "月薪資單";
//        dr["CompanyName"] = BL.Settings.CompanyChineseName;
//        dr["EmployeeName"] = employees.EmployeeName;
//        dr["IDNo"] = employees.IDNo;       

//        dr["EffectFactor"] =row["EffectFactor"] == null || row["EffectFactor"].ToString() == ""?0:decimal.Parse(row["EffectFactor"].ToString());

//        //日基数
//        dr["DaysFactor"] = row1["DaysFactor"] == null || row1["DaysFactor"].ToString() == "" ? "0" : decimal.Parse(row1["DaysFactor"].ToString()).ToString("0.#");
//        //非离职出勤不到满月  排除例假数。请假数，旷职数

//        if (row1["DutyDateCount"] != null && row1["DutyDateCount"].ToString() != "" && decimal.Parse(row1["DutyDateCount"].ToString()) < DateTime.DaysInMonth(hryear, hrmonth))
//        {
//            dr["DaysFactor"] = decimal.Parse(row1["MonthFactor"].ToString()) - (row1["TotalHoliday"] == null || row1["TotalHoliday"].ToString() == "" ? 0 : decimal.Parse(row1["TotalHoliday"].ToString())) - (row1["LeaveCount"] == null || row1["LeaveCount"].ToString() == "" ? 0 : decimal.Parse(row1["LeaveCount"].ToString())) - (row1["AbsentCount"] == null || row1["AbsentCount"].ToString() == "" ? 0 : decimal.Parse(row1["AbsentCount"].ToString()));
//        }

//        //月基数
//        dr["MonthFactor"] = row1["MonthFactor"] == null || row1["MonthFactor"].ToString() == "" ? 0 : decimal.Parse(row1["MonthFactor"].ToString());

//        #region 底薪
//        // 倒扣款例假数


//         PunishLeaveCount = 0;//倒扣款例假總數

//        //小於兩天不扣
//        if (row1["PunishLeaveCount"] != null && row1["PunishLeaveCount"].ToString() != "" && decimal.Parse(row1["PunishLeaveCount"].ToString()) < 2)
//            PunishLeaveCount = 0;
//        //2-2.5天扣1個例假日，3-3.5扣2個例假日，4-4.5天扣3個例假日，5-5.5天扣4個例假日
//        else
//        {

//            row1["TotalHoliday"] = row1["TotalHoliday"] == null || row1["TotalHoliday"].ToString() == "" ? 0 : row1["TotalHoliday"];

//            if (row1["PunishLeaveCount"] != null && row1["PunishLeaveCount"].ToString() != "" && decimal.Parse(row1["PunishLeaveCount"].ToString()) - decimal.Parse("1.05") >=    decimal.Parse( row1["TotalHoliday"].ToString()))
//                PunishLeaveCount = decimal.Parse(row1["TotalHoliday"].ToString());
//            else
//                PunishLeaveCount = decimal.Parse(row1["TotalHoliday"].ToString()) - decimal.Parse("1.05");
//        }
//        dr["DaysFactor"] = decimal.Parse(dr["DaysFactor"].ToString()) - PunishLeaveCount;
//        //底薪=月薪/当前月天数*月基数 + 日薪*（日基数-旷职日数-扣例假数）
//        dr["BasePay"] = employees.MonthlyPay.Value / DateTime.DaysInMonth(hryear, hrmonth) * decimal.Parse(dr["MonthFactor"].ToString()) + employees.DailyPay.Value * (decimal.Parse(dr["DaysFactor"].ToString()));

//        #endregion
//        dr["GivenDays"] = row["HolidayBonusGivenDays"] == null||row["HolidayBonusGivenDays"].ToString()=="" ? 0 : decimal.Parse( row["HolidayBonusGivenDays"].ToString());
//        dr["OtherPay"] = row["OtherPay"] == null || row["OtherPay"].ToString() == "" ? 0 :decimal.Parse( row["OtherPay"].ToString());
//        dr["OtherPunish"] = row["OtherPunish"] == null || row["OtherPunish"].ToString() == "" ? 0 : decimal.Parse(row["OtherPunish"].ToString());

//        dr["PostPay"] = employees.PostPay == null ? 0 : employees.PostPay.Value;
//        dr["DutyPay"] = employees.DutyPay == null ? 0 : employees.DutyPay.Value;
//        dr["DailyPay"] = employees.DailyPay == null ? 0 : employees.DailyPay.Value;
//        dr["MonthlyPay"] = employees.MonthlyPay == null ? 0 : employees.MonthlyPay.Value;
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
//       // decimal AllAttendBonus = decimal.Zero;

//        if (row1["DutyDateCount"] != null && row1["DutyDateCount"].ToString() != "" && decimal.Parse(row1["DutyDateCount"].ToString()) == DateTime.DaysInMonth(hryear, hrmonth))
//        {

//            //有做滿一個月：日薪三天
//            if (decimal.Parse( dr["MonthFactor"].ToString()) == DateTime.DaysInMonth(hryear, hrmonth))
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
//             dr["AllAttendBonus"] = 0;
//        }
//        #endregion
//       // dr["WorkBonus"] = 1000;
//        dr["EffectBonus"] = 1000;


//        #region //加班
//        DataSet overtimeData = this.OverTimeManger.SelectOverTimeInfoByEmployeeId(employees.EmployeeId, new DateTime(hryear, hrmonth, 01));



//    decimal NormalHour=decimal.Zero;
//    decimal NormalFee=decimal.Zero;
//    decimal HolidayCount=decimal.Zero;
//    decimal HolidayFee=decimal.Zero;
//       for (int i = 0; i < overtimeData.Tables[0].Rows.Count; i++)
//       {
//           //假日加班          
//          if ((bool)overtimeData.Tables[0].Rows[i][Model.OverTime.PROPERTY_ISHOLIDAY])
//          {
//              HolidayFee += decimal.Parse(overtimeData.Tables[0].Rows[i][Model.OverTime.PROPERTY_OVERTIMEFEE].ToString());
//              HolidayCount += decimal.Parse(overtimeData.Tables[0].Rows[i][Model.OverTime.PROPERTY_EOVERTIME].ToString());
//          }
//          else//平日加班
//          { 
//             NormalHour+=decimal.Parse( overtimeData.Tables[0].Rows[i][Model.OverTime.PROPERTY_EOVERTIME].ToString());
//             NormalFee += decimal.Parse(overtimeData.Tables[0].Rows[i][Model.OverTime.PROPERTY_OVERTIMEFEE].ToString());
//          }

//       }
//       dr["HolidayOverTimeFee"] = HolidayFee;
//       dr["HolidayOverTime"] =HolidayCount;
//       dr["GeneralOverTime"] =NormalHour;
//       dr["GeneralOverTimeFee"]=NormalFee;


//        #endregion
//         dr["AnnualHolidayFee"] = 0;


//        dr["LatePunish"] = 0;
//        dr["DailyPay"] = employees.DailyPay == null ? 0 : employees.DailyPay.Value;
//        dr["Insurance"] = employees.Insurance == null ? 0 : employees.Insurance.Value;
//       // dr["LoanFee"] = row1["LoanFee"] == null || row1["LoanFee"].ToString() == "" ? 0 : decimal.Parse(row1["LoanFee"].ToString()); // this.loandetailManage.SelectFeeSum(employees, hryear, hrmonth);
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

//}
#endregion