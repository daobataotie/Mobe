//------------------------------------------------------------------------------
//
// file name:EmployeeAccessor.cs
// author: peidun
// create date:2008/6/6 10:00:49
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Book.DA.SQLServer
{
    /// <summary>
    /// Data accessor of Employee
    /// </summary>
    public partial class EmployeeAccessor : EntityAccessor, IEmployeeAccessor
    {
        public IList<Book.Model.Employee> SelectOperators()
        {
            return sqlmapper.QueryForList<Model.Employee>("Employee.select_operators", null);
        }

        public Book.Model.Employee GetByOperatorName(string name)
        {
            return sqlmapper.QueryForObject<Model.Employee>("Employee.get_byName", name);
        }

        public Book.Model.Employee GetbyIdNo(string idno)
        {
            return sqlmapper.QueryForObject<Model.Employee>("Employee.getbyIdNo", idno);
        }

        public IList<Model.Employee> SelectOnActive()
        {

            return sqlmapper.QueryForList<Model.Employee>("Employee.SelectOnActive", Helper.DateTimeParse.NullDate);
        }

        public IList<Model.Employee> SelectLeaveJob()
        {
            return sqlmapper.QueryForList<Model.Employee>("Employee.SelectLeaveJob", Helper.DateTimeParse.NullDate);
        }

        public IList<Book.Model.Employee> Select(string _roleId)
        {
            return sqlmapper.QueryForList<Model.Employee>("Employee.select_by_roleId", _roleId);
        }

        public IList<Book.Model.Employee> Select(Model.Department department)
        {
            return sqlmapper.QueryForList<Model.Employee>("Employee.select_byDepartment", department.DepartmentId);
        }

        public Book.Model.Employee SelectByCardNo(string CardNo, DateTime dt)
        {
            Hashtable ht = new Hashtable();
            ht.Add("cardno", CardNo);
            ht.Add("dt", dt);
            return sqlmapper.QueryForObject<Model.Employee>("Employee.select_byCardId", ht);
        }

        public IList<Model.Employee> SelectbyIDsubstring(string charno)
        {
            return sqlmapper.QueryForList<Model.Employee>("Employee.SelectbyIDsub", charno);
        }

        public IList<Model.Employee> SelectbyPinYin(string pinyin)
        {
            return sqlmapper.QueryForList<Model.Employee>("Employee.select_byPinYin", pinyin);
        }

        public DataTable SelectPinyin()
        {
            SqlConnection conn = new SqlConnection(sqlmapper.DataSource.ConnectionString);
            SqlDataAdapter da = new SqlDataAdapter("select pinyin from employee group by pinyin", conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        public DataSet SelectOnActiveDataSet()
        {
            using (SqlConnection conn = new SqlConnection(sqlmapper.DataSource.ConnectionString))
            {
                SqlDataAdapter da = new SqlDataAdapter("select e.DepartmentId,e.DutyId,e.EmployeeId,e.IDNo,e.EmployeeName,e.EmployeeGender,e.IsCadre,convert(int,e.DailyPay) as DailyPay,convert(int,e.MonthlyPay) as MonthlyPay,convert(int,e.DutyPay) as DutyPay,convert(int,e.PostPay) as PostPay,convert(int,e.FieldPay) as FieldPay,convert(int,e.Insurance) as Insurance,convert(int,e.Tax) as Tax, DepartmentName,DutyName,Fromtime,ToTime from employee e left join Department on e.DepartmentId=Department.DepartmentId  left join Duty on e.dutyid=duty.dutyid  left join BusinessHours on BusinessHours.BusinessHoursId=e.BusinessHoursId" + " where EmployeeLeaveDate is null or EmployeeLeaveDate='" + Helper.DateTimeParse.NullDate.ToString("yyyy-MM-dd") + "' ORDER By (case when left(idno,1) like '[A-Za-z]' then (case when convert(int,substring(idno,2,2)) between 30 and 99 then left(idno,1)+cast(1911+convert(int,substring(idno,2,2)) as varchar(10)) + substring(idno,4,len(idno)) else left(idno,1)+convert(varchar(10),1911+convert(int,'1'+substring(idno,2,2)))+substring(idno,4,len(idno)) end ) else idno end) ", conn);
                DataSet ds = new DataSet();
                da.Fill(ds, "employee");
                return ds;
            }
        }

        public DataSet SelectOnActiveDataSetByEmployeeId(string employeeId)
        {
            using (SqlConnection conn = new SqlConnection(sqlmapper.DataSource.ConnectionString))
            {
                SqlDataAdapter da = new SqlDataAdapter("select e.*,DepartmentName,dutyname,Fromtime,ToTime from employee e left join Department on e.DepartmentId=Department.DepartmentId  left join Duty on e.dutyid=duty.dutyid  left join BusinessHours on BusinessHours.BusinessHoursId=e.BusinessHoursId where employee.employeeId = '" + employeeId + "' order by IDNo", conn);
                DataSet ds = new DataSet();
                da.Fill(ds, "employee");
                return ds;
            }
        }

        public void UpdateDataDataSet(DataSet dataSet)
        {
            using (SqlConnection conn = new SqlConnection(sqlmapper.DataSource.ConnectionString))
            {
                SqlDataAdapter da = new SqlDataAdapter();
                da.UpdateCommand = new SqlCommand("update  employee set DailyPay=@DailyPay,MonthlyPay=@MonthlyPay,DutyPay=@DutyPay,PostPay=@PostPay,FieldPay=@FieldPay,Insurance=@Insurance,Tax=@Tax,IsCadre=@IsCadre where EmployeeId=@EmployeeId", conn);
                SqlParameter[] sqlParameter = new SqlParameter[]{
                new SqlParameter("@DailyPay",SqlDbType.Money,8,"DailyPay"),
                new SqlParameter("@MonthlyPay",SqlDbType.Money,8,"MonthlyPay"),
                new SqlParameter("@DutyPay",SqlDbType.Money,8,"DutyPay"),
                new SqlParameter("@PostPay",SqlDbType.Money,8,"PostPay"),
                new SqlParameter("@FieldPay",SqlDbType.Money,8,"FieldPay"),
                new SqlParameter("@Insurance",SqlDbType.Money,8,"Insurance"),
                new SqlParameter("@Tax",SqlDbType.Money,8,"Tax"),
                new SqlParameter("@IsCadre",SqlDbType.Bit,2,"IsCadre"),
                new SqlParameter("@EmployeeId",SqlDbType.VarChar,50,"EmployeeId")
            };
                da.UpdateCommand.Parameters.AddRange(sqlParameter);
                da.Update(dataSet.Tables["employee"]);
            }
        }

        /// <summary>
        /// 查询当月离职员工薪资
        /// </summary>
        /// <returns></returns>
        public IList<Model.Employee> selectLeaverPayActive()
        {
            return sqlmapper.QueryForList<Model.Employee>("Employee.select_leaverPayActive", null);
        }

        public IList<Model.Employee> selectEmployeeSearch(DateTime rzbegin, DateTime rzend, DateTime lzbegin, DateTime lzend, string type, int flag)
        {
            IList<Model.Employee> list = null;
            #region 在職人員
            if (type == "0")
            {
                Hashtable ht = new Hashtable();
                ht.Add("rzbegin", rzbegin);
                ht.Add("rzend", rzend);
                list = sqlmapper.QueryForList<Model.Employee>("Employee.select_EmployeeSearch0", ht);
            }
            #endregion

            #region 離職人員
            if (type == "1")
            {
                Hashtable ht = new Hashtable();
                ht.Add("lzbegin", lzbegin);
                ht.Add("lzend", lzend);
                list = sqlmapper.QueryForList<Model.Employee>("Employee.select_EmployeeSearch1", ht);
            }
            #endregion

            #region 全部人員
            if (type == "2")
            {
                Hashtable ht = new Hashtable();
                ht.Add("rzbegin", rzbegin);
                ht.Add("rzend", rzend);
                ht.Add("lzbegin", lzbegin);
                ht.Add("lzend", lzend);
                if (flag == 0)
                    list = sqlmapper.QueryForList<Model.Employee>("Employee.select_EmployeeSearch2", ht);
                else
                    list = sqlmapper.QueryForList<Model.Employee>("Employee.select_EmployeeSearch3", null);
            }
            #endregion

            return list;
        }

        public Book.Model.Employee GetOldIDbyEmpID(string id)
        {
            return sqlmapper.QueryForObject<Model.Employee>("Employee.get_by_id", id);
        }

        public bool ExistsExceptUpdate(Book.Model.Employee emp)
        {
            Hashtable paras = new Hashtable();
            paras.Add("newId", emp.IDNo);
            paras.Add("oldId", Get(emp.EmployeeId) == null ? null : Get(emp.EmployeeId).IDNo);
            return sqlmapper.QueryForObject<bool>("Employee.existsexceptUpdate", paras);
        }

        /// <summary>
        /// 添加时判断是否存在
        /// </summary>
        /// <param name="emp"></param>
        /// <returns></returns>
        public bool ExistsExceptInsert(Book.Model.Employee emp)
        {
            return sqlmapper.QueryForObject<bool>("Employee.existsexceptInsert", emp.IDNo);
        }

        /// <summary>
        /// 根据日前查询 符合 月份发薪资的人员
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public IList<Model.Employee> SelectHrDailyAttend(DateTime date)
        {
            return sqlmapper.QueryForList<Model.Employee>("Employee.select_HrDailyAttendInfoByDay", date.Date);
        }

        public IList<Model.Employee> DailyEmployeeAttendInfo_EmpList(DateTime CheckDatetime)
        {
            return sqlmapper.QueryForList<Model.Employee>("Employee.DailyEmployeeAttendInfo_EmpList", CheckDatetime);
        }

        public IList<Book.Model.Employee> GetHasThereEmp_ListByDateTime(DateTime mdate)
        {
            Hashtable ht = new Hashtable();
            ht.Add("mYear", mdate.Year.ToString("0000"));
            ht.Add("mMonth", mdate.Month.ToString("00"));
            return sqlmapper.QueryForList<Model.Employee>("Employee.GetHasThereEmpListByDateTime", ht);
        }

        #region 分类构建
        public Book.Model.Employee mGetFirst()
        {
            return sqlmapper.QueryForObject<Model.Employee>("Employee.mGetFirst", Helper.DateTimeParse.NullDate.ToString("yyyy-MM-dd"));
        }

        public Book.Model.Employee mGetLast()
        {
            return sqlmapper.QueryForObject<Model.Employee>("Employee.mGetLast", Helper.DateTimeParse.NullDate.ToString("yyyy-MM-dd"));
        }

        public Book.Model.Employee mGetPrev(Book.Model.Employee emp)
        {
            Hashtable ht = new Hashtable();
            ht.Add("InsertTime", emp.InsertTime);
            ht.Add("EmployeeLeaveDate", Helper.DateTimeParse.NullDate.ToString("yyyy-MM-dd"));
            return sqlmapper.QueryForObject<Model.Employee>("Employee.mGetPrev", ht);
        }

        public Book.Model.Employee mGetNext(Book.Model.Employee emp)
        {
            Hashtable ht = new Hashtable();
            ht.Add("InsertTime", emp.InsertTime);
            ht.Add("EmployeeLeaveDate", Helper.DateTimeParse.NullDate.ToString("yyyy-MM-dd"));
            return sqlmapper.QueryForObject<Model.Employee>("Employee.mGetNext", ht);
        }

        public bool mHasRows()
        {
            return sqlmapper.QueryForObject<bool>("Employee.mHasRows", null);
        }

        public bool mHasRowsBefore(Book.Model.Employee emp)
        {
            Hashtable ht = new Hashtable();
            ht.Add("InsertTime", emp.InsertTime);
            ht.Add("EmployeeLeaveDate", Helper.DateTimeParse.NullDate.ToString("yyyy-MM-dd"));
            return sqlmapper.QueryForObject<bool>("Employee.mHasRowsBefore", ht);
        }

        public bool mHasRowsAfter(Book.Model.Employee emp)
        {
            Hashtable ht = new Hashtable();
            ht.Add("InsertTime", emp.InsertTime);
            ht.Add("EmployeeLeaveDate", Helper.DateTimeParse.NullDate.ToString("yyyy-MM-dd"));
            return sqlmapper.QueryForObject<bool>("Employee.mHasRowsAfter", ht);
        }

        #endregion

        public IList<Model.Employee> SelectIdAndName()
        {
            return sqlmapper.QueryForList<Model.Employee>("Employee.SelectIdAndName", null);
        }
    }
}
