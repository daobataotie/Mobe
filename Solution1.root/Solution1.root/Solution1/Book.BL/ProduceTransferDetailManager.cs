//------------------------------------------------------------------------------
//
// file name：ProduceTransferDetailManager.cs
// author: mayanjun
// create date：2011-4-6 10:53:37
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.ProduceTransferDetail.
    /// </summary>
    public partial class ProduceTransferDetailManager
    {
		
		/// <summary>
		/// Delete ProduceTransferDetail by primary key.
		/// </summary>
		public void Delete(string produceTransferDetailId)
		{
			//
			// todo:add other logic here
			//
			accessor.Delete(produceTransferDetailId);
		}

		/// <summary>
		/// Insert a ProduceTransferDetail.
		/// </summary>
        public void Insert(Model.ProduceTransferDetail produceTransferDetail)
        {
			//
			// todo:add other logic here
			//
            accessor.Insert(produceTransferDetail);
        }
		
		/// <summary>
		/// Update a ProduceTransferDetail.
		/// </summary>
        public void Update(Model.ProduceTransferDetail produceTransferDetail)
        {
			//
			// todo: add other logic here.
			//
            accessor.Update(produceTransferDetail);
        }
        public IList<Book.Model.ProduceTransferDetail> Select(Model.ProduceTransfer produceTransfer)
        {
            return accessor.Select(produceTransfer);
        }
    }
}

