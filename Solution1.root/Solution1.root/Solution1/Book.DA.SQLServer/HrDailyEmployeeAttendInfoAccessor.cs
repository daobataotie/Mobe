//------------------------------------------------------------------------------
//
// file name：HrDailyEmployeeAttendInfoAccessor.cs
// author: mayanjun
// create date：2010-5-19 11:29:45
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
    /// Data accessor of HrDailyEmployeeAttendInfo
    /// </summary>
    public partial class HrDailyEmployeeAttendInfoAccessor : EntityAccessor, IHrDailyEmployeeAttendInfoAccessor
    {
        private AnnualHolidayAccessor annualHolidayAccessor = new AnnualHolidayAccessor();
        private LeaveAccessor leaveAccessor = new LeaveAccessor();

        public DataSet SelectDailyInfoByEmployee(string employeeId, DateTime dutyDate, string state)
        {
            try
            {
                int year = dutyDate.Year;
                int month = dutyDate.Month;
                SqlConnection con = new SqlConnection(sqlmapper.DataSource.ConnectionString);
                StringBuilder sqlBuilder = new StringBuilder();
                sqlBuilder.Append("select HrDailyEmployeeAttendInfoId,DutyDate,ShouldCheckIn,ShouldCheckOut,a.EmployeeId,ActualCheckIn,ActualCheckOut, LateInMinute,DayFactor,SpecialBonus,MonthFactor,Note ,(select top 1 ActualcheckIn from HrDailyEmployeeAttendInfo where DutyDate=a.DutyDate and ShouldCheckIn is null and IsNormal=1  order by DutyDate) as SecondCheckIn,(select top 1 ActualcheckOut from HrDailyEmployeeAttendInfo where DutyDate=a.DutyDate and ShouldCheckIn is null and IsNormal=1  order by DutyDate)  as SecondCheckOut,(select Eovertime from OverTime where  datediff(d,DueDate,DutyDate)=0 and EmployeeId=a.EmployeeId) as EoverTime   from HrDailyEmployeeAttendInfo  as a   where  1=1");
                if (!string.IsNullOrEmpty(employeeId))
                    sqlBuilder.Append(" and a.employeeid='" + employeeId + "'");
                if (dutyDate != null)
                    sqlBuilder.Append(" and   year(DutyDate)=" + year + " and month(DutyDate)=" + month + "");
                if (!string.IsNullOrEmpty(state))
                    sqlBuilder.Append(" and IsNormal=" + state + " ");
                sqlBuilder.Append("order by DutyDate");
                SqlDataAdapter adapter = new SqlDataAdapter(sqlBuilder.ToString(), con);
                DataSet data = new DataSet();
                adapter.Fill(data);
                return data;
            }
            catch
            {
                throw;
            }

        }


        public DataSet SelectByEmpMonth(Model.Employee employee, DateTime date)
        {
            int year = date.Year;
            int month = date.Month;
            SqlConnection con = new SqlConnection(sqlmapper.DataSource.ConnectionString);
            StringBuilder sqlBuilder = new StringBuilder();
            sqlBuilder.Append("SELECT e.EmployeeName,'('+ substring(datename(weekday, DutyDate),3,1)+')' weekofday,HrDailyEmployeeAttendInfoId,ClockDataId,o.EmployeeId,DutyDate,ShouldCheckIn,ShouldCheckOut,isnull( CONVERT(varchar(10), Replicate('0',2-len(datepart(hh ,ActualCheckIn)))+convert(varchar(2), datepart(hh ,ActualCheckIn))+':'+ Replicate('0',2-len(datepart(n ,ActualCheckIn)))+convert(varchar(2), datepart(n ,ActualCheckIn))),'--:--') ActualCheckIn,isnull( CONVERT(varchar(10), Replicate('0',2-len(datepart(hh ,ActualCheckOut)))+convert(varchar(2), datepart(hh ,ActualCheckOut))+':'+  Replicate('0',2-len(datepart(n ,ActualCheckOut)))+convert(varchar(2), datepart(n ,ActualCheckOut))),'--:--')   ActualCheckOut,LateInMinute,isnull(DayFactor,0) DayFactor,MonthFactor,SpecialBonus,o.Note,   IsNormal,isnull(t.EoverTime,0) AS EoverTime FROM HrDailyEmployeeAttendInfo o left JOIN Employee e ON e.EmployeeId = o.EmployeeId left JOIN OverTime t ON  t.EmployeeId=o.EmployeeId  and  CONVERT(varchar(20),t.DueDate,101)= CONVERT(varchar(20),o.DutyDate,101) where 1=1   ");
            if (!string.IsNullOrEmpty(employee.EmployeeId))
                sqlBuilder.Append(" and o.employeeid='" + employee.EmployeeId + "'");
            if (date != null)
                sqlBuilder.Append(" and   year(o.DutyDate)=" + year + " and month(o.DutyDate)=" + month + "");
            sqlBuilder.Append("order by DutyDate");
            SqlDataAdapter adapter = new SqlDataAdapter(sqlBuilder.ToString(), con);
            DataSet data = new DataSet();
            adapter.Fill(data);
            return data;
        }

        public DataSet SelectByEmpMonth(DateTime date)
        {
            int year = date.Year;
            int month = date.Month;
            SqlConnection con = new SqlConnection(sqlmapper.DataSource.ConnectionString);
            StringBuilder sqlBuilder = new StringBuilder();
            sqlBuilder.Append("SELECT e.EmployeeName,'('+ substring(datename(weekday, DutyDate),3,1)+')' weekofday,HrDailyEmployeeAttendInfoId,ClockDataId,o.EmployeeId,DutyDate,ShouldCheckIn,ShouldCheckOut,isnull( CONVERT(varchar(10), Replicate('0',2-len(datepart(hh ,ActualCheckIn)))+convert(varchar(2), datepart(hh ,ActualCheckIn))+':'+ Replicate('0',2-len(datepart(n ,ActualCheckIn)))+convert(varchar(2), datepart(n ,ActualCheckIn))),'--:--') ActualCheckIn,isnull( CONVERT(varchar(10), Replicate('0',2-len(datepart(hh ,ActualCheckOut)))+convert(varchar(2), datepart(hh ,ActualCheckOut))+':'+  Replicate('0',2-len(datepart(n ,ActualCheckOut)))+convert(varchar(2), datepart(n ,ActualCheckOut))),'--:--')   ActualCheckOut,LateInMinute,isnull(DayFactor,0) DayFactor,MonthFactor,SpecialBonus,o.Note,   IsNormal,isnull(t.EoverTime,0) AS EoverTime FROM HrDailyEmployeeAttendInfo o left JOIN Employee e ON e.EmployeeId = o.EmployeeId left JOIN OverTime t ON  t.EmployeeId=o.EmployeeId  and  CONVERT(varchar(20),t.DueDate,101)= CONVERT(varchar(20),o.DutyDate,101) where 1=1 ");
            if (date != null)
                sqlBuilder.Append(" and   year(o.DutyDate)=" + year + " and month(o.DutyDate)=" + month + "");
            sqlBuilder.Append("order by DutyDate");
            SqlDataAdapter adapter = new SqlDataAdapter(sqlBuilder.ToString(), con);
            DataSet data = new DataSet();
            adapter.Fill(data);
            return data;
        }

        public DataSet SelectBeginAndEndTime(Model.Employee employee, DateTime date)
        {
            int year = date.Year;
            int month = date.Month;
            SqlConnection con = new SqlConnection(sqlmapper.DataSource.ConnectionString);
            StringBuilder sqlBuilder = new StringBuilder();
            sqlBuilder.Append("select isnull(CONVERT(varchar(10), Replicate('0',2-len(datepart(hh ,max(ShouldCheckIn))))+convert(varchar(2), datepart(hh ,max(ShouldCheckIn)))+':'+ Replicate('0',2-len(datepart(n ,max(ShouldCheckIn))))+convert(varchar(2), datepart(n ,max(ShouldCheckIn)))),'--:--') ShouldCheckIn,isnull(CONVERT(varchar(10), Replicate('0',2-len(datepart(hh ,max(ShouldCheckOut))))+convert(varchar(2), datepart(hh ,max(ShouldCheckOut)))+':'+ Replicate('0',2-len(datepart(n ,max(ShouldCheckOut))))+convert(varchar(2), datepart(n ,max(ShouldCheckOut)))),'--:--') ShouldCheckOut FROM HrDailyEmployeeAttendInfo  as a   where  1=1 ");
            if (!string.IsNullOrEmpty(employee.EmployeeId))
                sqlBuilder.Append(" and a.employeeid='" + employee.EmployeeId + "'");
            if (date != null)
                sqlBuilder.Append(" and   year(DutyDate)=" + year + " and month(DutyDate)=" + month + "");
            //sqlBuilder.Append("order by DutyDate");
            SqlDataAdapter adapter = new SqlDataAdapter(sqlBuilder.ToString(), con);
            DataSet data = new DataSet();
            adapter.Fill(data);
            return data;
        }

        public DataSet GetemployeeJoinDate(Model.Employee empoyee)
        {
            SqlConnection con = new SqlConnection(sqlmapper.DataSource.ConnectionString);
            StringBuilder sqlBuilder = new StringBuilder();
            sqlBuilder.Append("SELECT distinct CONVERT(nvarchar(10), year(e.EmployeeJoinDate))+'/'+CONVERT(nvarchar(10), month(e.EmployeeJoinDate))+'/'+CONVERT(nvarchar(10), day(e.EmployeeJoinDate)) FROM HrDailyEmployeeAttendInfo o INNER JOIN Employee e ON e.EmployeeId = o.EmployeeId where  1=1 ");
            if (!string.IsNullOrEmpty(empoyee.EmployeeId))
                sqlBuilder.Append(" and e.employeeid='" + empoyee.EmployeeId + "'");
            //sqlBuilder.Append("order by DutyDate");
            SqlDataAdapter adapter = new SqlDataAdapter(sqlBuilder.ToString(), con);
            DataSet data = new DataSet();
            adapter.Fill(data);
            return data;
        }

        public IList<Model.HrDailyEmployeeAttendInfo> SelectByEmpMonth(Model.Employee employee, int year, int month)
        {
            Hashtable ht = new Hashtable();
            ht.Add("employeeid", employee.EmployeeId);
            ht.Add("years", year);
            ht.Add("months", month);
            return sqlmapper.QueryForList<Model.HrDailyEmployeeAttendInfo>("HrDailyEmployeeAttendInfo.selectByEmpMonth", ht);
        }
        //public System.Data.DataSet SelectLateInMinute(string employeeId, DateTime dutyDate)
        //{
        //    SqlConnection con = new SqlConnection(sqlmapper.DataSource.ConnectionString);
        //    string sqlStr = "select * from HrDailyEmployeeAttendInfo where EmployeeId='"+employeeId+"' and year(DutyDate)=year('"+dutyDate.ToString()+"') and month(DutyDate)=month('"+dutyDate.ToString()+"')";
        //    SqlDataAdapter adapter = new SqlDataAdapter(sqlStr,con);
        //    DataSet data = new DataSet();
        //    adapter.Fill(data);
        //    return data;
        //}
        //public void UpdateActualCheckIn(string hrId)
        //{
        //    SqlConnection con = new SqlConnection(sqlmapper.DataSource.ConnectionString);
        //    string sqlStr = "update HrDailyEmployeeAttendInfo set ActualCheckIn=ShouldCheckIn,LateInMinute=null where HrDailyEmployeeAttendInfoId='" + hrId + "'";
        //    SqlCommand cmd = new SqlCommand(sqlStr, con);
        //    try
        //    {
        //        con.Open();
        //        cmd.ExecuteNonQuery();
        //    }
        //    catch (Exception ex)
        //    {
        //        System.Windows.Forms.MessageBox.Show(ex.Message, "提示", System.Windows.Forms.MessageBoxButtons.OK);
        //    }
        //    finally
        //    {
        //        con.Close();
        //    }     
        //}
        public System.Data.DataSet SelectLateInfo(string EmployeeId, DateTime ClockDate)
        {
            SqlConnection con = new SqlConnection(sqlmapper.DataSource.ConnectionString);
            string sqlStr = "select * from LateRecord  where EmployeeId='" + EmployeeId + "' and year(ClockDate)=year('" + ClockDate.ToString("yyyy-MM-dd") + "') and month(ClockDate)=month('" + ClockDate.ToString("yyyy-MM-dd") + "') ";
            SqlDataAdapter adapter = new SqlDataAdapter(sqlStr, con);
            DataSet data = new DataSet();
            adapter.Fill(data);
            return data;
        }
        public void InsertLateInfo(string id, string EmployeeId, DateTime ClockDate, int LateInMinute)
        {
            SqlConnection con = new SqlConnection(sqlmapper.DataSource.ConnectionString);
            string sqlStr = "insert into LateRecord values('" + id + "','" + EmployeeId + "','" + ClockDate.ToShortDateString() + "','" + LateInMinute + "') ";
            SqlCommand cmd = new SqlCommand(sqlStr, con);
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message, "提示", System.Windows.Forms.MessageBoxButtons.OK);
            }
            finally
            {
                con.Close();
            }
        }
        public System.Data.DataSet SelectHrInfoByStateAndDate(DateTime DutyDate)
        {
            SqlConnection con = new SqlConnection(sqlmapper.DataSource.ConnectionString);
            string sqlStr = "select a.HrDailyEmployeeAttendInfoId, a.DutyDate,b.EmployeeId,b.EmployeeName,b.IDNo,a.ShouldCheckIn,a.ShouldCheckOut,a.ActualCheckIn,a.ActualCheckOut,a.Note from  HrDailyEmployeeAttendInfo as a inner join Employee as b on a.EmployeeId=b.EmployeeId  where IsNormal=0  and DutyDate='" + DutyDate.ToShortDateString() + "' order by DutyDate";
            SqlDataAdapter adapter = new SqlDataAdapter(sqlStr, con);
            DataSet data = new DataSet();
            adapter.Fill(data);
            return data;
        }
        public System.Data.DataSet SelectHrInfoById(string HrDailyEmployeeAttendInfoId)
        {
            SqlConnection con = new SqlConnection(sqlmapper.DataSource.ConnectionString);
            string sqlStr = "select a.HrDailyEmployeeAttendInfoId, a.DutyDate,b.EmployeeId,b.EmployeeName,a.ShouldCheckIn,a.ShouldCheckOut,a.ActualCheckIn,a.ActualCheckOut,a.Note from  HrDailyEmployeeAttendInfo as a inner join Employee as b on a.EmployeeId=b.EmployeeId where HrDailyEmployeeAttendInfoId='" + HrDailyEmployeeAttendInfoId + "' order by DutyDate";
            SqlDataAdapter adapter = new SqlDataAdapter(sqlStr, con);
            DataSet data = new DataSet();
            adapter.Fill(data);
            return data;
        }
        public void DeleteLateInfoByEmployeeIdAndDate(string EmployeeId, DateTime ClockDate)
        {
            SqlConnection con = new SqlConnection(sqlmapper.DataSource.ConnectionString);
            string sqlStr = "delete from LateRecord  where EmployeeId='" + EmployeeId + "' and ClockDate='" + ClockDate + "'";
            SqlCommand cmd = new SqlCommand(sqlStr, con);
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                con.Close();
            }

        }
        //班别津贴
        public int SelectBusinessHourPaySum(int years, int months, string employeeid)
        {

            SqlConnection conn = new SqlConnection(sqlmapper.DataSource.ConnectionString);
            SqlCommand da = new SqlCommand("select @value=sum(specialbonus)   from HrDailyEmployeeAttendInfo where year(dutydate)=@years and month(dutydate)=@months and  employeeid=@employeeid ", conn);

            conn.Open();

            da.Parameters.AddRange(new SqlParameter[] { new SqlParameter("@years", SqlDbType.Int), new SqlParameter("@months", SqlDbType.Int), new SqlParameter("@employeeid", SqlDbType.VarChar, 50), new SqlParameter("@value", SqlDbType.Int) });


            da.Parameters["@years"].Value = years;
            da.Parameters["@months"].Value = months;
            da.Parameters["@employeeid"].Value = employeeid;
            da.Parameters["@value"].Direction = ParameterDirection.Output;

            da.ExecuteNonQuery();
            conn.Close();

            if (da.Parameters["@value"].Value == null || da.Parameters["@value"].Value.ToString() == "")
                da.Parameters["@value"].Value = 0;

            return Int32.Parse(da.Parameters["@value"].Value.ToString());

            //Hashtable ht = new Hashtable();
            //ht.Add("years", years);
            //ht.Add("months", months);
            //ht.Add("employeeid", employeeid);
            //return sqlmapper.QueryForObject<Int32>("HrDailyEmployeeAttendInfo.selectBusinessHourPaySum",ht);

            //SqlConnection conn = new SqlConnection(sqlmapper.DataSource.ConnectionString);
            //SqlCommand da = new SqlCommand("select sum(specialbonus) as sum  from HrDailyEmployeeAttendInfo where year(dutydate)=" + years + " and month(dutydate)=" + months + " and  employeeid='" + employeeid + "' ", conn);
            //conn.Open();
            //SqlDataReader reader = da.ExecuteReader(CommandBehavior.CloseConnection);
            //reader.Read();
            //decimal sum = 0;
            //if (reader.GetValue(0) != DBNull.Value)
            //    sum = decimal.Parse(reader.GetValue(0).ToString());
            //reader.Close();
            //return sum;

        }
        public decimal SelectDayFactorSum(int years, int months, Model.Employee employee)
        {
            decimal sum = decimal.Zero;
            SqlConnection conn = new SqlConnection(sqlmapper.DataSource.ConnectionString);
            SqlCommand da = new SqlCommand("select sum(dayfactor) from HrDailyEmployeeAttendInfo where employeeid='" + employee.EmployeeId + "' and year(dutydate)=" + years + " and month(dutydate)=" + months, conn);
            conn.Open();
            SqlDataReader reader = da.ExecuteReader(CommandBehavior.CloseConnection);
            reader.Read();
            if (!string.IsNullOrEmpty(reader.GetValue(0).ToString()))

                sum = decimal.Parse(reader.GetValue(0).ToString());
            //非离职出勤不到满月  排除例假数。请假数，旷职数
            if (this.SelectDutyDateCount(years, months, employee) < DateTime.DaysInMonth(years, months))
            {
                sum = SelectDayMonthSum(years, months, employee) - leaveAccessor.SelectTotalHolidayMonthEmp(employee, years, months) - leaveAccessor.SelectCountByMonthEmp(employee, years, months) - this.SelectAbsentCount(years, months, employee);

            }
            //else  if( this.SelectDutyDateCount(years, months, employee)<DateTime.DaysInMonth(years,months)&&employee.EmployeeLeaveDate==null||employee.EmployeeLeaveDate==global::Helper.DateTimeParse.NullDate )
            //{
            //    sum = SelectDayMonthSum(years, months, employee) - leaveAccessor.SelectTotalHolidayMonthEmp(employee, years, months) - leaveAccessor.SelectCountByMonthEmp(employee, years, months) - this.SelectAbsentCount(years, months, employee);

            //}             
            return sum;

        }
        public decimal SelectDayMonthSum(int years, int months, Model.Employee employee)
        {
            decimal sum = decimal.Zero;
            SqlConnection conn = new SqlConnection(sqlmapper.DataSource.ConnectionString);
            SqlCommand da = new SqlCommand("select sum(monthfactor) as sum  from HrDailyEmployeeAttendInfo where year(dutydate)=" + years + " and month(dutydate)=" + months + " and  employeeid='" + employee.EmployeeId + "'", conn);
            conn.Open();
            SqlDataReader reader = da.ExecuteReader(CommandBehavior.CloseConnection);
            reader.Read();
            if (!string.IsNullOrEmpty(reader.GetValue(0).ToString()))
                sum = decimal.Parse(reader.GetValue(0).ToString());
            reader.Close();
            return sum;
        }
        public System.Data.DataSet SelectHrInfoByEmployeeIdAndDueDate(string employeeId, DateTime dueDate)
        {
            SqlConnection con = new SqlConnection(sqlmapper.DataSource.ConnectionString);
            string sqlStr = "select * from HrDailyEmployeeAttendInfo where EmployeeId='" + employeeId + "' and DutyDate='" + dueDate.ToString("yyyy-MM-dd HH:ss:mm") + "' order by DutyDate";
            SqlDataAdapter adapter = new SqlDataAdapter(sqlStr, con);
            DataSet data = new DataSet();
            adapter.Fill(data);
            return data;
        }
        //总出勤数
        public int SelectDutyDateCount(int years, int months, Model.Employee employee)
        {
            int sum = 0;
            SqlConnection conn = new SqlConnection(sqlmapper.DataSource.ConnectionString);
            SqlCommand da = new SqlCommand("select count(*) from hrDailyEmployeeAttendInfo where employeeid='" + employee.EmployeeId + "' and year(dutydate)=" + years + " and month(dutydate)=" + months, conn);
            conn.Open();
            SqlDataReader reader = da.ExecuteReader(CommandBehavior.CloseConnection);
            reader.Read();
            if (!string.IsNullOrEmpty(reader.GetValue(0).ToString()))

                sum = Int32.Parse(reader.GetValue(0).ToString());
            reader.Close();
            return sum;
        }
        /// <summary>
        /// 月旷职数
        /// </summary>
        /// <param name="years"></param>
        /// <param name="months"></param>
        /// <param name="employee"></param>
        /// <returns></returns>
        public int SelectAbsentCount(int years, int months, Model.Employee employee)
        {
            int sum = 0;
            SqlConnection conn = new SqlConnection(sqlmapper.DataSource.ConnectionString);
            SqlCommand da = new SqlCommand("select count(*) from hrDailyEmployeeAttendInfo where employeeid='" + employee.EmployeeId + "' and year(dutydate)=" + years + " and month(dutydate)=" + months + " and Note like '%曠職%' ", conn);
            conn.Open();
            SqlDataReader reader = da.ExecuteReader(CommandBehavior.CloseConnection);
            reader.Read();
            if (!string.IsNullOrEmpty(reader.GetValue(0).ToString()))
                sum = Int32.Parse(reader.GetValue(0).ToString());
            reader.Close();
            return sum;
        }
        /// <summary>
        /// 迟到次数
        /// </summary>
        /// <param name="years"></param>
        /// <param name="months"></param>
        /// <param name="employee"></param>
        /// <returns></returns>

        public int SelectLateCount(int years, int months, Model.Employee employee)
        {
            int sum = 0;
            SqlConnection conn = new SqlConnection(sqlmapper.DataSource.ConnectionString);
            SqlCommand da = new SqlCommand("select count(*) from hrDailyEmployeeAttendInfo where employeeid='" + employee.EmployeeId + "' and year(dutydate)=" + years + " and month(dutydate)=" + months + " and LateInMinute > 0 ", conn);
            conn.Open();
            SqlDataReader reader = da.ExecuteReader(CommandBehavior.CloseConnection);
            reader.Read();
            if (!string.IsNullOrEmpty(reader.GetValue(0).ToString()))
                sum = Int32.Parse(reader.GetValue(0).ToString());
            reader.Close();
            return sum;
        }
        /// <summary>
        /// 总迟到分数
        /// </summary>
        /// <param name="years"></param>
        /// <param name="months"></param>
        /// <param name="employee"></param>
        /// <returns></returns>
        public int SelectLateSum(int years, int months, Model.Employee employee)
        {
            int sum = 0;
            SqlConnection conn = new SqlConnection(sqlmapper.DataSource.ConnectionString);
            SqlCommand da = new SqlCommand("select SUM(LateInMinute) from hrDailyEmployeeAttendInfo where employeeid='" + employee.EmployeeId + "' and year(dutydate)=" + years + " and month(dutydate)=" + months + " and LateInMinute > 0 ", conn);
            conn.Open();
            SqlDataReader reader = da.ExecuteReader(CommandBehavior.CloseConnection);
            reader.Read();
            if (!string.IsNullOrEmpty(reader.GetValue(0).ToString()))
                sum = Int32.Parse(reader.GetValue(0).ToString());
            reader.Close();
            return sum;
        }

        //最近更新的時間
        public DateTime GetUpdateTime(DateTime dt,string employeeid)
        {
            SqlConnection con = new SqlConnection(sqlmapper.DataSource.ConnectionString);
            con.Open();
            try
            {
                string sqlStr = "SELECT max(DutyDate) as DutyDate FROM HrDailyEmployeeAttendInfo WHERE  month(DutyDate)=month('" + dt.ToString("yyyy-MM-dd") + "') AND year(DutyDate)=year('" + dt.ToString("yyyy-MM-dd") + "') AND employeeid='" + employeeid + "'";
                SqlCommand cmd = new SqlCommand(sqlStr, con);
                cmd.ExecuteScalar();
                if (cmd.ExecuteScalar()!=System .DBNull.Value)
                    return Convert.ToDateTime(cmd.ExecuteScalar());
                else return global::Helper .DateTimeParse .NullDate;
               
            }
            catch (Exception)
            {
                throw;
            }
            con.Close();
        }

        public IList<Model.HrDailyEmployeeAttendInfo> SelectHrInfoByEmployeeIdAndDueDate1(string employeeId, DateTime dueDate)
        {
            Hashtable ht = new Hashtable();
            ht.Add("employeeid", employeeId);
            ht.Add("dueDate", dueDate);
            return sqlmapper.QueryForList<Model.HrDailyEmployeeAttendInfo>("HrDailyEmployeeAttendInfo.SelectHrInfoByEmployeeIdAndDueDate", ht);
        }

    }
}
