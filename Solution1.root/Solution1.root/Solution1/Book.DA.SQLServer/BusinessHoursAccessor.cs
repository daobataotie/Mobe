//------------------------------------------------------------------------------
//
// file name：BusinessHoursAccessor.cs
// author: peidun
// create date：2009-09-02 上午 10:38:13
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
    /// Data accessor of BusinessHours
    /// </summary>
    /// 
    public partial class BusinessHoursAccessor : EntityAccessor, IBusinessHoursAccessor
    {
        public DataSet SelectNoModel()
        {
            SqlConnection conn = new SqlConnection(sqlmapper.DataSource.ConnectionString);
            SqlDataAdapter adapter = new SqlDataAdapter("select * from BusinessHours",conn);
            DataSet data = new DataSet();
            adapter.Fill(data);
            return data;
        }
        public void UpdateDataTable(DataTable accounts)
        {

            SqlConnection conn = new SqlConnection(sqlmapper.DataSource.ConnectionString);

            SqlDataAdapter dataAdapter = new SqlDataAdapter();
            dataAdapter.InsertCommand = new SqlCommand("insert into BusinessHours(BusinessHoursId,InsertTime,BusinessHoursName,Fromtime,ToTime,SpecialPay,Description)  values(newid(),GetDate(),@BusinessHoursName,@FromTime,@ToTime,@SpecialPay,@Description)", conn);
            //dataAdapter.InsertCommand.Parameters.Add(new SqlParameter("@BusinessHoursId", SqlDbType.VarChar, 50));
            //dataAdapter.InsertCommand.Parameters["@BusinessHoursId"].Value = Guid.NewGuid().ToString();
            //dataAdapter.InsertCommand.Parameters.Add(new SqlParameter("@InsertTime", SqlDbType.DateTime));
            //dataAdapter.InsertCommand.Parameters["@InsertTime"].Value = DateTime.Now;
            dataAdapter.InsertCommand.Parameters.Add(new SqlParameter("@BusinessHoursName", SqlDbType.VarChar, 50, "BusinessHoursName"));
            dataAdapter.InsertCommand.Parameters.Add(new SqlParameter("@FromTime", SqlDbType.DateTime,0,"FromTime"));
            dataAdapter.InsertCommand.Parameters.Add(new SqlParameter("@ToTime",SqlDbType.DateTime,0,"ToTime"));
            dataAdapter.InsertCommand.Parameters.Add(new SqlParameter("@SpecialPay", SqlDbType.Money,0, "SpecialPay"));
            dataAdapter.InsertCommand.Parameters.Add(new SqlParameter("@Description", SqlDbType.VarChar, 50, "Description"));



            dataAdapter.UpdateCommand = new SqlCommand("update BusinessHours set  UpdateTime=GetDate(),BusinessHoursName=@BusinessHoursName,FromTime=@FromTime,ToTime=@ToTime ,SpecialPay=@SpecialPay,Description=@Description  where BusinessHoursId=@BusinessHoursId", conn);
            //dataAdapter.UpdateCommand.Parameters.Add(new SqlParameter("@UpdateTime", SqlDbType.DateTime));
            //dataAdapter.UpdateCommand.Parameters["@UpdateTime"].Value = DateTime.Now;
            dataAdapter.UpdateCommand.Parameters.Add(new SqlParameter("@BusinessHoursId", SqlDbType.VarChar, 50, "BusinessHoursId"));
            dataAdapter.UpdateCommand.Parameters.Add(new SqlParameter("@BusinessHoursName", SqlDbType.VarChar, 50, "BusinessHoursName"));
            dataAdapter.UpdateCommand.Parameters.Add(new SqlParameter("@FromTime", SqlDbType.DateTime, 0, "FromTime"));
            dataAdapter.UpdateCommand.Parameters.Add(new SqlParameter("@ToTime", SqlDbType.DateTime,0, "ToTime"));
            dataAdapter.UpdateCommand.Parameters.Add(new SqlParameter("@SpecialPay", SqlDbType.Money, 0, "SpecialPay"));
            dataAdapter.UpdateCommand.Parameters.Add(new SqlParameter("@Description", SqlDbType.VarChar, 50, "Description"));
            dataAdapter.Update(accounts);
        }
        public bool IsExistsBusinessName(string BusinessHoursId, string BusinessHoursName)
        {
            Hashtable ht=new Hashtable();
            ht.Add("id",BusinessHoursId);
            ht.Add("name",BusinessHoursName);
            return sqlmapper.QueryForObject<bool>("BusinessHours.IsExistsBusinessName", ht);
        }
    }
}
