//------------------------------------------------------------------------------
//
// file name:InvoicePTDetailAccessor.cs
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
    /// Data accessor of InvoicePTDetail
    /// </summary>
    public partial class InvoicePTDetailAccessor : EntityAccessor, IInvoicePTDetailAccessor
    {
        #region IInvoicePTDetailAccessor 成员


        public IList<Book.Model.InvoicePTDetail> Select(Book.Model.InvoicePT invoicePT)
        {
            return sqlmapper.QueryForList<Model.InvoicePTDetail>("InvoicePTDetail.select_by_invoiceid", invoicePT.InvoiceId);
        }

        public void Delete(Book.Model.InvoicePT invoice)
        {
            sqlmapper.Delete("InvoicePTDetail.delete_by_invoiceid", invoice.InvoiceId);
        }

        #endregion
    }
}
