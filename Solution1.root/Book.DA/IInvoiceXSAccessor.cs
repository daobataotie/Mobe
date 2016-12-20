//------------------------------------------------------------------------------
//
// file name：IInvoiceXSAccessor.cs
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
    /// Interface of data accessor of dbo.InvoiceXS
    /// </summary>
    public partial interface IInvoiceXSAccessor : IInvoiceAccessor
    {
        IList<Model.InvoiceXS> Select(DateTime start, DateTime end);
        void OwedIncrement(Model.InvoiceXS invoice, decimal money);
        void OwedDecrement(Model.InvoiceXS invoice, decimal money);
        void OwedIncrement(Model.InvoiceXS invoice, decimal? money);
        void OwedDecrement(Model.InvoiceXS invoice, decimal? money);
        void OwedIncrement(string invoice, decimal money);
        void OwedDecrement(string invoice, decimal money);
        void OwedIncrement(string invoice, decimal? money);
        void OwedDecrement(string invoice, decimal? money);
        
        IList<Model.InvoiceXS> Select(Helper.InvoiceStatus status);

        IList<Model.InvoiceXS> Select(DateTime start, DateTime end, Model.Employee employee);

        IList<Book.Model.InvoiceXS> Select(DateTime start, DateTime end, string startId, string endId);

        IList<Book.Model.InvoiceXS> Select1(DateTime start, DateTime end);

        IList<Book.Model.InvoiceXS> Select(Model.InvoiceXO invoicexo);

        IList<Book.Model.InvoiceXS> Select(Model.Customer customer);

        //IList<Book.Model.InvoiceXS> Select(Model.CustomerProducts customerProducts);

        IList<Book.Model.InvoiceXS> Select(string customerStart, string customerEnd, string productStart, string productEnd, DateTime dateStart, DateTime dateEnd);
        IList<Model.InvoiceXS> SelectInvoice(Model.Customer customer);

        IList<Model.InvoiceXS> SelectCustomerInfo(string xoid);

        IList<Book.Model.InvoiceXS> SelectDateRangAndWhere(Model.Customer customer1, Model.Customer customer2, DateTime? dateStart, DateTime? dateEnd, DateTime JHDate1, DateTime JHDate2, string cusxoid, Model.Product product, string invoicexoid1, string invoicexoid2);
    }
}
