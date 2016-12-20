using System;
using System.Collections.Generic;

using System.Text;
using System.Data.OleDb;
using System.Data;
using System.Data.SqlClient;
using Book.Model;

namespace Book.DA.SQLServer
{
    public  class CityService:ICityService
    {
        public  IList<City> GetAllCity(string sql, SqlParameter[] paras)
        {
            IList<City> list = null;
            DataTable table = SQLDBHelper.GetDataTable(sql, paras);
            if (table != null)
            {
                if (table.Rows.Count > 0)
                {
                    list = new List<City>();
                    foreach (DataRow row in table.Rows)
                    {
                        City city = new City();
                        city.CityId = int.Parse(row["cityId"].ToString());
                        city.CityName = row["cityName"].ToString();
                        list.Add(city);
                    }
                }
            }
            return list;
        }
        public  IList<City> GetCity()
        {
            string sql = "SELECT * FROM City";
            return GetAllCity(sql, null);
        }
        public  IList<City> GetCityByID(int Id)
        {
            string sql = "SELECT * FROM City where CityId=@CityId";
            SqlParameter[] parameters = {
					new SqlParameter("@CityId", SqlDbType.Int,4)};
            parameters[0].Value = Id;
            return GetAllCity(sql, parameters);
        }
        public  IList<City> GetCityByName(string name)
        {
            string sql = "SELECT * FROM City where CityName=@CityName";
            SqlParameter[] parameters = {
					new SqlParameter("@CityName", SqlDbType.VarChar,20)};
            parameters[0].Value = name;
            return GetAllCity(sql, parameters);
        }
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public  bool Add(City model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into City(");
            strSql.Append("CityName)");
            strSql.Append(" values (");
            strSql.Append("@CityName)");
            SqlParameter[] parameters = {
                          new SqlParameter("@CityName",SqlDbType.VarChar,50)};
            parameters[0].Value = model.CityName;

            return SQLDBHelper.ExcuteSql(strSql.ToString(), parameters);

        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public  bool Update(City model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update City set ");
            strSql.Append("CityName=@CityName ");
            strSql.Append(" where CityId=@CityId ");
            SqlParameter[] parameters = {
					
					new SqlParameter("@CityName", SqlDbType.VarChar,50),
                    new SqlParameter("@CityId", SqlDbType.Int,4)};
            parameters[0].Value = model.CityName;
            parameters[1].Value = model.CityId;
            return SQLDBHelper.ExcuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public  bool Delete(int CityId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from City ");
            strSql.Append(" where CityId=@CityId ");
            SqlParameter[] parameters = {
					new SqlParameter("@CityId", SqlDbType.Int,4)};
            parameters[0].Value = CityId;

            return SQLDBHelper.ExcuteSql(strSql.ToString(), parameters);
        }
    }
}
