//------------------------------------------------------------------------------
//
// file name：DepotOutDetailManager.cs
// author: mayanjun
// create date：2010-10-15 15:41:03
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.DepotOutDetail.
    /// </summary>
    public partial class DepotOutDetailManager
    {
		
		/// <summary>
		/// Delete DepotOutDetail by primary key.
		/// </summary>
		public void Delete(string depotOutDetailId)
		{
			//
			// todo:add other logic here
			//
			accessor.Delete(depotOutDetailId);
		}

		/// <summary>
		/// Insert a DepotOutDetail.
		/// </summary>
        public void Insert(Model.DepotOutDetail depotOutDetail)
        {
			//
			// todo:add other logic here
			//
            accessor.Insert(depotOutDetail);
        }
		
		/// <summary>
		/// Update a DepotOutDetail.
		/// </summary>
        public void Update(Model.DepotOutDetail depotOutDetail)
        {
			//
			// todo: add other logic here.
			//
            accessor.Update(depotOutDetail);
        }

        public IList<Model.DepotOutDetail> GetDepotOutDetailByDepotOutId(string depotOutId)
        {
            return accessor.GetDepotOutDetailByDepotOutId(depotOutId);
        }
        public void Delete(Model.DepotOut depotOut)
        {
            accessor.Delete(depotOut.DepotOutId);
        }
    }
}

