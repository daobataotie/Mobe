using System;
using System.Collections.Generic;

using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;
using Book.Model;

namespace Book.DA.SQLServer
{
    public  class ShreetService
    {
      private   DistrictService ds = new DistrictService();
        public  IList<Shreet> GetAllShreet(string sql, SqlParameter[] paras)
        {
            IList<Shreet> list = null;
          
            DataTable table = SQLDBHelper.GetDataTable(sql, paras);
            if (table != null)
            {
                if (table.Rows.Count > 0)
                {
                    list = new List<Shreet>();
                    foreach (DataRow row in table.Rows)
                    {
                        Shreet shreet = new Shreet();
                        shreet.ShreetId = int.Parse(row["shreetId"].ToString());
                        shreet.ShreetName = row["shreetName"].ToString();
                        shreet.DistrictId = int.Parse(row["districtId"].ToString());
                        if (ds.GetDistrictByID(shreet.DistrictId) != null)
                        {
                            shreet.District = ds.GetDistrictByID(shreet.DistrictId)[0];
                        }
                        list.Add(shreet);
                    }
                }
            }
            return list;
        }
        public  IList<Shreet> GetShreet()
        {
            string sql = "SELECT * FROM Shreet";
            return GetAllShreet(sql, null);
        }
        public  IList<Shreet> GetShreetTx()
        {
            string sql = "SELECT * FROM Shreet";
            IList<Shreet> list = null;
            DataTable table = SQLDBHelper.GetDataTable(sql, null);
            if (table != null)
            {
                if (table.Rows.Count > 0)
                {
                    list = new List<Shreet>();
                    foreach (DataRow row in table.Rows)
                    {
                        Shreet shreet = new Shreet();
                        shreet.ShreetId = int.Parse(row["shreetId"].ToString());
                        shreet.ShreetName = row["shreetName"].ToString();
                        shreet.DistrictId = int.Parse(row["districtId"].ToString());
                        list.Add(shreet);
                    }
                }
            }
            return list;
        }
        public  IList<Shreet> GetShreetByID(int Id)
        {
            string sql = "SELECT * FROM Shreet where ShreetId=@ShreetId";
            SqlParameter[] parameters = {
					new SqlParameter("@ShreetId", SqlDbType.Int,4)};
            parameters[0].Value = Id;
            return GetAllShreet(sql, parameters);
        }
        public  IList<Shreet> GetShreetByDistrictId(int districtId)
        {
            string sql = "SELECT * FROM Shreet where DistrictId=@DistrictId";
            SqlParameter[] parameters = {
					new SqlParameter("@DistrictId", SqlDbType.Int,4)};
            parameters[0].Value = districtId;
            return GetAllShreet(sql, parameters);
        }
        public  DataTable GetShreetByDistrictIdTwo(int districtId)
        {
            string sql = "SELECT * FROM Shreet where DistrictId=@DistrictId";
            SqlParameter[] parameters = {
					new SqlParameter("@DistrictId", SqlDbType.Int,4)};
            parameters[0].Value = districtId;
            return SQLDBHelper.GetDataTable(sql, parameters);
        }
        public  IList<Shreet> GetShreetByName(string name)
        {
            string sql = "SELECT * FROM Shreet where ShreetName=@ShreetName";
            SqlParameter[] parameters = {
					new SqlParameter("@ShreetName", SqlDbType.VarChar,20)};
            parameters[0].Value = name;
            return GetAllShreet(sql, parameters);
        }
        public  IList<Shreet> GetShreetByKeyName(string KeyName)
        {
            string sql = "SELECT * FROM Shreet where ShreetName like '%" + KeyName + "%'";
            SqlParameter[] parameters = {
					new SqlParameter("@ShreetName", SqlDbType.VarChar,20)};
            parameters[0].Value = KeyName;
            return GetAllShreet(sql, parameters);
        }
        public  DataTable GetShreetByKeyNameTooo(string KeyName)
        {
            string sql = "SELECT * FROM Shreet where ShreetName like '%" + KeyName + "%'";
            SqlParameter[] parameters = {
					new SqlParameter("@ShreetName", SqlDbType.VarChar,20)};
            parameters[0].Value = KeyName;
            return SQLDBHelper.GetDataTable(sql, parameters);
        }
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public  bool Add(Shreet model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Shreet(");
            strSql.Append("ShreetName,DistrictId)");
            strSql.Append(" values (");
            strSql.Append("@ShreetName,@DistrictId)");
            SqlParameter[] parameters = {
					      new SqlParameter("@ShreetName", SqlDbType.VarChar,20),
                          new SqlParameter("@DistrictId", SqlDbType.Int,4)};
            parameters[0].Value = model.ShreetName;
            parameters[1].Value = model.DistrictId;

            return SQLDBHelper.ExcuteSql(strSql.ToString(), parameters);

        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public  bool Update(Shreet model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Shreet set ");
            strSql.Append("ShreetName=@ShreetName,");
            strSql.Append("DistrictId=@DistrictId ");
            strSql.Append(" where ShreetId=@ShreetId ");
            SqlParameter[] parameters = {
					
                    new SqlParameter("@ShreetName",SqlDbType.VarChar,20),
					new SqlParameter("@DistrictId", SqlDbType.Int,4),
                    new SqlParameter("@ShreetId", SqlDbType.Int,4)};
           
            parameters[0].Value = model.ShreetName;
            parameters[1].Value = model.DistrictId;
            parameters[2].Value = model.ShreetId;
            return SQLDBHelper.ExcuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public  bool Delete(int ShreetId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Shreet ");
            strSql.Append(" where ShreetId=@ShreetId ");
            SqlParameter[] parameters = {
					new SqlParameter("@ShreetId", SqlDbType.Int,4)};
            parameters[0].Value = ShreetId;

            return SQLDBHelper.ExcuteSql(strSql.ToString(), parameters);
        }
    }
}
