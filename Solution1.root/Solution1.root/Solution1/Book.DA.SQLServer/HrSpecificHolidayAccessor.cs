//------------------------------------------------------------------------------
//
// file name：HrSpecificHolidayAccessor.cs
// author: mayanjun
// create date：2010-5-28 14:21:45
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
    /// Data accessor of HrSpecificHoliday
    /// </summary>
    public partial class HrSpecificHolidayAccessor : EntityAccessor, IHrSpecificHolidayAccessor
    {
        public System.Data.DataSet SelectSpecificHolidayInfo()
        {
            SqlConnection con = new SqlConnection(sqlmapper.DataSource.ConnectionString);
            string sqlStr = "select  * from HrSpecificHoliday ORDER BY  holidayDate";
            SqlDataAdapter adapter = new SqlDataAdapter(sqlStr, con);
            DataSet table = new DataSet();
            adapter.Fill(table);
            return table;
        }
        public void SaveSpecificHolidayInfo(DataTable table)
        {
            SqlConnection con = new SqlConnection(sqlmapper.DataSource.ConnectionString);
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.UpdateCommand = new SqlCommand("update HrSpecificHoliday set UpdateTime=getdate(),HolidayDate=@HolidayDate,Name=@Name where HrSpecificHolidayId=@HrSpecificHoliday", con);
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                new SqlParameter("@HolidayDate",  SqlDbType.VarChar, 10, "HolidayDate"), 
                    new SqlParameter("@Name", SqlDbType.VarChar, 50, "name"), 
                    new SqlParameter("@HrSpecificHoliday", SqlDbType.VarChar, 50, "HrSpecificHolidayId")
            };
            adapter.UpdateCommand.Parameters.AddRange(parameters);
            adapter.InsertCommand = new SqlCommand("insert into HrSpecificHoliday(HrSpecificHolidayId,InsertTime,HolidayDate,Name) values(newid(),getdate(),@HolidayDate,@name)", con);
            SqlParameter[] sqlParameters = new SqlParameter[] 
            { 
                new SqlParameter("@HolidayDate", SqlDbType.VarChar, 10, "HolidayDate"), 
                new SqlParameter("@name", SqlDbType.VarChar, 50, "name")
            };
            adapter.InsertCommand.Parameters.AddRange(sqlParameters);
            adapter.DeleteCommand = new SqlCommand("delete from HrSpecificHoliday where HrSpecificHolidayId=@HrSpecificHolidayId", con);
            SqlParameter[] sps = new SqlParameter[] 
            { 
                new SqlParameter("@HrSpecificHolidayId", SqlDbType.VarChar, 50, "HrSpecificHolidayId")
            };
            adapter.DeleteCommand.Parameters.AddRange(sps);
            adapter.Update(table);
            if (table.HasErrors)
            {
                table.GetErrors()[0].ClearErrors();
            }
            else
            {
                table.AcceptChanges();
            }
        }
    }
}
