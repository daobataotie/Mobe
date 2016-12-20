//------------------------------------------------------------------------------
//
// file name：ICustomerProductProcessAccessor.cs
// author: peidun
// create date：2009-09-25 下午 05:16:42
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.CustomerProductProcess
    /// </summary>
    public partial interface ICustomerProductProcessAccessor : IAccessor
    {
        IList<Book.Model.CustomerProductProcess> SelectProcessCategory(Book.Model.CustomerProducts customerProducts);

        void Delete(Book.Model.CustomerProducts customerProducts);
    }
}

