//------------------------------------------------------------------------------
//
// file name：IBGProductDepotOutAccessor.cs
// author: mayanjun
// create date：2014/3/25 18:18:02
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.BGProductDepotOut
    /// </summary>
    public partial interface IBGProductDepotOutAccessor : IAccessor
    {
        bool IsExistsDeclareCustomsIdInsert(string DeclareCustomsId);
        bool IsExistsDeclareCustomsIdUpdate(string DeclareCustomsId, string Id);
    }
}

