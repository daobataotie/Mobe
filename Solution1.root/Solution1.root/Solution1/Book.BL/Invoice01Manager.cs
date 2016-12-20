using System;
using System.Collections.Generic;
using System.Text;

namespace Book.BL
{
    public class Invoice01Manager
    {
        private static readonly DA.IInvoice01Accessor accessor = (DA.IInvoice01Accessor)Accessors.Get("Invoice01Accessor");
        public IList<Model.Invoice01> Select(string kind,string companyId) 
        {
            return accessor.Select(kind, companyId);
        }

        public IList<Book.Model.Invoice01> Select(string kind, string companyId, string invoiceFkId)
        {
            return accessor.Select(kind, companyId, invoiceFkId);
        }
    }
}
