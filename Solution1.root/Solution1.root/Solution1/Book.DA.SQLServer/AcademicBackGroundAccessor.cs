//------------------------------------------------------------------------------
//
// file name：AcademicBackGroundAccessor.cs
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
    /// Data accessor of AcademicBackGround
    /// </summary>
    public partial class AcademicBackGroundAccessor : EntityAccessor, IAcademicBackGroundAccessor
    {
        public void UpdateDataTable(DataTable accounts)
        {
            SqlConnection conn = new SqlConnection(sqlmapper.DataSource.ConnectionString);

            SqlDataAdapter dataAdapter = new SqlDataAdapter();
            dataAdapter.InsertCommand = new SqlCommand("insert into AcademicBackGround(AcademicBackGroundId,InsertTime,AcademicBackGroundName,Description)values(newid(),GetDate(),@AcademicBackGroundName,@Description)", conn);
            //dataAdapter.InsertCommand.Parameters.Add(new SqlParameter("@AcademicBackGroundId", SqlDbType.VarChar,50));
            //dataAdapter.InsertCommand.Parameters["@AcademicBackGroundId"].Value = Guid.NewGuid().ToString();
            //dataAdapter.InsertCommand.Parameters.Add(new SqlParameter("@InsertTime", SqlDbType.DateTime));
            //dataAdapter.InsertCommand.Parameters["@InsertTime"].Value = DateTime.Now;
            dataAdapter.InsertCommand.Parameters.Add(new SqlParameter("@AcademicBackGroundName", SqlDbType.VarChar, 50, "AcademicBackGroundName"));
            dataAdapter.InsertCommand.Parameters.Add(new SqlParameter("@Description", SqlDbType.VarChar, 50, "Description"));

            dataAdapter.UpdateCommand = new SqlCommand("update AcademicBackGround set UpdateTime=GetDate(),AcademicBackGroundName=@AcademicBackGroundName,Description=@Description where AcademicBackGroundId=@AcademicBackGroundId", conn);
            //dataAdapter.UpdateCommand.Parameters.Add(new SqlParameter("@UpdateTime", SqlDbType.DateTime));
            //dataAdapter.UpdateCommand.Parameters["@UpdateTime"].Value = DateTime.Now;
            dataAdapter.UpdateCommand.Parameters.Add(new SqlParameter("@AcademicBackGroundName", SqlDbType.VarChar, 50, "AcademicBackGroundName"));
            dataAdapter.UpdateCommand.Parameters.Add(new SqlParameter("@Description", SqlDbType.VarChar, 50, "Description"));

            dataAdapter.UpdateCommand.Parameters.Add(new SqlParameter("@AcademicBackGroundId", SqlDbType.VarChar, 50, "AcademicBackGroundId"));
            dataAdapter.Update(accounts);
        }
        public DataSet SelectNoModel()
        {
            SqlConnection conn = new SqlConnection(sqlmapper.DataSource.ConnectionString);
            SqlDataAdapter adapter = new SqlDataAdapter("select * from AcademicBackGround",conn);
            DataSet data = new DataSet();
            //adapter.FillSchema(data, SchemaType.Mapped);
            adapter.Fill(data);
            
            return data;
        }



        public bool Selectbyname(string academname)
        {

            IList<Model.AcademicBackGround> alist= sqlmapper.QueryForList<Model.AcademicBackGround>("AcademicBackGround.select_byname", academname);
            return alist.Count>0?true:false;
              
        }

        public bool IsExistName(string id, string name)
        {
            Hashtable ht = new Hashtable();
            ht.Add("id", id);
            ht.Add("name", name);
            return sqlmapper.QueryForObject<bool>("AcademicBackGround.IsExistAcademicBackGround", ht);
        }

    }
}

