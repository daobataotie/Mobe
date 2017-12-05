//------------------------------------------------------------------------------
//
// file name:ProductCategoryAccessor.cs
// author: peidun
// create date:2008/6/6 10:00:50
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
    /// Data accessor of ProductCategory
    /// </summary>
    public partial class ProductCategoryAccessor : EntityAccessor, IProductCategoryAccessor
    {

        public bool ExistsName(string productCategoryName, string ProductCategoryId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("ProductCategoryName", productCategoryName);
            ht.Add("ProductCategoryId", ProductCategoryId);
            return sqlmapper.QueryForObject<bool>("ProductCategory.existsName", ht);

        }

        public IList<string> SelectALLName()
        {
            return sqlmapper.QueryForList<string>("ProductCategory.SelectALLName", null);
        }

        public DataTable SelectDTByFilter(string filter)
        {
            using (SqlConnection conn = new SqlConnection(sqlmapper.DataSource.ConnectionString))
            {
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand(string.Format("select * from ProductCategory {0} order by id", filter), conn);

                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public IList<Model.ProductCategory> SelectListByFilter(string CategoryLevel, string ProductCategoryParentId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("CategoryLevel", CategoryLevel);
            if (CategoryLevel != "1")
            {
                ht.Add("sql", string.Format(" and ProductCategoryParentId='{0}'", ProductCategoryParentId));
            }
            return sqlmapper.QueryForList<Model.ProductCategory>("ProductCategory.SelectListByFilter", ht);
        }

        /// <summary>
        /// 2017-12-5 ：真正的查询所有，"Select"只查询Level为1的
        /// </summary>
        /// <returns></returns>
        public IList<Model.ProductCategory> SelectAll()
        {
            return this.Select<Model.ProductCategory>();
        }
    }
}
