//------------------------------------------------------------------------------
//
// file name：InvoiceXSDetailManager.cs
// author: peidun
// create date：2008/6/6 10:00:59
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.InvoiceXSDetail.
    /// </summary>
    public partial class InvoiceXSDetailManager : BaseManager
    {

        /// <summary>
        /// Delete InvoiceXSDetail by primary key.
        /// </summary>
        public void Delete(string invoiceXSDetailId)
        {
            //
            // todo:add other logic here
            //
            accessor.Delete(invoiceXSDetailId);
        }
        /// <summary>
        /// Insert a InvoiceXSDetail.
        /// </summary>
        public void Insert(Model.InvoiceXSDetail invoiceXSDetail)
        {
            //
            // todo:add other logic here
            //
            accessor.Insert(invoiceXSDetail);
        }

        /// <summary>
        /// Update a InvoiceXSDetail.
        /// </summary>
        public void Update(Model.InvoiceXSDetail invoiceXSDetail)
        {
            //
            // todo: add other logic here.
            //
            accessor.Update(invoiceXSDetail);
        }
        public IList<Model.InvoiceXSDetail> Select(Model.InvoiceXS invoiceXS)
        {
            return accessor.Select(invoiceXS);
        }

        //public IList<Book.Model.InvoiceXSDetail> Select(DateTime start, DateTime end, Book.Model.Company company)
        //{
        //    return accessor.Select(start, end, company);
        //}

        public IList<Book.Model.InvoiceXSDetail> Select(DateTime startDate, DateTime endDate, string csid, string ceid, string psid, string peid)
        {
            return accessor.Select(startDate, endDate, csid, ceid, psid, peid);
        }
        public IList<Model.InvoiceXSDetail> Select(Model.InvoiceXO invoiceXO)
        {
            return accessor.Select(invoiceXO);
        }
        //public IList<Book.Model.InvoiceXSDetail> Select(string customerProductsId, Model.InvoiceXO invoiceXO)
        //{
        //    return accessor.Select(customerProductsId, invoiceXO);
        //}
        public IList<Model.InvoiceXSDetail> Select(Model.InvoiceXS invoiceXS, string productStart, string productEnd)
        {
            return accessor.Select(invoiceXS, productStart, productEnd);
        }
        public IList<Book.Model.InvoiceXSDetail> Select(DateTime startDate, DateTime endDate, Model.Employee employee, Model.Customer customer, Model.Depot depot)
        {
            return accessor.Select(startDate, endDate, employee, customer, depot);
        }

        public Model.InvoiceXSDetail GetByProIdPosIdInvoiceId(string productId, string positionId, string invoiceId)
        {
            return accessor.GetByProIdPosIdInvoiceId(productId, positionId, invoiceId);
        }

        public double GetSumByProductIdAndInvoiceId(string productId, string invoiceId)
        {
            return accessor.GetSumByProductIdAndInvoiceId(productId, invoiceId);
        }

        public IList<Model.InvoiceXSDetail> Selectbyinvoiceidfz(Model.InvoiceXS inovicexs)
        {
            return accessor.Selectbyinvoiceidfz(inovicexs);
        }

        public IList<Model.InvoiceXSDetail> SelectByDateRange(DateTime startdate, DateTime enddate)
        {
            return accessor.SelectByDateRange(startdate, enddate);
        }

        public IList<Model.InvoiceXSDetail> SelectbyConditionX(DateTime StartDate, DateTime EndDate, DateTime Yjri1, DateTime Yjri2, Model.Customer Customer1, Model.Customer Customer2, string XOId1, string XOId2, Model.Product Product, Model.Product Product2, string CusXOId, int OrderColumn, int OrderType, string depotId, string handBookId)
        {
            return accessor.SelectbyConditionX(StartDate, EndDate, Yjri1, Yjri2, Customer1, Customer2, XOId1, XOId2, Product, Product2, CusXOId, OrderColumn, OrderType,  depotId,  handBookId);
        }

        public System.Data.DataTable SelectbyConditionXBiao(DateTime StartDate, DateTime EndDate, DateTime Yjri1, DateTime Yjri2, Model.Customer Customer1, Model.Customer Customer2, string XOId1, string XOId2, Model.Product Product, Model.Product Product2, string CusXOId, int OrderColumn, int OrderType)
        {
            return accessor.SelectbyConditionXBiao(StartDate, EndDate, Yjri1, Yjri2, Customer1, Customer2, XOId1, XOId2, Product, Product2, CusXOId, OrderColumn, OrderType);
        }

        public double SumBeeQuantityByHandbook(string handbookId, string handbookProductId)
        {
            return accessor.SumBeeQuantityByHandbook(handbookId, handbookProductId);
        }

        public double GetInvoiceXSDetailQuantity(string id)
        {
            return accessor.GetInvoiceXSDetailQuantity(id);
        }

        public IList<Model.InvoiceXSDetail> SelectByBGHandBook(DateTime startDate, DateTime endDate, string bgHandBookId, string bgProductId, string productId, string cusXOId)
        {
            return accessor.SelectByBGHandBook(startDate, endDate, bgHandBookId, bgProductId, productId, cusXOId);
        }

        public System.Data.DataTable SelectAnnualShipment(string ProductId, DateTime StartDate, DateTime EndDate, string CustomerId, int showType)
        {
            return accessor.SelectAnnualShipment(ProductId, StartDate, EndDate, CustomerId, showType);
        }
    }
}

