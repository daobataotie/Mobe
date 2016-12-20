//------------------------------------------------------------------------------
//
// file name:SpecialProfitMarginAccessor.cs
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
    /// Data accessor of SpecialProfitMargin
    /// </summary>
    public partial class SpecialProfitMarginAccessor : EntityAccessor, ISpecialProfitMarginAccessor
    {
        #region ISpecialProfitMarginAccessor 成员


        public DataTable SelectDataTable()
        {
            string strSql = "SELECT p.*, l.*, m.SpecialProfitMarginValue FROM [SpecialProfitMargin] m inner join product p on p.productid = m.productid inner join companylevel l on l.[CompanyLevelId] = m.[CompanyLevelId]";
            SqlDataAdapter dataAdapter = new SqlDataAdapter(strSql, sqlmapper.DataSource.ConnectionString);
            DataTable Stocks = new DataTable();
            dataAdapter.Fill(Stocks);
            return Stocks;
        }

        public void UpdateDataTable(DataTable table)
        {
            SqlConnection conn = new SqlConnection(sqlmapper.DataSource.ConnectionString);

            SqlDataAdapter dataAdapter = new SqlDataAdapter();

            dataAdapter.UpdateCommand = new SqlCommand("UPDATE [SpecialProfitMargin] SET [ProductId] = @ProductId,[CompanyLevelId] = @CompanyLevelId,[SpecialProfitMarginValue] = @SpecialProfitMarginValue WHERE [ProductId]=@ProductId AND [CompanyLevelId]=@CompanyLevelId", conn);            
            dataAdapter.UpdateCommand.Parameters.Add(new SqlParameter("@productid", SqlDbType.VarChar, 50, "Productid"));
            dataAdapter.UpdateCommand.Parameters.Add(new SqlParameter("@CompanyLevelId", SqlDbType.VarChar, 50, "CompanyLevelId"));
            dataAdapter.UpdateCommand.Parameters.Add(new SqlParameter("@SpecialProfitMarginValue", SqlDbType.Float, 32, "SpecialProfitMarginValue"));

            dataAdapter.InsertCommand = new SqlCommand("insert SpecialProfitMargin(ProductId,CompanyLevelId,SpecialProfitMarginValue) values(@ProductId,@CompanyLevelId,@SpecialProfitMarginValue)", conn);
            dataAdapter.InsertCommand.Parameters.Add(new SqlParameter("@productid", SqlDbType.VarChar, 50, "Productid"));
            dataAdapter.InsertCommand.Parameters.Add(new SqlParameter("@CompanyLevelId", SqlDbType.VarChar, 50, "CompanyLevelId"));
            dataAdapter.InsertCommand.Parameters.Add(new SqlParameter("@SpecialProfitMarginValue", SqlDbType.Float, 32, "SpecialProfitMarginValue"));

            dataAdapter.DeleteCommand = new SqlCommand("delete from SpecialProfitMargin where ProductId=@ProductId AND CompanyLevelId=@CompanyLevelId", conn);
            dataAdapter.DeleteCommand.Parameters.Add(new SqlParameter("@productid", SqlDbType.VarChar, 50, "Productid"));
            dataAdapter.DeleteCommand.Parameters.Add(new SqlParameter("@CompanyLevelId", SqlDbType.VarChar, 50, "CompanyLevelId"));

            dataAdapter.Update(table);
        }

        #endregion


    }
}
