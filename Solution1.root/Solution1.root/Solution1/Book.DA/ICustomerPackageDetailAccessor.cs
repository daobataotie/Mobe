//------------------------------------------------------------------------------
//
// file name：ICustomerPackageDetailAccessor.cs
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
    /// Interface of data accessor of dbo.CustomerPackageDetail
    /// </summary>
    public partial interface ICustomerPackageDetailAccessor : IAccessor
    {
       IList<Model.CustomerPackageDetail> GetByPackageId(string customerPackageId);
       
    }
}

