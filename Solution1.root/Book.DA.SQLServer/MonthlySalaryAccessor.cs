//------------------------------------------------------------------------------
//
// file name：MonthlySalaryAccessor.cs
// author: peidun
// create date：2010-3-24 11:21:42
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
    /// Data accessor of MonthlySalary
    /// </summary>
    /// 

    public partial class MonthlySalaryAccessor : EntityAccessor, IMonthlySalaryAccessor
    {
        public Model.MonthlySalary getMonthsalarybyempid(string employeeid)
        {
            return sqlmapper.QueryForObject<Model.MonthlySalary>("MonthlySalary.get_byempid", employeeid);
        }
        public Model.MonthlySalary GetByeEmpIdMonth(Model.Employee employee, int year, int month)
        {
            Hashtable ht = new Hashtable();
            ht.Add("employeeid", employee.EmployeeId);
            ht.Add("year", year);
            ht.Add("month", month);
            return sqlmapper.QueryForObject<Model.MonthlySalary>("MonthlySalary.get_byempidMonth", ht);
        }

        public void UpdateDataSet(Model.MonthlySalaryStruct monthlySalaryStruct)
        {
            using (SqlConnection conn = new SqlConnection(sqlmapper.DataSource.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("update MonthlySalary set EffectFactor=@EffectFactor,OtherPay=@OtherPay,OtherPunish=@OtherPunish,HolidayBonusGivenDays=@HolidayBonusGivenDays where EmployeeId=@EmployeeId and convert(varchar(20),IdentifyDate,101)=convert(varchar(20),@IdentifyDate,101)", conn))
                {
                    conn.Open();
                    //SqlParameter[] sqlParameter = new SqlParameter[]{
                    //     new SqlParameter("@MonthlySalaryId",SqlDbType.VarChar,50) , 
                    //    new SqlParameter("@EffectFactor",SqlDbType.Money,8),
                    //    new SqlParameter("@OtherPay",SqlDbType.Money,8),
                    //    new SqlParameter("@OtherPunish",SqlDbType.Money,8),
                    //    new SqlParameter("@AnnualBonus",SqlDbType.Money,8) ,
                    //    new SqlParameter("@EmployeeId",SqlDbType.VarChar,50),
                    //    new SqlParameter("@IdentifyDate",SqlDbType.DateTime,8)          
                    //    };
                    //da.InsertCommand.Parameters["@MonthlySalaryId"].Value = monthlySalaryStruct.MonthlySalaryId;
                    //da.InsertCommand.Parameters["@EffectFactor"].Value = monthlySalaryStruct.EffectFactor;
                    //da.InsertCommand.Parameters["@OtherPay"].Value = monthlySalaryStruct.OtherPay;
                    //da.InsertCommand.Parameters["@OtherPunish"].Value = monthlySalaryStruct.OtherPunish;
                    //da.InsertCommand.Parameters["@AnnualBonus"].Value = monthlySalaryStruct.AnnualBonus;
                    //da.InsertCommand.Parameters["@EmployeeId"].Value = monthlySalaryStruct.EmployeeId;
                    //da.InsertCommand.Parameters["@IdentifyDate"].Value = monthlySalaryStruct.IdentifyDate;
                    //da.UpdateCommand = new SqlCommand("update  MonthlySalary set EffectFactor=@EffectFactor,OtherPay=@OtherPay,OtherPunish=@OtherPunish,HolidayBonusGivenDays=@HolidayBonusGivenDays where EmployeeId=@EmployeeId and convert(varchar(20),IdentifyDate,101)=convert(varchar(20),@IdentifyDate,101)", conn);
                    SqlParameter[] sqlParameter1 = new SqlParameter[]{              
                new SqlParameter("@EffectFactor",SqlDbType.Money,8),
                new SqlParameter("@OtherPay",SqlDbType.Money,8),
                new SqlParameter("@OtherPunish",SqlDbType.Money,8),
                new SqlParameter("@AnnualBonus",SqlDbType.Money,8) ,
                new SqlParameter("@EmployeeId",SqlDbType.VarChar,50),
                new SqlParameter("@IdentifyDate",SqlDbType.DateTime,8),
                 new SqlParameter("@HolidayBonusGivenDays",SqlDbType.Float,8)        
                };
                    command.Parameters.AddRange(sqlParameter1);
                    command.Parameters["@EffectFactor"].Value = monthlySalaryStruct.EffectFactor;
                    command.Parameters["@OtherPay"].Value = monthlySalaryStruct.OtherPay;
                    command.Parameters["@OtherPunish"].Value = monthlySalaryStruct.OtherPunish;
                    command.Parameters["@AnnualBonus"].Value = monthlySalaryStruct.AnnualBonus;
                    command.Parameters["@EmployeeId"].Value = monthlySalaryStruct.EmployeeId;
                    command.Parameters["@IdentifyDate"].Value = monthlySalaryStruct.IdentifyDate;
                    command.Parameters["@HolidayBonusGivenDays"].Value = monthlySalaryStruct.HolidayBonusGivenDays;

                    command.ExecuteNonQuery();
                
                }
            }
        }

       
        public DataSet GetMonthlySummaryByMonth(int years, int months)
        {
              SqlParameter[] parameters = { new SqlParameter("@year", SqlDbType.Int), new SqlParameter("@month", SqlDbType.Int) };
        
            parameters[0].Value = years;
            parameters[1].Value = months;
            return   SQLDB.DbHelperSQL.RunProcedure("SelectMonthlySalaryByMM", parameters, "monthlySalary");
            //SqlConnection conn = new SqlConnection(sqlmapper.DataSource.ConnectionString);
            //SqlCommand command = new SqlCommand("select * from monthlySalary where year(IdentifyDate)=" + years + " and month(IdentifyDate)=" + months, conn);
            //command.CommandType = CommandType.StoredProcedure;
            //SqlParameter[] parameters = { new SqlParameter("@employeeid", SqlDbType.VarChar, 50), new SqlParameter("@year", SqlDbType.Int), new SqlParameter("@month", SqlDbType.Int) };
            //parameters[0].Value = employeeid;
            //parameters[1].Value = years;
            //parameters[2].Value = months;
            //command.Parameters.AddRange(parameters);
            //conn.Open();
            //SqlDataAdapter da = new SqlDataAdapter();
            //da.SelectCommand = command;
            //DataSet ds = new DataSet();
            //da.Fill(ds, "monthlySalary");
            //return ds;
            //;

            //// using (SqlDataReader dr = command.ExecuteReader())
            // {
            //     SqlDataReader dr = command.ExecuteReader();
            //     conn.Close();
            //     return dr;
            // }           

        }
        //取MonthlySalary
        public DataSet getMonthlySalary(string employeeid, DateTime date)
        {
            SqlParameter[] parameters = { new SqlParameter("@EmployeeId", SqlDbType.VarChar, 50), new SqlParameter("@IdentifyDate", SqlDbType.DateTime) };
            parameters[0].Value = employeeid;
            parameters[1].Value = date;
            return SQLDB.DbHelperSQL.RunProcedure("getMonthlySalary", parameters, "empmonthlySalary");         

        }
        //取考勤记录
        public DataSet getAttendInfoList(string employeeid, int years, int months)
        {
            SqlParameter[] parameters = { new SqlParameter("@EmployeeId", SqlDbType.VarChar, 50), new SqlParameter("@Year", SqlDbType.Int), new SqlParameter("@Month", SqlDbType.Int) };
            parameters[0].Value = employeeid;
            parameters[1].Value = years;
            parameters[2].Value = months;
            return SQLDB.DbHelperSQL.RunProcedure("getAttendInfoList", parameters, "AttendInfoList");         

            //SqlConnection conn = new SqlConnection(sqlmapper.DataSource.ConnectionString);
            //SqlCommand command = new SqlCommand("getAttendInfoList", conn);
            //command.CommandType = CommandType.StoredProcedure;
            //SqlParameter[] parameters = { new SqlParameter("@EmployeeId", SqlDbType.VarChar, 50), new SqlParameter("@Year", SqlDbType.Int), new SqlParameter("@Month", SqlDbType.Int) };
            //parameters[0].Value = employeeid;
            //parameters[1].Value = years;
            //parameters[2].Value = months;
            //command.Parameters.AddRange(parameters);
            //conn.Open();
            //SqlDataAdapter da = new SqlDataAdapter();
            //da.SelectCommand = command;
            //DataSet ds = new DataSet();
            //da.Fill(ds, "AttendInfo");
            //return ds;
            //;

            //// using (SqlDataReader dr = command.ExecuteReader())
            // {
            //     SqlDataReader dr = command.ExecuteReader();
            //     conn.Close();
            //     return dr;
            // }          

        }
        //取薪资计算 和
        public DataSet getMonthlySummaryFee(string employeeid, DateTime date, int years, int months)
        {
            SqlParameter[] parameters = { new SqlParameter("@EmployeeId", SqlDbType.VarChar, 50), new SqlParameter("@IdentifyDate", SqlDbType.DateTime), new SqlParameter("@Year", SqlDbType.Int), new SqlParameter("@Month", SqlDbType.Int) };
            parameters[0].Value = employeeid;
            parameters[1].Value = date;
            parameters[2].Value = years;
            parameters[3].Value = months;
            return SQLDB.DbHelperSQL.RunProcedure("getMonthlySummaryFee", parameters, "MonthlySummaryFee");  



            //SqlConnection conn = new SqlConnection(sqlmapper.DataSource.ConnectionString);
            //SqlCommand command = new SqlCommand("getMonthlySummaryFee", conn);
            //command.CommandType = CommandType.StoredProcedure;
            //SqlParameter[] parameters = { new SqlParameter("@EmployeeId", SqlDbType.VarChar, 50), new SqlParameter("@IdentifyDate", SqlDbType.DateTime), new SqlParameter("@Year", SqlDbType.Int), new SqlParameter("@Month", SqlDbType.Int) };
            //parameters[0].Value = employeeid;
            //parameters[1].Value = date;
            //parameters[2].Value = years;
            //parameters[3].Value = months;
            //command.Parameters.AddRange(parameters);
            //conn.Open();
            //SqlDataAdapter da = new SqlDataAdapter();
            //da.SelectCommand = command;
            //DataSet ds = new DataSet();
            //da.Fill(ds, "MonthlySummaryFee");
            //return ds;
        }

        //更新基础设置
        public int UpMonthSalFromClockFrm(Model.Employee emp, DateTime UpdateTime)
        {
            SqlParameter[] parameters = 
            {
              new SqlParameter("@Id",Guid.NewGuid()),
              new SqlParameter("@IdentifyDate",UpdateTime),
              new SqlParameter("@EmployeeId",emp.EmployeeId),
              new SqlParameter("@DailyPay",emp.DailyPay),
              new SqlParameter("@MonthlyPay",emp.MonthlyPay),
              new SqlParameter("@DutyPay",emp.DutyPay),
              new SqlParameter("@PostPay",emp.PostPay),
              new SqlParameter("@FieldPay",emp.FieldPay),
              new SqlParameter("@Insurance",emp.Insurance),
              new SqlParameter("@Tax",emp.Tax)
            };      
            int i=0;
            return  SQLDB.DbHelperSQL.RunProcedure("addSalaryList", parameters, out i);            
            //SqlConnection conn = new SqlConnection(sqlmapper.DataSource.ConnectionString);
            //SqlCommand command = new SqlCommand("addSalaryList", conn);
            //command.CommandType = CommandType.StoredProcedure;
            //SqlParameter[] parameters = 
            //{
            //  new SqlParameter("@Id",Guid.NewGuid()),
            //  new SqlParameter("@IdentifyDate",UpdateTime),
            //  new SqlParameter("@EmployeeId",emp.EmployeeId),
            //  new SqlParameter("@DailyPay",emp.DailyPay),
            //  new SqlParameter("@MonthlyPay",emp.MonthlyPay),
            //  new SqlParameter("@DutyPay",emp.DutyPay),
            //  new SqlParameter("@PostPay",emp.PostPay),
            //  new SqlParameter("@FieldPay",emp.FieldPay),
            //  new SqlParameter("@Insurance",emp.Insurance),
            //  new SqlParameter("@Tax",emp.Tax)
            //};
            //command.ExecuteNonQuery();
        }

        public DateTime getMaxIdentifyDateMonth()
        {
            return sqlmapper.QueryForObject<DateTime>("MonthlySalary.get_MaxIdentifyDateMonth", null);
        }
    }

}
