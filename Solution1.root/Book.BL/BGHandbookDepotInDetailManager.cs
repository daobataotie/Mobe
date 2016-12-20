//------------------------------------------------------------------------------
//
// file name：BGHandbookDepotInDetailManager.cs
// author: mayanjun
// create date：2013/12/19 18:37:43
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.BGHandbookDepotInDetail.
    /// </summary>
    public partial class BGHandbookDepotInDetailManager
    {
		
		/// <summary>
		/// Delete BGHandbookDepotInDetail by primary key.
		/// </summary>
		public void Delete(string bGHandbookDepotInDetailId)
		{
			//
			// todo:add other logic here
			//
			accessor.Delete(bGHandbookDepotInDetailId);
		}

		/// <summary>
		/// Insert a BGHandbookDepotInDetail.
		/// </summary>
        public void Insert(Model.BGHandbookDepotInDetail bGHandbookDepotInDetail)
        {
			//
			// todo:add other logic here
			//
            accessor.Insert(bGHandbookDepotInDetail);
        }
		
		/// <summary>
		/// Update a BGHandbookDepotInDetail.
		/// </summary>
        public void Update(Model.BGHandbookDepotInDetail bGHandbookDepotInDetail)
        {
			//
			// todo: add other logic here.
			//
            accessor.Update(bGHandbookDepotInDetail);
        }
    }
}

