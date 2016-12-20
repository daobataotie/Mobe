//------------------------------------------------------------------------------
//
// file name：OverTimeManager.cs
// author: peidun
// create date：2010-3-20 11:59:55
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.OverTime.
    /// </summary>
    public partial class OverTimeManager : BaseManager
    {

        public void Delete(Model.OverTime overtime)
        {
            accessor.Delete(overtime.OverTimeId);
        }

        public void Insert(Model.OverTime overTime)
        {
            Validate(overTime);
            overTime.InsertTime = DateTime.Now;
            accessor.Insert(overTime);
        }

        public void InsertList(Model.OverTime overTime, IList<Model.Employee> emps)
        {
            for (int i = 0; i < emps.Count; i++)
            {
                Model.OverTime ot = new Model.OverTime();
                if (i == 0)
                    ot.OverTimeId = overTime.OverTimeId;
                else
                    ot.OverTimeId = Guid.NewGuid().ToString();
                ot.DueDate = overTime.DueDate;
                ot.EoverTime = overTime.EoverTime;
                ot.IsHoliday = overTime.IsHoliday;
                ot.EmployeeId = emps[i].EmployeeId;

                if (!ot.IsHoliday)//平日加班
                {
                    if (emps[i].DailyPay > 0)
                    {
                        ot.OverTimeFee = emps[i].DailyPay.Value / 6 * decimal.Parse(ot.EoverTime.ToString());
                    }
                    if (emps[i].DailyPay == 0 && emps[i].MonthlyPay > 0)
                    {
                        ot.OverTimeFee = emps[i].MonthlyPay.Value / DateTime.DaysInMonth(ot.DueDate.Year, ot.DueDate.Month) / 6 * decimal.Parse(ot.EoverTime.ToString());
                    }
                }
                else  //假日加班
                {
                    if (emps[i].DailyPay > 0)
                    {
                        ot.OverTimeFee = emps[i].DailyPay.Value / 2 * 3 / 8 * decimal.Parse(ot.EoverTime.ToString());
                    }
                    if (emps[i].DailyPay == 0 && emps[i].MonthlyPay > 0)
                    {
                        ot.OverTimeFee = emps[i].MonthlyPay.Value / DateTime.DaysInMonth(ot.DueDate.Year, ot.DueDate.Month) / 2 * 3 / 8 * decimal.Parse(ot.EoverTime.ToString());
                    }
                }


                //if (emps[i].IsCadre.HasValue && emps[i].IsCadre.Value)
                //{
                //    if (overTime.EoverTime >= 2)
                //        ot.OverTimeBonus = 40;
                //    else
                //        ot.OverTimeBonus = 0;
                //}
                //else
                //{
                //    if (overTime.EoverTime >= 3)
                //        ot.OverTimeBonus = 40;
                //    else
                //        ot.OverTimeBonus = 0;
                //}

                ot.OverTimeBonus = overTime.OverTimeBonus;

                ot.Note = overTime.Note;

                this.Insert(ot);
            }
        }

        public void Update(Model.OverTime overTime)
        {
            //
            // todo: add other logic here.
            //
            Validate(overTime);
            overTime.UpdateTime = DateTime.Now;
            accessor.Update(overTime);
        }

        private void Validate(Model.OverTime overtime)
        {
            if (string.IsNullOrEmpty(overtime.EmployeeId))
            {
                throw new Helper.RequireValueException(Model.OverTime.PROPERTY_EMPLOYEEID);
            }
        }

        public IList<Model.OverTime> GetOverTimebyempiddate(string empid, DateTime starttime, DateTime endtime)
        {
            return accessor.GetOverTimebyempiddate(empid, starttime, endtime);
        }

        public System.Data.DataSet SelectOverTimeInfoByEmployeeId(string employeeId, DateTime dueDate)
        {
            return accessor.SelectOverTimeInfoByEmployeeId(employeeId, dueDate);
        }

        public IList<Model.OverTime> SelectByEmployeeAndMonth(Model.Employee employee, int year, int month)
        {
            return accessor.SelectByEmployeeAndMonth(employee, year, month);

        }

        //根据日期
        public System.Data.DataSet selectOverTimebyDate(string IDNo, int year, int month)
        {
            return accessor.selectOverTimebyDate(IDNo, year, month);
        }

        //查询所有
        public System.Data.DataSet selectOverTime()
        {
            return accessor.selectOverTime();
        }

        public Book.Model.OverTime GetLastForEmployeeYearMonth(string EmpID, int year, int month)
        {
            return accessor.GetLastForEmployeeYearMonth(EmpID, year, month);
        }

        public Book.Model.OverTime GetNextForEmployeeYearMonth(string EmpID, DateTime date, int year, int month)
        {
            return accessor.GetNextForEmployeeYearMonth(EmpID, date, year, month);
        }

        public Book.Model.OverTime GetPrevForEmployeeYearMonth(string EmpID, DateTime date, int year, int month)
        {
            return accessor.GetPrevForEmployeeYearMonth(EmpID, date, year, month);

        }

        public IList<Model.OverTime> SelectOverTimeList(DateTime startdate, DateTime enddate)
        {
            return accessor.SelectOverTimeList(startdate, enddate);
        }

        //判断是否有重复记录
        public bool JudgeRepeater(string empids, int Empcount, DateTime JudgeDate)
        {
            return accessor.JudgeRepeater(empids, Empcount, JudgeDate);
        }
    }
}

