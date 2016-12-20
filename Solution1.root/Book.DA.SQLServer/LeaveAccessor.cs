//------------------------------------------------------------------------------
//
// file name：LeaveAccessor.cs
// author: peidun
// create date：2010-3-16 16:05:48
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
    /// Data accessor of Leave
    /// </summary>
    public partial class LeaveAccessor : EntityAccessor, ILeaveAccessor
    {
        public IList<Model.Leave> Getleavebyempiddate(string empid, DateTime starttime, DateTime endtime)
        {
            Hashtable pars = new Hashtable();
            pars.Add("empid", empid);
            pars.Add("startdate", starttime.ToString("yyyy-MM-dd"));
            pars.Add("enddate", endtime.ToString("yyyy-MM-dd"));
            return sqlmapper.QueryForList<Model.Leave>("Leave.select_byempiddate", pars);
        }
        /// <summary>
        /// 本月員工請假
        /// </summary>
        /// <param name="employeeId"></param>
        /// <param name="leaveDate"></param>
        /// <returns></returns>
        public System.Data.DataSet GetLeaveInfoByEmployeeId(string employeeId, DateTime leaveDate)
        {
            using (SqlConnection con = new SqlConnection(sqlmapper.DataSource.ConnectionString))
            {
                string sqlStr = "select a.*,b.LeaveTypeName as LeaveTypeName,b.PayRate from Leave as a left join  LeaveType as b  on a.LeaveTypeId=b.LeaveTypeId where a.EmployeeId='" + employeeId + "' and  year( a.LeaveDate)=year('" + leaveDate.Date.ToString("yyyy-MM-dd") + "') and month(a.LeaveDate)=month('" + leaveDate.Date.ToString("yyyy-MM-dd") + "')";
                SqlDataAdapter adapter = new SqlDataAdapter(sqlStr, con);
                DataSet data = new DataSet();
                adapter.Fill(data);
                return data;
            }
        }

        public System.Data.DataSet GetLeaveInfoByEmployeeId(string employeeId, string year)
        {
            using (SqlConnection con = new SqlConnection(sqlmapper.DataSource.ConnectionString))
            {
                string sqlStr = "select a.*,b.LeaveTypeName as LeaveType from Leave as a left join  LeaveType as b  on a.LeaveTypeId=b.LeaveTypeId where a.EmployeeId='" + employeeId + "' and  year( a.LeaveDate)='" + year + "'";
                SqlDataAdapter adapter = new SqlDataAdapter(sqlStr, con);
                DataSet data = new DataSet();
                adapter.Fill(data);
                return data;
            }
        }
        /// <summary>
        /// 月请假总基数 包括有薪假等
        /// </summary>
        /// <param name="employee"></param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        public decimal SelectCountByMonthEmp(Model.Employee employee, int year, int month)
        {

            decimal sum = 0;
            using (SqlConnection conn = new SqlConnection(sqlmapper.DataSource.ConnectionString))
            {
                using (SqlCommand da = new SqlCommand("select ( (SELECT count(*) FROM Leave  where employeeid  ='" + employee.EmployeeId + "' and year(leavedate)=" + year + " and month(leavedate)=" + month + "   AND LeaveRange = 0) +  (SELECT count(*) FROM Leave WHERE EmployeeId='" + employee.EmployeeId + "' and year(leavedate)=" + year + " and month(leavedate)=" + month + "  AND LeaveRange <> 0 )*0.5) as da ", conn))
                {
                    conn.Open();
                    SqlDataReader reader = da.ExecuteReader(CommandBehavior.CloseConnection);
                    reader.Read();
                    if (!string.IsNullOrEmpty(reader.GetValue(0).ToString()))
                        sum = decimal.Parse(reader.GetValue(0).ToString());
                    reader.Close();
                }
            }
            return sum;

        }
        /// <summary>
        /// 倒扣款假总数
        /// </summary>
        /// <param name="employee"></param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        public decimal SelectPunishByMonthEmp(Model.Employee employee, int year, int month)
        {

            decimal sum = 0;
            using (SqlConnection conn = new SqlConnection(sqlmapper.DataSource.ConnectionString))
            {
                using (SqlCommand da = new SqlCommand("select ( (SELECT count(*) FROM Leave  where employeeid  ='" + employee.EmployeeId + "' and year(leavedate)=" + year + " and month(leavedate)=" + month + "   AND LeaveRange = 0 and LeaveTypeId in(select LeaveTypeId from LeaveType where IsCountToPunish=1 ) ) +  (SELECT count(*) FROM Leave WHERE EmployeeId='" + employee.EmployeeId + "' and year(leavedate)=" + year + " and month(leavedate)=" + month + "  AND LeaveRange <> 0 and LeaveTypeId in(select LeaveTypeId from LeaveType where IsCountToPunish=1 ) )*0.5)  as da  ", conn))
                {
                    conn.Open();
                    SqlDataReader reader = da.ExecuteReader(CommandBehavior.CloseConnection);
                    reader.Read();
                    if (!string.IsNullOrEmpty(reader.GetValue(0).ToString()))
                        sum = decimal.Parse(reader.GetValue(0).ToString());
                    reader.Close();
                }
            }
            return sum;

        }
        /// <summary>
        /// 出勤记录中 月假日(年假)总数
        /// </summary>
        /// <param name="employee"></param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        public decimal SelectTotalHolidayMonthEmp(Model.Employee employee, int year, int month)
        {

            decimal sum = 0;

            using (SqlConnection conn = new SqlConnection(sqlmapper.DataSource.ConnectionString))
            {
                using (SqlCommand da = new SqlCommand("SELECT count(DutyDate) FROM HrDailyEmployeeAttendInfo WHERE EmployeeId = '" + employee.EmployeeId + "' AND YEAR(DutyDate) = " + year + " AND MONTH(DutyDate) =  " + month + " AND convert(varchar(20),DutyDate,101) IN (SELECT convert(varchar(20),HolidayDate,101)  FROM AnnualHoliday)", conn))
                {
                    conn.Open();
                    SqlDataReader reader = da.ExecuteReader(CommandBehavior.CloseConnection);
                    reader.Read();
                    if (!string.IsNullOrEmpty(reader.GetValue(0).ToString()))
                        sum = decimal.Parse(reader.GetValue(0).ToString());
                    reader.Close();
                }
            }
            return sum;
        }
        /// <summary>
        /// 出勤记录中 月假日(劳动 清明 等)总数,不包含周日等年假
        /// </summary>
        /// <param name="employee"></param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        public int SelectSpecificHolidayMonthEmp(Model.Employee employee, int year, int month)
        {
            Hashtable ht = new Hashtable();
            ht.Add("employeeId", employee.EmployeeId);
            ht.Add("year", year);
            ht.Add("month", month);
            return sqlmapper.QueryForObject<int>("Leave.select_SpecificHolidayMonthEmp", ht);
        }

        /// <summary>
        /// 根据月份、员工ID查询
        /// </summary>
        /// <param name="empid"></param>
        /// <param name="starttime"></param>
        /// <param name="endtime"></param>
        /// <returns></returns>
        public IList<Model.Leave> Getleavebyempidmonth(string empid, int? year, int? month)
        {
            Hashtable pars = new Hashtable();
            pars.Add("employeeId", empid);
            pars.Add("year", year);
            pars.Add("month", month);
            return sqlmapper.QueryForList<Model.Leave>("Leave.select_byempidmonth", pars);
        }

        public Book.Model.Leave GetLastForEmployeeYear(string EmpID, DateTime LeaveYear)
        {
            Hashtable ht = new Hashtable();
            ht.Add("EmpID", EmpID);
            ht.Add("year", LeaveYear.Year);
            return sqlmapper.QueryForObject<Model.Leave>("Leave.GetLastForEmployeeYear", ht);
        }

        public Book.Model.Leave GetNextForEmployeeYear(string EmpID, DateTime LeaveYear)
        {
            Hashtable ht = new Hashtable();
            ht.Add("EmpID", EmpID);
            ht.Add("LeaveYear", LeaveYear.ToString("yyyy-MM-dd"));
            ht.Add("year", LeaveYear.Year);
            return sqlmapper.QueryForObject<Model.Leave>("Leave.GetNextForEmployeeYear", ht);
        }

        public Book.Model.Leave GetPrevForEmployeeYear(string EmpID, DateTime LeaveYear)
        {
            Hashtable ht = new Hashtable();
            ht.Add("EmpID", EmpID);
            ht.Add("LeaveYear", LeaveYear.ToString("yyyy-MM-dd"));
            ht.Add("year", LeaveYear.Year);
            return sqlmapper.QueryForObject<Model.Leave>("Leave.GetPrevForEmployeeYear", ht);
        }

        public IList<Model.Leave> SelectForMonthListPrint(DateTime startDate, DateTime endDate)
        {
            string str = string.Empty;
            str = " WHERE Leave.LeaveDate BETWEEN '" + startDate.ToString("yyyy-MM-dd") + "' AND '" + endDate.ToString("yyyy-MM-dd") + "'";
            return sqlmapper.QueryForList<Model.Leave>("Leave.SelectForMonthListPrint", str);
        }

        public void DeleteByDateRangeEmp(IList<Book.Model.Employee> emplist, DateTime startDate, DateTime enddate)
        {
            if (emplist != null && emplist.Count != 0)
            {
                Hashtable ht;
                while (startDate < enddate)
                {
                    foreach (Model.Employee emp in emplist)
                    {
                        ht = new Hashtable();
                        ht.Add("EmployeeId", emp.EmployeeId);
                        ht.Add("startDate", startDate.ToString("yyyy-MM-dd"));
                        sqlmapper.Delete("Leave.DeleteByDateRangeEmp", ht);
                    }
                    startDate = startDate.Date.AddDays(1);
                }
            }
        }

        //返回前半年以后的所有请假记录
        public IList<Book.Model.Leave> SelectLeaveListbyEmp(string empid)
        {
            string startdate = DateTime.Now.Date.AddMonths(-6).ToString("yyyy-MM-dd");
            Hashtable ht = new Hashtable();
            ht.Add("empid", empid);
            ht.Add("startdate", startdate);
            return sqlmapper.QueryForList<Model.Leave>("Leave.SelectLeaveListbyEmp", ht);
        }

        public IList<Book.Model.Leave> SelectByDateRangeEmp(string emps, DateTime startdate, DateTime enddate)
        {
            Hashtable ht = new Hashtable();
            ht.Add("emps", emps);
            ht.Add("startdate", startdate.ToString("yyyy-MM-dd"));
            ht.Add("enddate", enddate.ToString("yyyy-MM-dd"));

            return sqlmapper.QueryForList<Model.Leave>("Leave.SelectByDateRangeEmp", ht);
        }

        public IList<Book.Model.Leave> GetEmployeeLeavebyDate(string EmpID, DateTime LeaveDate)
        {
            Hashtable param = new Hashtable();
            param.Add("EmpID", EmpID);
            param.Add("LeaveDate", LeaveDate.Date.ToString("yyyy-MM-dd"));
            return sqlmapper.QueryForList<Model.Leave>("Leave.select_GetEmployeeLeavebyDate", param);
        }

        public void DeleteByDateRange(DateTime startdate, DateTime enddate,string EmployeeId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("startdate", startdate.ToString("yyyy-MM-dd"));
            ht.Add("enddate", enddate.ToString("yyyy-MM-dd"));
            ht.Add("EmployeeId", EmployeeId);
            sqlmapper.Delete("Leave.DeleteByDateRange", ht);
        }
    }
}
