//------------------------------------------------------------------------------
//
// file name：AcInvoiceXOBillDetailManager.cs
// author: mayanjun
// create date：2011-09-28 08:45:15
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.AcInvoiceXOBillDetail.
    /// </summary>
    public partial class AcInvoiceXOBillDetailManager
    {

        /// <summary>
        /// Delete AcInvoiceXOBillDetail by primary key.
        /// </summary>
        public void Delete(string acInvoiceCOBillDetailId)
        {
            //
            // todo:add other logic here
            //
            accessor.Delete(acInvoiceCOBillDetailId);
        }

        /// <summary>
        /// Insert a AcInvoiceXOBillDetail.
        /// </summary>
        public void Insert(Model.AcInvoiceXOBillDetail acInvoiceXOBillDetail)
        {
            //
            // todo:add other logic here
            //
            accessor.Insert(acInvoiceXOBillDetail);
        }

        /// <summary>
        /// Update a AcInvoiceXOBillDetail.
        /// </summary>
        public void Update(Model.AcInvoiceXOBillDetail acInvoiceXOBillDetail)
        {
            //
            // todo: add other logic here.
            //
            accessor.Update(acInvoiceXOBillDetail);
        }
        public IList<Model.AcInvoiceXOBillDetail> SelectByAcInvoiceXOBill(Model.AcInvoiceXOBill acInvoiceXoBill)
        {
            return accessor.SelectByAcInvoiceXOBill(acInvoiceXoBill);
        }

        public void Delete(Model.AcInvoiceXOBill acInvoiceXoBill)
        {
            accessor.Delete(acInvoiceXoBill);
        }

        public IList<Model.AcInvoiceXOBillDetail> selectByConditionInvoiceXODetail(DateTime? startdate, DateTime? enddate, string IdStart, string IdEnd, Model.Customer startCustomer, Model.Customer endCustomer)
        {
            return accessor.selectByConditionInvoiceXODetail(startdate, enddate, IdStart, IdEnd, startCustomer, endCustomer);
        }
    }
}

