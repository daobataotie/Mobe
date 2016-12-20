//------------------------------------------------------------------------------
//
// file name：AnnualHolidayAccessor.cs
// author: peidun
// create date：2010-2-6 10:33:09
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Windows.Forms;

namespace Book.DA.SQLServer
{
    /// <summary>
    /// Data accessor of AnnualHoliday
    /// </summary>
    public partial class AnnualHolidayAccessor : EntityAccessor, IAnnualHolidayAccessor
    {
        public System.Data.DataSet SelectAllAnnualInfo()
        {
            SqlConnection con = new SqlConnection(sqlmapper.DataSource.ConnectionString);
            string sqlStr = "select * from AnnualHoliday";
            SqlDataAdapter adapter = new SqlDataAdapter(sqlStr, con);
            DataSet data = new DataSet();
            adapter.Fill(data);
            return data;
        }

        public void SaveAnnualInfo(System.Data.DataTable table, string years)
        {
            using (SqlConnection conn = new SqlConnection(sqlmapper.DataSource.ConnectionString))
            {
                SqlDataAdapter da = new SqlDataAdapter();

                da.UpdateCommand = new SqlCommand("update AnnualHoliday set HolidayDate=@HolidayDate,HolidayName=@HolidayName,Departs=@Departs where AnnualHolidayId=@AnnualHolidayId", conn);

                SqlParameter[] UpdateParameters = new SqlParameter[]{
                new SqlParameter("@HolidayDate",SqlDbType.DateTime,0,"HolidayDate"),
                new SqlParameter("@HolidayName",SqlDbType.VarChar,50,"HolidayName"),
                new SqlParameter("@AnnualHolidayId",SqlDbType.VarChar,50,"AnnualHolidayId"),
                new SqlParameter("@Departs",SqlDbType.VarChar,4000,"Departs")
                };

                da.InsertCommand = new SqlCommand("insert into AnnualHoliday values(newid(),@HolidayDate,@HolidayName,'" + years + "',@Departs)", conn);
                SqlParameter[] Insertparameters = new SqlParameter[] { new SqlParameter("@HolidayDate", SqlDbType.DateTime, 0, "HolidayDate"), new SqlParameter("@HolidayName", SqlDbType.VarChar, 50, "HolidayName"), new SqlParameter("@Departs", SqlDbType.VarChar, 4000, "Departs") };


                da.DeleteCommand = new SqlCommand("delete from AnnualHoliday where AnnualHolidayId=@AnnualHolidayId", conn);

                SqlParameter[] Deleteparameters = new SqlParameter[] { new SqlParameter("@AnnualHolidayId", SqlDbType.VarChar, 50, "AnnualHolidayId") };

                da.DeleteCommand.Parameters.AddRange(Deleteparameters);
                da.InsertCommand.Parameters.AddRange(Insertparameters);
                da.UpdateCommand.Parameters.AddRange(UpdateParameters);
                da.Update(table);
            }
        }

        public System.Data.DataSet SelectSingleAnnualInfo(DateTime HolidayDate)
        {
            SqlConnection con = new SqlConnection(sqlmapper.DataSource.ConnectionString);
            string sqlStr = "select * from AnnualHoliday where  year(HolidayDate)=year('" + HolidayDate.ToString("yyyy-MM-dd") + "') and month(HolidayDate)=month('" + HolidayDate.ToString("yyyy-MM-dd") + "')";
            SqlDataAdapter adapter = new SqlDataAdapter(sqlStr, con);
            DataSet data = new DataSet();
            adapter.Fill(data);
            return data;
        }

        public System.Data.DataSet SelectAnnualInfoByDueDate(DateTime dueDate)
        {
            SqlConnection con = new SqlConnection(sqlmapper.DataSource.ConnectionString);
            string sqlStr = "select * from AnnualHoliday  where convert(varchar(20),HolidayDate,101)=convert(varchar(20),'" + dueDate + "',101)";
            SqlDataAdapter adapter = new SqlDataAdapter(sqlStr, con);
            DataSet data = new DataSet();
            adapter.Fill(data);
            return data;
        }

        public int SelectCountByMonth(int year, int month)
        {
            int sum = 0;
            SqlConnection conn = new SqlConnection(sqlmapper.DataSource.ConnectionString);
            SqlCommand da = new SqlCommand("select count(*) from AnnualHoliday  where  year(HolidayDate)=" + year + " and month(HolidayDate)=" + month, conn);
            conn.Open();
            SqlDataReader reader = da.ExecuteReader(CommandBehavior.CloseConnection);
            reader.Read();
            if (!string.IsNullOrEmpty(reader.GetValue(0).ToString()))
                sum = Int32.Parse(reader.GetValue(0).ToString());
            reader.Close();
            return sum;
        }

        public System.Data.DataSet SelectAnnualInfoByYear(int year)
        {
            using (SqlConnection con = new SqlConnection(sqlmapper.DataSource.ConnectionString))
            {
                SqlDataAdapter adapter = new SqlDataAdapter("select * from AnnualHoliday  where year(HolidayDate)=" + year, con);
                DataSet data = new DataSet();
                adapter.Fill(data);
                return data;
            }
        }

        public IList<Book.Model.AnnualHoliday> SelectAnnualInfoByYear_list(int year)
        {
            return sqlmapper.QueryForList<Book.Model.AnnualHoliday>("AnnualHoliday.SelectAnnualInfoByYear", year);
        }

        public bool ExistsAutoYear(string years)
        {
            return sqlmapper.QueryForObject<bool>("AnnualHoliday.existsAutoYear", years);
        }

        public bool IsExistForHolidayDate(DataRow dr)
        {
            return sqlmapper.QueryForObject<bool>("AnnualHoliday.IsExistForHolidayDate", Convert.ToDateTime(dr["HolidayDate"].ToString()));
        }

        public IList<Book.Model.AnnualHoliday> SelectBigThanDate(DateTime limitdate)
        {
            return sqlmapper.QueryForList<Model.AnnualHoliday>("AnnualHoliday.SelectBigThanDate", limitdate.Date);
        }

    }
}
