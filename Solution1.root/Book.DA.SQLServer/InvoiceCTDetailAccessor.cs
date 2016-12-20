//------------------------------------------------------------------------------
//
// file name:InvoiceCTDetailAccessor.cs
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
    /// Data accessor of InvoiceCTDetail
    /// </summary>
    public partial class InvoiceCTDetailAccessor : EntityAccessor, IInvoiceCTDetailAccessor
    {
        #region IInvoiceCTDetailAccessor 成员

        public IList<Book.Model.InvoiceCTDetail> Select(Book.Model.InvoiceCT invoiceCT)
        {
            return sqlmapper.QueryForList<Model.InvoiceCTDetail>("InvoiceCTDetail.select_by_invoiceid", invoiceCT.InvoiceId);
        }

        public void Delete(Book.Model.InvoiceCT invoice)
        {
            sqlmapper.Delete("InvoiceCTDetail.delete_by_invoiceid", invoice.InvoiceId);
        }

        #endregion
    }
}
