using System;
using System.Collections.Generic;
using System.Text;

namespace Book.BL
{
    public class Invoice00Manager
    {
        private static readonly DA.IInvoice00Accessor accessor = (DA.IInvoice00Accessor)Accessors.Get("Invoice00Accessor");

        public IList<Model.Invoice00> Select()
        {
            return accessor.Select();
        }
    }
}
