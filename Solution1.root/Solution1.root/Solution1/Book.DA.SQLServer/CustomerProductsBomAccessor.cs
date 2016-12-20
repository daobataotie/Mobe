//------------------------------------------------------------------------------
//
// file name：CustomerProductsBomAccessor.cs
// author: peidun
// create date：2009-10-13 上午 11:45:04
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
    /// Data accessor of CustomerProductsBom
    /// </summary>
    public partial class CustomerProductsBomAccessor : EntityAccessor, ICustomerProductsBomAccessor
    {
        #region ICustomerProductsBomAccessor Members


        public IList<Book.Model.CustomerProductsBom> SelectBomInfos(Book.Model.CustomerProducts product)
        {
            return sqlmapper.QueryForList<Model.CustomerProductsBom>("CustomerProductsBom.selectBoms_by_product", product.PrimaryKeyId);
        }

        public void Delete(Book.Model.CustomerProducts customerProducts)
        {
            if (customerProducts == null) return;
            sqlmapper.Delete("CustomerProductsBom.delete_by_product", customerProducts.PrimaryKeyId);
        }

        #endregion
    }
}
