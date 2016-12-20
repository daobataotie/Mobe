//------------------------------------------------------------------------------
//
// file name：IProduceOtherReturnDetailAccessor.cs
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
    /// Interface of data accessor of dbo.ProduceOtherReturnDetail
    /// </summary>
    public partial interface IProduceOtherReturnDetailAccessor : IAccessor
    {
        System.Collections.Generic.IList<Model.ProduceOtherReturnDetail> Select(Model.ProduceOtherReturnMaterial Material);
    }
}

