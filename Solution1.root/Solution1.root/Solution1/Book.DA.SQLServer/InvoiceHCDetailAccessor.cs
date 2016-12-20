//------------------------------------------------------------------------------
//
// file name：InvoiceHCDetailAccessor.cs
// author: peidun
// create date：2008-11-29 12:52:19
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Book.DA.SQLServer
{
    /// <summary>
    /// Data accessor of InvoiceHCDetail
    /// </summary>
    public partial class InvoiceHCDetailAccessor : EntityAccessor, IInvoiceHCDetailAccessor
    {
        public IList<Book.Model.InvoiceHCDetail> Select(Book.Model.InvoiceHC invoice)
        {
            return sqlmapper.QueryForList<Model.InvoiceHCDetail>("InvoiceHCDetail.select_by_invoiceid", invoice.InvoiceId);
        }

        public void Delete(Book.Model.InvoiceHC invoice)
        {
            sqlmapper.Delete("InvoiceHCDetail.delete_by_invoiceid", invoice.InvoiceId);
        }
    }
}
