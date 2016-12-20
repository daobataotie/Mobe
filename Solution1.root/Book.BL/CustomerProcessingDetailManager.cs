//------------------------------------------------------------------------------
//
// file name：CustomerProcessingDetailManager.cs
// author: mayanjun
// create date：2010-7-30 19:31:55
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.CustomerProcessingDetail.
    /// </summary>
    public partial class CustomerProcessingDetailManager
    {
		
		/// <summary>
		/// Delete CustomerProcessingDetail by primary key.
		/// </summary>
		public void Delete(string customerProcessingDetailId)
		{
			//
			// todo:add other logic here
			//
			accessor.Delete(customerProcessingDetailId);
		}

		/// <summary>
		/// Insert a CustomerProcessingDetail.
		/// </summary>
        public void Insert(Model.CustomerProcessingDetail customerProcessingDetail)
        {
			//
			// todo:add other logic here
			//
            accessor.Insert(customerProcessingDetail);
        }
		
		/// <summary>
		/// Update a CustomerProcessingDetail.
		/// </summary>
        public void Update(Model.CustomerProcessingDetail customerProcessingDetail)
        {
			//
			// todo: add other logic here.
			//
            accessor.Update(customerProcessingDetail);
        }
        public IList<Model.CustomerProcessingDetail> Select(Model.CustomerProcessing CustomerProcessing)
        {
            //
            // todo: add other logic here.
            //
            return accessor.Select(CustomerProcessing);
        }

        public IList<Model.CustomerProcessingDetail> SelectbyBomId(string bomid)
        {
            return accessor.SelectbyBomId(bomid);
        }
    }
}

