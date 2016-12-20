//------------------------------------------------------------------------------
//
// file name：ProduceOtherCompactDetailManager.cs
// author: peidun
// create date：2010-1-4 15:32:38
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.ProduceOtherCompactDetail.
    /// </summary>
    public partial class ProduceOtherCompactDetailManager
    {

        /// <summary>
        /// Delete ProduceOtherCompactDetail by primary key.
        /// </summary>
        public void Delete(string otherCompactDetailId)
        {
            //
            // todo:add other logic here
            //
            accessor.Delete(otherCompactDetailId);
        }

        /// <summary>
        /// Insert a ProduceOtherCompactDetail.
        /// </summary>
        public void Insert(Model.ProduceOtherCompactDetail produceOtherCompactDetail)
        {
            //
            // todo:add other logic here
            //
            accessor.Insert(produceOtherCompactDetail);
        }

        /// <summary>
        /// Update a ProduceOtherCompactDetail.
        /// </summary>
        public void Update(Model.ProduceOtherCompactDetail produceOtherCompactDetail)
        {
            //
            // todo: add other logic here.
            //
            accessor.Update(produceOtherCompactDetail);
        }
        public IList<Book.Model.ProduceOtherCompactDetail> Select(Model.ProduceOtherCompact produceOtherCompact)
        {
            return accessor.Select(produceOtherCompact);
        }

        public IList<Book.Model.ProduceOtherCompactDetail> SelectIsInDepot(Model.ProduceOtherCompact produceOtherCompact)
        {
            return accessor.SelectIsInDepot(produceOtherCompact);
        }

        public double GetByMPSdetail(string mPSDetailId)
        {
            return accessor.GetByMPSdetail(mPSDetailId);
        }
        public IList<Book.Model.ProduceOtherCompactDetail> Select(string customerStart, string customerEnd, DateTime dateStart, DateTime dateEnd)
        {
            return accessor.Select(customerStart, customerEnd, dateStart, dateEnd);
        }
        public IList<Model.ProduceOtherCompactDetail> SelectByConditoin(string cid1, string cid2, DateTime startdate, DateTime enddate, string pid0, string pid1, string sid0, string sid1)
        {
            return accessor.SelectByConditoin(cid1, cid2, startdate, enddate, pid0, pid1, sid0, sid1);
        }
        public IList<Book.Model.ProduceOtherCompactDetail> GetThreeMaths()
        {
            return accessor.GetThreeMaths();
        }
        public IList<Book.Model.ProduceOtherCompactDetail> GetDate(DateTime startDate, DateTime endDate)
        {
            return accessor.GetDate(startDate, endDate);
        }
        public IList<Book.Model.ProduceOtherCompactDetail> SelectCompactDetailAndFlag(Model.ProduceOtherCompact produceOtherCompact)
        {
            return accessor.SelectCompactDetailAndFlag(produceOtherCompact);
        }
        public IList<Model.ProduceOtherCompactDetail> Select(string CompactId, string StartpId, string EndpId)
        {
            return accessor.Select(CompactId, StartpId, EndpId);
        }

        public IList<Book.Model.ProduceOtherCompactDetail> Select(string produceOtherCompactId)
        {
            return accessor.Select(produceOtherCompactId);
        }
    }
}

