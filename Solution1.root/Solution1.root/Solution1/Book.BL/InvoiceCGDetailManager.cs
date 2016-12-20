//------------------------------------------------------------------------------
//
// file name：InvoiceCGDetailManager.cs
// author: peidun
// create date：2008/6/6 10:00:59
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.InvoiceCGDetail.
    /// </summary>
    public partial class InvoiceCGDetailManager : BaseManager
    {
		
		/// <summary>
		/// Delete InvoiceCGDetail by primary key.
		/// </summary>
		public void Delete(string invoiceCGDetailId)
		{
			//
			// todo:add other logic here
			//
			accessor.Delete(invoiceCGDetailId);
		}

		/// <summary>
		/// Insert a InvoiceCGDetail.
		/// </summary>
        public void Insert(Model.InvoiceCGDetail invoiceCGDetail)
        {
			//
			// todo:add other logic here
			//
            accessor.Insert(invoiceCGDetail);
        }
		
		/// <summary>
		/// Update a InvoiceCGDetail.
		/// </summary>
        public void Update(Model.InvoiceCGDetail invoiceCGDetail)
        {
			//
			// todo: add other logic here.
			//
            accessor.Update(invoiceCGDetail);
        }

        public void InsertUpdate(Model.InvoiceCGDetail invoiceCGDetail)
        {
            if (invoiceCGDetail.InvoiceCGDetailId == null)
                this.Insert(invoiceCGDetail);
            else
                this.Update(invoiceCGDetail);
        }

        /// <summary>
        /// 获取属于指定采购单的明细
        /// </summary>
        /// <param name="invoice"></param>
        /// <returns></returns>
        public IList<Model.InvoiceCGDetail> Select(Model.InvoiceCG invoice)
        {
            return accessor.Select(invoice);
        }

        public IList<Book.Model.InvoiceCGDetail> Select(DateTime startDate, DateTime endDate, string startId, string endId)
        {
            return accessor.Select(startDate, endDate, startId, endId);
        }
        public IList<Book.Model.InvoiceCGDetail> Select(DateTime startDate, DateTime endDate, string csid, string ceid, string psid, string peid)
        {
            return accessor.Select(startDate, endDate, csid, ceid,psid,peid);
        }
        public IList<Model.InvoiceCGDetail> SelectCount(Model.InvoiceCO invoice)
        {
            return accessor.SelectCount(invoice);
        }
        public IList<Model.InvoiceCGDetail> Select(Model.InvoiceCG invoice,string productStart,string productEnd)
        {
            return accessor.Select(invoice, productStart,productEnd);
        }

        public Model.InvoiceCGDetail SelectByProductIdAndHeadIdAndPositionId(string productId, string invoiceId, string positionId)
        {
            return accessor.SelectByProductIdAndHeadIdAndPositionId(productId, invoiceId, positionId);
        }

        public double GetSumByProductIdAndInvoiceId(string productId, string invoiceId)
        {
            return accessor.GetSumByProductIdAndInvoiceId(productId, invoiceId);
        }

        public IList<Model.InvoiceCGDetail> SelectbyinvoiceIdfz(Model.InvoiceCG invoicecg)
        {
            return accessor.SelectbyinvoiceIdfz(invoicecg);
        }
    }
}

