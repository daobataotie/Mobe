//------------------------------------------------------------------------------
//
// file name：IProduceOtherInDepotDetailAccessor.cs
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
    /// Interface of data accessor of dbo.ProduceOtherInDepotDetail
    /// </summary>
    public partial interface IProduceOtherInDepotDetailAccessor : IAccessor
    {
        IList<Book.Model.ProduceOtherInDepotDetail> Select(Model.ProduceOtherInDepot produceOtherInDepot);
        IList<Book.Model.ProduceOtherInDepotDetail> Select(Model.ProduceOtherCompact startPronoteHeader, Model.ProduceOtherCompact endPronoteHeader, DateTime startDate, DateTime endDate);
        IList<Model.ProduceOtherInDepotDetail> SelectByCondition(string indepotId, string productId1, string productId2);
        IList<Model.ProduceOtherInDepotDetail> SelectByProduceotherInDepotId(string id);
        void Delete(Model.ProduceOtherInDepot indepot);

    }
}

