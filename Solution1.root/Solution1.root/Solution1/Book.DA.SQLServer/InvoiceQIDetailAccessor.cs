//------------------------------------------------------------------------------
//
// file name:InvoiceQIDetailAccessor.cs
// author: peidun
// create date:2008/6/6 10:00:50
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
    /// Data accessor of InvoiceQIDetail
    /// </summary>
    public partial class InvoiceQIDetailAccessor : EntityAccessor, IInvoiceQIDetailAccessor
    {
        #region IInvoiceQIDetailAccessor 成员


        public IList<Book.Model.InvoiceQIDetail> Select(Book.Model.InvoiceQI invoiceQI)
        {
            return sqlmapper.QueryForList<Model.InvoiceQIDetail>("InvoiceQIDetail.select_by_invoiceid", invoiceQI.InvoiceId);
        }

        public void Delete(Book.Model.InvoiceQI invoice)
        {
            sqlmapper.Delete("InvoiceQI.delete_by_invoiceid", invoice.InvoiceId);
        }

        #endregion
    }
}
