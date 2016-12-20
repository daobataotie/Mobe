//------------------------------------------------------------------------------
//
// file name：LeaveManager.cs
// author: peidun
// create date：2010-3-16 16:05:47
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.Leave.
    /// </summary>
    public partial class LeaveManager
    {
        private HrDailyEmployeeAttendInfoManager hrDatilyManager = new HrDailyEmployeeAttendInfoManager();

        public void Delete(string leaveId)
        {
            accessor.Delete(leaveId);
        }

        public void Delete(Model.Leave Leave)
        {
            try
            {
                BL.V.BeginTransaction();
                this.Delete(Leave.LeaveId);
                hrDatilyManager.ReCheck(Leave.LeaveDate.Value.Date, Leave.Employee);
                BL.V.CommitTransaction();
            }
            catch (Exception ex)
            {
                BL.V.RollbackTransaction();
                throw ex;
            }
        }

        public void Insert(Model.Leave leave, IList<Model.Employee> empList)
        {
            Validate(leave);

            //for (int i = 0; i < a; i++)
            //{
            //    if (i > 0)
            //    {
            //        leave.LeaveId = Guid.NewGuid().ToString();
            //        leave.LeaveDate = leave.LeaveDate.Value.Date.AddDays(1);
            //    }
            //    leave.LeaveRange = 0;
            //    accessor.Insert(leave);
            //}
            //if (a < leave.LeaveDateCount.Value)
            //{
            //    leave.LeaveId = Guid.NewGuid().ToString();
            //    leave.LeaveDate = leave.LeaveDate.Value.Date.AddDays(1);
            //    leave.LeaveRange = 1;
            //    accessor.Insert(leave);
            //}

            IList<Model.Leave> mLeaveList;
            StringBuilder sb = new StringBuilder();
            foreach (Model.Employee emp in empList)
            {
                sb.Append("'" + emp.EmployeeId + "',");
            }
            mLeaveList = this.SelectLeaveListbyEmp(sb.ToString().Substring(0, sb.ToString().Length - 1));

            //获得假期列表
            IList<Model.AnnualHoliday> holidaylist = new BL.AnnualHolidayManager().SelectBigThanDate(DateTime.Parse(DateTime.Now.Date.AddMonths(-1).ToString("yyyy-MM-01")));

            bool cando;
            try
            {
                BL.V.BeginTransaction();
                Model.Leave model;
                int flag = 0;

                decimal a = Math.Truncate(leave.LeaveDateCount.Value);

                foreach (Model.Employee emp in empList)
                {
                    if (a > 1)
                    {
                        for (int i = 0; i < a; i++)
                        {
                            cando = holidaylist.Any(mel => mel.HolidayDate.Value.Date == leave.LeaveDate.Value.AddDays(i).Date);
                            if (cando)
                                continue;
                            cando = mLeaveList.Any(mle => mle.LeaveDate.Value.ToString("yyyy-MM-dd") == leave.LeaveDate.Value.AddDays(i).ToString("yyyy-MM-dd") && mle.LeaveRange.Value == leave.LeaveRange.Value && mle.EmployeeId == emp.EmployeeId);
                            if (cando)
                                continue;
                            model = new Book.Model.Leave();
                            model.EmployeeId = emp.EmployeeId;
                            model.LeaveDate = leave.LeaveDate.Value.Date.AddDays(i);
                            model.LeaveDateCount = leave.LeaveDateCount;
                            if (flag == 0)
                                model.LeaveId = leave.LeaveId;
                            else
                                model.LeaveId = Guid.NewGuid().ToString();
                            model.LeaveRange = leave.LeaveRange;
                            model.LeaveText = leave.LeaveText;
                            model.LeaveTypeId = leave.LeaveTypeId;
                            accessor.Insert(model);
                            //增加列表
                            //mLeaveList.Add(model);
                            if (DateTime.Now.Date > model.LeaveDate)
                                hrDatilyManager.ReCheck(leave.LeaveDate.Value.Date, leave.Employee);
                            flag = 1;
                        }
                    }
                    else
                    {
                        cando = holidaylist.Any(mel => mel.HolidayDate.Value.Date == leave.LeaveDate.Value.Date);
                        if (cando)
                            throw new Helper.InvalidValueException(Model.Leave.PRO_LeaveDate + "_IsHoliday");
                        cando = mLeaveList.Any(mle => mle.LeaveDate.Value.ToString("yyyy-MM-dd") == leave.LeaveDate.Value.ToString("yyyy-MM-dd") && mle.LeaveRange.Value == leave.LeaveRange.Value && mle.EmployeeId == emp.EmployeeId);
                        if (cando)
                            throw new Helper.InvalidValueException(Model.Leave.PRO_LeaveId + "_1");

                        model = new Book.Model.Leave();
                        model.EmployeeId = emp.EmployeeId;
                        model.LeaveDate = leave.LeaveDate.Value.Date;
                        model.LeaveDateCount = leave.LeaveDateCount;
                        if (flag == 0)
                            model.LeaveId = leave.LeaveId;
                        else
                            model.LeaveId = Guid.NewGuid().ToString();
                        model.LeaveRange = leave.LeaveRange;
                        model.LeaveText = leave.LeaveText;
                        model.LeaveTypeId = leave.LeaveTypeId;
                        accessor.Insert(model);
                        //增加列表项
                        //mLeaveList.Add(model);
                        hrDatilyManager.ReCheck(leave.LeaveDate.Value.Date, leave.Employee);
                        flag = 1;
                    }
                }
                BL.V.CommitTransaction();
            }
            catch (Exception ex)
            {
                BL.V.RollbackTransaction();
                throw;
            }


        }

        public void Insert(Model.Leave leave)
        {
            Validate(leave);
            Model.Leave model;
            int flag = 0;

            decimal a = Math.Truncate(leave.LeaveDateCount.Value);

            IList<Model.Leave> mLeaveList = this.SelectLeaveListbyEmp("'" + leave.EmployeeId + "'");

            //获得假期列表
            IList<Model.AnnualHoliday> holidaylist = new BL.AnnualHolidayManager().SelectBigThanDate(DateTime.Parse(DateTime.Now.Date.AddMonths(-1).ToString("yyyy-MM-01")));

            bool cando;

            #region 注释
            //decimal a = Math.Truncate(leave.LeaveDateCount.Value);
            //for (int i = 0; i < a; i++)
            //{
            //    if (i > 0)
            //    {
            //        leave.LeaveId = Guid.NewGuid().ToString();
            //        leave.LeaveDate = leave.LeaveDate.Value.Date.AddDays(1);
            //    }
            //    leave.LeaveRange = 0;
            //    accessor.Insert(leave);
            //}
            //if (a < leave.LeaveDateCount.Value)
            //{
            //    leave.LeaveId = Guid.NewGuid().ToString();
            //    leave.LeaveDate = leave.LeaveDate.Value.Date.AddDays(1);
            //    leave.LeaveRange = 1;
            //    accessor.Insert(leave);
            //}
            #endregion

            try
            {
                BL.V.BeginTransaction();
                if (a > 1)
                {
                    for (int i = 0; i < a; i++)
                    {
                        cando = holidaylist.Any(mel => mel.HolidayDate.Value.Date == leave.LeaveDate.Value.AddDays(i).Date);
                        if (cando)
                            continue;
                        cando = mLeaveList.Any(mle => mle.LeaveDate.Value.ToString("yyyy-MM-dd") == leave.LeaveDate.Value.AddDays(i).ToString("yyyy-MM-dd") && mle.LeaveRange.Value == leave.LeaveRange.Value);
                        if (cando)
                            continue;
                        model = new Book.Model.Leave();
                        model.EmployeeId = leave.EmployeeId;
                        model.LeaveDate = leave.LeaveDate.Value.Date.AddDays(i);
                        model.LeaveDateCount = leave.LeaveDateCount;
                        if (flag == 0)
                            model.LeaveId = leave.LeaveId;
                        else
                            model.LeaveId = Guid.NewGuid().ToString();
                        model.LeaveRange = leave.LeaveRange;
                        model.LeaveText = leave.LeaveText;
                        model.LeaveTypeId = leave.LeaveTypeId;
                        accessor.Insert(model);
                        //增加列表项
                        //mLeaveList.Add(model);
                        if (DateTime.Now.Date > model.LeaveDate)
                            hrDatilyManager.ReCheck(leave.LeaveDate.Value.Date, leave.Employee);
                        flag = 1;
                    }
                }
                else
                {
                    cando = holidaylist.Any(mel => mel.HolidayDate.Value.Date == leave.LeaveDate.Value.Date);
                    if (cando)
                        throw new Helper.InvalidValueException(Model.Leave.PRO_LeaveDate + "_IsHoliday");
                    cando = mLeaveList.Any(mle => mle.LeaveDate.Value.ToString("yyyy-MM-dd") == leave.LeaveDate.Value.ToString("yyyy-MM-dd") && mle.LeaveRange.Value == leave.LeaveRange.Value);
                    if (cando)
                        throw new Helper.InvalidValueException(Model.Leave.PRO_LeaveId + "_1");
                    accessor.Insert(leave);
                    hrDatilyManager.ReCheck(leave.LeaveDate.Value.Date, leave.Employee);
                }
                BL.V.CommitTransaction();
            }
            catch (Exception ex)
            {
                BL.V.RollbackTransaction();
                throw ex;
            }
        }

        //请假修改.需要得知会修改成的日期间隔.以便删除本身之外.删除原有
        public void Update(Model.Leave leave)
        {
            Validate(leave);

            //首先删除本条
            this.Delete(leave.LeaveId);
            //删除日期区间内所有
            if (Math.Truncate(leave.LeaveDateCount.Value) > 1)
            {
                DateTime startdate = leave.LeaveDate.Value;
                DateTime enddate = leave.LeaveDate.Value.AddDays(double.Parse((leave.LeaveDateCount.Value - 1).ToString()));
                accessor.DeleteByDateRange(startdate, enddate, leave.EmployeeId);
            }


            this.Insert(leave);

            #region 原有修改方法
            //IList<Model.Leave> mLeaveList = this.SelectLeaveListbyEmp(leave.EmployeeId);
            //bool cando;
            ////获得假期列表
            //IList<Model.AnnualHoliday> holidaylist = new BL.AnnualHolidayManager().SelectBigThanDate(DateTime.Parse(DateTime.Now.Date.AddMonths(-1).ToString("yyyy-MM-01")));
            //cando = holidaylist.Any(mel => mel.HolidayDate.Value.Date == leave.LeaveDate.Value.Date);
            //if (cando)
            //    throw new Helper.InvalidValueException(Model.Leave.PRO_LeaveDate + "_IsHoliday");
            //cando = mLeaveList.Any(mle => mle.LeaveDate.Value.ToString("yyyy-MM-dd") == leave.LeaveDate.Value.ToString("yyyy-MM-dd") && mle.LeaveRange.Value == leave.LeaveRange.Value && mle.LeaveId != leave.LeaveId);
            //if (cando)
            //    throw new Helper.InvalidValueException(Model.Leave.PRO_LeaveId + "_1");
            //accessor.Update(leave);
            #endregion
        }

        //返回前半年以后的所有请假记录
        public IList<Model.Leave> SelectLeaveListbyEmp(string empid)
        {
            return accessor.SelectLeaveListbyEmp(empid);
        }

        public IList<Model.Leave> Getleavebyempiddate(string empid, DateTime starttime, DateTime endtime)
        {
            return accessor.Getleavebyempiddate(empid, starttime, endtime);
        }

        private void Validate(Model.Leave leave)
        {
            if (string.IsNullOrEmpty(leave.EmployeeId))
            {
                throw new Helper.RequireValueException(Model.Leave.PRO_EmployeeId);
            }
            if (string.IsNullOrEmpty(leave.LeaveTypeId))
            {
                throw new Helper.RequireValueException(Model.Leave.PRO_LeaveTypeId);
            }

        }

        public System.Data.DataSet GetLeaveInfoByEmployeeId(string employeeId, DateTime leaveDate)
        {
            return accessor.GetLeaveInfoByEmployeeId(employeeId, leaveDate);
        }

        public System.Data.DataSet GetLeaveInfoByEmployeeId(string employeeId, string year)
        {
            return accessor.GetLeaveInfoByEmployeeId(employeeId, year);
        }

        /// <summary>
        /// 月总请假总基数 包括无薪假，病假
        /// </summary>
        /// <param name="employee"></param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        public decimal SelectCountByMonthEmp(Model.Employee employee, int year, int month)
        {
            return accessor.SelectCountByMonthEmp(employee, year, month);
        }

        /// <summary>
        /// 倒扣款总基数和
        /// </summary>
        /// <param name="employee"></param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        public decimal SelectPunishByMonthEmp(Model.Employee employee, int year, int month)
        {
            return accessor.SelectPunishByMonthEmp(employee, year, month);
        }

        /// <summary>
        /// 出勤记录中  月实际假日总数 不包括劳动，清明等
        /// </summary>
        /// <param name="employee"></param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        public decimal SelectTotalHolidayMonthEmp(Model.Employee employee, int year, int month)
        {
            return accessor.SelectTotalHolidayMonthEmp(employee, year, month);
        }

        /// <summary>
        /// 出勤月记录中 假日(劳动 清明 等)总数,不包含周日等年假
        /// </summary>
        /// <param name="employee"></param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        public int SelectSpecificHolidayMonthEmp(Model.Employee employee, int year, int month)
        {
            return accessor.SelectSpecificHolidayMonthEmp(employee, year, month);
        }

        public IList<Model.Leave> Getleavebyempidmonth(string empid, int? year, int? month)
        {
            return accessor.Getleavebyempidmonth(empid, year, month);
        }

        //查询员工请假记录
        public IList<Model.Leave> GetEmployeeLeavebyDate(string EmpID, DateTime LeaveDate)
        {
            return accessor.GetEmployeeLeavebyDate(EmpID, LeaveDate.Date);
        }

        //查询当前员工今年最后一个请假记录
        public Model.Leave GetLastForEmployeeYear(string EmpID, DateTime LeaveYear)
        {
            return accessor.GetLastForEmployeeYear(EmpID, LeaveYear);
        }

        public Book.Model.Leave GetNextForEmployeeYear(string EmpID, DateTime LeaveYear)
        {
            return accessor.GetNextForEmployeeYear(EmpID, LeaveYear);
        }

        public Book.Model.Leave GetPrevForEmployeeYear(string EmpID, DateTime LeaveYear)
        {
            return accessor.GetPrevForEmployeeYear(EmpID, LeaveYear);

        }

        public IList<Model.Leave> SelectForMonthListPrint(DateTime startdate, DateTime enddate)
        {
            return accessor.SelectForMonthListPrint(startdate, enddate);
        }

        public void DeleteByDateRangeEmp(IList<Model.Employee> emplist, DateTime startDate, DateTime enddate)
        {
            accessor.DeleteByDateRangeEmp(emplist, startDate, enddate);
        }

        //按日期区间查询员工请假记录
        public IList<Model.Leave> SelectByDateRangeEmp(string emps, DateTime startdate, DateTime enddate)
        {
            return accessor.SelectByDateRangeEmp(emps, startdate, enddate);
        }
    }
}

