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
        public Model.MonthlySalary GetByeEmpIdMonth(Model.Employee employee,int year,int month  )
        {
            Hashtable ht = new Hashtable();
            ht.Add("employeeid", employee.EmployeeId);
            ht.Add("year", year);
            ht.Add("month", month);
            return sqlmapper.QueryForObject<Model.MonthlySalary>("MonthlySalary.get_byempidMonth", ht);
        }

        public void UpdateDataSet(Model.MonthlySalaryStruct monthlySalaryStruct)
        {
            SqlConnection conn = new SqlConnection(sqlmapper.DataSource.ConnectionString);
          
           SqlCommand command= new SqlCommand("update  MonthlySalary set EffectFactor=@EffectFactor,OtherPay=@OtherPay,OtherPunish=@OtherPunish,HolidayBonusGivenDays=@HolidayBonusGivenDays where EmployeeId=@EmployeeId and convert(varchar(20),IdentifyDate,101)=convert(varchar(20),@IdentifyDate,101)", conn);
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
                 new SqlParameter("@MonthlySalaryId",SqlDbType.VarChar,50) , 
                new SqlParameter("@EffectFactor",SqlDbType.Money,8),
                new SqlParameter("@OtherPay",SqlDbType.Money,8),
                new SqlParameter("@OtherPunish",SqlDbType.Money,8),
                new SqlParameter("@AnnualBonus",SqlDbType.Money,8) ,
                new SqlParameter("@EmployeeId",SqlDbType.VarChar,50),
                new SqlParameter("@IdentifyDate",SqlDbType.DateTime,8),
                 new SqlParameter("@HolidayBonusGivenDays",SqlDbType.Float,8)        
                };
             command.Parameters.AddRange(sqlParameter1);

             command.Parameters["@MonthlySalaryId"].Value = monthlySalaryStruct.MonthlySalaryId;
             command.Parameters["@EffectFactor"].Value = monthlySalaryStruct.EffectFactor;
             command.Parameters["@OtherPay"].Value = monthlySalaryStruct.OtherPay;
             command.Parameters["@OtherPunish"].Value = monthlySalaryStruct.OtherPunish;
             command.Parameters["@AnnualBonus"].Value = monthlySalaryStruct.AnnualBonus;
             command.Parameters["@EmployeeId"].Value = monthlySalaryStruct.EmployeeId;
             command.Parameters["@IdentifyDate"].Value = monthlySalaryStruct.IdentifyDate;
             command.Parameters["@HolidayBonusGivenDays"].Value = monthlySalaryStruct.HolidayBonusGivenDays; 
           
        
           // da.InsertCommand.Parameters.AddRange(sqlParameter);
             command.ExecuteNonQuery();
             conn.Close();
        }

        public DataSet GetMonthlySummaryFee(string employeeid, int years, int months)
        {
           
            SqlConnection conn = new SqlConnection(sqlmapper.DataSource.ConnectionString);
            SqlCommand command = new SqlCommand("MonthlySummaryS", conn);
            command.CommandType = CommandType.StoredProcedure;
            SqlParameter[] parameters = { new SqlParameter("@EmployeeId", SqlDbType.VarChar,50), new SqlParameter("@year", SqlDbType.Int), new SqlParameter("@month", SqlDbType.Int) };

            parameters[0].Value = employeeid;
            parameters[1].Value = years;
            parameters[2].Value = months;
            command.Parameters.AddRange(parameters);
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = command;
            DataSet ds = new DataSet();
            da.Fill(ds,"monthlySalary");
            return ds;                         
        }
        public DataSet  GetMonthlySummaryByMonth( int years, int months)
        {
            SqlConnection conn = new SqlConnection(sqlmapper.DataSource.ConnectionString);
            SqlCommand command = new SqlCommand("select * from monthlySalary where year(IdentifyDate)=" + years + " and month(IdentifyDate)="+months, conn);
            //command.CommandType = CommandType.StoredProcedure;
            //SqlParameter[] parameters = { new SqlParameter("@employeeid", SqlDbType.VarChar, 50), new SqlParameter("@year", SqlDbType.Int), new SqlParameter("@month", SqlDbType.Int) };
            //parameters[0].Value = employeeid;
            //parameters[1].Value = years;
            //parameters[2].Value = months;
            //command.Parameters.AddRange(parameters);
            conn.Open();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = command;
            DataSet ds = new DataSet();
            da.Fill(ds, "monthlySalary");
            return ds;
           //;

           //// using (SqlDataReader dr = command.ExecuteReader())
           // {
           //     SqlDataReader dr = command.ExecuteReader();
           //     conn.Close();
           //     return dr;
           // }

           

        }

    }
}
