//------------------------------------------------------------------------------
//
// file name：CustomerMarksManager.cs
// author: mayanjun
// create date：2013-5-8 13:43:43
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.CustomerMarks.
    /// </summary>
    public partial class CustomerMarksManager
    {

        /// <summary>
        /// Delete CustomerMarks by primary key.
        /// </summary>
        public void Delete(string customerMarksId)
        {
            //
            // todo:add other logic here
            //
            accessor.Delete(customerMarksId);
        }

        /// <summary>
        /// Insert a CustomerMarks.
        /// </summary>
        public void Insert(Model.CustomerMarks customerMarks)
        {
            //
            // todo:add other logic here
            //
            accessor.Insert(customerMarks);
        }

        /// <summary>
        /// Update a CustomerMarks.
        /// </summary>
        public void Update(Model.CustomerMarks customerMarks)
        {
            //
            // todo: add other logic here.
            //
            accessor.Update(customerMarks);
        }

        public IList<Model.CustomerMarks> SelectByCustomerId(string customerId)
        {
            return accessor.SelectByCustomerId(customerId);
        }

        public void DeleteByCustomerId(string customerId)
        {
            accessor.DeleteByCustomerId(customerId);
        }

        public IList<Model.CustomerMarks> SelectByInvoicePackingId(string InvoicePackingId)
        {
            return accessor.SelectByInvoicePackingId(InvoicePackingId);
        }

        public void DeleteByInvoicePackingId(string InvoicePackingId)
        {
            accessor.DeleteByInvoicePackingId(InvoicePackingId);
        }
    }
}

