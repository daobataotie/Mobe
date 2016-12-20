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
            pars.Add("startdate", starttime);
            pars.Add("enddate", endtime);
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
            SqlConnection con = new SqlConnection(sqlmapper.DataSource.ConnectionString);
            string sqlStr = "select a.*,b.LeaveTypeName as LeaveTypeName,b.PayRate from Leave as a left join  LeaveType as b  on a.LeaveTypeId=b.LeaveTypeId where a.EmployeeId='" + employeeId + "' and  year( a.LeaveDate)=year('" + leaveDate.ToString("yyyy-MM-dd") + "') and month(a.LeaveDate)=month('" + leaveDate.ToString("yyyy-MM-dd") + "')";
            SqlDataAdapter adapter = new SqlDataAdapter(sqlStr, con);
            DataSet data = new DataSet();
            adapter.Fill(data);
            return data;
        }

        public System.Data.DataSet GetLeaveInfoByEmployeeId(string employeeId, string year)
        {
            SqlConnection con = new SqlConnection(sqlmapper.DataSource.ConnectionString);
            string sqlStr = "select a.*,b.LeaveTypeName as LeaveType from Leave as a left join  LeaveType as b  on a.LeaveTypeId=b.LeaveTypeId where a.EmployeeId='" + employeeId + "' and  year( a.LeaveDate)='" + year + "'";
            SqlDataAdapter adapter = new SqlDataAdapter(sqlStr, con);
            DataSet data = new DataSet();
            adapter.Fill(data);
            return data;
        }
        /// <summary>
        /// 月请假总基数 包括有薪假等
        /// </summary>
        /// <param name="employee"></param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        public decimal SelectCountByMonthEmp(Model.Employee employee, int year,int month)
        {

            decimal sum = 0;
            SqlConnection conn = new SqlConnection(sqlmapper.DataSource.ConnectionString);
            SqlCommand da = new SqlCommand("select ( (SELECT count(*) FROM Leave  where employeeid  ='" + employee.EmployeeId + "' and year(leavedate)=" + year + " and month(leavedate)=" + month + "   AND LeaveRange = 0) +  (SELECT count(*) FROM Leave WHERE EmployeeId='" + employee.EmployeeId + "' and year(leavedate)=" + year + " and month(leavedate)=" + month + "  AND LeaveRange <> 0 )*0.5) as da ", conn);
            conn.Open();
            SqlDataReader reader = da.ExecuteReader(CommandBehavior.CloseConnection);
            reader.Read();
            if (!string.IsNullOrEmpty(reader.GetValue(0).ToString()))

                sum = decimal.Parse(reader.GetValue(0).ToString());
            reader.Close();
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
            SqlConnection conn = new SqlConnection(sqlmapper.DataSource.ConnectionString);
            SqlCommand da = new SqlCommand("select ( (SELECT count(*) FROM Leave  where employeeid  ='" + employee.EmployeeId + "' and year(leavedate)=" + year + " and month(leavedate)=" + month + "   AND LeaveRange = 0 and LeaveTypeId in(select LeaveTypeId from LeaveType where IsCountToPunish=1 ) ) +  (SELECT count(*) FROM Leave WHERE EmployeeId='" + employee.EmployeeId + "' and year(leavedate)=" + year + " and month(leavedate)=" + month + "  AND LeaveRange <> 0 and LeaveTypeId in(select LeaveTypeId from LeaveType where IsCountToPunish=1 ) )*0.5)  as da  ", conn);
            conn.Open();
            SqlDataReader reader = da.ExecuteReader(CommandBehavior.CloseConnection);
            reader.Read();
            if (!string.IsNullOrEmpty(reader.GetValue(0).ToString()))

                sum = decimal.Parse(reader.GetValue(0).ToString());
            reader.Close();
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
            SqlConnection conn = new SqlConnection(sqlmapper.DataSource.ConnectionString);
            SqlCommand da = new SqlCommand("SELECT count(DutyDate) FROM HrDailyEmployeeAttendInfo WHERE EmployeeId = '" + employee.EmployeeId + "' AND YEAR(DutyDate) = " + year + " AND MONTH(DutyDate) =  " + month + " AND convert(varchar(20),DutyDate,101) IN (SELECT convert(varchar(20),HolidayDate,101)  FROM AnnualHoliday)", conn);
            conn.Open();
            SqlDataReader reader = da.ExecuteReader(CommandBehavior.CloseConnection);
            reader.Read();
            if (!string.IsNullOrEmpty(reader.GetValue(0).ToString()))

                sum = decimal.Parse(reader.GetValue(0).ToString());
            reader.Close();
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
            ht.Add("year",year);
            ht.Add("month",month);
            return sqlmapper.QueryForObject<int>("Leave.select_SpecificHolidayMonthEmp", ht);
        }

        /// <summary>
        /// 根据月份、员工ID查询
        /// </summary>
        /// <param name="empid"></param>
        /// <param name="starttime"></param>
        /// <param name="endtime"></param>
        /// <returns></returns>
        public IList<Model.Leave> Getleavebyempidmonth(string empid,int year,int month)
        {
            Hashtable pars = new Hashtable();
            pars.Add("employeeId", empid);
            pars.Add("year", year);
            pars.Add("month", month);
            return sqlmapper.QueryForList<Model.Leave>("Leave.select_byempidmonth", pars);
        }
    }
}
