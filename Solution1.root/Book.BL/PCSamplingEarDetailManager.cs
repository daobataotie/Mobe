//------------------------------------------------------------------------------
//
// file name：PCSamplingEarDetailManager.cs
// author: mayanjun
// create date：2015/10/31 16:25:11
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.PCSamplingEarDetail.
    /// </summary>
    public partial class PCSamplingEarDetailManager
    {
		
		/// <summary>
		/// Delete PCSamplingEarDetail by primary key.
		/// </summary>
		public void Delete(string pCSamplingEarDetailId)
		{
			//
			// todo:add other logic here
			//
			accessor.Delete(pCSamplingEarDetailId);
		}

		/// <summary>
		/// Insert a PCSamplingEarDetail.
		/// </summary>
        public void Insert(Model.PCSamplingEarDetail pCSamplingEarDetail)
        {
			//
			// todo:add other logic here
			//
            accessor.Insert(pCSamplingEarDetail);
        }
		
		/// <summary>
		/// Update a PCSamplingEarDetail.
		/// </summary>
        public void Update(Model.PCSamplingEarDetail pCSamplingEarDetail)
        {
			//
			// todo: add other logic here.
			//
            accessor.Update(pCSamplingEarDetail);
        }

        public IList<Model.PCSamplingEarDetail> SelectByCondition(DateTime StartDate, DateTime EndDate, string StartPId, string EndPId, string InvoiceCusId)
        {
            return accessor.SelectByCondition(StartDate, EndDate, StartPId, EndPId, InvoiceCusId);
        }
    }
}
