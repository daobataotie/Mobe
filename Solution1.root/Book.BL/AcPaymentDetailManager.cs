//------------------------------------------------------------------------------
//
// file name：AcPaymentDetailManager.cs
// author: mayanjun
// create date：2011-6-23 09:29:20
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.AcPaymentDetail.
    /// </summary>
    public partial class AcPaymentDetailManager
    {
		
		/// <summary>
		/// Delete AcPaymentDetail by primary key.
		/// </summary>
		public void Delete(string acPaymentDetailId)
		{
			//
			// todo:add other logic here
			//
			accessor.Delete(acPaymentDetailId);
		}

		/// <summary>
		/// Insert a AcPaymentDetail.
		/// </summary>
        public void Insert(Model.AcPaymentDetail acPaymentDetail)
        {
			//
			// todo:add other logic here
			//
            accessor.Insert(acPaymentDetail);
        }
		
		/// <summary>
		/// Update a AcPaymentDetail.
		/// </summary>
        public void Update(Model.AcPaymentDetail acPaymentDetail)
        {
			//
			// todo: add other logic here.
			//
            accessor.Update(acPaymentDetail);
        }

        public IList<Model.AcPaymentDetail> Select(Model.AcPayment acPayment)
        {
            return accessor.Select(acPayment);
        }
    }
}

