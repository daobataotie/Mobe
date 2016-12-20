//------------------------------------------------------------------------------
//
// file name：AcInvoiceCOBillDetailManager.cs
// author: mayanjun
// create date：2011-06-27 15:07:20
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.AcInvoiceCOBillDetail.
    /// </summary>
    public partial class AcInvoiceCOBillDetailManager
    {
		
		/// <summary>
		/// Delete AcInvoiceCOBillDetail by primary key.
		/// </summary>
		public void Delete(string acInvoiceCOBillDetailId)
		{
			//
			// todo:add other logic here
			//
			accessor.Delete(acInvoiceCOBillDetailId);
		}

		/// <summary>
		/// Insert a AcInvoiceCOBillDetail.
		/// </summary>
        public void Insert(Model.AcInvoiceCOBillDetail acInvoiceCOBillDetail)
        {
			//
			// todo:add other logic here
			//
            accessor.Insert(acInvoiceCOBillDetail);
        }
		
		/// <summary>
		/// Update a AcInvoiceCOBillDetail.
		/// </summary>
        public void Update(Model.AcInvoiceCOBillDetail acInvoiceCOBillDetail)
        {
			//
			// todo: add other logic here.
			//
            accessor.Update(acInvoiceCOBillDetail);
        }

        public IList<Model.AcInvoiceCOBillDetail> SelectByAcInvoiceCOBill(Model.AcInvoiceCOBill acInvoiceCoBill)
        {
            return accessor.SelectByAcInvoiceCOBill(acInvoiceCoBill);
        }
    }
}

