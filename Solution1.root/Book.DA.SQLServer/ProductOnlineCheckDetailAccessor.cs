//------------------------------------------------------------------------------
//
// file name：ProductOnlineCheckDetailAccessor.cs
// author: mayanjun
// create date：2013-3-25 17:51:18
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
    /// Data accessor of ProductOnlineCheckDetail
    /// </summary>
    public partial class ProductOnlineCheckDetailAccessor : EntityAccessor, IProductOnlineCheckDetailAccessor
    {
        public IList<Model.ProductOnlineCheckDetail> SelectByProductOnlineCheckId(string ProductOnlineCheckId)
        {
            return sqlmapper.QueryForList<Model.ProductOnlineCheckDetail>("ProductOnlineCheckDetail.SelectByProductOnlineCheckId", ProductOnlineCheckId);
        }

        public void DelectByProductOnlineCheckId(string ProductOnlineCheckId)
        {
             sqlmapper.Delete("ProductOnlineCheckDetail.DelectByProductOnlineCheckId", ProductOnlineCheckId);
        }
    }
}
