//------------------------------------------------------------------------------
//
// file name：IRelationXOAccessor.cs
// author: mayanjun
// create date：2015/4/19 下午 08:06:08
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.RelationXO
    /// </summary>
    public partial interface IRelationXOAccessor : IAccessor
    {
        bool ExistsXO(string CusId, string RelationXOId);
        Model.RelationXO SelectByInvoiceXOCusId(string id);
    }
}
