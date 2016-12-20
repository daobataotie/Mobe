//------------------------------------------------------------------------------
//
// file name：IProduceOtherCompactDetailAccessor.cs
// author: peidun
// create date：2010-1-4 15:32:39
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.ProduceOtherCompactDetail
    /// </summary>
    public partial interface IProduceOtherCompactDetailAccessor : IAccessor
    {
        IList<Book.Model.ProduceOtherCompactDetail> Select(Model.ProduceOtherCompact produceOtherCompact);
        IList<Book.Model.ProduceOtherCompactDetail> SelectIsInDepot(Model.ProduceOtherCompact produceOtherCompact);
        double GetByMPSdetail(string mPSDetailId);
        IList<Book.Model.ProduceOtherCompactDetail> Select(string customerStart, string customerEnd, DateTime dateStart, DateTime dateEnd);
        IList<Model.ProduceOtherCompactDetail> SelectByConditoin(string cid1, string cid2, DateTime startdate, DateTime enddate, string pid0, string pid1, string sid0, string sid1);
        IList<Book.Model.ProduceOtherCompactDetail> GetThreeMaths();
        IList<Book.Model.ProduceOtherCompactDetail> GetDate(DateTime startDate, DateTime endDate);
        IList<Book.Model.ProduceOtherCompactDetail> SelectCompactDetailAndFlag(Model.ProduceOtherCompact produceOtherCompact);
        IList<Model.ProduceOtherCompactDetail> Select(string CompactId, string StartpId, string EndpId);
        IList<Book.Model.ProduceOtherCompactDetail> Select(string produceOtherCompactId);
    }

}

