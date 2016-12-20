//------------------------------------------------------------------------------
//
// file name：FlextimeManager.cs
// author: peidun
// create date：2010-2-6 10:33:08
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo. Flextime.
    /// </summary>

    public partial class FlextimeManager
    {
        private HrDailyEmployeeAttendInfoManager hrDatilyManager = new HrDailyEmployeeAttendInfoManager();
        public void Delete(string flextimeId)
        {
            accessor.Delete(flextimeId);
        }
        private void _Delete(Model.Flextime flextime)
        {
            this.Delete(flextime.FlextimeId);
            if (DateTime.Now.Date > flextime.FlexDate.Value.Date)
                hrDatilyManager.ReCheck(flextime.FlexDate.Value.Date, flextime.Employee);
        }

        public void Delete(Model.Flextime flextime)
        {
            try
            {
                BL.V.BeginTransaction();
                _Delete(flextime);
                BL.V.CommitTransaction();
            }
            catch
            {
                BL.V.RollbackTransaction();
                throw;
            }
        }
        public void DeleteFilextime(string employeeId)
        {
            accessor.DeleteFilextime(employeeId);
        }

        public void Insert(Model.Flextime flextime)
        {
            accessor.Insert(flextime);
            hrDatilyManager.ReCheck(flextime.FlexDate.Value.Date, flextime.Employee);
        }

        public void Update(Model.Flextime flextime)
        {
            accessor.Update(flextime);
        }

        //查询员工某日的排班记录
        public Model.Flextime getbyempiddate(string empid, DateTime flexdate)
        {
            return accessor.getbyempiddate(empid, flexdate);
        }

        public bool selectbyempiddate(string empid, DateTime flexdate)
        {
            return accessor.selectbyempiddate(empid, flexdate);
        }

        //去除以存在条件
        public void selectbyempListdate(IList<Model.Employee> emplist, DateTime flexdate)
        {
            for (int i = 0; i < emplist.Count; i++)
            {
                if (this.selectbyempiddate(emplist[i].EmployeeId, flexdate))
                {
                    emplist.Remove(emplist[i]);
                    if (i == 0)
                        i = -1;
                    else
                        i = i - 1;
                }
            }
        }

        public IList<Model.Flextime> getByempid(DateTime flexdate)
        {
            return accessor.getByempid(flexdate);
        }

        //根据员工编号查询
        public IList<Model.Flextime> getByempid(string employeeId)
        {
            return accessor.getByempid(employeeId);
        }

        //根据员工编号和日期删除
        public void DeleteByEmpidDate(string empid, DateTime date)
        {
            accessor.DeleteByEmpidDate(empid, date);
        }

        public void Insert(Model.Flextime flextime, IList<Model.Employee> emplist)
        {
            bool flag = false;

            try
            {
                BL.V.BeginTransaction();

                foreach (Model.Employee emp in emplist)
                {
                    Model.Flextime ft = new Book.Model.Flextime();
                    if (flag)
                        ft.FlextimeId = Guid.NewGuid().ToString();
                    else
                        ft.FlextimeId = flextime.FlextimeId;

                    ft.BusinessHours = flextime.BusinessHours;
                    ft.BusinessHoursId = flextime.BusinessHoursId;
                    ft.EmployeeId = emp.EmployeeId;
                    ft.FlexDate = flextime.FlexDate;
                    accessor.Insert(ft);
                    if (DateTime.Now.Date > flextime.FlexDate.Value.Date)
                        hrDatilyManager.ReCheck(flextime.FlexDate.Value.Date, flextime.Employee);
                    flag = true;
                }

                BL.V.CommitTransaction();
            }
            catch (Exception ex)
            {
                BL.V.RollbackTransaction();
                throw ex;
            }
        }

        /// <summary>
        /// 获取存在弹性排班记录
        /// </summary>
        /// <param name="empids">员工列表</param>
        /// <param name="dates">日期列表</param>
        /// <returns></returns>
        public IList<Model.Flextime> selectByEmpidsAndDates(string empids, string dates)
        {
            return accessor.selectByEmpidsAndDates(empids, dates);
        }
    }
}

