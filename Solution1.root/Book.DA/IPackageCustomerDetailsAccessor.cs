//------------------------------------------------------------------------------
//
// file name：IPackageCustomerDetailsAccessor.cs
// author: peidun
// create date：2009-11-10 18:27:47
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.PackageCustomerDetails
    /// </summary>
    public partial interface IPackageCustomerDetailsAccessor : IAccessor
    {
        IList<Model.PackageCustomerDetails> Select(string customerProductId);
        void Delete(Model.CustomerProducts custeomerProduct);
    }
}

