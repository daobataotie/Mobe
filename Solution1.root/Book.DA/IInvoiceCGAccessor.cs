//------------------------------------------------------------------------------
//
// file name：IInvoiceCGAccessor.cs
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
    /// Interface of data accessor of dbo.InvoiceCG
    /// </summary>
    public partial interface IInvoiceCGAccessor : IInvoiceAccessor
    {
        IList<Model.InvoiceCG> Select(DateTime start, DateTime end);
        void OwedIncrement(Model.InvoiceCG invoice, decimal money);
        void OwedDecrement(Model.InvoiceCG invoice, decimal money);
        void OwedIncrement(Model.InvoiceCG invoice, decimal? money);
        void OwedDecrement(Model.InvoiceCG invoice, decimal? money);
        void OwedIncrement(string invoice, decimal money);
        void OwedDecrement(string invoice, decimal money);
        void OwedIncrement(string invoice, decimal? money);
        void OwedDecrement(string invoice, decimal? money);

        IList<Model.InvoiceCG> Select(Helper.InvoiceStatus status);        

        IList<Book.Model.InvoiceCG> Select(DateTime start, DateTime end, string startID, string endID);


        IList<Book.Model.InvoiceCG> Select1(DateTime start, DateTime end);
        IList<Book.Model.InvoiceCG> Select(Model.Supplier supplier);
        IList<Book.Model.InvoiceCG> Select(string costartid, string coendid, Model.Supplier SupplierStart, Model.Supplier SupplierEnd, DateTime dateStart, DateTime dateEnd, Model.Product productStart, Model.Product productEnd ,string cusxoid, DateTime dateJHStart, DateTime dateJHEnd);
    }
}

