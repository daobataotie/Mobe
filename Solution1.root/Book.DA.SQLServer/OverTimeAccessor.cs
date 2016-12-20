//------------------------------------------------------------------------------
//
// file name：OverTimeAccessor.cs
// author: peidun
// create date：2010-3-20 11:59:56
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
    /// Data accessor of OverTime
    /// </summary>
    public partial class OverTimeAccessor : EntityAccessor, IOverTimeAccessor
    {
        public IList<Model.OverTime> GetOverTimebyempiddate(string empid, DateTime starttime, DateTime endtime)
        {
            Hashtable pars = new Hashtable();
            pars.Add("empid", empid);
            pars.Add("startdate", starttime);
            pars.Add("enddate", endtime);
            return sqlmapper.QueryForList<Model.OverTime>("OverTime.select_empiddate", pars);
        }
        public System.Data.DataSet SelectOverTimeInfoByEmployeeId(string employeeId, DateTime dueDate)
        {
            SqlConnection con = new SqlConnection(sqlmapper.DataSource.ConnectionString);
            string sqlStr = "select * from  OverTime where EmployeeId='" + employeeId + "' and  year(dueDate)=" + dueDate.Year + " and  month(dueDate)=" + dueDate.Month + " ORDER BY DueDate ASC";
            SqlDataAdapter adapter = new SqlDataAdapter(sqlStr, con);
            DataSet data = new DataSet();
            adapter.Fill(data);
            return data;
        }
        public IList<Model.OverTime> SelectByEmployeeAndMonth(Model.Employee employee, int year, int month)
        {
            Hashtable pars = new Hashtable();
            pars.Add("employeeid", employee.EmployeeId);
            pars.Add("year", year);
            pars.Add("month", month);
            return sqlmapper.QueryForList<Model.OverTime>("OverTime.select_ByEmployeeAndMonth", pars);
        }

        /// <summary>
        /// 根据日期查加班记录
        /// </summary>
        /// <param name="empid"></param>
        /// <param name="starttime"></param>
        /// <param name="endtime"></param>
        /// <returns></returns>
        public DataSet selectOverTimebyDate(string IDNo, int year, int month)
        {
            SqlConnection conn = new SqlConnection(sqlmapper.DataSource.ConnectionString);
            string sql = "select e.IDNo,e.EmployeeId,e.EmployeeName,d.DepartmentName,o.* from employee e left join  department d on e.DepartmentId = d.DepartmentId left join OverTime o on e.EmployeeId = o.EmployeeId where e.IDNo= '" + IDNo + "' and  year(o.DueDate)= " + year + "and month(o.DueDate)=" + month;
            SqlDataAdapter da = new SqlDataAdapter(sql, conn);
            DataSet ds = new DataSet();
            da.Fill(ds, "OverTimeInfo");
            return ds;
        }

        /// <summary>
        /// 查询所有加班记录
        /// </summary>
        /// <param name="empid"></param>
        /// <param name="starttime"></param>
        /// <param name="endtime"></param>
        /// <returns></returns>
        public DataSet selectOverTime()
        {
            SqlConnection conn = new SqlConnection(sqlmapper.DataSource.ConnectionString);
            SqlDataAdapter da = new SqlDataAdapter("select e.IDNo,e.EmployeeName,d.DepartmentName from employee e left join  department d on e.DepartmentId = d.DepartmentId left join OverTime o on e.EmployeeId = o.EmployeeId ", conn);
            DataSet ds = new DataSet();
            da.Fill(ds, "OverTime");
            return ds;
        }

        public Book.Model.OverTime GetLastForEmployeeYearMonth(string EmpID, int year, int month)
        {
            Hashtable ht = new Hashtable();
            ht.Add("empid", EmpID);
            ht.Add("year", year);
            ht.Add("month", month);
            return sqlmapper.QueryForObject<Model.OverTime>("OverTime.GetLastForEmployeeYearMonth", ht);
        }
        public Book.Model.OverTime GetNextForEmployeeYearMonth(string EmpID, DateTime date, int year, int month)
        {
            Hashtable ht = new Hashtable();
            ht.Add("empid", EmpID);
            ht.Add("inserttime", date);
            ht.Add("year", year);
            ht.Add("month", month);
            return sqlmapper.QueryForObject<Model.OverTime>("OverTime.GetNextForEmployeeYearMonth", ht);
        }

        public Book.Model.OverTime GetPrevForEmployeeYearMonth(string EmpID, DateTime date, int year, int month)
        {
            Hashtable ht = new Hashtable();
            ht.Add("empid", EmpID);
            ht.Add("inserttime", date);
            ht.Add("year", year);
            ht.Add("month", month);
            return sqlmapper.QueryForObject<Model.OverTime>("OverTime.GetPrevForEmployeeYearMonth", ht);
        }

        public IList<Book.Model.OverTime> SelectOverTimeList(DateTime startdate, DateTime enddate)
        {
            Hashtable ht = new Hashtable();
            ht.Add("startdate", startdate.ToString("yyyy-MM-dd"));
            ht.Add("enddate", enddate.ToString("yyyy-MM-dd"));
            return sqlmapper.QueryForList<Model.OverTime>("OverTime.SelectOverTimeList", ht);
        }

        public bool JudgeRepeater(string empids, int Empcount, DateTime JudgeDate)
        {
            Hashtable ht = new Hashtable();
            ht.Add("empids", empids);
            ht.Add("year", JudgeDate.Year.ToString());
            ht.Add("month", JudgeDate.Month.ToString());
            ht.Add("day", JudgeDate.Day.ToString());
            if (sqlmapper.QueryForObject<int>("OverTime.JudgeRepeater", ht) > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
