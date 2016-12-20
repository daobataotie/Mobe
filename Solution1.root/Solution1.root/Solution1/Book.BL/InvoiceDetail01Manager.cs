using System;
using System.Collections.Generic;
using System.Text;

namespace Book.BL
{
    public class InvoiceDetail01Manager
    {
        private static readonly DA.IInvoiceDetail01Accessor accessor = (DA.IInvoiceDetail01Accessor)Accessors.Get("InvoiceDetail01Accessor");
                
        public IList<Model.InvoiceDetail01> Select(DateTime start, DateTime end) 
        {
            return accessor.Select(start, end);
        }

        public IList<Book.Model.InvoiceDetail01> Select(DateTime start, DateTime end, string startId, string endId, Helper.CompanyKind companyKind)
        {
            return accessor.Select(start, end, startId, endId, companyKind);
        }

        public IList<Book.Model.InvoiceDetail01> Select1(DateTime start, DateTime end, string startId, string endId, Helper.CompanyKind companyKind)
        {
            return accessor.Select1(start, end, startId, endId, companyKind);
        }
    }
}
