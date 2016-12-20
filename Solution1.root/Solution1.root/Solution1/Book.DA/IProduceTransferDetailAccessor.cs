//------------------------------------------------------------------------------
//
// file name：IProduceTransferDetailAccessor.cs
// author: mayanjun
// create date：2011-4-6 10:53:37
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.ProduceTransferDetail
    /// </summary>
    public partial interface IProduceTransferDetailAccessor : IAccessor
    {
        IList<Book.Model.ProduceTransferDetail> Select(Model.ProduceTransfer produceTransfer);
    }
}

