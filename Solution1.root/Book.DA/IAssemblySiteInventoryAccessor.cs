//------------------------------------------------------------------------------
//
// file name：IAssemblySiteInventoryAccessor.cs
// author: mayanjun
// create date：2018-05-14 19:16:31
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.AssemblySiteInventory
    /// </summary>
    public partial interface IAssemblySiteInventoryAccessor : IAccessor
    {
        void UpdateState(bool state, string assemblySiteInventoryId);
    }
}
