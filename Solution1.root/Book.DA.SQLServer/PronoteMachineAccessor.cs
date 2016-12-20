//------------------------------------------------------------------------------
//
// file name：PronoteMachineAccessor.cs
// author: mayanjun
// create date：2010-9-16 9:27:25
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
    /// Data accessor of PronoteMachine
    /// </summary>
    public partial class PronoteMachineAccessor : EntityAccessor, IPronoteMachineAccessor
    {
        public System.Data.DataTable GetAll()
        {
            SqlDataAdapter dataAdapter = new SqlDataAdapter("select * from dbo.PronoteMachine", sqlmapper.DataSource.ConnectionString);
            DataTable accounts = new DataTable();
            dataAdapter.Fill(accounts);
            return accounts;
        }

        public bool ExistsId(string id, string pid)
        {
            Hashtable ht = new Hashtable();
            ht.Add("proid", pid);
            ht.Add("id", id);
            return sqlmapper.QueryForObject<bool>("PronoteMachine.Existsid", ht);
        }

        public bool ExistsName(string name, string pid)
        {
            Hashtable ht = new Hashtable();
            ht.Add("name", name);
            ht.Add("id", pid);
            return sqlmapper.QueryForObject<bool>("PronoteMachine.ExistsName", ht);
        }

        public void SaveInfo(System.Data.DataTable Deport)
        {
            foreach (DataRow item in Deport.Rows)
            {
                string MachineId = "";
                string MachineName = "";
                try
                {

                    MachineId = item["PronoteMachineId"].ToString();
                    MachineName = item["PronoteMachineName"].ToString();
                }
                catch (Exception)
                {

                }

                if (string.IsNullOrEmpty(MachineName))
                    throw new Helper.RequireValueException(Model.PronoteMachine.PROPERTY_PRONOTEMACHINENAME);
                if (ExistsName(MachineName, MachineId))
                    throw new Helper.InvalidValueException(Model.PronoteMachine.PROPERTY_PRONOTEMACHINENAME);
            }

            SqlConnection con = new SqlConnection(sqlmapper.DataSource.ConnectionString);
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.UpdateCommand = new SqlCommand("update PronoteMachine set PronoteMachineName=@PronoteMachineName where PronoteMachineId=@PronoteMachineId", con);
            SqlParameter[] parameters = new SqlParameter[] 
            {
                new SqlParameter("@PronoteMachineName",SqlDbType.VarChar,50,"PronoteMachineName"),
                new SqlParameter("@PronoteMachineId",SqlDbType.VarChar,50,"PronoteMachineId")
            };
            adapter.UpdateCommand.Parameters.AddRange(parameters);
            adapter.InsertCommand = new SqlCommand("insert into PronoteMachine(PronoteMachineId,PronoteMachineName) values(newid(),@PronoteMachineName)", con);
            SqlParameter[] sqlParameters = new SqlParameter[] 
            {
                new SqlParameter("@PronoteMachineName", SqlDbType.VarChar, 50, "PronoteMachineName")
            };
            adapter.InsertCommand.Parameters.AddRange(sqlParameters);

            adapter.DeleteCommand = new SqlCommand("delete from PronoteMachine where PronoteMachineId=@PronoteMachineId ", con);
            SqlParameter[] delParameters = new SqlParameter[] 
            { 
                new SqlParameter("@PronoteMachineId", SqlDbType.VarChar, 50, "PronoteMachineId") 
            };
            adapter.DeleteCommand.Parameters.AddRange(delParameters);

            adapter.Update(Deport);
        }

        public IList<Model.PronoteMachine> SelectMachineByProduresId(string ProduresId)
        {
            return sqlmapper.QueryForList<Model.PronoteMachine>("PronoteMachine.selectMachineByProduresId", ProduresId);
        }

        public Model.PronoteMachine SelectMachineByName(string name)
        {
            return sqlmapper.QueryForObject<Model.PronoteMachine>("PronoteMachine.selectMachineByName", name);
        }

        public IList<Model.PronoteMachine> GetPronoteMachineByPronoteProceduresDetailId(string PronoteProceduresDetailId)
        {
            return sqlmapper.QueryForList<Model.PronoteMachine>("PronoteMachine.GetPronoteMachineByPronoteProceduresDetailId", PronoteProceduresDetailId);
        }
    }
}
