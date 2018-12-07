//------------------------------------------------------------------------------
//
// file name：IInvoiceXODetailAccessor.cs
// author: peidun
// create date：2008/6/20 15:51:01
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.InvoiceXODetail
    /// </summary>
    public partial interface IInvoiceXODetailAccessor : IEntityAccessor
    {
        IList<Book.Model.InvoiceXODetail> Select(Book.Model.InvoiceXO invoiceXO, bool detailsFlag);

        void Delete(Model.InvoiceXO invoice);

        float GetByInvoiceXODetailId(string invoiceXODetailId);
        Book.Model.InvoiceXODetail GetInvoiceXOAndProductById(string invoiceXODetailId);
        Book.Model.InvoiceXODetail GetAllCurrentNum();
        IList<Book.Model.InvoiceXODetail> select_XOnotInMps();
        IList<Model.InvoiceXODetail> SelectByDateRangeAndPid(string productid, DateTime startdate, DateTime enddate);
        IList<Model.InvoiceXODetail> SelectByHeaderProRang(Model.InvoiceXO invoecexo, Model.Product product1, Model.Product product2, bool isclose);
        IList<Model.InvoiceXODetail> Select(Model.Customer customer1, Model.Customer customer2, DateTime startDate, DateTime endDate, DateTime yjrq1, DateTime yjrq2, Model.Employee employee1, Model.Employee employee2, string xoid1, string xoid2, string cusxoidkey, Model.Product product, Model.Product product2, bool isclose, bool mpsIsClose, int orderColumn, int orderType, bool detailFlag, string depotId, string handBookId);
        void UpdateProofUnitPrice(Model.InvoiceXODetail e);
        double SumOrderQuantityByHandbook(string handbookId, string handbookProductId);

        IList<string> SelectProductIDs(string PronoteHeaderId, string handbookProductId);
    }
}

