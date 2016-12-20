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
            string EmployeeID = string.IsNullOrEmpty(employeeId) ? null : employeeId;
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
            using (SqlConnection conn = new SqlConnection(sqlmapper.DataSource.ConnectionString))
            {
                SqlCommand da = new SqlCommand("select sum(LoanFee) as sum  from LoanDetail where year(IdentifyDate)=" + year + " and month(IdentifyDate)=" + month + " and  employeeid='" + employee.EmployeeId + "'", conn);
                conn.Open();
                SqlDataReader reader = da.ExecuteReader(CommandBehavior.CloseConnection);
                reader.Read();
                if (!string.IsNullOrEmpty(reader.GetValue(0).ToString()))
                    sum = decimal.Parse(reader.GetValue(0).ToString());
                reader.Close();
                return sum;
            }
        }

        /// <summary>
        /// 借出记录查询，返回dataset
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        public DataSet SelestLoanList(int year, int month)
        {
            string DateStr = year.ToString() + "-" + month.ToString() + "-01";
            using (SqlConnection conn = new SqlConnection(sqlmapper.DataSource.ConnectionString))
            {
                SqlDataAdapter da = new SqlDataAdapter("select e.IDNo,e.EmployeeId,e.EmployeeName,d.DepartmentName,l.LoanId ,l.IsCash,isnull(l.LoanFee,0) LoanFee,l.IdentifyDate from employee e left join Department d on e.DepartmentId = d.DepartmentId left join (SELECT * FROM LoanDetail WHERE year(IdentifyDate)=" + year + " AND month(IdentifyDate)=" + month + ") l on e.EmployeeId = l.EmployeeId WHERE e.EmployeeLeaveDate IS NULL OR e.EmployeeLeaveDate >= '" + DateStr + "' ORDER BY (case when left(idno,1) like '[A-Za-z]' then (case when convert(int,substring(idno,2,2)) between 30 and 99 then left(idno,1)+cast(1911+convert(int,substring(idno,2,2)) as varchar(10)) + substring(idno,4,len(idno)) else left(idno,1)+convert(varchar(10),1911 + convert(int,'1'+substring(idno,2,2)))+substring(idno,4,len(idno)) end ) else idno end)", conn);
                DataSet ds = new DataSet();
                da.Fill(ds, "LoanListInfo");
                conn.Close();
                return ds;
            }

        }

        /// <summary>
        /// 查询所有借出记录
        /// </summary>
        /// <returns></returns>
        public DataSet SelestLoanAll()
        {
            using (SqlConnection conn = new SqlConnection(sqlmapper.DataSource.ConnectionString))
            {
                SqlDataAdapter da = new SqlDataAdapter("select e.IDNo ,e.EmployeeId ,e.EmployeeName ,d.DepartmentName,l.LoanId,l.IsCash, l.LoanFee,l.IdentifyDate from employee e left join Department d on e.DepartmentId = d.DepartmentId left join LoanDetail l on e.EmployeeId = l.EmployeeId order by e.IDNo ORDER BY (case when left(idno,1) like '[A-Za-z]' then (case when convert(int,substring(idno,2,2)) between 30 and 99 then left(idno,1)+cast(1911+convert(int,substring(idno,2,2)) as varchar(10)) + substring(idno,4,len(idno)) else left(idno,1)+convert(varchar(10),1911 + convert(int,'1'+substring(idno,2,2)))+substring(idno,4,len(idno)) end) else idno end)", conn);
                DataSet ds = new DataSet();
                da.Fill(ds, "LoanListInfo");
                return ds;
            }
        }

        /// <summary>
        /// 根据借出编号删除
        /// </summary>
        /// <returns></returns>1
        public int DeleteByIDNo(string LoanId)
        {
            using (SqlConnection conn = new SqlConnection(sqlmapper.DataSource.ConnectionString))
            {
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
        }

        /// <summary>
        /// 修改借出记录
        /// </summary>
        /// <param name="loanDetail"></param>
        /// <returns></returns>
        public int UpdateLoanDetail(DataSet ds, DateTime SelectDate)
        {
            DataTable dt = ds.Tables[0];
            int Result = 0;
            using (SqlConnection con = new SqlConnection(sqlmapper.DataSource.ConnectionString))
            {

                con.Open();
                SqlCommand cmd;

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    cmd = new SqlCommand();
                    cmd.Connection = con;
                    if (string.IsNullOrEmpty(dt.Rows[i]["LoanId"].ToString()))
                    {

                        cmd.CommandText = "insert into LoanDetail values(newid(),'" + dt.Rows[i]["EmployeeId"] + "','" + dt.Rows[i]["IsCash"] + "','" + dt.Rows[i]["LoanFee"] + "','" + SelectDate.ToString("yyyy-MM-dd") + "')";
                    }
                    else
                    {
                        cmd.CommandText = "UPDATE LoanDetail SET IsCash = '" + dt.Rows[i]["IsCash"] + "',LoanFee = '" + dt.Rows[i]["LoanFee"] + "' WHERE LoanId = '" + dt.Rows[i]["LoanId"] + "'";
                    }

                    Result += cmd.ExecuteNonQuery();
                }
            }

            return Result;
        }
    }
}
