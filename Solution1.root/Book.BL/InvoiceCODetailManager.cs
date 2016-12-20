//------------------------------------------------------------------------------
//
// file name：InvoiceCODetailManager.cs
// author: peidun
// create date：2008/6/20 15:33:49
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.InvoiceCODetail.
    /// </summary>
    public partial class InvoiceCODetailManager : BaseManager
    {
        /// <summary>
        /// Delete InvoiceCODetail by primary key.
        /// </summary>
        public void Delete(string invoiceCODetailId)
        {
            //
            // todo:add other logic here
            //
            accessor.Delete(invoiceCODetailId);
        }

        /// <summary>
        /// Insert a InvoiceCODetail.
        /// </summary>
        public void Insert(Model.InvoiceCODetail invoiceCODetail)
        {
            //
            // todo:add other logic here
            //
            accessor.Insert(invoiceCODetail);
        }

        /// <summary>
        /// Update a InvoiceCODetail.
        /// </summary>
        public void Update(Model.InvoiceCODetail invoiceCODetail)
        {
            accessor.Update(invoiceCODetail);
        }

        /// <summary>
        /// 单价校对修改
        /// </summary>
        public void UpdateProofUnitPrice(Model.InvoiceCODetail e)
        {
            //修改详细
            accessor.UpdateProofUnitPrice(e);
        }
        public IList<Model.InvoiceCODetail> Select(Model.InvoiceCO invoice)
        {
            return accessor.Select(invoice);
        }

        public IList<Model.InvoiceCODetail> Select(string invoiceId)
        {
            return accessor.Select(invoiceId);
        }

        public IList<Model.InvoiceCODetail> SelectByDateRangeAndPid(string pid, DateTime startdate, DateTime enddate)
        {
            return accessor.SelectByDateRangeAndPid(pid, startdate, enddate);
        }

        public IList<Model.InvoiceCODetail> SelectByHeaderProRang(string InvoiceId, Model.Product productStart, Model.Product productEnd)
        {
            return accessor.SelectByHeaderProRang(InvoiceId, productStart, productEnd);
        }

        public IList<Model.InvoiceCODetail> Select(string costartid, string coendid, Model.Supplier SupplierStart, Model.Supplier SupplierEnd, DateTime? dateStart, DateTime? dateEnd, Model.Product productStart, Model.Product productEnd, string cusxoid, DateTime dateJHStart, DateTime dateJHEnd, int? invoiceFlag, Model.Employee empStart, Model.Employee empEnd)
        {
            return accessor.Select(costartid, coendid, SupplierStart, SupplierEnd, dateStart, dateEnd, productStart, productEnd, cusxoid, dateJHStart, dateJHEnd, invoiceFlag, empStart, empEnd);
        }
    }
}

