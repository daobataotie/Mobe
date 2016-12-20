//------------------------------------------------------------------------------
//
// file name：IBomPackageDetailsAccessor.cs
// author: peidun
// create date：2009-11-12 11:03:18
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.BomPackageDetails
    /// </summary>
    public partial interface IBomPackageDetailsAccessor : IAccessor
    {
         IList<Model.BomPackageDetails> Select(string bomId);
         void Delete(Model.BomParentPartInfo bom);
    }
}

