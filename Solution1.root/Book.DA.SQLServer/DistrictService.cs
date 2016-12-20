using System;
using System.Collections.Generic;

using System.Text;
using System.Data;
using System.Data.SqlClient;
using Book.Model;

using System.Data.OleDb;
namespace Book.DA.SQLServer
{
    public  class DistrictService : IDistrictService
    {
        private CityService cs = new CityService();
        public  IList<District> GetAllDistrict(string sql, SqlParameter[] paras)
        {
            IList<District> list = null;
            DataTable table = SQLDBHelper.GetDataTable(sql, paras);
            if (table != null)
            {
                if (table.Rows.Count > 0)
                {
                    list = new List<District>();
                    foreach (DataRow row in table.Rows)
                    {
                        District district = new District();
                        district.DistrictId = int.Parse(row["districtId"].ToString());
                        district.DistrictName = row["districtName"].ToString();
                        district.CityId = int.Parse(row["cityId"].ToString());
                        if (cs.GetCityByID(district.CityId) != null)
                        {
                            district.City = cs.GetCityByID(district.CityId)[0];
                        }
                        list.Add(district);
                    }
                }
            }
            return list;
        }
        public  IList<District> GetDistrict()
        {
            string sql = "SELECT * FROM District";
            return GetAllDistrict(sql, null);
        }
        public  IList<District> GetDistrictByID(int Id)
        {
            string sql = "SELECT * FROM District where DistrictId=@DistrictId";
            SqlParameter[] parameters = {
					new SqlParameter("@DistrictId", SqlDbType.Int,4)};
            parameters[0].Value = Id;
            return GetAllDistrict(sql, parameters);
        }
        public  IList<District> GetDistrictByCityID(int cityId)
        {
            string sql = "SELECT * FROM District where CityId=@CityId";
            SqlParameter[] parameters = {
					new SqlParameter("@CityId", SqlDbType.Int,4)};
            parameters[0].Value = cityId;
            return GetAllDistrict(sql, parameters);
        }
        public  DataTable GetDistrictByCityIDTwo(int cityId)
        {
            string sql = "SELECT * FROM District where CityId=@CityId";
            SqlParameter[] parameters = {
					new SqlParameter("@CityId", SqlDbType.Int,4)};
            parameters[0].Value = cityId;
            return SQLDBHelper.GetDataTable(sql, parameters);
        }
        public  IList<District> GetDistrictByName(string name)
        {
            string sql = "SELECT * FROM District where DistrictName=@DistrictName";
            SqlParameter[] parameters = {
					new SqlParameter("@DistrictName", SqlDbType.VarChar,20)};
            parameters[0].Value = name;
            return GetAllDistrict(sql, parameters);
        }
        public  IList<District> GetDistrictByNameAndCityId(string name,int cityId)
        {
            string sql = "SELECT * FROM District where DistrictName=@DistrictName and CityId=@CityId";
            SqlParameter[] parameters = {
					new SqlParameter("@DistrictName", SqlDbType.VarChar,20),
                    new SqlParameter("@CityId",SqlDbType.Int,4)};
            parameters[0].Value = name;
            parameters[1].Value = cityId;
            return GetAllDistrict(sql, parameters);
        }
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public  bool Add(District model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into District(");
            strSql.Append("DistrictName,CityId)");
            strSql.Append(" values (");
            strSql.Append("@DistrictName,@CityId)");
            SqlParameter[] parameters = {
					      new SqlParameter("@DistrictName",SqlDbType.VarChar,20),
                          new SqlParameter("@CityId", SqlDbType.Int,4)};
            parameters[0].Value = model.DistrictName;
            parameters[1].Value = model.CityId;

            return SQLDBHelper.ExcuteSql(strSql.ToString(), parameters);

        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public  bool Update(District model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update District set ");
            strSql.Append("DistrictName=@DistrictName,");
            strSql.Append("CityId=@CityId ");
            strSql.Append(" where DistrictId=@DistrictId ");
            SqlParameter[] parameters = {
					
                    new SqlParameter("@DistrictName",SqlDbType.VarChar,20),
					new SqlParameter("@CityId", SqlDbType.Int,4),
                    new SqlParameter("@DistrictId", SqlDbType.Int,4)};
            
            parameters[0].Value = model.DistrictName;
            parameters[1].Value = model.CityId;
            parameters[2].Value = model.DistrictId;
            return SQLDBHelper.ExcuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public  bool Delete(int districtId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from District ");
            strSql.Append(" where DistrictId=@DistrictId ");
            SqlParameter[] parameters = {
					new SqlParameter("@DistrictId", SqlDbType.Int,4)};
            parameters[0].Value = districtId;

            return SQLDBHelper.ExcuteSql(strSql.ToString(), parameters);
        }
    }
}
