//------------------------------------------------------------------------------
//
// file name：ICustomerPackageAccessor.cs
// author: peidun
// create date：2010-2-4 11:15:12
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.CustomerPackage
    /// </summary>
    public partial interface ICustomerPackageAccessor : IAccessor
    {
        IList<Model.CustomerPackage> Select(Model.Customer customer);
     
    }
}

