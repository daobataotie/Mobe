//------------------------------------------------------------------------------
//
// file name：IProduceInDepotAccessor.cs
// author: peidun
// create date：2010-1-8 13:43:36
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.ProduceInDepot
    /// </summary>
    public partial interface IProduceInDepotAccessor : IAccessor
    {
        IList<Model.ProduceInDepot> SelectByDateRange(DateTime stardate, DateTime enddate);
    }
}

