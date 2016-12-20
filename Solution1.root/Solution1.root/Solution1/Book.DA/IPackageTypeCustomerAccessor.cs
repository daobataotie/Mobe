//------------------------------------------------------------------------------
//
// file name：IPackageTypeCustomerAccessor.cs
// author: peidun
// create date：2009-08-13 15:38:47
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.PackageTypeCustomer
    /// </summary>
    public partial interface IPackageTypeCustomerAccessor : IAccessor
    {
        IList<Book.Model.PackageTypeCustomer> Select(Book.Model.PackageType type);

        void Delete(Book.Model.PackageType packageType);
    }
}

