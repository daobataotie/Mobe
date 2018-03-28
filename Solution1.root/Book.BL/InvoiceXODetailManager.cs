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
        public IList<Book.Model.InvoiceXODetail> Select(Book.Model.InvoiceXO invoiceXO, bool detailsFlag)
        {
            return accessor.Select(invoiceXO, detailsFlag);
        }
        public float GetByInvoiceXODetailId(string invoiceXODetailId)
        {
            return accessor.GetByInvoiceXODetailId(invoiceXODetailId);
        }

        public float GetCurrentNum(Model.InvoiceXODetail invoiceXODetail)
        {
            float currentNum = 0;
            BL.CustomerProductsManager customerProductsManager = new BL.CustomerProductsManager();
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
        public IList<Model.InvoiceXODetail> SelectByHeaderProRang(Model.InvoiceXO invoecexo, Model.Product product1, Model.Product product2, bool isclose)
        {
            return accessor.SelectByHeaderProRang(invoecexo, product1, product2, isclose);
        }

        /// <summary>
        /// 单价校对修改
        /// </summary>
        public void UpdateProofUnitPrice(Model.InvoiceXODetail e)
        {
            //修改详细
            accessor.UpdateProofUnitPrice(e);
        }

        public IList<Model.InvoiceXODetail> Select(Model.Customer customer1, Model.Customer customer2, DateTime startDate, DateTime endDate, DateTime yjrq1, DateTime yjrq2, Model.Employee employee1, Model.Employee employee2, string xoid1, string xoid2, string cusxoidkey, Model.Product product, Model.Product product2, bool isclose, bool mpsIsClose, int orderColumn, int orderType, bool detailFlag)
        {
            return accessor.Select(customer1, customer2, startDate, endDate, yjrq1, yjrq2, employee1, employee2, xoid1, xoid2, cusxoidkey, product, product2, isclose, mpsIsClose, orderColumn, orderType, detailFlag);
        }

        public double SumOrderQuantityByHandbook(string handbookId, string handbookProductId)
        {
            return accessor.SumOrderQuantityByHandbook(handbookId, handbookProductId);
        }

        public IList<string> SelectProductIDs(string PronoteHeaderId, string handbookProductId)
        {
            return accessor.SelectProductIDs(PronoteHeaderId, handbookProductId);
        }
    }
}

