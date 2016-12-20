//------------------------------------------------------------------------------
//
// file name：IProduceOtherExitMaterialAccessor.cs
// author: peidun
// create date：2010-1-6 10:20:16
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.ProduceOtherExitMaterial
    /// </summary>
    public partial interface IProduceOtherExitMaterialAccessor : IAccessor
    {
        IList<Model.ProduceOtherExitMaterial> SelectByCondition(DateTime startDate, DateTime endDate, string compactId1, string compactId2, string supperId1, string supperId2);
    }
}

