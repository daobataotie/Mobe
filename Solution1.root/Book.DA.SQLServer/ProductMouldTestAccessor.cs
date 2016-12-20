//------------------------------------------------------------------------------
//
// file name：ProductMouldTestAccessor.cs
// author: mayanjun
// create date：2010-9-24 16:24:34
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Linq;

namespace Book.DA.SQLServer
{
    /// <summary>
    /// Data accessor of ProductMouldTest
    /// </summary>
    public partial class ProductMouldTestAccessor : EntityAccessor, IProductMouldTestAccessor
    {
        public bool IsExistId(Model.ProductMouldTest test)
        {
            Hashtable ht = new Hashtable();
            ht.Add("tid", test.ProductMouldTestId);
            ht.Add("id", test.Id);
            return sqlmapper.QueryForObject<bool>("ProductMouldTest.IsExistId", ht);
        }

        public IList<Book.Model.ProductMouldTest> SelectByDateRage(DateTime StartDate, DateTime EndDate)
        {
            Hashtable ht = new Hashtable();
            ht.Add("startdate", StartDate);
            ht.Add("enddate", EndDate);
            return sqlmapper.QueryForList<Model.ProductMouldTest>("ProductMouldTest.SelectByDateRage", ht);
        }

        public Model.ProductMouldTest SelectByMouldId(string MouldId)
        {
            return sqlmapper.QueryForObject<Model.ProductMouldTest>("ProductMouldTest.SelectByMouldId", MouldId);
        }

        //public void DeleteByMouldId(string MouldId)
        //{
        //    sqlmapper.Delete("ProductMouldTest.DeleteByMouldId", MouldId);
        //}

        //public DataTable SelectOrderByMouldId()
        //{
        //    string sql = "SELECT * FROM ProductMouldTest ORDER BY MouldId,InsertTime";
        //    SqlDataAdapter sda = new SqlDataAdapter(sql, sqlmapper.DataSource.ConnectionString);
        //    DataTable dt = new DataTable();
        //    sda.Fill(dt);
        //    return dt;
        //}

        public IList<Model.ProductMouldTest> SelectOrderByMouldId()
        {
            return sqlmapper.QueryForList<Model.ProductMouldTest>("ProductMouldTest.SelectOrderByMouldId", null);
        }
    }
}
