//------------------------------------------------------------------------------
//
// file name：IAcItemAccessor.cs
// author: mayanjun
// create date：2012-2-21 13:36:08
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.AcItem
    /// </summary>
    public partial interface IAcItemAccessor : IAccessor
    {
        void DeleteALL();
        string SelectPriIdByName(string itemname);
    }
}

