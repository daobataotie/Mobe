//------------------------------------------------------------------------------
//
// file name：IMaterialTypeAccessor.cs
// author: peidun
// create date：2009-12-2 16:19:27
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.MaterialType
    /// </summary>
    public partial interface IMaterialTypeAccessor : IAccessor
    {
        bool ExistsName(string name, string id);
    }
}

