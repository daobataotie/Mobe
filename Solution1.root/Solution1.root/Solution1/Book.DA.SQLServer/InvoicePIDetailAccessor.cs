//------------------------------------------------------------------------------
//
// file name：InvoicePIDetailAccessor.cs
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
    /// Data accessor of InvoicePIDetail
    /// </summary>
    public partial class InvoicePIDetailAccessor : EntityAccessor, IInvoicePIDetailAccessor
    {
        public IList<Book.Model.InvoicePIDetail> Select(Book.Model.InvoicePI invoice)
        {
            return sqlmapper.QueryForList<Model.InvoicePIDetail>("InvoicePIDetail.select_by_invoiceid", invoice.InvoiceId);
        }

        public void Delete(Book.Model.InvoicePI invoice)
        {
            sqlmapper.Delete("InvoicePIDetail.delete_by_invoiceid", invoice.InvoiceId);
        }
    }
}
