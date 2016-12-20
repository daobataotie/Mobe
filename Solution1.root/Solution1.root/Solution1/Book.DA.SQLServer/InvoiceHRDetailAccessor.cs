//------------------------------------------------------------------------------
//
// file name：InvoiceHRDetailAccessor.cs
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
    /// Data accessor of InvoiceHRDetail
    /// </summary>
    public partial class InvoiceHRDetailAccessor : EntityAccessor, IInvoiceHRDetailAccessor
    {
        public IList<Book.Model.InvoiceHRDetail> Select(Book.Model.InvoiceHR invoice)
        {
            return sqlmapper.QueryForList<Model.InvoiceHRDetail>("InvoiceHRDetail.select_by_invoiceid", invoice.InvoiceId);
        }

        public void Delete(Book.Model.InvoiceHR invoice)
        {
            sqlmapper.Delete("InvoiceHRDetail.delete_by_invoiceid", invoice.InvoiceId);
        }
    }
}
