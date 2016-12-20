//------------------------------------------------------------------------------
//
// file name：InvoiceZGDetailManager.cs
// author: mayanjun
// create date：2012-11-19 14:13:50
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.InvoiceZGDetail.
    /// </summary>
    public partial class InvoiceZGDetailManager
    {

        /// <summary>
        /// Delete InvoiceZGDetail by primary key.
        /// </summary>
        public void Delete(string invoiceZGDetailId)
        {
            //
            // todo:add other logic here
            //
            accessor.Delete(invoiceZGDetailId);
        }

        /// <summary>
        /// Insert a InvoiceZGDetail.
        /// </summary>
        public void Insert(Model.InvoiceZGDetail invoiceZGDetail)
        {
            //
            // todo:add other logic here
            //
            accessor.Insert(invoiceZGDetail);
        }

        /// <summary>
        /// Update a InvoiceZGDetail.
        /// </summary>
        public void Update(Model.InvoiceZGDetail invoiceZGDetail)
        {
            //
            // todo: add other logic here.
            //
            accessor.Update(invoiceZGDetail);
        }

        public void DeleteByInvoiceZGId(string InvoiceZGId)
        {
            accessor.DeleteByInvoiceZGId(InvoiceZGId);
        }


        public IList<Model.InvoiceZGDetail> SelectByInvoiceZGId(string InvoiceZGId)
        {
            return accessor.SelectByInvoiceZGId(InvoiceZGId);
        }

        public void DeleteByInvoicePackingId(string InvoicePackingId)
        {
            accessor.DeleteByInvoiceZGId(InvoicePackingId);
        }
    }
}

