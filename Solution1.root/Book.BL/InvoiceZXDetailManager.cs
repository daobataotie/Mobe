//------------------------------------------------------------------------------
//
// file name：InvoiceZXDetailManager.cs
// author: mayanjun
// create date：2012-10-29 14:32:19
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.InvoiceZXDetail.
    /// </summary>
    public partial class InvoiceZXDetailManager
    {

        /// <summary>
        /// Delete InvoiceZXDetail by primary key.
        /// </summary>
        public void Delete(string invoiceZXDetailId)
        {
            //
            // todo:add other logic here
            //
            accessor.Delete(invoiceZXDetailId);
        }

        /// <summary>
        /// Insert a InvoiceZXDetail.
        /// </summary>
        public void Insert(Model.InvoiceZXDetail invoiceZXDetail)
        {
            //
            // todo:add other logic here
            //
            accessor.Insert(invoiceZXDetail);
        }

        /// <summary>
        /// Update a InvoiceZXDetail.
        /// </summary>
        public void Update(Model.InvoiceZXDetail invoiceZXDetail)
        {
            //
            // todo: add other logic here.
            //
            accessor.Update(invoiceZXDetail);
        }

        /// <summary>
        ///  通过装箱编号查询装箱详细
        /// </summary>
        /// <returns></returns>
        public IList<Model.InvoiceZXDetail> SelectByInvoiceZXId(string id)
        {
            return accessor.SelectByInvoiceZXId(id);
        }

        public void DeleteByInvoiceZXId(string id)
        {
            accessor.DeleteByInvoiceZXId(id);
        }
    }
}

