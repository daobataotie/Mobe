//------------------------------------------------------------------------------
//
// file name：IRelationXODetailAccessor.cs
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
    /// Interface of data accessor of dbo.RelationXODetail
    /// </summary>
    public partial interface IRelationXODetailAccessor : IAccessor
    {
        IList<Model.RelationXODetail> SelectByHeaderId(string id);
        void DeleteByHeaderId(string id);
    }
}
