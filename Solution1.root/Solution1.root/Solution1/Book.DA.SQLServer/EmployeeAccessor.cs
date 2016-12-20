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
        #region IEmployeeAccessor 成员


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
        public Book.Model.Employee SelectByCardNo(string CardNo,DateTime dt)
        {
            Hashtable ht = new Hashtable();
            ht.Add("cardno",CardNo);
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
            SqlConnection conn = new SqlConnection(sqlmapper.DataSource.ConnectionString);
            SqlDataAdapter da = new SqlDataAdapter("select employee.*,DepartmentName,dutyname,Fromtime,ToTime from employee left join Department on employee.DepartmentId=Department.DepartmentId  left join Duty on employee.dutyid=duty.dutyid  left join BusinessHours on BusinessHours.BusinessHoursId=employee.BusinessHoursId" + " where EmployeeLeaveDate is null or EmployeeLeaveDate='" + Helper.DateTimeParse.NullDate+ "' order by IDNo ", conn);
            DataSet ds = new DataSet();
            da.Fill(ds, "employee");
            return ds;
        }
        public DataSet SelectOnActiveDataSetByEmployeeId(string employeeId)
        {
            SqlConnection conn = new SqlConnection(sqlmapper.DataSource.ConnectionString);
            SqlDataAdapter da = new SqlDataAdapter("select employee.*,DepartmentName,dutyname,Fromtime,ToTime from employee left join Department on employee.DepartmentId=Department.DepartmentId  left join Duty on employee.dutyid=duty.dutyid  left join BusinessHours on BusinessHours.BusinessHoursId=employee.BusinessHoursId where employee.employeeId = '" + employeeId + "' order by IDNo", conn);
            DataSet ds = new DataSet();
            da.Fill(ds, "employee");
            return ds;
        }
        public void UpdateDataDataSet(DataSet dataSet)
        {
            SqlConnection conn = new SqlConnection(sqlmapper.DataSource.ConnectionString);
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
        #endregion

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
    }
}
