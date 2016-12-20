using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.IO;
using Helper;
//打卡記錄管理
//2010-10-12 l 修改


namespace Book.UI.Hr.Attendance.ClockData
{
    public partial class ClockForm : DevExpress.XtraEditors.XtraForm
    {
        #region 變量
        /// <summary>
        /// 打卡記錄管理實例
        /// </summary>
        BL.ClockDataManager _clockdata = new Book.BL.ClockDataManager();
        /// <summary>
        /// 员工管理实例
        /// </summary>
        protected BL.EmployeeManager employeeManager = new Book.BL.EmployeeManager();
        /// <summary>
        /// 部门管理实例
        /// </summary>
        protected BL.DepartmentManager departmentManager = new Book.BL.DepartmentManager();
        /// <summary>
        /// 当前待处理员工
        /// </summary>
        private Book.Model.Employee employee = null;
        /// <summary>
        /// 當前待處理打卡記錄
        /// </summary>
        private Model.ClockData clock = null;
        private BL.TempCardManager tempcarm = new Book.BL.TempCardManager();
        /// <summary>
        /// 出勤记录管理
        /// </summary>
        private Model.HrDailyEmployeeAttendInfo _dailyInfo;
        private BL.HrDailyEmployeeAttendInfoManager _dailyManager = new Book.BL.HrDailyEmployeeAttendInfoManager();
        private BL.LeaveManager _leaveManager = new Book.BL.LeaveManager();
        /// <summary>
        /// 出勤记录管理
        /// </summary>
        //BL.AttenManager attemange = new Book.BL.AttenManager();
        /// <summary>
        /// 弹性排班管理
        /// </summary>
        BL.FlextimeManager Flextimemanage = new Book.BL.FlextimeManager();
        BL.AnnualHolidayManager _annualHolidayManager = new Book.BL.AnnualHolidayManager();

        //用於上班中間吃飯的時間斷
        private readonly int minute = 30;
        private readonly int hour = 4;
        #endregion

        public ClockForm()
        {
            InitializeComponent();
        }

        private void ClockForm_Load(object sender, EventArgs e)
        {
            //DateTime d = new DateTime(1900, 1, 1);
            //DateTime startdate = new DateTime(d.Year, d.Month, 1);//以最小日期作为开始日期
            //DateTime maxDate = new DateTime(2100, 1, 1);//最大日期
            //DateTime enddate = this.getendmonthdate(maxDate);
            this.repositoryItemCheckEdit1.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.InactiveChecked;

            this.EmployeeSource.DataSource = employeeManager.SelectOnActive();
            this.DepartSource.DataSource = departmentManager.Select();
            //this.ClockSource.DataSource = _clockdata.selectClockTopTreeMonth();
            //loadclock(startdate, enddate);//加载打卡数据
        }

        private void loadclock(DateTime starttime, DateTime endtime)
        {
            this.ClockSource.DataSource = _clockdata.getbydate(starttime, endtime);
        }

        //导入打卡数据
        private void barBtnDaoRu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            List<string> FileNames = new List<string>();
            StringBuilder sbNotFile = new StringBuilder();
            StringBuilder sbHaveFile = new StringBuilder();
            this.openFileDialog1.Filter = "文檔(*.txt)|*.txt";
            this.openFileDialog1.Title = "選擇導入文件";
            this.openFileDialog1.Multiselect = true;
            if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                foreach (string fileN in this.openFileDialog1.FileNames)
                {
                    if (!File.Exists(fileN) && fileN.Substring(fileN.LastIndexOf('.')) == "txt")
                    {
                        sbNotFile.Append(fileN + "\n");
                    }
                    else
                    {
                        FileNames.Add(fileN);
                        sbHaveFile.Append(fileN + "\n");
                    }
                }
                if (sbNotFile != null && sbNotFile.ToString() != "")
                {
                    MessageBox.Show(this, sbNotFile.ToString(), "FileNotFound", MessageBoxButtons.OK);
                    return;
                }
            }
            if (FileNames.Count != 0)
            {
                if (GetData(FileNames))
                {
                    DateTime d = DateTime.Now;
                    DateTime startdate = new DateTime(d.Year, d.Month, 1);
                    DateTime enddate = new DateTime(d.Year, d.Month, 1);
                    loadclock(startdate, enddate);
                }
            }

        }

        //获取导入文件中的数据
        protected bool GetData(List<string> Path)
        {
            for (int i = 0; i < Path.Count; i++)
            {
                string FileAllContent = File.ReadAllText(Path[i]);  //读取到文本文件中所有内容
                List<string> FileAllContentList = new List<string>(FileAllContent.Split('\n')); //创建以行为元素的List
                string thisFileName = Path[i].Substring(Path[i].LastIndexOf('\\') + 1);
                if (!_clockdata.SearchDistinctFileName(thisFileName))
                {
                    DialogResult dr = MessageBox.Show(this, "曾經導入過文件:" + thisFileName + ",是否繼續導入", Properties.Resources.AreYouSureLeading, MessageBoxButtons.OKCancel);
                    if (dr == DialogResult.Cancel)
                    {
                        return false;
                    }
                    else
                    {
                        this._clockdata.DeleteByFileName(thisFileName);
                    }
                }
                string[] s = null;
                DataSet _businessHours;
                Model.ClockData clockdata = null;
                BL.ClockDataManager datamanger = new Book.BL.ClockDataManager();
                string CardNo = string.Empty;
                BL.EmployeeManager employeemanager = new Book.BL.EmployeeManager();
                Model.Employee employee = null;
                #region 开始循环遍历行数据
                foreach (string message in FileAllContentList)
                {
                    clockdata = new Book.Model.ClockData();
                    if (message != null && message != string.Empty)
                    {
                        s = message.Split('\t');
                        clockdata.ClockDataId = Guid.NewGuid().ToString();
                        string datestring = FormatString(s[1]);//日期
                        DateTime da = DateTime.Parse(datestring);
                        //clockdata.Empclockdate = da;
                        string timestring = s[2];//时间
                        DateTime td = DateTime.Parse(datestring + " " + timestring);
                        clockdata.Clocktime = td;
                        string cardnostring = s[3].Substring(1);//卡号
                        employee = employeemanager.SelectByCardNo(cardnostring, da);
                        clockdata.CardNo = cardnostring;
                        clockdata.InsertTime = DateTime.Now;
                        string clocktypestring = s[5] == "0002\r" ? "0002" : "0003";//类别
                        clockdata.ClockType = clocktypestring;
                        clockdata.Description = false;
                        clockdata.FileNames = thisFileName;
                        DateTime ShouldCheckIn = DateTimeParse.NullDate;
                        DateTime ShouldCheckOut = DateTimeParse.NullDate;
                        DataSet data = this._clockdata.SearchClockDataInfoByCarNoAndClockDate(clockdata.CardNo, Convert.ToDateTime(clockdata.Clocktime));
                        #region 打卡记录有卡号
                        if (employee != null)
                        {
                            clockdata.EmployeeId = employee.EmployeeId;
                            #region 註釋部份
                            //_businessHours = _clockdata.SearchBusinessHoursInfoByEmployeeId(employee.EmployeeId);
                            //if (_businessHours.Tables[0].Rows.Count > 0)
                            //{
                            //    if (_businessHours.Tables[0].Rows[0][Model.BusinessHours.PROPERTY_FROMTIME] != null)
                            //        // ShouldCheckIn = Convert.ToDateTime(Empdate.ToString("yyyy-MM-dd") + " " + this._dailyInfo.ShouldCheckIn.Value.ToString("HH:ss:mm"));
                            //        ShouldCheckIn = Convert.ToDateTime(_businessHours.Tables[0].Rows[0][Model.BusinessHours.PROPERTY_FROMTIME]);
                            //    if (_businessHours.Tables[0].Rows[0][Model.BusinessHours.PROPERTY_TOTIME] != null)
                            //        ShouldCheckOut = Convert.ToDateTime(_businessHours.Tables[0].Rows[0][Model.BusinessHours.PROPERTY_TOTIME]);
                            //}
                            //if (ShouldCheckIn.Hour == 0 && clocktypestring == "0002" || Convert.ToDateTime(ShouldCheckIn).Hour == 0 && clocktypestring == "0003")
                            //{
                            //    //上班时间在凌晨 01:20:00 00:00:00

                            //    if (DateTime.Parse(timestring).Hour >= 0 && DateTime.Parse(timestring).Hour < 8)
                            //    {
                            //        clockdata.Empclockdate = da.AddDays(-1);

                            //    }
                            //    else if (clocktypestring == "0003" && ShouldCheckOut.AddHours(2).Hour > DateTime.Parse(timestring).Hour && ShouldCheckIn.Hour < DateTime.Parse(timestring).Hour)
                            //    {
                            //        clockdata.Empclockdate = da.AddDays(-1);
                            //    }
                            //    else
                            //    {
                            //        clockdata.Empclockdate = da;
                            //    }

                            //}
                            //else
                            //{
                            //    if (ShouldCheckOut.Hour == 0 && clocktypestring == "0003")
                            //    {
                            //        //下班在凌晨 打卡在第二天00:00:00
                            //        if (DateTime.Parse(timestring).Hour >= 0 && DateTime.Parse(timestring).Hour < 8)
                            //        {
                            //            clockdata.Empclockdate = da.AddDays(-1);
                            //        }
                            //        else
                            //        {
                            //            clockdata.Empclockdate = da;
                            //        }
                            //    }
                            //    else
                            //    {
                            //        clockdata.Empclockdate = da;
                            //    }
                            //}
                            #endregion
                            clockdata.Empclockdate = da;

                            if (data.Tables[0].Rows.Count == 0)
                            {
                                datamanger.Insert(clockdata);
                            }
                        }
                        #endregion
                        #region 员工使用零时卡
                        else
                        {
                            IList<Model.TempCard> temlist = tempcarm.SelectbyCardnoDate(da, cardnostring);
                            if (temlist.Count > 0)
                            {
                                employee = (temlist[0] as Model.TempCard).Employee;
                                clockdata.EmployeeId = employee.EmployeeId;
                                clockdata.Empclockdate = da;
                                #region 註釋部份
                                //_businessHours = _clockdata.SearchBusinessHoursInfoByEmployeeId(employee.EmployeeId);
                                //if (_businessHours.Tables[0].Rows.Count > 0)
                                //{
                                //    if (_businessHours.Tables[0].Rows[0][Model.BusinessHours.PROPERTY_FROMTIME] != null)
                                //        ShouldCheckIn = Convert.ToDateTime(_businessHours.Tables[0].Rows[0][Model.BusinessHours.PROPERTY_FROMTIME]);
                                //    if (_businessHours.Tables[0].Rows[0][Model.BusinessHours.PROPERTY_TOTIME] != null)
                                //        ShouldCheckOut = Convert.ToDateTime(_businessHours.Tables[0].Rows[0][Model.BusinessHours.PROPERTY_TOTIME]);
                                //}
                                //if (Convert.ToDateTime(ShouldCheckIn).Hour == 0 && clocktypestring == "0002")
                                //{
                                //    if (DateTime.Parse(DateTime.Parse(timestring).ToShortDateString()) > Convert.ToDateTime(ShouldCheckOut.ToShortDateString()))
                                //    {
                                //        clockdata.Empclockdate = da.AddDays(1);
                                //    }
                                //    else
                                //    {
                                //        clockdata.Empclockdate = da;
                                //    }
                                //}
                                //else
                                //{
                                //    if (Convert.ToDateTime(ShouldCheckOut).Hour == 0 && clocktypestring == "0003")
                                //    {
                                //        if (DateTime.Parse(timestring) < Convert.ToDateTime(ShouldCheckIn.ToShortTimeString()))
                                //        {
                                //            clockdata.Empclockdate = da.AddDays(-1);
                                //        }
                                //        else
                                //        {
                                //            clockdata.Empclockdate = da;
                                //        }
                                //    }
                                //    else
                                //    {
                                //        clockdata.Empclockdate = da;
                                //    }
                                //}
                                #endregion
                                if (data.Tables[0].Rows.Count == 0)
                                {
                                    datamanger.Insert(clockdata);
                                }
                            }
                        }
                        #endregion
                    }
                }
                #endregion
            }
            MessageBox.Show("全部導入完成！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return true;
        }

        protected string FormatString(string s)
        {
            string[] m = s.Split('-');
            return m[2] + "-" + m[1] + "-" + m[0];
        }

        private DateTime getendmonthdate(DateTime d)
        {
            DateTime endmonthdate = DateTime.Now;

            int month = d.Month;
            int year = d.Year;


            switch (month)
            {
                case 1:
                    endmonthdate = new DateTime(year, month, 31);
                    break;
                case 2:
                    if (year % 4 == 0 && (year % 100 != 0 || year % 400 == 0))
                    {
                        endmonthdate = new DateTime(year, month, 29);
                    }
                    else
                    {
                        endmonthdate = new DateTime(year, month, 28);
                    }
                    break;
                case 3:
                    endmonthdate = new DateTime(year, month, 31);
                    break;
                case 4:
                    endmonthdate = new DateTime(year, month, 30);
                    break;
                case 5:
                    endmonthdate = new DateTime(year, month, 31);
                    break;
                case 6:
                    endmonthdate = new DateTime(year, month, 30);
                    break;
                case 7:
                    endmonthdate = new DateTime(year, month, 31);
                    break;
                case 8:
                    endmonthdate = new DateTime(year, month, 31);
                    break;
                case 9:
                    endmonthdate = new DateTime(year, month, 30);
                    break;
                case 10:
                    endmonthdate = new DateTime(year, month, 31);
                    break;
                case 11:
                    endmonthdate = new DateTime(year, month, 30);
                    break;
                case 12:
                    endmonthdate = new DateTime(year, month, 31);
                    break;
            }
            return endmonthdate;
        }

        //查询打卡记录
        private void barButtonItem3_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.gridView1.OptionsView.ShowAutoFilterRow = this.gridView1.OptionsView.ShowAutoFilterRow == true ? false : true;
        }

        //点击列表引发事件
        private void sbtn_select_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.StartDate.Text) || string.IsNullOrEmpty(this.EndDate.Text))
            {
                MessageBox.Show(Properties.Resources.RequireDateToDate, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            DateTime starttime = this.StartDate.DateTime;
            DateTime endtime = this.EndDate.DateTime;
            if (starttime > endtime)
            {
                MessageBox.Show(Properties.Resources.StartDateGTEndDate, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            this.ClockSource.DataSource = _clockdata.getbydate(starttime, endtime);
        }

        private void gridView1_CustomColumnDisplayText_1(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            //if (e.ListSourceRowIndex < 0) return;
            //IList<Model.ClockData> clockData = this.ClockSource.DataSource as IList<Model.ClockData>;
            //if (clockData == null || clockData.Count < 1) return;
            //Model.Employee employee = clockData[e.ListSourceRowIndex].Employee;
            //if (employee == null) return;
            ////switch (e.Column.Name)
            ////{
            ////    case "EmployeeId":
            ////        e.DisplayText = string.IsNullOrEmpty(employee.IDNo) ? string.Empty : employee.IDNo;
            ////        break;
            ////    case "EmployeeName":
            ////        e.DisplayText = string.IsNullOrEmpty(employee.EmployeeName) ? string.Empty : employee.EmployeeName;
            ////        break;
            ////    case "Clocktime":
            ////        e.DisplayText = clockData[e.ListSourceRowIndex].Clocktime.Value.Hour.ToString() + ":" + clockData[e.ListSourceRndex].Clocktime.Value.Minute.ToString();
            ////        break;
            ////    case "Clocktype":
            ////        e.DisplayText = clockData[e.ListSourceRowIndex].ClockType == "0002" ? "上班" : "下班";
            ////        break;
            ////}
            //switch (e.Column.Name)
            //{
            //    case "Clocktime":
            //        e.DisplayText = clockData[e.ListSourceRowIndex].Clocktime.ToString();
            //        break;
            //    case "CardNo":
            //        e.DisplayText = string.IsNullOrEmpty(employee.CardNo) ? string.Empty : employee.CardNo;
            //        break;
            //    case "Clocktype":
            //        e.DisplayText = clockData[e.ListSourceRowIndex].ClockType == "0002" ? "上班" : "下班";
            //        break;

            //}
        }
        //更新考勤记录
        private void barBtn_update_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.DailyEmployeeAttendInfo(Convert.ToDateTime(DateTime.Now.Date.ToShortDateString()));
            if (DateTime.Now.Day == DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month))
            {
                this.MakeSalaryList(Convert.ToDateTime(DateTime.Now.Year.ToString() + "/" + DateTime.Now.Month.ToString() + "/1"));
            }
            MessageBox.Show("更新出勤記錄完成", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }

        //更新出勤记录
        private void DailyEmployeeAttendInfo(DateTime checkDate)
        {
            IList<Model.Employee> EmpIDList = this.employeeManager.DailyEmployeeAttendInfo_EmpList(checkDate.Date);
            try
            {
                BL.V.BeginTransaction();
                if (EmpIDList != null || EmpIDList.Count != 0)
                {
                    foreach (Model.Employee emp in EmpIDList)
                    {
                        if (emp != null)
                        {
                            _dailyManager.ReCheck(checkDate, emp);
                        }
                    }
                }
                BL.V.CommitTransaction();
            }
            catch (Exception ex)
            {
                BL.V.RollbackTransaction();
                throw ex;
            }
        }

        //更新基础设置
        private void MakeSalaryList(DateTime baseDate)
        {
            IList<string> EmpIDList = _clockdata.GetMakeSalaryList_DisEmpID(baseDate);
            foreach (string EmpID in EmpIDList)
            {
                Model.Employee emp = employeeManager.Get(EmpID);
                if (emp != null)
                {
                    BL.MonthlySalaryManager ms = new BL.MonthlySalaryManager();
                    ms.UpMonthSalFromClockFrm(emp, baseDate);
                }
            }
        }


        #region @更新出勤记录
        //private void barButtonItem2_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        //{
        //    bool isAll = bool.Parse(barEditItem5.EditValue.ToString());//isAll来判断是更新全部员工还是单个员工
        //    if (isAll)
        //    {
        //        DataSet employeeList = _clockdata.SearchDistinctEmployee();//查询无重复的员工
        //        DataSet dateList = _clockdata.SearchDistinctDate();//查询无重复的日期
        //        Model.Employee employee = new Book.Model.Employee();
        //        DateTime _date;
        //        string _employeeId = string.Empty;
        //        DataSet _businessHours; //班别的数据
        //        DataSet _morningShift;//早班数据集合
        //        DataSet _eveningShift;//晚班数据集合
        //        DataSet _overtTime;//加班时间集合

        //        //根据员工编号和日期来查询打卡数据，并且进行筛选
        //        for (int i = 0; i < employeeList.Tables[0].Rows.Count; i++)
        //        {
        //            _employeeId = employeeList.Tables[0].Rows[i][Model.ClockData.PRO_EmployeeId].ToString();
        //            employee = employeeManager.Get(_employeeId);
        //            _businessHours = _clockdata.SearchBusinessHoursInfoByEmployeeId(employee.EmployeeId);

        //            for (int j = 0; j < dateList.Tables[0].Rows.Count; j++)
        //            {
        //                _dailyInfo = new Book.Model.HrDailyEmployeeAttendInfo();
        //                _dailyInfo.EmployeeId = employee.EmployeeId;
        //                _date = Convert.ToDateTime(dateList.Tables[0].Rows[j][Model.ClockData.PRO_Empclockdate]);
        //                //_overtTime = _clockdata.SearchOvertimeInfoByEmployeeId(employee.EmployeeId, _date);
        //                _morningShift = _clockdata.SearchSingleClockByCondition(employee.EmployeeId, _date, "0002");
        //                _eveningShift = _clockdata.SearchSingleClockByCondition(employee.EmployeeId, _date, "0003");
        //                List<DateTime> _earliestList = new List<DateTime>();
        //                List<DateTime> _latestList = new List<DateTime>();

        //                #region 早班數據集
        //                for (int ii = 0; ii < _morningShift.Tables[0].Rows.Count; ii++)
        //                {
        //                    int count = 0;
        //                    Boolean IsOk = false;
        //                    if (_eveningShift.Tables[0].Rows.Count > 0)
        //                    {
        //                        for (int jj = 0; jj < _eveningShift.Tables[0].Rows.Count; jj++)
        //                        {
        //                            if (DateTime.Compare(Convert.ToDateTime(_morningShift.Tables[0].Rows[ii]["ClockTime"]), Convert.ToDateTime(_eveningShift.Tables[0].Rows[jj]["Clocktime"])) < 1)
        //                            {
        //                                IsOk = true;
        //                                count = jj;
        //                                break;
        //                            }
        //                            else
        //                            {
        //                                count = jj;
        //                            }
        //                        }
        //                        //都打卡
        //                        if (IsOk)
        //                        {
        //                            _earliestList.Add(Convert.ToDateTime(_morningShift.Tables[0].Rows[ii]["Clocktime"]));
        //                            _latestList.Add(Convert.ToDateTime(_eveningShift.Tables[0].Rows[count]["Clocktime"]));
        //                            _eveningShift.Tables[0].Rows.Remove(_eveningShift.Tables[0].Rows[count]);
        //                            _morningShift.Tables[0].Rows.Remove(_morningShift.Tables[0].Rows[ii]);
        //                        }
        //                        //早上打卡、晚上沒打卡
        //                        else
        //                        {

        //                            _earliestList.Add(Convert.ToDateTime(_morningShift.Tables[0].Rows[ii]["Clocktime"]));
        //                            _morningShift.Tables[0].Rows.Remove(_morningShift.Tables[0].Rows[ii]);
        //                            _latestList.Add(DateTimeParse.NullDate);
        //                        }
        //                    }
        //                    //早上打卡，晚上沒打卡
        //                    else
        //                    {
        //                        _earliestList.Add(Convert.ToDateTime(_morningShift.Tables[0].Rows[ii]["Clocktime"]));
        //                        _morningShift.Tables[0].Rows.Remove(_morningShift.Tables[0].Rows[ii]);
        //                        _latestList.Add(DateTimeParse.NullDate);
        //                    }

        //                }
        //                #endregion

        //                #region 晚班數據集合
        //                for (int k = 0; k < _eveningShift.Tables[0].Rows.Count; k++)
        //                {
        //                    int count = 0;
        //                    Boolean IsOk = false;
        //                    if (_morningShift.Tables[0].Rows.Count > 0)
        //                    {
        //                        for (int jj = 0; jj < _morningShift.Tables[0].Rows.Count; jj++)
        //                        {
        //                            if (DateTime.Compare(Convert.ToDateTime(_eveningShift.Tables[0].Rows[k]["ClockTime"]), Convert.ToDateTime(_morningShift.Tables[0].Rows[jj]["Clocktime"])) > 0)
        //                            {
        //                                IsOk = true;
        //                                count = jj;
        //                                break;
        //                            }
        //                            else
        //                            {
        //                                count = jj;
        //                            }
        //                        }
        //                        //都打卡
        //                        if (IsOk)
        //                        {
        //                            _earliestList.Add(Convert.ToDateTime(_morningShift.Tables[0].Rows[count]["Clocktime"]));
        //                            _latestList.Add(Convert.ToDateTime(_eveningShift.Tables[0].Rows[k]["Clocktime"]));
        //                            _eveningShift.Tables[0].Rows.Remove(_eveningShift.Tables[0].Rows[k]);
        //                            _morningShift.Tables[0].Rows.Remove(_morningShift.Tables[0].Rows[count]);
        //                        }
        //                        //早上沒打卡晚上打卡
        //                        else
        //                        {
        //                            _latestList.Add(Convert.ToDateTime(_eveningShift.Tables[0].Rows[k]["Clocktime"]));
        //                            _eveningShift.Tables[0].Rows.Remove(_eveningShift.Tables[0].Rows[k]);
        //                            _earliestList.Add(DateTimeParse.NullDate);
        //                        }
        //                    }
        //                    //早上沒打卡晚上打卡
        //                    else
        //                    {
        //                        _latestList.Add(Convert.ToDateTime(_eveningShift.Tables[0].Rows[k]["Clocktime"]));
        //                        _eveningShift.Tables[0].Rows.Remove(_eveningShift.Tables[0].Rows[k]);
        //                        _earliestList.Add(DateTimeParse.NullDate);
        //                    }
        //                }
        //                #endregion

        //                SharedOperating(_earliestList, _latestList, employee, _businessHours, _date);
        //            }
        //        }
        //    }
        //    else
        //    {
        //        if (this.employee == null)
        //        {
        //            MessageBox.Show(Properties.Resources.EmployeeNotNull, this.Text, MessageBoxButtons.OK);
        //            return;
        //        }
        //        DataSet dateList = _clockdata.SearchDistinctDate();
        //        Model.Employee employee = new Model.Employee();
        //        DateTime _date;
        //        DataSet _businessHours;
        //        string _employeeId = string.Empty;
        //        DataSet _morningShift;
        //        DataSet _eveningShift;
        //        DataSet _overtTime;
        //        employee = this.employee;
        //        _businessHours = _clockdata.SearchBusinessHoursInfoByEmployeeId(employee.EmployeeId);
        //        for (int j = 0; j < dateList.Tables[0].Rows.Count; j++)
        //        {
        //            _dailyInfo = new Book.Model.HrDailyEmployeeAttendInfo();
        //            _dailyInfo.EmployeeId = employee.EmployeeId;
        //            _date = Convert.ToDateTime(dateList.Tables[0].Rows[j][Model.ClockData.PRO_Empclockdate]);
        //            _overtTime = _clockdata.SearchOvertimeInfoByEmployeeId(employee.EmployeeId, _date);
        //            _morningShift = _clockdata.SearchSingleClockByCondition(employee.EmployeeId, _date, "0002");
        //            _eveningShift = _clockdata.SearchSingleClockByCondition(employee.EmployeeId, _date, "0003");
        //            List<DateTime> _earliestList = new List<DateTime>();
        //            List<DateTime> _latestList = new List<DateTime>();
        //            for (int ii = 0; ii < _morningShift.Tables[0].Rows.Count; ii++)
        //            {
        //                if (_eveningShift.Tables[0].Rows.Count > 0)
        //                {
        //                    int count = 0;
        //                    Boolean IsOk = false;
        //                    for (int jj = 0; jj < _eveningShift.Tables[0].Rows.Count; jj++)
        //                    {
        //                        if (DateTime.Compare(Convert.ToDateTime(_morningShift.Tables[0].Rows[ii]["ClockTime"]), Convert.ToDateTime(_eveningShift.Tables[0].Rows[jj]["Clocktime"])) < 1)
        //                        {

        //                            IsOk = true;
        //                            count = jj;
        //                            break;
        //                        }
        //                        else
        //                        {
        //                            count = jj;
        //                        }
        //                    }
        //                    if (IsOk)
        //                    {
        //                        _earliestList.Add(Convert.ToDateTime(_morningShift.Tables[0].Rows[ii]["Clocktime"]));
        //                        _latestList.Add(Convert.ToDateTime(_eveningShift.Tables[0].Rows[count]["Clocktime"]));
        //                        _eveningShift.Tables[0].Rows.Remove(_eveningShift.Tables[0].Rows[count]);
        //                        _morningShift.Tables[0].Rows.Remove(_morningShift.Tables[0].Rows[ii]);
        //                    }
        //                    else
        //                    {

        //                        _earliestList.Add(Convert.ToDateTime(_morningShift.Tables[0].Rows[ii]["Clocktime"]));
        //                        _morningShift.Tables[0].Rows.Remove(_morningShift.Tables[0].Rows[ii]);
        //                        _latestList.Add(DateTimeParse.NullDate);
        //                    }
        //                }
        //                else
        //                {
        //                    _earliestList.Add(Convert.ToDateTime(_morningShift.Tables[0].Rows[ii]["Clocktime"]));
        //                    _morningShift.Tables[0].Rows.Remove(_morningShift.Tables[0].Rows[ii]);
        //                    _latestList.Add(DateTimeParse.NullDate);
        //                }

        //            }

        //            for (int k = 0; k < _eveningShift.Tables[0].Rows.Count; k++)
        //            {
        //                int count = 0;
        //                Boolean IsOk = false;

        //                if (_morningShift.Tables[0].Rows.Count > 0)
        //                {
        //                    for (int jj = 0; jj < _morningShift.Tables[0].Rows.Count; jj++)
        //                    {
        //                        if (DateTime.Compare(Convert.ToDateTime(_eveningShift.Tables[0].Rows[k - 1]["ClockTime"]), Convert.ToDateTime(_morningShift.Tables[0].Rows[jj]["Clocktime"])) < 1 && DateTime.Compare(Convert.ToDateTime(_eveningShift.Tables[0].Rows[k]["ClockTime"]), Convert.ToDateTime(_morningShift.Tables[0].Rows[jj]["Clocktime"])) > 0)
        //                        {
        //                            IsOk = true;
        //                            count = jj;
        //                            break;
        //                        }
        //                        else
        //                        {
        //                            count = jj;
        //                        }
        //                    }
        //                    if (IsOk)
        //                    {
        //                        _earliestList.Add(Convert.ToDateTime(_morningShift.Tables[0].Rows[count]["Clocktime"]));
        //                        _latestList.Add(Convert.ToDateTime(_eveningShift.Tables[0].Rows[k]["Clocktime"]));
        //                        _eveningShift.Tables[0].Rows.Remove(_eveningShift.Tables[0].Rows[k]);
        //                        _morningShift.Tables[0].Rows.Remove(_morningShift.Tables[0].Rows[count]);
        //                    }
        //                    else
        //                    {
        //                        _latestList.Add(Convert.ToDateTime(_eveningShift.Tables[0].Rows[k]["Clocktime"]));
        //                        _eveningShift.Tables[0].Rows.Remove(_eveningShift.Tables[0].Rows[k]);
        //                        _earliestList.Add(DateTimeParse.NullDate);
        //                    }
        //                }
        //                else
        //                {
        //                    _latestList.Add(Convert.ToDateTime(_eveningShift.Tables[0].Rows[k]["Clocktime"]));
        //                    _eveningShift.Tables[0].Rows.Remove(_eveningShift.Tables[0].Rows[k]);
        //                    _earliestList.Add(DateTimeParse.NullDate);
        //                }
        //            }
        //            SharedOperating(_earliestList, _latestList, employee, _businessHours, _date);
        //        }
        //    }
        //    MessageBox.Show(Properties.Resources.SuccessfullySaved, this.Text, MessageBoxButtons.OK);
        //    }
        #endregion
        #region @更新具體操作
        //protected void SharedOperating(List<DateTime> _earliestDate, List<DateTime> _latestDate, Model.Employee _employee, DataSet _businessHours, DateTime Empdate)
        //{
        //    for (int i = 0; i < _earliestDate.Count; i++)
        //    {
        //        this._dailyInfo = new Book.Model.HrDailyEmployeeAttendInfo();
        //        this._dailyInfo.EmployeeId = _employee.EmployeeId;
        //        DateTime d = Convert.ToDateTime(_businessHours.Tables[0].Rows[0]["FromTime"]).AddMinutes(-30);
        //        this._dailyInfo.ActualCheckIn = _earliestDate[i];
        //        this._dailyInfo.ActualCheckOut = _latestDate[i];
        //        if (_businessHours.Tables[0].Rows.Count > 0)
        //        {
        //            if (_businessHours.Tables[0].Rows[0][Model.BusinessHours.PROPERTY_FROMTIME] != null)
        //                this._dailyInfo.ShouldCheckIn = Convert.ToDateTime(Empdate.ToString("yyyy-MM-dd") + " " + Convert.ToDateTime(_businessHours.Tables[0].Rows[0][Model.BusinessHours.PROPERTY_FROMTIME]).ToString("HH:mm:ss"));
        //            if (_businessHours.Tables[0].Rows[0][Model.BusinessHours.PROPERTY_TOTIME] != null)
        //                this._dailyInfo.ShouldCheckOut = Convert.ToDateTime(Empdate.ToString("yyyy-MM-dd") + " " + Convert.ToDateTime(_businessHours.Tables[0].Rows[0][Model.BusinessHours.PROPERTY_TOTIME]).ToString("HH:mm:ss"));
        //            if (_businessHours.Tables[0].Rows[0][Model.BusinessHours.PROPERTY_SPECIALPAY] != DBNull.Value)
        //                this._dailyInfo.SpecialBonus = Convert.ToInt32(_businessHours.Tables[0].Rows[0][Model.BusinessHours.PROPERTY_SPECIALPAY]);
        //        }
        //        DateTime shouldin = Convert.ToDateTime(this._dailyInfo.ShouldCheckIn);
        //        if (_earliestDate[i] >= shouldin.AddHours(hour).AddMinutes(-minute) && _earliestDate[i] <= shouldin.AddHours(hour + 1).AddMinutes(minute))
        //        {
        //            this._dailyInfo.ShouldCheckIn = _dailyInfo.ShouldCheckIn.Value.AddHours(hour + 1);
        //            this._dailyInfo.Note = Properties.Resources.Absenteeism + "(" + Properties.Resources.Morning + ")";
        //            this._dailyInfo.DayFactor = 0;
        //            this._dailyInfo.SpecialBonus = this._dailyInfo.SpecialBonus / 2;
        //        }
        //        if (_latestDate[i] >= shouldin.AddHours(hour).AddMinutes(-minute) && _latestDate[i] <= shouldin.AddHours(hour + 1))
        //        {
        //            this._dailyInfo.ShouldCheckOut = _dailyInfo.ShouldCheckIn.Value.AddHours(hour);
        //            this._dailyInfo.Note = Properties.Resources.Absenteeism + "(" + Properties.Resources.Evening + ")";
        //            this._dailyInfo.DayFactor = 0;
        //            this._dailyInfo.SpecialBonus = this._dailyInfo.SpecialBonus / 2;
        //        }
        //        if (Empdate != null)
        //        {
        //            this._dailyInfo.DutyDate = Empdate;
        //        }
        //        //加班情況
        //        if (DateTime.Compare(_earliestDate[i], DateTimeParse.NullDate) != 0 && DateTime.Compare(_latestDate[i], DateTimeParse.NullDate) != 0)
        //        {
        //            if (this._dailyInfo.ShouldCheckIn.Value.Hour == 0)
        //            {
        //                if (_earliestDate[i] > this._dailyInfo.ShouldCheckOut && _latestDate[i] < this._dailyInfo.ShouldCheckIn.Value.AddDays(1))
        //                {
        //                    this._dailyInfo.ShouldCheckIn = null;
        //                    this._dailyInfo.ShouldCheckOut = null;
        //                }
        //            }
        //            else
        //            {
        //                if (_latestDate[i] < this._dailyInfo.ShouldCheckIn && _earliestDate[i] > this._dailyInfo.ShouldCheckOut)
        //                {
        //                    this._dailyInfo.ShouldCheckIn = null;
        //                    this._dailyInfo.ShouldCheckOut = null;
        //                }
        //            }
        //        }
        //        if (this._dailyInfo.ShouldCheckIn != null && this._dailyInfo.ShouldCheckOut != null)
        //        {
        //            if (this._dailyInfo.ShouldCheckOut.Value.Hour == 0)
        //            {
        //                this._dailyInfo.ShouldCheckOut = this._dailyInfo.ShouldCheckOut.Value.AddDays(1);
        //            }
        //            if (this._dailyInfo.ShouldCheckIn.Value.Hour == 0)
        //            {
        //                this._dailyInfo.ShouldCheckIn = this._dailyInfo.ShouldCheckIn.Value.AddDays(1);
        //                this._dailyInfo.ShouldCheckOut = this._dailyInfo.ShouldCheckOut.Value.AddDays(1);
        //            }
        //        }

        //        #region 上班打卡記錄是否迟到
        //        if (this._dailyInfo.ShouldCheckIn != null && this._dailyInfo.ActualCheckIn != null)
        //        {
        //            if (DateTime.Compare(_earliestDate[i], DateTimeParse.NullDate) != 0)
        //            {
        //                //沒有遲到
        //                // if (DateTime.Parse(_earliestDate[i].ToString("yyyy-MM-dd HH:mm")) <= DateTime.Parse(dt1.ToString("yyyy-MM-dd HH:ss")))
        //                if (DateTime.Compare(_earliestDate[i], Convert.ToDateTime(this._dailyInfo.ShouldCheckIn.Value)) == -1 || DateTime.Compare(_earliestDate[i], Convert.ToDateTime(this._dailyInfo.ShouldCheckIn.Value)) == 0)
        //                {
        //                    this._dailyInfo.LateInMinute = 0;
        //                    if (this._dailyInfo.Note != null)
        //                    {
        //                        this._dailyInfo.Note = this._dailyInfo.Note;
        //                    }
        //                    else
        //                    {
        //                        this._dailyInfo.Note = null;
        //                    }
        //                    if (this._dailyInfo.ActualCheckOut == DateTimeParse.NullDate)
        //                    {
        //                        this._dailyInfo.Note = Properties.Resources.DailyInfoLotcard;
        //                        this._dailyInfo.ActualCheckOut = null;
        //                        this._dailyInfo.DayFactor = 0;
        //                        this._dailyInfo.MonthFactor = 0;
        //                    }
        //                    else
        //                    {
        //                        if (_latestDate[i] < this._dailyInfo.ShouldCheckOut)
        //                        {
        //                            if (this._dailyInfo.Note != null)
        //                            {
        //                                this._dailyInfo.Note = this._dailyInfo.Note + ";" + Properties.Resources.DailyInfoLeaveEarly;
        //                            }
        //                            else
        //                            {
        //                                this._dailyInfo.Note = Properties.Resources.DailyInfoLeaveEarly;
        //                            }
        //                        }
        //                    }
        //                }
        //                //遲到
        //                else
        //                {
        //                    double shouldCheckIn = 0;
        //                    if (Convert.ToDateTime(this._dailyInfo.ShouldCheckIn).Minute < 10)
        //                    {
        //                        shouldCheckIn = Convert.ToDouble(Convert.ToDateTime(this._dailyInfo.ShouldCheckIn).Hour + ".0" + Convert.ToDateTime(this._dailyInfo.ShouldCheckIn).Minute);
        //                    }
        //                    else
        //                    {
        //                        shouldCheckIn = Convert.ToDouble(Convert.ToDateTime(this._dailyInfo.ShouldCheckIn).Hour + "." + Convert.ToDateTime(this._dailyInfo.ShouldCheckIn).Minute);
        //                    }
        //                    double actualCheckIn = 0;

        //                    if (Convert.ToDateTime(this._dailyInfo.ActualCheckIn).Minute < 10)
        //                    {
        //                        actualCheckIn = Convert.ToDouble(Convert.ToDateTime(this._dailyInfo.ActualCheckIn).Hour + ".0" + Convert.ToDateTime(this._dailyInfo.ActualCheckIn).Minute);
        //                    }
        //                    else
        //                    {
        //                        actualCheckIn = Convert.ToDouble(Convert.ToDateTime(this._dailyInfo.ActualCheckIn).Hour + "." + Convert.ToDateTime(this._dailyInfo.ActualCheckIn).Minute);
        //                    }
        //                    double result = actualCheckIn - shouldCheckIn;
        //                    result = Math.Round(result, 3);
        //                    if (result < 0.10 && result >= 0)
        //                    {
        //                        //遲到沒有達到十分終
        //                        //判斷纍計三次遲到的情況
        //                        DataSet ds = _dailyManager.SelectLateInfo(_employee.EmployeeId, _earliestDate[i]);
        //                        if (ds.Tables[0].Rows.Count < 3)
        //                        {
        //                            int total = 0;
        //                            for (int mm = 0; mm < ds.Tables[0].Rows.Count; mm++)
        //                            {
        //                                total += Convert.ToInt32(ds.Tables[0].Rows[mm]["LateInMinute"]);
        //                            }
        //                            if (result == 0)
        //                            {
        //                                this._dailyInfo.LateInMinute = 1;
        //                            }
        //                            else
        //                            {
        //                                this._dailyInfo.LateInMinute = Convert.ToInt32(result.ToString().Substring(0, result.ToString().IndexOf('.'))) * 60 + (Convert.ToInt32(result.ToString().Substring(result.ToString().IndexOf('.') + 1)));
        //                            }
        //                            //this._dailyInfo.Note = Properties.Resources.DailyInfoLate;
        //                            total += Convert.ToInt32(this._dailyInfo.LateInMinute);
        //                            this._dailyInfo.IsNormal = false;
        //                            if (total <= 10)
        //                            {
        //                                this._dailyManager.InsertLateInfo(Guid.NewGuid().ToString(), _employee.EmployeeId, Convert.ToDateTime(this._dailyInfo.DutyDate), Convert.ToInt32(this._dailyInfo.LateInMinute));
        //                                this._dailyInfo.LateInMinute = 0;
        //                                this._dailyInfo.IsNormal = true;
        //                                if (this._dailyInfo.Note != null)
        //                                {
        //                                    this._dailyInfo.Note = this._dailyInfo.Note;
        //                                }
        //                                else
        //                                {
        //                                    this._dailyInfo.Note = null;
        //                                }
        //                                this._dailyInfo.ActualCheckIn = this._dailyInfo.ShouldCheckIn;
        //                                if (_employee.EmployeeJoinDate != null && DateTime.Compare(Convert.ToDateTime(this._dailyInfo.ActualCheckOut), DateTime.Parse("1900-01-01")) != 0)
        //                                {
        //                                    if (DateTime.Now.Subtract(Convert.ToDateTime(_employee.EmployeeJoinDate)).Days > 20)
        //                                    {
        //                                        this._dailyInfo.DayFactor = 1;
        //                                        this._dailyInfo.MonthFactor = 1;
        //                                    }
        //                                }
        //                            }
        //                        }
        //                        else
        //                        {
        //                            this._dailyInfo.LateInMinute = Convert.ToInt32(result.ToString().Substring(result.ToString().IndexOf('.') + 1));
        //                            this._dailyInfo.IsNormal = false;
        //                            if (this._dailyInfo.Note != null)
        //                            {
        //                                this._dailyInfo.Note = this._dailyInfo.Note + ";" + Properties.Resources.DailyInfoLate;
        //                            }
        //                            else
        //                            {
        //                                this._dailyInfo.Note = Properties.Resources.DailyInfoLate;
        //                            }
        //                        }
        //                        if (this._dailyInfo.ActualCheckOut == DateTimeParse.NullDate)
        //                        {
        //                            if (this._dailyInfo.Note != null)
        //                            {
        //                                this._dailyInfo.Note = this._dailyInfo.Note + ";" + Properties.Resources.DailyInfoLotcard;
        //                            }
        //                            else
        //                            {
        //                                this._dailyInfo.Note = Properties.Resources.DailyInfoLotcard;
        //                            }
        //                            this._dailyInfo.ActualCheckOut = null;
        //                            this._dailyInfo.DayFactor = 0;
        //                            this._dailyInfo.MonthFactor = 0;
        //                        }
        //                        else
        //                        {
        //                            if (_latestDate[i] < Convert.ToDateTime(this._dailyInfo.ShouldCheckOut.Value))
        //                            {
        //                                if (this._dailyInfo.Note != null)
        //                                {
        //                                    this._dailyInfo.Note = this._dailyInfo.Note + ";" + Properties.Resources.DailyInfoLeaveEarly;
        //                                }
        //                                else
        //                                {
        //                                    this._dailyInfo.Note = Properties.Resources.DailyInfoLeaveEarly;
        //                                }
        //                            }
        //                        }
        //                    }
        //                    else
        //                    {
        //                        this._dailyInfo.IsNormal = false;
        //                        if (result.ToString().Length == 4)
        //                        {
        //                            if (result > 1)
        //                            {
        //                                this._dailyInfo.LateInMinute = Convert.ToInt32(result.ToString().Substring(0, result.ToString().IndexOf('.'))) * 60 + (Convert.ToInt32(result.ToString().Substring(result.ToString().IndexOf('.') + 1, 2)));
        //                            }
        //                            this._dailyInfo.LateInMinute = Convert.ToInt32(result.ToString().Substring(result.ToString().IndexOf('.') + 1));
        //                        }
        //                        if (result.ToString().Length == 3)
        //                        {

        //                            if (result > 1)
        //                            {
        //                                this._dailyInfo.LateInMinute = Convert.ToInt32(result.ToString().Substring(0, result.ToString().IndexOf('.'))) * 60 + (Convert.ToInt32(result.ToString().Substring(result.ToString().IndexOf('.') + 1)) * 10);
        //                            }
        //                            this._dailyInfo.LateInMinute = Convert.ToInt32(result.ToString().Substring(result.ToString().IndexOf('.') + 1)) * 10;
        //                        }
        //                        if (this._dailyInfo.Note != null)
        //                        {
        //                            this._dailyInfo.Note = this._dailyInfo.Note + ";" + Properties.Resources.DailyInfoLate;
        //                        }
        //                        else
        //                        {
        //                            this._dailyInfo.Note = Properties.Resources.DailyInfoLate;
        //                        }
        //                        if (this._dailyInfo.ActualCheckOut == DateTimeParse.NullDate)
        //                        {
        //                            if (this._dailyInfo.Note != null)
        //                            {
        //                                this._dailyInfo.Note = this._dailyInfo.Note + ";" + Properties.Resources.DailyInfoLotcard;
        //                            }
        //                            else
        //                            {
        //                                this._dailyInfo.Note = Properties.Resources.DailyInfoLotcard;
        //                            }
        //                            this._dailyInfo.ActualCheckOut = null;
        //                            this._dailyInfo.DayFactor = 0;
        //                            this._dailyInfo.MonthFactor = 0;
        //                        }
        //                        else
        //                        {
        //                            if (_latestDate[i] < Convert.ToDateTime(this._dailyInfo.ShouldCheckOut.Value))
        //                            {
        //                                if (this._dailyInfo.Note != null)
        //                                {
        //                                    this._dailyInfo.Note = this._dailyInfo.Note + ";" + Properties.Resources.DailyInfoLeaveEarly;
        //                                }
        //                                else
        //                                {
        //                                    this._dailyInfo.Note = Properties.Resources.DailyInfoLeaveEarly;
        //                                }
        //                            }
        //                        }
        //                    }
        //                }
        //            }
        //            //缺失打卡
        //            else
        //            {
        //                if (this._dailyInfo.ActualCheckIn == DateTimeParse.NullDate)
        //                {
        //                    if (this._dailyInfo.Note != null)
        //                    {
        //                        this._dailyInfo.Note = this._dailyInfo.Note + ";" + Properties.Resources.DailyInfoLotcard;
        //                    }
        //                    else
        //                    {
        //                        this._dailyInfo.Note = Properties.Resources.DailyInfoLotcard;
        //                    }
        //                    this._dailyInfo.ActualCheckIn = null;
        //                    this._dailyInfo.DayFactor = 0;
        //                    this._dailyInfo.MonthFactor = 0;
        //                }
        //                if (this._dailyInfo.ActualCheckOut == DateTimeParse.NullDate)
        //                {
        //                    if (this._dailyInfo.Note != null)
        //                    {
        //                        this._dailyInfo.Note = this._dailyInfo.Note + ";" + Properties.Resources.DailyInfoLotcard;
        //                    }
        //                    else
        //                    {
        //                        this._dailyInfo.Note = Properties.Resources.DailyInfoLotcard;
        //                    }
        //                    this._dailyInfo.ActualCheckOut = null;
        //                    this._dailyInfo.DayFactor = 0;
        //                    this._dailyInfo.MonthFactor = 0;
        //                }
        //                else
        //                {
        //                    if (_latestDate[i] < Convert.ToDateTime(this._dailyInfo.ShouldCheckOut.Value))
        //                    {
        //                        if (this._dailyInfo.Note != null)
        //                        {
        //                            this._dailyInfo.Note = this._dailyInfo.Note + ";" + Properties.Resources.DailyInfoLeaveEarly;
        //                        }
        //                        else
        //                        {
        //                            this._dailyInfo.Note = Properties.Resources.DailyInfoLeaveEarly;
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //        //此处为默认情况下的日基数
        //        if (this._dailyInfo.DayFactor == null)
        //            this._dailyInfo.DayFactor = 1;
        //        if (this._dailyInfo.MonthFactor == null)
        //            this._dailyInfo.MonthFactor = 1;
        //        if (this._dailyInfo.ActualCheckOut == DateTimeParse.NullDate)
        //        {
        //            this._dailyInfo.ActualCheckOut = null;
        //            if (this._dailyInfo.Note != null)
        //            {
        //                this._dailyInfo.Note = this._dailyInfo.Note + ";" + Properties.Resources.DailyInfoLotcard;
        //            }
        //            else
        //            {
        //                this._dailyInfo.Note = Properties.Resources.DailyInfoLotcard;

        //            }
        //        }
        //        #endregion
        //        DataSet leaveInfo = this._leaveManager.GetLeaveInfoByEmployeeId(this._dailyInfo.EmployeeId, Convert.ToDateTime(this._dailyInfo.DutyDate));
        //        #region  本月员工請假集合
        //        DateTime updatetime = this._dailyManager.GetUpdateTime(Empdate, this._dailyInfo.EmployeeId);
        //        foreach (DataRow item in leaveInfo.Tables[0].Rows)
        //        {
        //            Model.HrDailyEmployeeAttendInfo dailyInfo = new Book.Model.HrDailyEmployeeAttendInfo();
        //            //判断请假日是否在上次更新和这次更新之间
        //            string dttt = Convert.ToDateTime(item[Model.Leave.PROPERTY_LEAVEDATE]).ToShortDateString();
        //            if (Convert.ToDateTime(dttt) <= Convert.ToDateTime(Empdate.ToShortDateString()) && Convert.ToDateTime(item[Model.Leave.PROPERTY_LEAVEDATE]) >= updatetime)
        //            {
        //                dailyInfo.EmployeeId = this._dailyInfo.EmployeeId;
        //                if (item[Model.LeaveType.PROPERTY_PAYRATE] != DBNull.Value)
        //                {
        //                    //病假和無薪假
        //                    if (Convert.ToInt32(item[Model.LeaveType.PROPERTY_PAYRATE]) == 0)
        //                    {
        //                        switch (Convert.ToInt32(item[Model.Leave.PROPERTY_LEAVERANGE]))
        //                        {
        //                            case 0:
        //                                dailyInfo.DayFactor = 0;
        //                                dailyInfo.MonthFactor = 1;
        //                                dailyInfo.Note = item[Model.LeaveType.PROPERTY_LEAVETYPENAME].ToString() + "(" + Properties.Resources.Allday + ")";
        //                                break;
        //                            case 1:
        //                                dailyInfo.DayFactor = 0.5;
        //                                dailyInfo.MonthFactor = 1;
        //                                dailyInfo.ShouldCheckIn = this._dailyInfo.ShouldCheckIn;
        //                                dailyInfo.ShouldCheckOut = this._dailyInfo.ShouldCheckOut;
        //                                dailyInfo.ActualCheckIn = _dailyInfo.ActualCheckIn;
        //                                dailyInfo.ActualCheckOut = _dailyInfo.ActualCheckOut;
        //                                dailyInfo.LateInMinute = this._dailyInfo.LateInMinute;
        //                                dailyInfo.SpecialBonus = this._dailyInfo.SpecialBonus;
        //                                if (this._dailyInfo.Note != null)
        //                                {
        //                                    if (this._dailyInfo.Note.Contains(Properties.Resources.Absenteeism + "(" + Properties.Resources.Morning + ")"))
        //                                    {
        //                                        string str = item[Model.LeaveType.PROPERTY_LEAVETYPENAME].ToString() + "(" + Properties.Resources.Morning + ")";
        //                                        dailyInfo.Note = this._dailyInfo.Note.Replace(Properties.Resources.Absenteeism + "(" + Properties.Resources.Morning + ")", str);
        //                                    }
        //                                }
        //                                else
        //                                    dailyInfo.Note = item[Model.LeaveType.PROPERTY_LEAVETYPENAME].ToString() + "(" + Properties.Resources.Morning + ")";
        //                                break;
        //                            case 2:
        //                                dailyInfo.DayFactor = 0.5;
        //                                dailyInfo.MonthFactor = 1;
        //                                dailyInfo.ShouldCheckIn = this._dailyInfo.ShouldCheckIn;
        //                                dailyInfo.ShouldCheckOut = this._dailyInfo.ShouldCheckOut;
        //                                dailyInfo.ActualCheckIn = _dailyInfo.ActualCheckIn;
        //                                dailyInfo.ActualCheckOut = _dailyInfo.ActualCheckOut;
        //                                dailyInfo.LateInMinute = this._dailyInfo.LateInMinute;
        //                                dailyInfo.SpecialBonus = this._dailyInfo.SpecialBonus;
        //                                if (this._dailyInfo.Note != null)
        //                                {
        //                                    if (this._dailyInfo.Note.Contains(Properties.Resources.Absenteeism + "(" + Properties.Resources.Evening + ")"))
        //                                    {
        //                                        string str = item[Model.LeaveType.PROPERTY_LEAVETYPENAME].ToString() + "(" + Properties.Resources.Evening + ")";
        //                                        dailyInfo.Note = this._dailyInfo.Note.Replace(Properties.Resources.Absenteeism + "(" + Properties.Resources.Evening + ")", str);
        //                                    }
        //                                }
        //                                else
        //                                    dailyInfo.Note = item[Model.LeaveType.PROPERTY_LEAVETYPENAME].ToString() + "(" + Properties.Resources.Evening + ")";
        //                                break;
        //                        }
        //                    }
        //                    else
        //                    {
        //                        dailyInfo.DayFactor = Convert.ToInt32(item[Model.LeaveType.PROPERTY_PAYRATE]);
        //                        dailyInfo.MonthFactor = Convert.ToInt32(item[Model.LeaveType.PROPERTY_PAYRATE]);

        //                        switch (Convert.ToInt32(item[Model.Leave.PROPERTY_LEAVERANGE]))
        //                        {
        //                            case 0:
        //                                dailyInfo.Note = item[Model.LeaveType.PROPERTY_LEAVETYPENAME].ToString() + "(" + Properties.Resources.Allday + ")";
        //                                break;
        //                            case 1:
        //                                dailyInfo.ShouldCheckIn = this._dailyInfo.ShouldCheckIn;
        //                                dailyInfo.ShouldCheckOut = this._dailyInfo.ShouldCheckOut;
        //                                dailyInfo.ActualCheckIn = _dailyInfo.ActualCheckIn;
        //                                dailyInfo.ActualCheckOut = _dailyInfo.ActualCheckOut;
        //                                dailyInfo.LateInMinute = this._dailyInfo.LateInMinute;
        //                                dailyInfo.SpecialBonus = this._dailyInfo.SpecialBonus;
        //                                if (this._dailyInfo.Note != null)
        //                                {

        //                                    if (this._dailyInfo.Note.Contains(Properties.Resources.Absenteeism + "(" + Properties.Resources.Morning + ")"))
        //                                    {
        //                                        string str = item[Model.LeaveType.PROPERTY_LEAVETYPENAME].ToString() + "(" + Properties.Resources.Morning + ")";
        //                                        dailyInfo.Note = this._dailyInfo.Note.Replace(Properties.Resources.Absenteeism + "(" + Properties.Resources.Morning + ")", str);
        //                                    }
        //                                }
        //                                else dailyInfo.Note = item[Model.LeaveType.PROPERTY_LEAVETYPENAME].ToString() + "(" + Properties.Resources.Morning + ")";

        //                                break;
        //                            case 2:
        //                                dailyInfo.ShouldCheckIn = this._dailyInfo.ShouldCheckIn;
        //                                dailyInfo.ShouldCheckOut = this._dailyInfo.ShouldCheckOut;
        //                                dailyInfo.ActualCheckIn = _dailyInfo.ActualCheckIn;
        //                                dailyInfo.ActualCheckOut = _dailyInfo.ActualCheckOut;
        //                                dailyInfo.LateInMinute = this._dailyInfo.LateInMinute;
        //                                dailyInfo.SpecialBonus = this._dailyInfo.SpecialBonus;
        //                                if (this._dailyInfo.Note != null)
        //                                {
        //                                    if (this._dailyInfo.Note.Contains(Properties.Resources.Absenteeism + "(" + Properties.Resources.Evening + ")"))
        //                                    {
        //                                        string str = item[Model.LeaveType.PROPERTY_LEAVETYPENAME].ToString() + "(" + Properties.Resources.Evening + ")";
        //                                        dailyInfo.Note = this._dailyInfo.Note.Replace(Properties.Resources.Absenteeism + "(" + Properties.Resources.Evening + ")", str);
        //                                    }
        //                                }
        //                                else
        //                                    dailyInfo.Note = item[Model.LeaveType.PROPERTY_LEAVETYPENAME].ToString() + "(" + Properties.Resources.Evening + ")";
        //                                break;
        //                        }

        //                    }
        //                    if (dailyInfo.LateInMinute == null)
        //                        dailyInfo.LateInMinute = 0;
        //                    if (dailyInfo.SpecialBonus == null)
        //                        dailyInfo.SpecialBonus = 0;
        //                    dailyInfo.DutyDate = Convert.ToDateTime(DateTime.Parse(item[Model.Leave.PROPERTY_LEAVEDATE].ToString()).ToShortDateString());
        //                }
        //                if (dailyInfo.Note != null)
        //                {
        //                    if (dailyInfo.Note.ToString().Contains(Properties.Resources.DailyInfoLate) || dailyInfo.Note.ToString().Contains(Properties.Resources.DailyInfoLotcard) || dailyInfo.Note.ToString().Contains(Properties.Resources.DailyInfoLeaveEarly))
        //                        dailyInfo.IsNormal = false;
        //                    else
        //                        dailyInfo.IsNormal = true;
        //                }
        //                else
        //                    dailyInfo.IsNormal = true;
        //                IList<Model.HrDailyEmployeeAttendInfo> sameData = this._dailyManager.SelectHrInfoByEmployeeIdAndDueDate1(this._dailyInfo.EmployeeId, Convert.ToDateTime(dailyInfo.DutyDate));
        //                if (sameData.Count < 1)
        //                {
        //                    this._dailyManager.Insert(dailyInfo);
        //                }
        //                else
        //                {
        //                    this._dailyManager.UpdateDailyInfo(_dailyInfo, sameData);
        //                }
        //            }
        //        }
        //        #endregion
        //        IList<Model.HrDailyEmployeeAttendInfo> sameDataInfo = this._dailyManager.SelectHrInfoByEmployeeIdAndDueDate1(this._dailyInfo.EmployeeId, Convert.ToDateTime(this._dailyInfo.DutyDate));
        //        if (this._dailyInfo.Note != null)
        //        {
        //            if (this._dailyInfo.Note.ToString().Contains(Properties.Resources.DailyInfoLate) || this._dailyInfo.Note.ToString().Contains(Properties.Resources.DailyInfoLotcard) || this._dailyInfo.Note.ToString().Contains(Properties.Resources.DailyInfoLeaveEarly))
        //                this._dailyInfo.IsNormal = false;
        //            else
        //                this._dailyInfo.IsNormal = true;
        //        }
        //        else
        //            this._dailyInfo.IsNormal = true;
        //        if (sameDataInfo.Count < 1)
        //        {
        //            if (this._dailyInfo.ShouldCheckIn == null && this._dailyInfo.ShouldCheckOut == null)
        //            {
        //                Model.HrDailyEmployeeAttendInfo dailyinfo = new Book.Model.HrDailyEmployeeAttendInfo();
        //                dailyinfo.EmployeeId = this._dailyInfo.EmployeeId;
        //                dailyinfo.DutyDate = this._dailyInfo.DutyDate;
        //                dailyinfo.OverTimeOff = this._dailyInfo.ActualCheckOut == null ? null : this._dailyInfo.ActualCheckOut;
        //                dailyinfo.OverTimeON = this._dailyInfo.ActualCheckIn == null ? null : this._dailyInfo.ActualCheckIn;
        //                this._dailyManager.Insert(dailyinfo);
        //            }
        //            else
        //            {
        //                this._dailyManager.Insert(this._dailyInfo);
        //            }
        //        }
        //        else
        //        {
        //            this._dailyManager.UpdateDailyInfo(this._dailyInfo, sameDataInfo);
        //        }
        //        #region 将星期添加到考勤表
        //        DataSet annualHolidayData = this._annualHolidayManager.SelectSingleAnnualInfo(Convert.ToDateTime(this._dailyInfo.DutyDate));
        //        for (int a = 0; a < annualHolidayData.Tables[0].Rows.Count; a++)
        //        {
        //            if (Convert.ToDateTime(annualHolidayData.Tables[0].Rows[a][Model.AnnualHoliday.PROPERTY_HOLIDAYDATE]).Subtract(Convert.ToDateTime(this._dailyInfo.DutyDate)).Days == 1)
        //            {
        //                if (_employee.EmployeeJoinDate != null)
        //                {
        //                    //新入職員工
        //                    if (DateTime.Now.Subtract(Convert.ToDateTime(_employee.EmployeeJoinDate)).Days > 20)
        //                    {
        //                        this._dailyInfo.DayFactor = 1;
        //                        this._dailyInfo.MonthFactor = 1;
        //                    }
        //                    else
        //                    {
        //                        this._dailyInfo.DayFactor = 0;
        //                        this._dailyInfo.MonthFactor = 0;
        //                    }
        //                }
        //                this._dailyInfo.ShouldCheckIn = null;
        //                this._dailyInfo.ShouldCheckOut = null;
        //                this._dailyInfo.ActualCheckIn = null;
        //                this._dailyInfo.ActualCheckOut = null;
        //                this._dailyInfo.SpecialBonus = 0;
        //                this._dailyInfo.LateInMinute = 0;
        //                this._dailyInfo.IsNormal = true;
        //                this._dailyInfo.DutyDate = Convert.ToDateTime(annualHolidayData.Tables[0].Rows[a][Model.AnnualHoliday.PROPERTY_HOLIDAYDATE]);
        //                this._dailyInfo.Note = annualHolidayData.Tables[0].Rows[a][Model.AnnualHoliday.PROPERTY_HOLIDAYNAME].ToString();
        //                // DataSet same = this._dailyManager.SelectHrInfoByEmployeeIdAndDueDate(this._dailyInfo.EmployeeId, Convert.ToDateTime(annualHolidayData.Tables[0].Rows[a][Model.AnnualHoliday.PROPERTY_HOLIDAYDATE]));
        //                IList<Model.HrDailyEmployeeAttendInfo> same = this._dailyManager.SelectHrInfoByEmployeeIdAndDueDate1(this._dailyInfo.EmployeeId, Convert.ToDateTime(this._dailyInfo.DutyDate));
        //                if (same.Count < 1)
        //                {
        //                    this._dailyManager.Insert(this._dailyInfo);
        //                }
        //                else
        //                {
        //                    this._dailyManager.UpdateDailyInfo(this._dailyInfo, same);
        //                }
        //            }
        //        }
        //        #endregion
        //    }
        //}
        #endregion
    }
}