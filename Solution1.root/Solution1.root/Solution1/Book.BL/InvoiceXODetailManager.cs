//------------------------------------------------------------------------------
//
// file name：InvoiceXODetailManager.cs
// author: peidun
// create date：2008/6/20 15:33:49
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.InvoiceXODetail.
    /// </summary>
    public partial class InvoiceXODetailManager : BaseManager
    {
		
		/// <summary>
		/// Delete InvoiceXODetail by primary key.
		/// </summary>
		public void Delete(string invoiceXODetailId)
		{
			//
			// todo:add other logic here
			//
			accessor.Delete(invoiceXODetailId);
		}

		/// <summary>
		/// Insert a InvoiceXODetail.
		/// </summary>
        public void Insert(Model.InvoiceXODetail invoiceXODetail)
        {
			//
			// todo:add other logic here
			//
            accessor.Insert(invoiceXODetail);
        }
		
		/// <summary>
		/// Update a InvoiceXODetail.
		/// </summary>
        public void Update(Model.InvoiceXODetail invoiceXODetail)
        {
			//
			// todo: add other logic here.
			//
            accessor.Update(invoiceXODetail);
        }
        public IList<Book.Model.InvoiceXODetail> Select(Book.Model.InvoiceXO invoiceXO)
        {
            return accessor.Select(invoiceXO);
        }
        public float GetByInvoiceXODetailId(string invoiceXODetailId)
        {
            return accessor.GetByInvoiceXODetailId(invoiceXODetailId);
        }

        public float GetCurrentNum(Model.InvoiceXODetail invoiceXODetail)
        {
            float currentNum = 0;
            BL.CustomerProductsManager customerProductsManager=new BL.CustomerProductsManager();
            float orderNum = this.GetByInvoiceXODetailId(invoiceXODetail.InvoiceXODetailId);
            float stockNum = customerProductsManager.GetStocksQuantityById(invoiceXODetail.PrimaryKeyId);
            if (orderNum < stockNum)
            {
                currentNum = orderNum - stockNum;
            }
            return currentNum;
        }
        public Book.Model.InvoiceXODetail GetInvoiceXOAndProductByNum(string invoiceXODetailId)
        {
            return accessor.GetInvoiceXOAndProductById(invoiceXODetailId);
        }
        public Book.Model.InvoiceXODetail GetAllCurrentNum()
        {
            return accessor.GetAllCurrentNum();
        }
        public IList<Book.Model.InvoiceXODetail> select_XOnotInMps()
        {
            return accessor.select_XOnotInMps();
        }
        public IList<Model.InvoiceXODetail> SelectByDateRangeAndPid(string productid, DateTime startdate, DateTime enddate)
        {
            return accessor.SelectByDateRangeAndPid(productid, startdate, enddate);
        }
    }
}

