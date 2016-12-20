//------------------------------------------------------------------------------
//
// file name：ProduceOtherExitDetailManager.cs
// author: peidun
// create date：2010-1-6 10:20:16
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.ProduceOtherExitDetail.
    /// </summary>
    public partial class ProduceOtherExitDetailManager
    {
		
		/// <summary>
		/// Delete ProduceOtherExitDetail by primary key.
		/// </summary>
		public void Delete(string produceOtherExitDetailId)
		{
			//
			// todo:add other logic here
			//
			accessor.Delete(produceOtherExitDetailId);
		}

		/// <summary>
		/// Insert a ProduceOtherExitDetail.
		/// </summary>
        public void Insert(Model.ProduceOtherExitDetail produceOtherExitDetail)
        {
			//
			// todo:add other logic here
			//
            accessor.Insert(produceOtherExitDetail);
        }
		
		/// <summary>
		/// Update a ProduceOtherExitDetail.
		/// </summary>
        public void Update(Model.ProduceOtherExitDetail produceOtherExitDetail)
        {
			//
			// todo: add other logic here.
			//
            accessor.Update(produceOtherExitDetail);
        }
        public IList<Book.Model.ProduceOtherExitDetail> Select(Model.ProduceOtherExitMaterial produceOtherExitMaterial)
        {
            return accessor.Select(produceOtherExitMaterial);
        }
        public IList<Book.Model.ProduceOtherExitDetail> Select(string houseid, DateTime startDate, DateTime endDate)
        {
            return accessor.Select(houseid, startDate, endDate);
        }
        public IList<Model.ProduceOtherExitDetail> SelectByProductAndMaterialId(string ProduceOtherExitMaterialId, string productId1, string productId2)
        {
            return accessor.SelectByProductAndMaterialId(ProduceOtherExitMaterialId, productId1, productId2);
        }
        /// <summary>
        /// Delete ProduceOtherExitDetail by header.
        /// </summary>
        public void Delete(Model.ProduceOtherExitMaterial produceOtherExitMaterial)
        {
            //
            // todo:add other logic here
            //
            accessor.Delete(produceOtherExitMaterial);
        }
    }
}

