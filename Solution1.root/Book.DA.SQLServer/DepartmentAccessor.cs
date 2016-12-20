//------------------------------------------------------------------------------
//
// file name：DepartmentAccessor.cs
// author: peidun
// create date：2008-11-29 12:52:19
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
    /// Data accessor of Department
    /// </summary>
    public partial class DepartmentAccessor : EntityAccessor, IDepartmentAccessor
    {
        public bool ExistsName(string name, string id)
        {
            Hashtable ht = new Hashtable();
            ht.Add("name", name);
            ht.Add("id", id);
            return sqlmapper.QueryForObject<bool>("Department.ExistsName", ht);
        }

        public void SaveInfo(System.Data.DataTable Deport)
        {
            foreach (DataRow item in Deport.Rows)
            {
                string deportid = "";
                string deportname = "";
                try
                {

                    deportid = item["DepartmentId"].ToString();
                    deportname = item["DepartmentName"].ToString();
                }
                catch (Exception)
                {

                }
                if (string.IsNullOrEmpty(deportname))
                    throw new Helper.RequireValueException(Model.Department.PROPERTY_DEPARTMENTNAME);
                if (ExistsName(deportname, deportid))
                    throw new Helper.InvalidValueException(Model.Department.PROPERTY_DEPARTMENTNAME);
            }

            SqlConnection con = new SqlConnection(sqlmapper.DataSource.ConnectionString);
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.UpdateCommand = new SqlCommand("update Department set UpdateTime=getdate(),DepartmentName=@DepartmentName where DepartmentId=@DepartmentId", con);
            SqlParameter[] parameters = new SqlParameter[] 
            { new SqlParameter("@DepartmentName",SqlDbType.VarChar,50,"DepartmentName"),
                //new SqlParameter("@PayRate",SqlDbType.Float,0,"PayRate"),
                //new SqlParameter("@IsCountToPunish",SqlDbType.Bit,0,"IsCountToPunish"),
                new SqlParameter("@DepartmentId",SqlDbType.VarChar,50,"DepartmentId")};
            adapter.UpdateCommand.Parameters.AddRange(parameters);
            adapter.InsertCommand = new SqlCommand("insert into Department(DepartmentId,InsertTime,DepartmentName) values(newid(),getdate(),@DepartmentName)", con);
            SqlParameter[] sqlParameters = new SqlParameter[] 
            { new SqlParameter("@DepartmentName", SqlDbType.VarChar, 50, "DepartmentName"),
               // new SqlParameter("@PayRate", SqlDbType.Float, 0, "PayRate"), 
                //new SqlParameter("@IsCountToPunish", SqlDbType.Bit, 0, "IsCountToPunish")
            };
            adapter.InsertCommand.Parameters.AddRange(sqlParameters);

            adapter.DeleteCommand = new SqlCommand("delete from Department where DepartmentId=@DepartmentId ", con);
            SqlParameter[] delParameters = new SqlParameter[] 
            { 
                new SqlParameter("@DepartmentId", SqlDbType.VarChar, 50, "DepartmentId") 
            };
            adapter.DeleteCommand.Parameters.AddRange(delParameters);

            adapter.Update(Deport);
        }

        public System.Data.DataTable GetAll()
        {
            SqlDataAdapter dataAdapter = new SqlDataAdapter("select * from dbo.Department order by inserttime", sqlmapper.DataSource.ConnectionString);
            DataTable accounts = new DataTable();
            dataAdapter.Fill(accounts);
            return accounts;
        }
    }
}
