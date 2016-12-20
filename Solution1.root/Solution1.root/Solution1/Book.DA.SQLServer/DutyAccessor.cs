//------------------------------------------------------------------------------
//
// file name：DutyAccessor.cs
// author: peidun
// create date：2008-11-24 11:10:36
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
    /// Data accessor of Duty
    /// </summary>
    public partial class DutyAccessor : EntityAccessor, IDutyAccessor
    {

        public DataSet SelectNoModel()
        {
            SqlConnection conn = new SqlConnection(sqlmapper.DataSource.ConnectionString);
            SqlDataAdapter adapter = new SqlDataAdapter("select * from Duty",conn);
            DataSet data = new DataSet();
            adapter.Fill(data);
            return data;
        }
        public void UpdateDataTable(DataTable accounts)
        {
            SqlConnection conn = new SqlConnection(sqlmapper.DataSource.ConnectionString);

            SqlDataAdapter dataAdapter = new SqlDataAdapter();
            dataAdapter.InsertCommand = new SqlCommand("insert into Duty(Id,DutyId,InsertTime,DutyName,DutyNote)  values(@Id,newid(),GetDate(),@DutyName,@DutyNote)", conn);
            //dataAdapter.InsertCommand.Parameters.Add(new SqlParameter("@DutyId", SqlDbType.VarChar, 50));
            //dataAdapter.InsertCommand.Parameters["@DutyId"].Value = Guid.NewGuid().ToString();
            dataAdapter.InsertCommand.Parameters.Add(new SqlParameter("@Id",SqlDbType.VarChar,50,"Id"));
            //dataAdapter.InsertCommand.Parameters.Add(new SqlParameter("@InsertTime", SqlDbType.DateTime));
            //dataAdapter.InsertCommand.Parameters["@InsertTime"].Value = DateTime.Now;
            dataAdapter.InsertCommand.Parameters.Add(new SqlParameter("@DutyName", SqlDbType.VarChar, 50, "DutyName"));
            dataAdapter.InsertCommand.Parameters.Add(new SqlParameter("@DutyNote", SqlDbType.VarChar, 50, "DutyNote"));
            string sqlStr = dataAdapter.InsertCommand.CommandText;

            dataAdapter.UpdateCommand = new SqlCommand("update Duty set Id=@Id, UpdateTime=GetDate(),DutyName=@DutyName,DutyNote=@DutyNote where DutyId=@DutyId", conn);
            //dataAdapter.UpdateCommand.Parameters.Add(new SqlParameter("@UpdateTime", SqlDbType.DateTime));
            //dataAdapter.UpdateCommand.Parameters["@UpdateTime"].Value = DateTime.Now;
            dataAdapter.UpdateCommand.Parameters.Add(new SqlParameter("@Id",SqlDbType.VarChar,50,"Id"));
            dataAdapter.UpdateCommand.Parameters.Add(new SqlParameter("@DutyName", SqlDbType.VarChar, 50, "DutyName"));
            dataAdapter.UpdateCommand.Parameters.Add(new SqlParameter("@DutyNote", SqlDbType.VarChar, 50, "DutyNote"));

            dataAdapter.UpdateCommand.Parameters.Add(new SqlParameter("@DutyId", SqlDbType.VarChar, 50, "DutyId"));
            dataAdapter.Update(accounts);
        }

        public bool IsExistsName(string dutyid, string dutyname)
        {
            Hashtable ht = new Hashtable();
            ht.Add("id", dutyid);
            ht.Add("name", dutyname);
            return sqlmapper.QueryForObject<bool>("Duty.IsExistName", ht);
        }
    }
}
