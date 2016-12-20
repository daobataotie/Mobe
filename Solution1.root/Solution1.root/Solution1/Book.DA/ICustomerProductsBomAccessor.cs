//------------------------------------------------------------------------------
//
// file name：ICustomerProductsBomAccessor.cs
// author: peidun
// create date：2009-10-13 上午 11:45:03
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.CustomerProductsBom
    /// </summary>
    public partial interface ICustomerProductsBomAccessor : IAccessor
    {
        IList<Book.Model.CustomerProductsBom> SelectBomInfos(Book.Model.CustomerProducts product);

        void Delete(Book.Model.CustomerProducts customerProducts);
    }
}

