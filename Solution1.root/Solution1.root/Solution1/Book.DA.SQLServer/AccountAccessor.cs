//------------------------------------------------------------------------------
//
// file name:AccountAccessor.cs
// author: peidun
// create date:2008/6/6 10:00:49
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
    /// Data accessor of Account
    /// </summary>
    public partial class AccountAccessor : EntityAccessor, IAccountAccessor
    {
        #region IAccountAccessor Members

        public DataTable SelectDataTable()
        {
            SqlDataAdapter dataAdapter = new SqlDataAdapter("select * from dbo.account", sqlmapper.DataSource.ConnectionString);
            DataTable accounts = new DataTable();
            dataAdapter.Fill(accounts);
            return accounts;
        }

        public void UpdateDataTable(DataTable accounts)
        {
            SqlConnection conn = new SqlConnection(sqlmapper.DataSource.ConnectionString);

            SqlDataAdapter dataAdapter = new SqlDataAdapter();
            dataAdapter.InsertCommand = new SqlCommand("insert into dbo.account(accountid,accountname,accountbalance0,accountbalance1,accountdescription)values(@accountid,@accountname,@accountbalance0,@accountbalance1,@accountdescription)", conn);
            dataAdapter.InsertCommand.Parameters.Add(new SqlParameter("@accountid", SqlDbType.VarChar, 50, "accountid"));
            dataAdapter.InsertCommand.Parameters.Add(new SqlParameter("@accountname", SqlDbType.VarChar, 50, "accountname"));
            dataAdapter.InsertCommand.Parameters.Add(new SqlParameter("@accountbalance0", SqlDbType.Money, 8, "accountbalance0"));
            dataAdapter.InsertCommand.Parameters.Add(new SqlParameter("@accountbalance1", SqlDbType.Money, 8, "accountbalance1"));
            dataAdapter.InsertCommand.Parameters.Add(new SqlParameter("@accountdescription", SqlDbType.Text, 16, "accountdescription"));

            dataAdapter.UpdateCommand = new SqlCommand("update dbo.account set accountname=@accountname,accountbalance0=@accountbalance0,accountbalance1=@accountbalance1,accountdescription=@accountdescription where accountid=@accountid", conn);
            dataAdapter.UpdateCommand.Parameters.Add(new SqlParameter("@accountid", SqlDbType.VarChar, 50, "accountid"));
            dataAdapter.UpdateCommand.Parameters.Add(new SqlParameter("@accountname", SqlDbType.VarChar, 50, "accountname"));
            dataAdapter.UpdateCommand.Parameters.Add(new SqlParameter("@accountbalance0", SqlDbType.Money, 8, "accountbalance0"));
            dataAdapter.UpdateCommand.Parameters.Add(new SqlParameter("@accountbalance1", SqlDbType.Money, 8, "accountbalance1"));
            dataAdapter.UpdateCommand.Parameters.Add(new SqlParameter("@accountdescription", SqlDbType.Text, 16, "accountdescription"));

            dataAdapter.Update(accounts);
        }

        public void Increment(Book.Model.Account account, decimal value)
        {
            Hashtable paras = new Hashtable();
            paras.Add("AccountId", account.AccountId);
            paras.Add("Money", value);
            sqlmapper.Update("Account.increment", paras);
        }

        public void Decrement(Book.Model.Account account, decimal value)
        {
            Hashtable paras = new Hashtable();
            paras.Add("AccountId", account.AccountId);
            paras.Add("Money", value);
            sqlmapper.Update("Account.decrement", paras);
        }

        public void Increment(Book.Model.Account account, decimal? value)
        {
            this.Increment(account, value.Value);
        }

        public void Decrement(Book.Model.Account account, decimal? value)
        {
            this.Decrement(account, value.Value);
        }

        #endregion
    }
}
