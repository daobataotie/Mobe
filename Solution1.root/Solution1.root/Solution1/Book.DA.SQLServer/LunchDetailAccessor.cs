//------------------------------------------------------------------------------
//
// file name：LunchDetailAccessor.cs
// author: peidun
// create date：2010-3-26 11:08:17
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Book.Model;

namespace Book.DA.SQLServer
{
    /// <summary>
    /// Data accessor of LunchDetail
    /// </summary>
    public partial class LunchDetailAccessor : EntityAccessor, ILunchDetailAccessor
    {

        /// <summary>
        /// 根据员工和年月查询
        /// </summary>
        /// <param name="emp"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public IList<Model.LunchDetail> selectByempAndDate(Employee emp, int year,int month)
        {
            Hashtable pars = new Hashtable();
            pars.Add("EmployeeId", emp.EmployeeId);
            pars.Add("year", year);
            pars.Add("month",month);
            return sqlmapper.QueryForList<Model.LunchDetail>("LunchDetail.select_byempAnddate", pars);
        }



        public IList<Model.LunchDetail> GetLunchDetailbyempiddate(string empid, DateTime starttime, DateTime endtime)
        {
            Hashtable pars = new Hashtable();
            pars.Add("empid", empid);
            pars.Add("startdate", starttime);
            pars.Add("enddate", endtime);
            return sqlmapper.QueryForList<Model.LunchDetail>("LunchDetail.select_byempiddate", pars);
        }
        public decimal SelectFeeSum(Model.Employee employee, int year, int month)
        {

            decimal sum = decimal.Zero;
            SqlConnection conn = new SqlConnection(sqlmapper.DataSource.ConnectionString);
            SqlCommand da = new SqlCommand("select sum(ShouldPay) as sum  from LunchDetail where year(MarkDate)=" + year + " and month(MarkDate)=" + month + " and  employeeid='" + employee.EmployeeId + "'", conn);
            conn.Open();
            SqlDataReader reader = da.ExecuteReader(CommandBehavior.CloseConnection);
            reader.Read();
            if (!string.IsNullOrEmpty(reader.GetValue(0).ToString()))

                sum = decimal.Parse(reader.GetValue(0).ToString());
            reader.Close();
            return sum;
        }

        #region 根据时间查询符合条件的员工信息
        public DataSet GetEmployeeInfo(DateTime date)
        {
            SqlConnection conn = new SqlConnection(sqlmapper.DataSource.ConnectionString);
            SqlDataAdapter da = new SqlDataAdapter("select e.EmployeeId,e.idno ,e.employeename ,t.departmentid,t.departmentname ,isnull(l.LunchFee,0) LunchFee,l.MarkDate as MarkDate ,isnull(l.ShouldPay,0) ShouldPay ,l.LunchDetailId  from employee e LEFT join department t on t.DepartmentId =e.DepartmentId LEFT join (SELECT * FROM LunchDetail WHERE year(MarkDate)=" + date.Year + " and month(MarkDate)=" + date.Month + " and day(MarkDate)=" + date.Day + ") l on l.EmployeeId=e.EmployeeId WHERE e.EmployeeLeaveDate IS NULL OR (year(e.EmployeeLeaveDate)=1900 AND month(e.EmployeeLeaveDate)=01 AND day(e.EmployeeLeaveDate)=01) order by e.IDNo ", conn);
            DataSet ds = new DataSet();
            da.Fill(ds, "employee");
            return ds;
        }
        #endregion

        public DataSet GetEmployeeInfo(string year,string month)
        {
            SqlConnection conn = new SqlConnection(sqlmapper.DataSource.ConnectionString);
            SqlDataAdapter da = new SqlDataAdapter("select e.EmployeeId,e.idno ,e.employeename ,t.departmentid,t.departmentname ,isnull(l.LunchFee,0) LunchFee,l.MarkDate as MarkDate ,isnull(l.ShouldPay,0) ShouldPay ,l.LunchDetailId  from employee e LEFT join department t on t.DepartmentId =e.DepartmentId LEFT join (SELECT * FROM LunchDetail WHERE year(MarkDate)=" + year + " and month(MarkDate)=" + month + ") l on l.EmployeeId=e.EmployeeId WHERE e.EmployeeLeaveDate IS NULL OR (year(e.EmployeeLeaveDate)=1900 AND month(e.EmployeeLeaveDate)=01 AND day(e.EmployeeLeaveDate)=01) order by e.IDNo ", conn);
            DataSet ds = new DataSet();
            da.Fill(ds, "employee");
            return ds;
        }

        #region 修改
        public void UpdateLunchDetail(DataSet dataset, DateTime date)
        {
            for (int i = 0; i < dataset.Tables.Count; i++)
            {
                this.Delete(dataset.Tables[0].Rows[i]["LunchDetailId"].ToString());
            }

            SqlConnection conn = new SqlConnection(sqlmapper.DataSource.ConnectionString);
            SqlDataAdapter da = new SqlDataAdapter();
            da.UpdateCommand = new SqlCommand("insert into lunchdetail(LunchDetailId,EmployeeId,InsertTime,UpdateTime,LunchFee,ShouldPay,MarkDate) values(newid(),@EmployeeId,getdate(),getdate(),@LunchFee,@ShouldPay,'" + date .Date+ "')", conn);
            SqlParameter[] spr = new SqlParameter[] { 
                new SqlParameter("@EmployeeId",SqlDbType.VarChar,50,"EmployeeId"),
                new SqlParameter("@LunchFee",SqlDbType.Money,8,"LunchFee"),
                new SqlParameter("@ShouldPay",SqlDbType.Money,8,"ShouldPay"),
                
            };
            da.UpdateCommand.Parameters.AddRange(spr);
            da.Update(dataset.Tables["employee"]);
        }
        #endregion
    }
}
