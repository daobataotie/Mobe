//------------------------------------------------------------------------------
//
// file name：IProcessCategoryAccessor.cs
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
    /// Interface of data accessor of dbo.ProcessCategory
    /// </summary>
    public partial interface IProcessCategoryAccessor : IAccessor
    {
         bool ExistsName(string name, string id);
    }
}

