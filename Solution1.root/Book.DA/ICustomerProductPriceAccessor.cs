//------------------------------------------------------------------------------
//
// file name：ICustomerProductPriceAccessor.cs
// author: mayanjun
// create date：2013-3-8 16:09:36
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.CustomerProductPrice
    /// </summary>
    public partial interface ICustomerProductPriceAccessor : IAccessor
    {
        IList<Model.CustomerProductPrice> SelectByCustomerId(string CustomerId);

        IList<Model.CustomerProductPrice> SelectByProductId(string ProductId);

        void UpdateByCustomerProductsId(Model.CustomerProductPrice model);

        string SelectPriceByProductId(string ProductId);

        IList<Model.CustomerProductPrice> SelectAll();
    }
}

