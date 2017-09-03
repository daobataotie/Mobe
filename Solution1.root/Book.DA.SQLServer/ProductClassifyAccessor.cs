//------------------------------------------------------------------------------
//
// file name：ProductClassifyAccessor.cs
// author: mayanjun
// create date：2017-08-24 21:36:04
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
    /// Data accessor of ProductClassify
    /// </summary>
    public partial class ProductClassifyAccessor : EntityAccessor, IProductClassifyAccessor
    {
        public bool IsExistsKeyWordForInsert(Model.ProductClassify productClassify)
        {
            return sqlmapper.QueryForObject<bool>("ProductClassify.IsExistsKeyWordForInsert", productClassify.KeyWord);
        }

        public bool IsExistsKeyWordForUpdate(Model.ProductClassify productClassify)
        {
            Hashtable ht = new Hashtable();
            ht.Add("ProductClassifyId", productClassify.ProductClassifyId);
            ht.Add("KeyWord", productClassify.KeyWord);
            return sqlmapper.QueryForObject<bool>("ProductClassify.IsExistsKeyWordForUpdate", ht);
        }

        public IList<string> SelectAllKeyWord()
        {
            return sqlmapper.QueryForList<string>("ProductClassify.SelectAllKeyWord", null);
        }

        public IList<Model.ProductClassify> SelectCondtidion(DateTime startDate, DateTime endDate, string keyWord)
        {
            Hashtable ht = new Hashtable();
            ht.Add("startDate", startDate.Date);
            ht.Add("endDate", endDate.Date.AddDays(1).AddSeconds(-1));

            if (!string.IsNullOrEmpty(keyWord))
                ht.Add("sql", " and KeyWord='" + keyWord + "'");
            return sqlmapper.QueryForList<Model.ProductClassify>("ProductClassify.SelectCondtidion", ht);
        }
    }
}
