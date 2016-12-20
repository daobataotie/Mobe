//------------------------------------------------------------------------------
//
// file name：CustomerProductProcessAccessor.cs
// author: peidun
// create date：2009-09-25 下午 05:16:42
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
    /// Data accessor of CustomerProductProcess
    /// </summary>
    public partial class CustomerProductProcessAccessor : EntityAccessor, ICustomerProductProcessAccessor
    {
        #region ICustomerProductProcessAccessor Members


        public IList<Model.CustomerProductProcess> SelectProcessCategory(Book.Model.CustomerProducts customerProducts)
        {

            return sqlmapper.QueryForList<Model.CustomerProductProcess>("CustomerProductProcess.select_processcategory_by_customerproductid", customerProducts.PrimaryKeyId);
            //IList<Model.CustomerProductProcess> list = null;
            //if (string.IsNullOrEmpty(customerProducts.PrimaryKeyId))
            //{
            //    list = sqlmapper.QueryForList<Model.CustomerProductProcess>("CustomerProductProcess.select_processcategory", null);
            //}
            //else
            //{
                //list = sqlmapper.QueryForList<Model.CustomerProductProcess>("CustomerProductProcess.select_processcategory_by_customerproductid", customerProducts.PrimaryKeyId);
            //    if (list.Count == 0)
            //    {
            //        list = sqlmapper.QueryForList<Model.CustomerProductProcess>("CustomerProductProcess.select_processcategory", null);
            //    }
            //}

            //return list;
        }

        public void Delete(Book.Model.CustomerProducts customerProducts)
        {
            sqlmapper.Delete("CustomerProductProcess.delete_customerproductId", customerProducts.PrimaryKeyId);
        }

        #endregion
    }
}
