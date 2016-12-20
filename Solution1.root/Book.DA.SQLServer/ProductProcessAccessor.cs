//------------------------------------------------------------------------------
//
// file name：ProductProcessAccessor.cs
// author: peidun
// create date：2010-1-27 10:46:40
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
    /// Data accessor of ProductProcess
    /// </summary>
    public partial class ProductProcessAccessor : EntityAccessor, IProductProcessAccessor
    {
        public void Delete(Model.Product Product)
        {
            sqlmapper.Delete("ProductProcess.delete_byProductId", Product.ProductId);
        }
         public IList<Model.ProductProcess> Select(string productId)
         {
             return sqlmapper.QueryForList<Model.ProductProcess>("ProductProcess.select_byProductId", productId);
         }
         public IList<Model.ProductProcess> SelectByBomId(string bomid)
         {
             return sqlmapper.QueryForList<Model.ProductProcess>("ProductProcess.select_byBomId", bomid);
         }
    }
}
