//------------------------------------------------------------------------------
//
// file name：ICustomerProductsAccessor.cs
// author: peidun
// create date：2009-09-14 下午 05:25:48
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.CustomerProducts
    /// </summary>
    public partial interface ICustomerProductsAccessor : IAccessor
    {
        IList<Book.Model.CustomerProducts> Select(Book.Model.Customer customer);

        bool Exists(Book.Model.CustomerProducts customerProducts);

        bool ExistsExcept(Book.Model.CustomerProducts customerProducts);

        Book.Model.CustomerProducts GetById(string customerProductId);

        float GetStocksQuantityById(string primaryKeyId);

        bool IsExistsCustomerProductId(string customerProductId, string primaryKeyId);

        bool SelectByCustomerIdAndProductId(string customerProductId, string primaryKeyId, string customerId);
        //IList<Book.Model.CustomerProducts> Select(string customerStart, string customerEnd, string productStart, string productEnd, DateTime dateStart, DateTime dateEnd);

        Book.Model.CustomerProducts SelectByCustomerProductProceId(string productid);

        string SelectPrimaryIdByProceName(string customerProductProceName);
    }
}

