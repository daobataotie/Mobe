//------------------------------------------------------------------------------
//
// file name：AcOtherShouldPaymentDetailManager.cs
// author: mayanjun
// create date：2011-6-10 10:11:49
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.AcOtherShouldPaymentDetail.
    /// </summary>
    public partial class AcOtherShouldPaymentDetailManager
    {
		
		/// <summary>
		/// Delete AcOtherShouldPaymentDetail by primary key.
		/// </summary>
		public void Delete(string acOtherShouldPaymentDetailId)
		{
			//
			// todo:add other logic here
			//
			accessor.Delete(acOtherShouldPaymentDetailId);
		}
		/// <summary>
		/// Insert a AcOtherShouldPaymentDetail.
		/// </summary>
        public void Insert(Model.AcOtherShouldPaymentDetail acOtherShouldPaymentDetail)
        {
			//
			// todo:add other logic here
			//
            accessor.Insert(acOtherShouldPaymentDetail);
        }
		
		/// <summary>
		/// Update a AcOtherShouldPaymentDetail.
		/// </summary>
        public void Update(Model.AcOtherShouldPaymentDetail acOtherShouldPaymentDetail)
        {
			//
			// todo: add other logic here.
			//
            accessor.Update(acOtherShouldPaymentDetail);
        }
        public IList<Model.AcOtherShouldPaymentDetail> Select(Model.AcOtherShouldPayment acOtherShouldPayment)
        {
            return accessor.Select(acOtherShouldPayment);
        }
    }
}

