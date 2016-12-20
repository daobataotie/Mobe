//------------------------------------------------------------------------------
//
// file name：LoanDetailAccessor.cs
// author: peidun
// create date：2010-3-15 14:29:52
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
    /// Data accessor of LoanDetail
    /// </summary>
    public partial class LoanDetailAccessor : EntityAccessor, ILoanDetailAccessor
    {
        public IList<Model.LoanDetail> Select(Model.LoanDetail loandetails)
        {
            return sqlmapper.QueryForList<Model.LoanDetail>("SelectLoanInfoBy_EmployeeID", loandetails);
        }

        public IList<Model.LoanDetail> SelectByCondition(string employeeId, DateTime startDate, DateTime endDate)
        {
            Hashtable hash = new Hashtable();
            string EmployeeID = string.IsNullOrEmpty(employeeId) ?null : employeeId;
            hash.Add("EmployeeID", EmployeeID);
            hash.Add("startDate", startDate);
            hash.Add("endDate", endDate);
            return sqlmapper.QueryForList<Model.LoanDetail>("LoanDetail.SelectLoanInfoBy_DateOrEmployee", hash);
        }


        public IList<Model.LoanDetail> GetLoanDetailbyempiddate(string empid, DateTime starttime, DateTime endtime)
        {
            Hashtable pars = new Hashtable();
            pars.Add("empid", empid);
            pars.Add("startdate", starttime);
            pars.Add("enddate", endtime);
            return sqlmapper.QueryForList<Model.LoanDetail>("LoanDetail.select_byempiddate", pars);
        }
        public decimal SelectFeeSum(Model.Employee employee, int year, int month)
        {

            decimal sum = decimal.Zero;
            SqlConnection conn = new SqlConnection(sqlmapper.DataSource.ConnectionString);
            SqlCommand da = new SqlCommand("select sum(LoanFee) as sum  from LoanDetail where year(IdentifyDate)=" + year + " and month(IdentifyDate)=" + month + " and  employeeid='" + employee.EmployeeId + "'", conn);
            conn.Open();
            SqlDataReader reader = da.ExecuteReader(CommandBehavior.CloseConnection);
            reader.Read();
            if (!string.IsNullOrEmpty( reader.GetValue(0).ToString()))
                sum = decimal.Parse(reader.GetValue(0).ToString());
            reader.Close();
            return sum;
        }



        /// <summary>
        /// 借出记录查询，返回dataset
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        public DataSet SelestLoanList(int year, int month)
        {
            SqlConnection conn = new SqlConnection(sqlmapper.DataSource.ConnectionString);
            SqlDataAdapter da = new SqlDataAdapter("select e.IDNo,e.EmployeeId,e.EmployeeName,d.DepartmentName,l.LoanId ,l.IsCash,isnull(l.LoanFee,0) LoanFee,l.IdentifyDate from employee e left join Department d on e.DepartmentId = d.DepartmentId left join (SELECT * FROM LoanDetail WHERE year(IdentifyDate)=" + year + " AND month(IdentifyDate)=" + month + ") l on e.EmployeeId = l.EmployeeId WHERE e.EmployeeLeaveDate IS NULL OR (year(e.EmployeeLeaveDate)=1900 AND month(e.EmployeeLeaveDate)=01 AND day(e.EmployeeLeaveDate)=01)", conn);

            DataSet ds = new DataSet();
            da.Fill(ds, "LoanListInfo");
            return ds;

        }


        /// <summary>
        /// 查询所有借出记录
        /// </summary>
        /// <returns></returns>
        public DataSet SelestLoanAll()
        {
            SqlConnection conn = new SqlConnection(sqlmapper.DataSource.ConnectionString);
            SqlDataAdapter da = new SqlDataAdapter("select e.IDNo ,e.EmployeeId ,e.EmployeeName ,d.DepartmentName , l.LoanId,l.IsCash, l.LoanFee,l.IdentifyDate from employee e left join Department d on e.DepartmentId = d.DepartmentId left join LoanDetail l on e.EmployeeId = l.EmployeeId order by e.IDNo  ", conn);

            DataSet ds = new DataSet();
            da.Fill(ds, "LoanListInfo");
            return ds;
        }


        /// <summary>
        /// 根据借出编号删除
        /// </summary>
        /// <returns></returns>
        public int DeleteByIDNo(string LoanId)
        {
            SqlConnection conn = new SqlConnection(sqlmapper.DataSource.ConnectionString);
            SqlCommand da = new SqlCommand("delete  from LoanDetail where LoanId='" + LoanId + "' ", conn);
            int result;
            conn.Open();
            try
            {
                 result = da.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                return 0;
                throw e;
            }
            return result;
        }





        /// <summary>
        /// 修改借出记录
        /// </summary>
        /// <param name="loanDetail"></param>
        /// <returns></returns>
        public int UpdateLoanDetail(DataSet ds, DateTime SelectDate)
        {


            for(int i=0;i<ds.Tables.Count;i++)
            {
                this.DeleteByIDNo(ds.Tables[0].Rows[i]["LoanId"].ToString());
            }


            SqlConnection conn = new SqlConnection(sqlmapper.DataSource.ConnectionString);
            SqlDataAdapter da = new SqlDataAdapter();
            da.UpdateCommand = new SqlCommand("insert into LoanDetail values(newid(),@EmployeeId,@IsCash,@LoanFee,'" + SelectDate + "') ", conn);

            SqlParameter[] par = new SqlParameter[]
            {
                new SqlParameter("@EmployeeId",SqlDbType.VarChar,50,"EmployeeId"),
                new SqlParameter("@IsCash",SqlDbType.Int,10,"IsCash"),
                new SqlParameter("@LoanFee",SqlDbType.Money,10,"LoanFee")
            };
            da.UpdateCommand.Parameters.AddRange(par);

            int result = da.Update(ds.Tables["LoanListInfo"]);
            return result;
        }
    }
}
