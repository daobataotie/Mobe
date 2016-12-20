using System;
using System.Collections.Generic;
using System.Text;

namespace Book.DA
{
    public interface IInvoiceDetail01Accessor : IAccessor
    {
        IList<Model.InvoiceDetail01> Select(DateTime start, DateTime end);

        IList<Book.Model.InvoiceDetail01> Select(DateTime start, DateTime end, string startId, string endId, Helper.CompanyKind companyKind);

        IList<Book.Model.InvoiceDetail01> Select1(DateTime start, DateTime end, string startId, string endId, Helper.CompanyKind companyKind);
    }
}
