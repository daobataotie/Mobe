//------------------------------------------------------------------------------
//
// file name：EmployeeManager.cs
// author: peidun
// create date：2008/6/6 10:00:59
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Data;
namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.Employee.
    /// </summary>
    public partial class EmployeeManager : BaseManager
    {
        private static readonly DA.IFamilyMembersAccessor familyMembersAccessor = (DA.IFamilyMembersAccessor)Accessors.Get("FamilyMembersAccessor");

        /// <summary>
        /// Delete Employee by primary key.
        /// </summary>
        public void Delete(string employeeId)
        {
            //
            // todo:add other logic here
            //
            accessor.Delete(employeeId);
        }

        public void Delete(Model.Employee employee)
        {
            this.Delete(employee.EmployeeId);
        }

        /// <summary>
        /// Insert a Employee.
        /// </summary>
        public void Insert(Model.Employee employee)
        {
            Validate_add(employee);
            //
            // todo:add other logic here
            //
            try
            {
                BL.V.BeginTransaction();
                employee.InsertTime = DateTime.Now;
                //employee.RoleId = employee.Role.RoleId;
                _Insert(employee);

                string invoiceKind = "emp";

                DateTime now = DateTime.Now;

                string sequencekey_y = string.Format("{0}-y-{1}", invoiceKind, now.Year);
                string sequencekey_m = string.Format("{0}-m-{1}-{2}", invoiceKind, now.Year, now.Month);
                string sequencekey_d = string.Format("{0}-d-{1}", invoiceKind, now.ToString("yyyy-MM-dd"));
                string sequencekey = string.Format(invoiceKind);

                SequenceManager.Increment(sequencekey_y);
                SequenceManager.Increment(sequencekey_m);
                SequenceManager.Increment(sequencekey_d);
                SequenceManager.Increment(sequencekey);

                BL.V.CommitTransaction();
            }
            catch (Exception ex)
            {
                BL.V.RollbackTransaction();
                throw ex;
            }
        }

        private static void _Insert(Model.Employee employee)
        {
            if (employee.AcademicBackGround != null)
                employee.AcademicBackGroundId = employee.AcademicBackGround.AcademicBackGroundId;
            if (employee.Bank != null)
                employee.BankId = employee.Bank.BankId;
            if (employee.BusinessHours != null)
                employee.BusinessHoursId = employee.BusinessHours.BusinessHoursId;
            if (employee.Company != null)
                employee.CompanyId = employee.Company.CompanyId;
            if (employee.Department != null)
                employee.DepartmentId = employee.Department.DepartmentId;
            if (employee.Duty != null)
                employee.DutyId = employee.Duty.DutyId;
            accessor.Insert(employee);

            foreach (Model.FamilyMembers member in employee.FamilyMembers)
            {
                member.EmployeeId = employee.EmployeeId;
                familyMembersAccessor.Insert(member);
            }
        }

        /// <summary>
        /// Update a Employee.
        /// </summary>
        public void Update(Model.Employee employee)
        {
            Validate_update(employee);
            try
            {
                BL.V.BeginTransaction();
                //this.Delete(employee.EmployeeId);
                employee.UpdateTime = DateTime.Now;
                DateTime? joinOld = accessor.Get(employee.EmployeeId).EmployeeJoinDate;
                DateTime? limitDate;  //最后考勤日
                if (DateTime.Now >= DateTime.Parse(DateTime.Now.ToShortDateString() + " 12:50"))
                {
                    limitDate = DateTime.Parse(DateTime.Now.AddDays(-1).ToShortDateString());
                }
                else
                {
                    limitDate = DateTime.Parse(DateTime.Now.AddDays(-2).ToShortDateString());
                }

                if (employee.EmployeeLeaveDate != null || joinOld.Value.Date != employee.EmployeeJoinDate.Value.Date)
                {


                    //已經自動考勤過了，所以必需再重新考勤,到职日期调后,之前记录也删除
                    //如果 大于旧 到职日 并且旧的  已经考勤过  删除 新的之前  考勤记录

                    if ((employee.EmployeeLeaveDate != null && limitDate >= employee.EmployeeLeaveDate.Value) || (joinOld.Value.Date != employee.EmployeeJoinDate.Value.Date && (employee.EmployeeJoinDate.Value.Date > joinOld.Value.Date && joinOld.Value.Date <= limitDate)))
                    {
                        new BL.HrDailyEmployeeAttendInfoManager().DeleteForChangeLeaveDateEmpHrDay(employee);
                    }

                    if (employee.EmployeeJoinDate.Value.Date < joinOld.Value.Date && employee.EmployeeJoinDate.Value.Date <= limitDate)
                    {
                        //如果 大于旧 到职日 并且 新的  已经考勤过   重新 中间 进行考勤 
                        TimeSpan t = joinOld.Value.Date - employee.EmployeeJoinDate.Value.Date;
                        for (int i = 0; i < t.Days; i++)
                            new BL.HrDailyEmployeeAttendInfoManager().Reatten_Controller(employee.EmployeeJoinDate.Value.Date.AddDays(i), employee);
                    }
                }

                accessor.Update(employee);
                //修改家属
                familyMembersAccessor.DeleteByEmployeeId(employee.EmployeeId);
                foreach (Model.FamilyMembers member in employee.FamilyMembers)
                {
                    member.EmployeeId = employee.EmployeeId;
                    familyMembersAccessor.Insert(member);
                }
                BL.V.CommitTransaction();
            }
            catch (Exception ex)
            {
                BL.V.RollbackTransaction();
                throw ex;
            }
        }

        public IList<Model.Employee> SelectOnActive()
        {
            return accessor.SelectOnActive();
        }

        public IList<Model.Employee> SelectLeaveActive()
        {
            return accessor.SelectLeaveJob();
        }

        void Validate_add(Model.Employee employee)
        {
            if (this.ExistsExceptInsert(employee))
            {
                throw new Helper.InvalidValueException(Model.Employee.PROPERTY_IDNO);
            }
            #region 非空验证
            if (string.IsNullOrEmpty(employee.IDNo))
            {
                throw new Helper.RequireValueException(Model.Employee.PROPERTY_IDNO);
            }
            if (string.IsNullOrEmpty(employee.EmployeeName))
            {
                throw new Helper.RequireValueException(Model.Employee.PROPERTY_EMPLOYEENAME);
            }
            if (string.IsNullOrEmpty(employee.EmployeeGender.ToString()))
            {
                throw new Helper.RequireValueException(Model.Employee.PROPERTY_EMPLOYEEGENDER);
            }
            if (!employee.EmployeeJoinDate.HasValue)
            {
                throw new Helper.RequireValueException(Model.Employee.PROPERTY_EMPLOYEEJOINDATE);
            }
            #endregion
            //编号判断
            try
            {
                int.Parse(employee.IDNo.Substring(1, 2));
            }
            catch
            {
                throw new Helper.InvalidValueException(Model.Employee.PROPERTY_IDNO + "1");
            }

        }

        void Validate_update(Model.Employee employee)
        {
            if (this.ExistsExceptUpdate(employee))
            {
                throw new Helper.InvalidValueException(Model.Employee.PROPERTY_IDNO);
            }
            #region 非空验证
            if (string.IsNullOrEmpty(employee.IDNo))
            {
                throw new Helper.RequireValueException(Model.Employee.PROPERTY_IDNO);
            }
            if (string.IsNullOrEmpty(employee.EmployeeName))
            {
                throw new Helper.RequireValueException(Model.Employee.PROPERTY_EMPLOYEENAME);
            }
            if (string.IsNullOrEmpty(employee.EmployeeGender.ToString()))
            {
                throw new Helper.RequireValueException(Model.Employee.PROPERTY_EMPLOYEEGENDER);
            }
            if (!employee.EmployeeJoinDate.HasValue)
            {
                throw new Helper.RequireValueException(Model.Employee.PROPERTY_EMPLOYEEJOINDATE);
            }
            #endregion
            //编号判断
            try
            {
                int.Parse(employee.IDNo.Substring(1, 2));
            }
            catch
            {
                throw new Helper.InvalidValueException(Model.Employee.PROPERTY_IDNO + "1");
            }
        }

        /// <summary>
        /// 查找员工编号
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Model.Employee GetOldIDbyEmpID(string id)
        {
            return accessor.GetOldIDbyEmpID(id);
        }

        public bool ExistsExceptUpdate(Model.Employee emp)
        {
            return accessor.ExistsExceptUpdate(emp);
        }

        /// <summary>
        /// 添加时验证是否存在
        /// </summary>
        /// <param name="emp"></param>
        /// <returns></returns>
        public bool ExistsExceptInsert(Model.Employee emp)
        {
            return accessor.ExistsExceptInsert(emp);
        }

        public Book.Model.Employee GetByOperatorName(string name)
        {
            return accessor.GetByOperatorName(name);
        }

        public Model.Employee GetbyIdNo(string idno)
        {
            return accessor.GetbyIdNo(idno);
        }

        //protected override string GetInvoiceKind()
        //{
        //    return "Employee";
        //}

        //protected override string GetSettingId()
        //{
        //    return "EmployeeNumberRule";
        //}

        /// <summary>
        /// Select by primary key.
        /// </summary>		
        public Model.Employee Get(string employeeId)
        {
            Model.Employee employee = accessor.Get(employeeId);
            if (employee != null)
            {
                employee.FamilyMembers = familyMembersAccessor.Select(employee);
            }
            return employee;
        }

        public IList<Model.Employee> Select(string _roleId)
        {
            return accessor.Select(_roleId);
        }

        public IList<Book.Model.Employee> Select(Model.Department department)
        {
            return accessor.Select(department);
        }

        public Book.Model.Employee SelectByCardNo(string CardNo, DateTime dt)
        {
            return accessor.SelectByCardNo(CardNo, dt);
        }

        /// <summary>
        /// 根據編號首字符查詢
        /// </summary>
        /// <param name="charno"></param>
        /// <returns></returns>
        public IList<Model.Employee> SelectbyIDsubstring(string charno)
        {
            return accessor.SelectbyIDsubstring(charno);
        }

        public IList<Model.Employee> SelectbyPinYin(string pinyin)
        {
            return accessor.SelectbyPinYin(pinyin);
        }

        public DataTable SelectPinyin()
        {
            return accessor.SelectPinyin();
        }

        public DataSet SelectOnActiveDataSet()
        {
            return accessor.SelectOnActiveDataSet();
        }

        public void UpdateDataDataSet(DataSet dataSet)
        {
            try
            {
                BL.V.BeginTransaction();
                accessor.UpdateDataDataSet(dataSet);
                BL.V.CommitTransaction();
            }
            catch (Exception ex)
            {
                BL.V.RollbackTransaction();
                throw ex;
            }
        }

        public DataSet SelectOnActiveDataSetByEmployeeId(string employeeId)
        {
            return accessor.SelectOnActiveDataSetByEmployeeId(employeeId);
        }

        public IList<Model.Employee> selectLeaverPayActive()
        {
            return accessor.selectLeaverPayActive();
        }

        public IList<Model.Employee> selectEmployeeSearch(DateTime rzbegin, DateTime rzend, DateTime lzbegin, DateTime lzend, string type, int f)
        {
            return accessor.selectEmployeeSearch(rzbegin, rzend, lzbegin, lzend, type, f);
        }

        /// <summary>
        /// 根据日前查询 符合 月份发薪资的人员
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public IList<Model.Employee> SelectHrDailyAttend(DateTime date)
        {
            return accessor.SelectHrDailyAttend(date);
        }

        /// <summary>
        /// 更新出勤记录查出员工列表
        /// </summary>
        /// <param name="CheckDateTime"></param>
        /// <returns></returns>
        public IList<Model.Employee> DailyEmployeeAttendInfo_EmpList(DateTime CheckDateTime)
        {
            return accessor.DailyEmployeeAttendInfo_EmpList(CheckDateTime);
        }

        /// <summary>
        /// 获取选定日期内还在职员工列表
        /// </summary>
        /// <param name="mdate">截止日期</param>
        /// <returns></returns>
        public IList<Model.Employee> GetHasThereEmp_ListByDateTime(DateTime mdate)
        {
            return accessor.GetHasThereEmp_ListByDateTime(mdate);
        }

        #region 分类构建
        public Model.Employee mGetFirst()
        {
            return accessor.mGetFirst();
        }

        public Model.Employee mGetLast()
        {
            return accessor.mGetLast();
        }

        public Model.Employee mGetPrev(Model.Employee emp)
        {
            return accessor.mGetPrev(emp);
        }

        public Model.Employee mGetNext(Model.Employee emp)
        {
            return accessor.mGetNext(emp);
        }

        public bool mHasRows()
        {
            return accessor.mHasRows();
        }

        public bool mHasRowsBefore(Model.Employee emp)
        {
            return accessor.mHasRowsBefore(emp);
        }

        public bool mHasRowsAfter(Model.Employee emp)
        {
            return accessor.mHasRowsAfter(emp);
        }
        #endregion

        public IList<Model.Employee> SelectIdAndName()
        {
            return accessor.SelectIdAndName();
        }
    }
}

