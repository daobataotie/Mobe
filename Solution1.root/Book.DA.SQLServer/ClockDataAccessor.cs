//------------------------------------------------------------------------------
//
// file name：ClockDataAccessor.cs
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
    /// Data accessor of ClockData
    /// </summary>
    public partial class ClockDataAccessor : EntityAccessor, IClockDataAccessor
    {
        //public DataSet GetOntimeDetails(string employeeId, DateTime clockDate)
        //{
        //    string mydate = string.Empty;
        //    if (clockDate.Month < 10&&clockDate.Day<10)
        //    {
        //        mydate = clockDate.Year.ToString() + "-0" + clockDate.Month.ToString() + "-0" + clockDate.Day.ToString();
        //    }
        //    else if (clockDate.Month < 10 && clockDate.Day > 9)
        //    {
        //        mydate = clockDate.Year.ToString() + "-0" + clockDate.Month.ToString() + "-" + clockDate.Day.ToString();
        //    }
        //    else if (clockDate.Month > 9 && clockDate.Day < 10)
        //    {
        //        mydate = clockDate.Year.ToString() + "-" + clockDate.Month.ToString() + "-0" + clockDate.Day.ToString();
        //    }
        //    else
        //    {
        //        mydate = clockDate.ToString();
        //    }
        //    string sqlStr = "select top 1  *    from     (select Empclockdate,EmployeeId ,(select ClockTime  from ClockData where a.EmployeeId=EmployeeId and ClockType=0) as CurrentOnTime, (select ClockTime  from ClockData where a.EmployeeId=EmployeeId and ClockType=1) as CurrentTime,ClockType,ClockTime,(select EmployeeName from Employee  where EmployeeId=a.EmployeeId) as EmployeeName,(select (select FromHour from BusinessHours where b.BusinessHoursId=BusinessHoursId) as FromHour from Flextime as b where a.EmployeeId=EmployeeId and a.EmpclockDate=FlexDate) as FromHour ,(select (select FromMinute from BusinessHours where b.BusinessHoursId=BusinessHoursId) as Mytime  from Flextime as b where a.EmployeeId=EmployeeId and a.EmpclockDate=FlexDate ) as FromMinute,(select (select ToHour from BusinessHours where b.BusinessHoursId=BusinessHoursId) as Mytime  from Flextime as b where a.EmployeeId=EmployeeId and a.EmpclockDate=FlexDate ) as ToHour,(select (select ToMinute from BusinessHours where b.BusinessHoursId=BusinessHoursId) as Mytime  from Flextime as b where a.EmployeeId=EmployeeId and a.EmpclockDate=FlexDate ) as ToMinute    from  ClockData a where EmployeeId='" + employeeId + "' and substring(convert(char,EmpclockDate,120),1,10)=substring(convert(char,'" + mydate + "',120),1,10)) as dd";
        //    SqlDataAdapter adapter = new SqlDataAdapter( sqlStr,sqlmapper.DataSource.ConnectionString);
        //    DataSet data = new DataSet();
        //    adapter.Fill(data);
        //    return data;
        //}
        //public DataSet GetOnMonthDetails(string employeeId, DateTime clockDate)
        //{
        //    string mydate = string.Empty;
        //    if (clockDate.Month < 10 && clockDate.Day < 10)
        //    {
        //        mydate = clockDate.Year.ToString() + "-0" + clockDate.Month.ToString() + "-0" + clockDate.Day.ToString();
        //    }
        //    else if (clockDate.Month < 10 && clockDate.Day > 9)
        //    {
        //        mydate = clockDate.Year.ToString() + "-0" + clockDate.Month.ToString() + "-" + clockDate.Day.ToString();
        //    }
        //    else if (clockDate.Month > 9 && clockDate.Day < 10)
        //    {
        //        mydate = clockDate.Year.ToString() + "-" + clockDate.Month.ToString() + "-0" + clockDate.Day.ToString();
        //    }
        //    else
        //    {
        //        mydate = clockDate.ToString();
        //    }
        //    string sqlStr = "select  * from (select Empclockdate,EmployeeId ,(select ClockTime  from ClockData where a.EmployeeId=EmployeeId and ClockType=0) as CurrentOnTime, (select ClockTime  from ClockData where a.EmployeeId=EmployeeId and ClockType=1) as CurrentTime,ClockType,ClockTime,(select EmployeeName from Employee  where EmployeeId=a.EmployeeId) as EmployeeName,(select (select FromHour from BusinessHours where b.BusinessHoursId=BusinessHoursId) as FromHour from Flextime as b where a.EmployeeId=EmployeeId and a.EmpclockDate=FlexDate) as FromHour ,(select (select FromMinute from BusinessHours where b.BusinessHoursId=BusinessHoursId) as Mytime  from Flextime as b where a.EmployeeId=EmployeeId and a.EmpclockDate=FlexDate ) as FromMinute,(select (select ToHour from BusinessHours where b.BusinessHoursId=BusinessHoursId) as Mytime  from Flextime as b where a.EmployeeId=EmployeeId and a.EmpclockDate=FlexDate ) as ToHour,(select (select ToMinute from BusinessHours where b.BusinessHoursId=BusinessHoursId) as Mytime  from Flextime as b where a.EmployeeId=EmployeeId and a.EmpclockDate=FlexDate ) as ToMinute    from  ClockData a where EmployeeId='" + employeeId + "' and ClockType=0 and year(Empclockdate)=year('" + mydate + "') and month(Empclockdate)=month('" + mydate + "')) as dd";
        //    SqlDataAdapter adapter = new SqlDataAdapter(sqlStr, sqlmapper.DataSource.ConnectionString);
        //    DataSet data = new DataSet();
        //    adapter.Fill(data);
        //    return data;
        //}
        public IList<Model.ClockData> getClockbyempid(string employeeId)
        {
            return sqlmapper.QueryForList<Model.ClockData>("ClockData.select_byempid", employeeId);
        }
        /// <summary>
        /// 员工在时间段内最早上班打卡记录
        /// </summary>
        /// <param name="empid">员工编号</param>
        /// <param name="starttime">开始时间</param>
        /// <param name="endtime">结束时间</param>
        /// <returns></returns>
        public Model.ClockData Getfirstclosck(string empid, DateTime starttime, DateTime endtime)
        {
            Hashtable parms = new Hashtable();
            parms.Add("empid", empid);
            parms.Add("starttime", starttime);
            parms.Add("endtime", endtime);
            return sqlmapper.QueryForObject<Model.ClockData>("ClockData.select_firsttime", parms);
        }
        /// <summary>
        /// 员工在时间段内最晚上班打卡记录
        /// </summary>
        /// <param name="empid">员工编号</param>
        /// <param name="starttime">开始时间</param>
        /// <param name="endtime">结束时间</param>
        /// <returns></returns>
        public Model.ClockData Getlastclosck(string empid, DateTime starttime, DateTime endtime)
        {
            Hashtable parms = new Hashtable();
            parms.Add("empid", empid);
            parms.Add("starttime", starttime);
            parms.Add("endtime", endtime);

            return sqlmapper.QueryForObject<Model.ClockData>("ClockData.select_lasttime", parms);

        }
        /// <summary>
        /// 员工某段时间的打卡记录
        /// </summary>
        /// <param name="empid"></param>
        /// <param name="starttime"></param>
        /// <param name="endtime"></param>
        /// <returns></returns>
        public IList<Model.ClockData> getbydateandempid(string empid, DateTime starttime, DateTime endtime)
        {
            Hashtable parms = new Hashtable();
            parms.Add("empid", empid);
            parms.Add("starttime", starttime);
            parms.Add("endtime", endtime);

            return sqlmapper.QueryForList<Model.ClockData>("ClockData.select_Clockbydateempid", parms);
        }
        /// <summary>
        /// 某段时间的所有员工打卡记录
        /// </summary>
        /// <param name="empid"></param>
        /// <param name="starttime"></param>
        /// <param name="endtime"></param>
        /// <returns></returns>
        public IList<Model.ClockData> getbydate(DateTime starttime, DateTime endtime)
        {
            Hashtable parms = new Hashtable();

            parms.Add("starttime", starttime);
            parms.Add("endtime", endtime);

            return sqlmapper.QueryForList<Model.ClockData>("ClockData.select_Clockbydate", parms);
        }
        /// <summary>
        /// 获取所有 不重复的日期
        /// </summary>
        /// <returns></returns>
        public DataSet SearchDistinctDate()
        {
            SqlConnection con = new SqlConnection(sqlmapper.DataSource.ConnectionString);
            SqlDataAdapter adapter = new SqlDataAdapter("select distinct Empclockdate from Clockdata ", con);
            DataSet data = new DataSet();
            adapter.Fill(data);
            return data;
        }
        /// <summary>
        /// 获取所有不重复的员工
        /// </summary>
        /// <returns></returns>
        public DataSet SearchDistinctEmployee()
        {
            SqlConnection con = new SqlConnection(sqlmapper.DataSource.ConnectionString);
            SqlDataAdapter adapter = new SqlDataAdapter("select distinct EmployeeId from clockdata", con);
            DataSet data = new DataSet();
            adapter.Fill(data);
            return data;
        }
        /// <summary>
        /// 根据员工编号获取打卡信息
        /// </summary>
        /// <param name="employeeId"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public DataSet SearchSingleClockByCondition(string employeeId, DateTime date, string clockType)
        {
            SqlConnection con = new SqlConnection(sqlmapper.DataSource.ConnectionString);
            StringBuilder sql = new StringBuilder();
            sql.Append("select min(employeeid) as employeeid ,min(Empclockdate) as Empclockdate,min(clocktime) as clocktime,min(clocktype) as  clocktype   from clockdata where 1=1  ");
            if (!string.IsNullOrEmpty(employeeId))
            {
                sql.Append(" and EmployeeId='" + employeeId + "'");
            }
            if (date != null)
            {
                sql.Append(" and Empclockdate='" + date.ToString("yyyy-MM-dd HH:mm:ss") + "'");
            }
            if (!string.IsNullOrEmpty(clockType))
            {
                sql.Append(" and ClockType=" + clockType);
            }
            sql.Append(" group by datepart(hour,Clocktime) order by Clocktime asc ");
            SqlDataAdapter adapter = new SqlDataAdapter(sql.ToString(), con);
            DataSet data = new DataSet();
            adapter.Fill(data);
            return data;
        }
        /// <summary>
        /// 根据员工编号查询排班信息
        /// </summary>
        /// <param name="EmployeeId"></param>
        /// <returns></returns>
        public DataSet SearchBusinessHoursInfoByEmployeeId(string EmployeeId)
        {
            SqlConnection con = new SqlConnection(sqlmapper.DataSource.ConnectionString);
            StringBuilder sql = new StringBuilder();
            sql.Append(" select * from Businesshours where 1=1");
            if (!string.IsNullOrEmpty(EmployeeId))
            {
                sql.Append(" and BusinessHoursId =(select businesshoursId  from  Employee where EmployeeId='" + EmployeeId + "')");
            }
            SqlDataAdapter adapter = new SqlDataAdapter(sql.ToString(), con);
            DataSet data = new DataSet();
            adapter.Fill(data);
            return data;
        }
        /// <summary>
        /// 根据员工的编号和日期查询加班信息
        /// </summary>
        /// <param name="employeeId"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public DataSet SearchOvertimeInfoByEmployeeId(string employeeId, DateTime date)
        {
            SqlConnection con = new SqlConnection(sqlmapper.DataSource.ConnectionString);
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from OverTime  where 1=1 ");
            if (!string.IsNullOrEmpty(employeeId))
            {
                sql.Append(" and EmployeeId='" + employeeId + "'");
            }
            if (date != null)
            {
                sql.Append(" and  DueDate='" + date + "'");
            }
            SqlDataAdapter adapter = new SqlDataAdapter(sql.ToString(), con);
            DataSet data = new DataSet();
            adapter.Fill(data);
            return data;
        }
        /// <summary>
        /// 根据打卡卡号和日期来查询数据库是否有相同的记录
        /// </summary>
        /// <param name="employeeId"></param>
        /// <param name="clockDate"></param>
        /// <returns></returns>
        public DataSet SearchClockDataInfoByCarNoAndClockDate(string CardNo, DateTime clockDate)
        {
            SqlConnection con = new SqlConnection(sqlmapper.DataSource.ConnectionString);
            string sqlStr = "select * from ClockData where  CardNo='" + CardNo + "' and  Clocktime='" + clockDate.ToString("yyyy-MM-dd HH:mm:ss") + "'";
            SqlDataAdapter adapter = new SqlDataAdapter(sqlStr, con);
            DataSet data = new DataSet();
            adapter.Fill(data);
            return data;
        }
        /// <summary>
        /// 近三月的所有打卡记录
        /// </summary>
        /// <returns></returns>
        public IList<Model.ClockData> selectClockTopTreeMonth()
        {
            return sqlmapper.QueryForList<Book.Model.ClockData>("ClockData.select_ClockTopTreeMonth", null);
        }
        /// <summary>
        /// 根据上传文件名初步判断欲上传文档内容是否已存在
        /// </summary>
        /// <param name="FileName"></param>
        /// <returns></returns>
        public bool SearchDistinctFileName(string FileName)
        {
            bool a = sqlmapper.QueryForObject<string>("ClockData.select_DistinctFileName", FileName) == null ? true : false;

            return sqlmapper.QueryForObject<string>("ClockData.select_DistinctFileName", FileName) == null ? true : false;
            //if (sqlmapper.QueryForList<Model.ClockData>("ClockData.select_DistinctFileName", FileName).Count == 0)
            //{
            //    return true;
            //}
            //else
            //{
            //    return false;
            //}
        }
        /// <summary>
        /// 获取实际上班,实际下班,假日上班,假日下班时间
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="MinDateTime"></param>ddd
        /// <param name="MaxDateTime"></param>
        /// <param name="ordertype"></param>
        /// <returns></returns>
        public DateTime? GetAnyInOut(string cardNo, DateTime MinDateTime, DateTime MaxDateTime, string ordertype)
        {
            Hashtable parm = new Hashtable();
            parm.Add("cardNo", cardNo);
            parm.Add("MinDateTime", MinDateTime);
            parm.Add("MaxDateTime", MaxDateTime);
            parm.Add("ordertype", ordertype);
            return sqlmapper.QueryForObject<DateTime?>("ClockData.select_GetAnyInOut", parm);
        }
        /// <summary>
        /// 取考勤月雇员编号集合
        /// </summary>
        /// <param name="CheckDateTime"></param>
        /// <returns></returns>
        public IList<string> GetMakeSalaryList_DisEmpID(DateTime CheckDateTime)
        {
            Hashtable param = new Hashtable();
            param.Add("Year", CheckDateTime.Year);
            param.Add("Month", CheckDateTime.Month);
            return sqlmapper.QueryForList<string>("ClockData.GetMakeSalaryList_DisEmpID", param);
        }

        public void DeleteByFileName(string FileName)
        {
            sqlmapper.Delete("ClockData.DeleteByFileName", FileName);
        }

    }
}
