//------------------------------------------------------------------------------
//
// file name：InvoiceXJDetailManager.cs
// author: peidun
// create date：2008/6/20 15:33:49
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.InvoiceXJDetail.
    /// </summary>
    public partial class InvoiceXJDetailManager : BaseManager
    {
		
		/// <summary>
		/// Delete InvoiceXJDetail by primary key.
		/// </summary>
		public void Delete(string invoiceXJDetailId)
		{
			//
			// todo:add other logic here
			//
			accessor.Delete(invoiceXJDetailId);
		}

		/// <summary>
		/// Insert a InvoiceXJDetail.
		/// </summary>
        public void Insert(Model.InvoiceXJDetail invoiceXJDetail)
        {
			//
			// todo:add other logic here
			//
            accessor.Insert(invoiceXJDetail);
        }
		
		/// <summary>
		/// Update a InvoiceXJDetail.
		/// </summary>
        public void Update(Model.InvoiceXJDetail invoiceXJDetail)
        {
			//
			// todo: add other logic here.
			//
            accessor.Update(invoiceXJDetail);
        }
        public IList<Model.InvoiceXJDetail> Select(Model.InvoiceXJ invoiceXJ) 
        {
            return accessor.Select(invoiceXJ);
        }
        /// <summary>
        /// 产品类型为公司产品
        /// </summary>
        /// <returns></returns>
        public IList<Model.InvoiceXJDetail> SelectProductType()
        {
            return accessor.SelectProductType();
        }
    }
}

