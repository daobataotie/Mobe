using System;
using System.Collections.Generic;
using System.Text;

namespace Book.DA
{
    public interface IInvoice01Accessor : IAccessor
    {
        IList<Model.Invoice01> Select(string kind, string companyId);

        void Update(Book.Model.Invoice01 detail, Book.Model.InvoiceFK invoiceFK);

        void Update(Book.Model.Invoice01 detail, Book.Model.InvoiceSK invoiceSK);

        IList<Book.Model.Invoice01> Select(string kind, string companyId, string invoiceFkId);
    }
}
