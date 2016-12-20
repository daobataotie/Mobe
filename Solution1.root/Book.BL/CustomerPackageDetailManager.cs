//------------------------------------------------------------------------------
//
// file name：CustomerPackageDetailManager.cs
// author: peidun
// create date：2010-2-4 11:15:11
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.CustomerPackageDetail.
    /// </summary>
    public partial class CustomerPackageDetailManager
    {
		
		/// <summary>
		/// Delete CustomerPackageDetail by primary key.
		/// </summary>
		public void Delete(string customerPackageDetailId)
		{
			//
			// todo:add other logic here
			//
			accessor.Delete(customerPackageDetailId);
		}

		/// <summary>
		/// Insert a CustomerPackageDetail.
		/// </summary>
        public void Insert(Model.CustomerPackageDetail customerPackageDetail)
        {
			//
			// todo:add other logic here
			//
            accessor.Insert(customerPackageDetail);
        }
		
		/// <summary>
		/// Update a CustomerPackageDetail.
		/// </summary>
        public void Update(Model.CustomerPackageDetail customerPackageDetail)
        {
			//
			// todo: add other logic here.
			//
            accessor.Update(customerPackageDetail);
        }
        public IList<Model.CustomerPackageDetail> GetByPackageId(string customerPackageId)
        {
            return accessor.GetByPackageId(customerPackageId);
        }
    }
}

