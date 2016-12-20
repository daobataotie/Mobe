//------------------------------------------------------------------------------
//
// file name：BankAccessor.cs
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
    /// Data accessor of Bank
    /// </summary>
    public partial class BankAccessor : EntityAccessor, IBankAccessor
    {
        public DataSet SelectNoModel()
        {
            SqlConnection conn = new SqlConnection(sqlmapper.DataSource.ConnectionString);
            SqlDataAdapter adapter = new SqlDataAdapter("select * from Bank",conn);
            DataSet data = new DataSet();
            //adapter.FillSchema(data,SchemaType.Mapped);
            adapter.Fill(data);
            return data;
        }
        public void UpdateDataTable(DataTable accounts)
        {
            SqlConnection conn = new SqlConnection(sqlmapper.DataSource.ConnectionString);

            SqlDataAdapter dataAdapter = new SqlDataAdapter();
            dataAdapter.InsertCommand = new SqlCommand("insert into Bank(BankId,InsertTime,BankName,BankAddress,BankPhone,Description)  values(newid(),GetDate(),@BankName,@BankAddress,@BankPhone,@Description)", conn);
            //dataAdapter.InsertCommand.Parameters.Add(new SqlParameter("@BankId", SqlDbType.VarChar, 50));
            //dataAdapter.InsertCommand.Parameters["@BankId"].Value = Guid.NewGuid().ToString();
            //dataAdapter.InsertCommand.Parameters.Add(new SqlParameter("@InsertTime", SqlDbType.DateTime));
            //dataAdapter.InsertCommand.Parameters["@InsertTime"].Value = DateTime.Now;
            dataAdapter.InsertCommand.Parameters.Add(new SqlParameter("@BankName", SqlDbType.VarChar, 50, "BankName"));
            dataAdapter.InsertCommand.Parameters.Add(new SqlParameter("@BankAddress", SqlDbType.VarChar, 50, "BankAddress"));
            dataAdapter.InsertCommand.Parameters.Add(new SqlParameter("@BankPhone", SqlDbType.VarChar, 50, "BankPhone"));
            dataAdapter.InsertCommand.Parameters.Add(new SqlParameter("@Description", SqlDbType.VarChar, 50, "Description"));
            string sqlStr = dataAdapter.InsertCommand.CommandText;

            dataAdapter.UpdateCommand = new SqlCommand("update Bank set  UpdateTime=GetDate(),BankName=@BankName,BankAddress=@BankAddress,BankPhone=@BankPhone,Description=@Description  where BankId=@BankId", conn);
            //dataAdapter.UpdateCommand.Parameters.Add(new SqlParameter("@UpdateTime", SqlDbType.DateTime));
            //dataAdapter.UpdateCommand.Parameters["@UpdateTime"].Value = DateTime.Now;
            dataAdapter.UpdateCommand.Parameters.Add(new SqlParameter("@BankId", SqlDbType.VarChar, 50, "BankId"));
            dataAdapter.UpdateCommand.Parameters.Add(new SqlParameter("@BankName", SqlDbType.VarChar, 50, "BankName"));
            dataAdapter.UpdateCommand.Parameters.Add(new SqlParameter("@BankAddress", SqlDbType.VarChar, 50, "BankAddress"));

            dataAdapter.UpdateCommand.Parameters.Add(new SqlParameter("@BankPhone", SqlDbType.VarChar, 50, "BankPhone"));
            dataAdapter.UpdateCommand.Parameters.Add(new SqlParameter("@Description", SqlDbType.VarChar, 50, "Description"));
            dataAdapter.Update(accounts);
        }
        public bool IsEixstsBankName(string id, string name)
        {
            Hashtable ht = new Hashtable();
            ht.Add("bankid", id);
            ht.Add("bankname", name);
            return sqlmapper.QueryForObject<bool>("Bank.IsExistsBankName", ht);
        }


    }
}
