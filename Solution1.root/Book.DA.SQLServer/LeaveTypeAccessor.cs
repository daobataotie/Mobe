//------------------------------------------------------------------------------
//
// file name：LeaveTypeAccessor.cs
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

namespace Book.DA.SQLServer
{
    /// <summary>
    /// Data accessor of LeaveType
    /// </summary>
    public partial class LeaveTypeAccessor : EntityAccessor, ILeaveTypeAccessor
    {
        public System.Data.DataSet SelectLeaveTypeInfo()
        {
            SqlConnection con = new SqlConnection(sqlmapper.DataSource.ConnectionString);
            string sqlStr = "select * from LeaveType";
            SqlDataAdapter adapter = new SqlDataAdapter(sqlStr, con);
            DataSet data = new DataSet();
            adapter.Fill(data);
            return data;
        }
        public void SaveLeaveTypeInfo(System.Data.DataTable table)
        {
            foreach (DataRow item in table.Rows)
            {
                string typeid = "";
                string typename = "";
                try
                {
                    typeid = item["LeaveTypeId"].ToString();
                    typename = item["LeaveTypeName"].ToString();
                }
                catch (Exception)
                {

                }
                if (IsExitsName(typeid, typename))
                    throw new Helper.InvalidValueException(typename);
            }

            SqlConnection con = new SqlConnection(sqlmapper.DataSource.ConnectionString);
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.UpdateCommand = new SqlCommand("update LeaveType set UpdateTime=getdate(),LeaveTypeName=@LeaveTypeName,PayRate=@PayRate,IsCountToPunish=@IsCountToPunish,doAttendance=@doAttendance where LeaveTypeId=@LeaveTypeId", con);
            SqlParameter[] parameters = new SqlParameter[] 
            {   
                new SqlParameter("@LeaveTypeName",SqlDbType.VarChar,50,"LeaveTypeName"),
                new SqlParameter("@PayRate",SqlDbType.Float,0,"PayRate"),
                new SqlParameter("@IsCountToPunish",SqlDbType.Bit,0,"IsCountToPunish"),
                new SqlParameter("@LeaveTypeId",SqlDbType.VarChar,50,"LeaveTypeId"),
                new SqlParameter("@doAttendance",SqlDbType.Bit,0,"doAttendance")
            };
            adapter.UpdateCommand.Parameters.AddRange(parameters);
            adapter.InsertCommand = new SqlCommand("insert into LeaveType(LeaveTypeId,InsertTime,LeaveTypeName,PayRate,IsCountToPunish,doAttendance) values(newid(),getdate(),@LeaveTypeName,@PayRate,@IsCountToPunish,@doAttendance)", con);
            SqlParameter[] sqlParameters = new SqlParameter[]
            { 
              new SqlParameter("@LeaveTypeName", SqlDbType.VarChar, 50, "LeaveTypeName"),
              new SqlParameter("@PayRate", SqlDbType.Float, 0, "PayRate"), 
              new SqlParameter("@IsCountToPunish", SqlDbType.Bit, 0, "IsCountToPunish"), 
              new SqlParameter("@doAttendance",SqlDbType.Bit,0,"doAttendance")
            };
            adapter.InsertCommand.Parameters.AddRange(sqlParameters);

            adapter.DeleteCommand = new SqlCommand("delete from LeaveType where LeaveTypeId=@LeaveTypeId ", con);
            SqlParameter[] delParameters = new SqlParameter[] { new SqlParameter("@LeaveTypeId", SqlDbType.VarChar, 50, "LeaveTypeId") };
            adapter.DeleteCommand.Parameters.AddRange(delParameters);

            adapter.Update(table);
        }
        public bool IsExitsName(string typeid, string typename)
        {
            Hashtable ht = new Hashtable();
            ht.Add("id", typeid);
            ht.Add("name", typename);
            return sqlmapper.QueryForObject<bool>("LeaveType.ExistsName", ht);
        }
    }
}
