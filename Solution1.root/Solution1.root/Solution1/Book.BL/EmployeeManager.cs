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
            Validate(employee);
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
            if (string.IsNullOrEmpty(employee.EmployeeId))
            {
                throw new Helper.RequireValueException(Model.Employee.PROPERTY_EMPLOYEEID);
            }
            //
            // todo:add other logic here
            //
            try
            {
                BL.V.BeginTransaction();
                //        this.Delete(employee.EmployeeId);
                employee.UpdateTime = DateTime.Now;
        

                accessor.Update(employee);
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


        void Validate(Model.Employee employee)
        {
            if (string.IsNullOrEmpty(employee.EmployeeId))
            {
                throw new Helper.RequireValueException(Model.Employee.PROPERTY_EMPLOYEEID);
            }
            if (this.HasRows(employee.EmployeeId))
            {
                throw new Helper.InvalidValueException(Model.Employee.PROPERTY_EMPLOYEEID);
            }
            if (this.HasRows(employee.EmployeeName))
            {
                throw new Helper.InvalidValueException(Model.Employee.PROPERTY_EMPLOYEENAME);
            }
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
        public Book.Model.Employee SelectByCardNo(string CardNo,DateTime dt)
        {
            return accessor.SelectByCardNo(CardNo,dt);
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
        public DataSet  SelectOnActiveDataSet()
        {
            return accessor.SelectOnActiveDataSet();    
        }
        public void UpdateDataDataSet(DataSet dataSet)
        {
            accessor.UpdateDataDataSet(dataSet);
        }

        public DataSet SelectOnActiveDataSetByEmployeeId(string employeeId)
        {
            return accessor.SelectOnActiveDataSetByEmployeeId(employeeId);
        }

        public IList<Model.Employee> selectLeaverPayActive()
        {
            return accessor.selectLeaverPayActive();
        }

        public IList<Model.Employee> selectEmployeeSearch(DateTime rzbegin, DateTime rzend, DateTime lzbegin, DateTime lzend, string type,int f)
        {
            return accessor.selectEmployeeSearch(rzbegin, rzend, lzbegin, lzend, type,f);
        }


    }
}

