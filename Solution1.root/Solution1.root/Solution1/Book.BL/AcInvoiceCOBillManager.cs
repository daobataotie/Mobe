//------------------------------------------------------------------------------
//
// file name：AcInvoiceCOBillManager.cs
// author: mayanjun
// create date：2011-06-27 15:07:20
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.AcInvoiceCOBill.
    /// </summary>
    public partial class AcInvoiceCOBillManager : BaseManager
    {

        private static readonly DA.IAcInvoiceCOBillDetailAccessor accessorDetails = (DA.IAcInvoiceCOBillDetailAccessor)Accessors.Get("AcInvoiceCOBillDetailAccessor");
        /// <summary>
        /// Delete AcInvoiceCOBill by primary key.
        /// </summary>
        public void Delete(string acInvoiceCOBillId)
        {
            //
            // todo:add other logic here
            //
            accessor.Delete(acInvoiceCOBillId);
        }

        /// <summary>
        /// Insert a AcInvoiceCOBill.
        /// </summary>
        public void Insert(Model.AcInvoiceCOBill acInvoiceCOBill)
        {
            //
            // todo:add other logic here
            //
            accessor.Insert(acInvoiceCOBill);
        }

        public Model.AcInvoiceCOBill GetDetails(Model.AcInvoiceCOBill acInvoiceCoBill)
        {
            Model.AcInvoiceCOBill temp = accessor.Get(acInvoiceCoBill.AcInvoiceCOBillId);
            if (temp != null)
                temp.Details = accessorDetails.SelectByAcInvoiceCOBill(temp);
            return temp;
        }

        /// <summary>
        /// Update a AcInvoiceCOBill.
        /// </summary>
        public void Update(Model.AcInvoiceCOBill acInvoiceCOBill)
        {
            //
            // todo: add other logic here.
            //
            accessor.Update(acInvoiceCOBill);
        }
    }
}

