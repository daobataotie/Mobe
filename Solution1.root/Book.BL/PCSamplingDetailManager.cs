//------------------------------------------------------------------------------
//
// file name：PCSamplingDetailManager.cs
// author: mayanjun
// create date：2015/10/30 17:07:36
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.PCSamplingDetail.
    /// </summary>
    public partial class PCSamplingDetailManager
    {
		
		/// <summary>
		/// Delete PCSamplingDetail by primary key.
		/// </summary>
		public void Delete(string pCSamplingDetailId)
		{
			//
			// todo:add other logic here
			//
			accessor.Delete(pCSamplingDetailId);
		}

		/// <summary>
		/// Insert a PCSamplingDetail.
		/// </summary>
        public void Insert(Model.PCSamplingDetail pCSamplingDetail)
        {
			//
			// todo:add other logic here
			//
            accessor.Insert(pCSamplingDetail);
        }
		
		/// <summary>
		/// Update a PCSamplingDetail.
		/// </summary>
        public void Update(Model.PCSamplingDetail pCSamplingDetail)
        {
			//
			// todo: add other logic here.
			//
            accessor.Update(pCSamplingDetail);
        }

        public IList<Model.PCSamplingDetail> SelectByCondition(DateTime StartDate, DateTime EndDate, string StartPId, string EndPId, string InvoiceCusId)
        {
            return accessor.SelectByCondition(StartDate, EndDate, StartPId, EndPId, InvoiceCusId);
        }
    }
}
