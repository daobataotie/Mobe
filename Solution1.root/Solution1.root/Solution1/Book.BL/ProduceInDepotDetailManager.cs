//------------------------------------------------------------------------------
//
// file name：ProduceInDepotDetailManager.cs
// author: peidun
// create date：2010-1-8 13:43:35
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.ProduceInDepotDetail.
    /// </summary>
    public partial class ProduceInDepotDetailManager
    {
		
		/// <summary>
		/// Delete ProduceInDepotDetail by primary key.
		/// </summary>
		public void Delete(string produceInDepotDetailId)
		{
			//
			// todo:add other logic here
			//
			accessor.Delete(produceInDepotDetailId);
		}

		/// <summary>
		/// Insert a ProduceInDepotDetail.
		/// </summary>
        public void Insert(Model.ProduceInDepotDetail produceInDepotDetail)
        {
			//
			// todo:add other logic here
			//
            accessor.Insert(produceInDepotDetail);
        }
		
		/// <summary>
		/// Update a ProduceInDepotDetail.
		/// </summary>
        public void Update(Model.ProduceInDepotDetail produceInDepotDetail)
        {
			//
			// todo: add other logic here.
			//
            accessor.Update(produceInDepotDetail);
        }
        public IList<Book.Model.ProduceInDepotDetail> Select(Model.ProduceInDepot produceInDepot)
        {
            return accessor.Select(produceInDepot);
        }
        public IList<Book.Model.ProduceInDepotDetail> Select(Model.PronoteHeader startPronoteHeader, Model.PronoteHeader endPronoteHeader, DateTime startDate, DateTime endDate)

        {
            return accessor.Select(startPronoteHeader, endPronoteHeader,  startDate,  endDate);
        
        }
    }
}

