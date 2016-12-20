//------------------------------------------------------------------------------
//
// file name：IInvoiceCOAccessor.cs
// author: peidun
// create date：2008/6/6 10:00:47
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.InvoiceCO
    /// </summary>
    public partial interface IInvoiceCOAccessor : IInvoiceAccessor
    {
        IList<Model.InvoiceCO> Select(DateTime start, DateTime end);

        IList<Model.InvoiceCO> Select(Helper.InvoiceStatus status);
        IList<Book.Model.InvoiceCO> Select(Model.Supplier supplier);
        void Updates(Book.Model.InvoiceCO invoiceCO);
        IList<Model.InvoiceCO> SelectbySupplierAndinvoiceId(Model.Supplier supplier, string invoiceId);
        IList<Model.InvoiceCO> SelectByMrsHeaderId(string MrsHeaderId);
        IList<Book.Model.InvoiceCO> SelectDateRangAndWhere(string costartid, string coendid, Model.Supplier SupplierStart, Model.Supplier SupplierEnd, DateTime? dateStart, DateTime? dateEnd, Model.Product productStart, Model.Product productEnd, string cusxoid, DateTime dateJHStart, DateTime dateJHEnd, int invoiceFlag);
      
    }
}

