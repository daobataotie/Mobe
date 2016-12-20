//------------------------------------------------------------------------------
//
// file name：ICustomerContactAccessor.cs
// author: peidun
// create date：2009-08-06 14:53:56
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.CustomerContact
    /// </summary>
    public partial interface ICustomerContactAccessor : IAccessor
    {
        IList<Model.CustomerContact> Select(Model.Customer customer);

        void Delete(Book.Model.Customer customer);
    }
}

