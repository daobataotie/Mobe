//------------------------------------------------------------------------------
//
// file name：ProduceOtherInDepotDetailManager.cs
// author: peidun
// create date：2010-1-8 13:43:35
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.ProduceOtherInDepotDetail.
    /// </summary>
    public partial class ProduceOtherInDepotDetailManager
    {
		
		/// <summary>
		/// Delete ProduceOtherInDepotDetail by primary key.
		/// </summary>
		public void Delete(string produceOtherInDepotDetailId)
		{
			//
			// todo:add other logic here
			//
			accessor.Delete(produceOtherInDepotDetailId);
		}

		/// <summary>
		/// Insert a ProduceOtherInDepotDetail.
		/// </summary>
        public void Insert(Model.ProduceOtherInDepotDetail produceOtherInDepotDetail)
        {
			//
			// todo:add other logic here
			//
            accessor.Insert(produceOtherInDepotDetail);
        }
		
		/// <summary>
		/// Update a ProduceOtherInDepotDetail.
		/// </summary>
        public void Update(Model.ProduceOtherInDepotDetail produceOtherInDepotDetail)
        {
			//
			// todo: add other logic here.
			//
            accessor.Update(produceOtherInDepotDetail);
        }
        public IList<Book.Model.ProduceOtherInDepotDetail> Select(Model.ProduceOtherInDepot produceOtherInDepot)
        {
            return accessor.Select(produceOtherInDepot);
        }
        public IList<Book.Model.ProduceOtherInDepotDetail> Select(Model.ProduceOtherCompact startPronoteHeader, Model.ProduceOtherCompact endPronoteHeader, DateTime startDate, DateTime endDate)
        {
            return accessor.Select(startPronoteHeader, endPronoteHeader, startDate, endDate);
        }
        public IList<Model.ProduceOtherInDepotDetail> SelectByCondition(string indepotId, string productId1, string productId2)
        {
            return accessor.SelectByCondition(indepotId, productId1, productId2);
        }
    }
}

