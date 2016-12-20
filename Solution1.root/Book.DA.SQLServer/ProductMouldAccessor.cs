//------------------------------------------------------------------------------
//
// file name：ProductMouldAccessor.cs
// author: peidun
// create date：2009-07-24 11:18:43
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
    /// Data accessor of ProductMould
    /// </summary>
    public partial class ProductMouldAccessor : EntityAccessor, IProductMouldAccessor
    {
        public IList<Model.ProductMould> SelectProductMouldByProductMouldTestId(string ProductMouldTestId)
        {
            return sqlmapper.QueryForList<Model.ProductMould>("ProductMould.SelectProductMouldByProductMouldTestId", ProductMouldTestId);
        }

        public IList<Model.ProductMould> SelectByDateRage(DateTime StartDate, DateTime EndDate, string MouldId, string MouldName, Model.MouldCategory mouldCategory)
        {
            Hashtable ht = new Hashtable();
            ht.Add("StartDate", StartDate.ToString("yyyy-MM-dd HH:mm:ss"));
            ht.Add("EndDate", EndDate.ToString("yyyy-MM-dd HH:mm:ss"));
            StringBuilder sql = new StringBuilder();
            if (!string.IsNullOrEmpty(MouldId))
                sql.Append(" And Id='" + MouldId + "'");
            if (!string.IsNullOrEmpty(MouldName))
                sql.Append(" And MouldName='" + MouldName + "'");
            if (mouldCategory != null)
                sql.Append(" And MouldCategoryId='" + mouldCategory.MouldCategoryId + "'");
            sql.Append("ORDER BY StartTime DESC");
            ht.Add("sql", sql);
            return sqlmapper.QueryForList<Model.ProductMould>("ProductMould.SelectByDateRage", ht);
        }

        public Model.ProductMould SelectByMouldId(string MouldId)
        {
            return sqlmapper.QueryForObject<Model.ProductMould>("ProductMould.SelectByMouldId", MouldId);
        }
    }
}
