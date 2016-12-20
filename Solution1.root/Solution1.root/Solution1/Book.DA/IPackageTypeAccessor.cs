//------------------------------------------------------------------------------
//
// file name：IPackageTypeAccessor.cs
// author: peidun
// create date：2009-08-12 9:45:11
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.PackageType
    /// </summary>
    public partial interface IPackageTypeAccessor : IAccessor
    {
         IList<Book.Model.PackageType> Select(Book.Model.Customer customer);
    }
}

