//------------------------------------------------------------------------------
//
// file name：PCOtherCheckDetailManager.cs
// author: mayanjun
// create date：2011-11-10 15:05:55
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.PCOtherCheckDetail.
    /// </summary>
    public partial class PCOtherCheckDetailManager
    {
		
		/// <summary>
		/// Delete PCOtherCheckDetail by primary key.
		/// </summary>
		public void Delete(string pCOtherCheckDetailId)
		{
			//
			// todo:add other logic here
			//
			accessor.Delete(pCOtherCheckDetailId);
		}

		/// <summary>
		/// Insert a PCOtherCheckDetail.
		/// </summary>
        public void Insert(Model.PCOtherCheckDetail pCOtherCheckDetail)
        {
			//
			// todo:add other logic here
			//
            accessor.Insert(pCOtherCheckDetail);
        }
		
		/// <summary>
		/// Update a PCOtherCheckDetail.
		/// </summary>
        public void Update(Model.PCOtherCheckDetail pCOtherCheckDetail)
        {
			//
			// todo: add other logic here.
			//
            accessor.Update(pCOtherCheckDetail);
        }

        public IList<Model.PCOtherCheckDetail> SelectByConditon(DateTime StartDate, DateTime EndDate, Book.Model.Product product, string CusXOId)
        {
            return accessor.SelectByConditon(StartDate, EndDate, product, CusXOId);
        }
    }
}

