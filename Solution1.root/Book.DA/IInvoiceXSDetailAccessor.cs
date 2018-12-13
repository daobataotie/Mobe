//------------------------------------------------------------------------------
//
// file name：IInvoiceXSDetailAccessor.cs
// author: peidun
// create date：2008/6/6 10:00:48
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.InvoiceXSDetail
    /// </summary>
    public partial interface IInvoiceXSDetailAccessor : IEntityAccessor
    {
        IList<Model.InvoiceXSDetail> Select(Model.InvoiceXS invoiceXS);

        void Delete(Model.InvoiceXS invoice);

        //IList<Book.Model.InvoiceXSDetail> Select(DateTime start, DateTime end, Book.Model.Company company);
        IList<Book.Model.InvoiceXSDetail> Select(DateTime startDate, DateTime endDate, string csid, string ceid, string psid, string peid);
        IList<Model.InvoiceXSDetail> Select(Model.InvoiceXO invoiceXO);
        // IList<Book.Model.InvoiceXSDetail> Select(string customerProductsId, Model.InvoiceXO invoiceXO);
        IList<Model.InvoiceXSDetail> Select(Model.InvoiceXS invoiceXS, string productStart, string productEnd);
        IList<Book.Model.InvoiceXSDetail> Select(DateTime startDate, DateTime endDate, Model.Employee employee, Model.Customer customer, Model.Depot depot);
        Model.InvoiceXSDetail GetByProIdPosIdInvoiceId(string productId, string positionId, string invoiceId);
        double GetSumByProductIdAndInvoiceId(string productId, string invoiceId);
        IList<Model.InvoiceXSDetail> Selectbyinvoiceidfz(Model.InvoiceXS inovicexs);
        IList<Model.InvoiceXSDetail> SelectByDateRange(DateTime startdate, DateTime enddate);

        IList<Model.InvoiceXSDetail> SelectbyConditionX(DateTime StartDate, DateTime EndDate, DateTime Yjri1, DateTime Yjri2, Model.Customer Customer1, Model.Customer Customer2, string XOId1, string XOId2, Model.Product Product, Model.Product Product2, string CusXOId, int OrderColumn, int OrderType);

        System.Data.DataTable SelectbyConditionXBiao(DateTime StartDate, DateTime EndDate, DateTime Yjri1, DateTime Yjri2, Book.Model.Customer Customer1, Book.Model.Customer Customer2, string XOId1, string XOId2, Book.Model.Product Product, Book.Model.Product Product2, string CusXOId, int OrderColumn, int OrderType);

        double SumBeeQuantityByHandbook(string handbookId, string handbookProductId);

        double GetInvoiceXSDetailQuantity(string id);

        IList<Model.InvoiceXSDetail> SelectByBGHandBook(DateTime startDate, DateTime endDate, string bgHandBookId, string bgProductId, string productId, string cusXOId);
    }

}

