//------------------------------------------------------------------------------
//
// file name：IProduceInDepotDetailAccessor.cs
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
    /// Interface of data accessor of dbo.ProduceInDepotDetail
    /// </summary>
    public partial interface IProduceInDepotDetailAccessor : IAccessor
    {
        IList<Book.Model.ProduceInDepotDetail> Select(Model.ProduceInDepot produceInDepot);
        IList<Book.Model.ProduceInDepotDetail> Select(Model.PronoteHeader startPronoteHeader, Model.PronoteHeader endPronoteHeader, DateTime startDate, DateTime endDate);
    }
}

