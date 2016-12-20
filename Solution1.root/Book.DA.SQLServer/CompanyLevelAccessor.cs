//------------------------------------------------------------------------------
//
// file name:CompanyLevelAccessor.cs
// author: peidun
// create date:2008/6/30 14:20:09
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
    /// Data accessor of CompanyLevel
    /// </summary>
    public partial class CompanyLevelAccessor : EntityAccessor, ICompanyLevelAccessor
    {
        #region ICompanyLevelAccessor 成员


        public DataTable SelectDateTable()
        {
            SqlDataAdapter da = new SqlDataAdapter("select * from CompanyLevel", sqlmapper.DataSource.ConnectionString);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
            //return sqlmapper.QueryForList("CompanyLevel.select_datatable", null);
        
        }

        public void UpdateDataTable(DataTable table)
        {
            SqlConnection conn = new SqlConnection(sqlmapper.DataSource.ConnectionString);
            SqlDataAdapter dataAdapter = new SqlDataAdapter();
            dataAdapter.UpdateCommand = new SqlCommand("UPDATE [CompanyLevel] SET [CompanyLevelId] = @CompanyLevelId,[CompanyLevelName] = @CompanyLevelName,[CompanyLevelProfitMargin] = @CompanyLevelProfitMargin WHERE [CompanyLevelId] = @CompanyLevelId", conn);

            dataAdapter.UpdateCommand.Parameters.Add(new SqlParameter("@CompanyLevelId", SqlDbType.VarChar, 50, "CompanyLevelId"));
            dataAdapter.UpdateCommand.Parameters.Add(new SqlParameter("@CompanyLevelName", SqlDbType.VarChar, 50, "CompanyLevelName"));
            dataAdapter.UpdateCommand.Parameters.Add(new SqlParameter("@CompanyLevelProfitMargin", SqlDbType.Float, 32, "CompanyLevelProfitMargin"));

            dataAdapter.Update(table);
        }

        #endregion
    }
}
