//------------------------------------------------------------------------------
//
// file name：IProduceOtherReturnMaterialAccessor.cs
// author: mayanjun
// create date：2011-08-31 15:05:11
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.ProduceOtherReturnMaterial
    /// </summary>
    public partial interface IProduceOtherReturnMaterialAccessor : IAccessor
    {
        IList<Model.ProduceOtherReturnMaterial> Select(DateTime startdate, DateTime enddate);
    }
}

