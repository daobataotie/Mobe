//------------------------------------------------------------------------------
//
// file name：IMouldCategoryAccessor.cs
// author: peidun
// create date：2009-07-24 11:18:42
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.MouldCategory
    /// </summary>
    public partial interface IMouldCategoryAccessor : IAccessor
    {
        bool IsExistMouldCategoryName(Model.MouldCategory mould);
        bool IsExistId(Model.MouldCategory mould);
    }
}

