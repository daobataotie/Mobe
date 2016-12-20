//------------------------------------------------------------------------------
//
// file name:InvoiceQODetailAccessor.cs
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
    /// Data accessor of InvoiceQODetail
    /// </summary>
    public partial class InvoiceQODetailAccessor : EntityAccessor, IInvoiceQODetailAccessor
    {
        #region IInvoiceQODetailAccessor 成员


        public IList<Book.Model.InvoiceQODetail> Select(Book.Model.InvoiceQO invoiceQO)
        {
            return sqlmapper.QueryForList<Model.InvoiceQODetail>("InvoiceQODetail.select_by_invoiceid", invoiceQO.InvoiceId);
        }

        public void Delete(Book.Model.InvoiceQO invoice)
        {
            sqlmapper.Delete("InvoiceQODetail.delete_by_invoiceid", invoice.InvoiceId);
        }

        #endregion
    }
}
