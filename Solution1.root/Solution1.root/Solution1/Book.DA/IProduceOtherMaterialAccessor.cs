//------------------------------------------------------------------------------
//
// file name：IProduceOtherMaterialAccessor.cs
// author: peidun
// create date：2010-1-5 15:26:21
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.ProduceOtherMaterial
    /// </summary>
    public partial interface IProduceOtherMaterialAccessor : IAccessor
    {
        IList<Model.ProduceOtherMaterial> SelectState();
        IList<Model.ProduceOtherMaterial> SelectByCondition(DateTime startdate, DateTime enddate, string supperId1, string supperId2, string cid1, string cid2);
    }
}

