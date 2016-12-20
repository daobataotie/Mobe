//------------------------------------------------------------------------------
//
// file name:InvoiceZSDetailAccessor.cs
// author: peidun
// create date:2008/6/20 15:52:13
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
    /// Data accessor of InvoiceZSDetail
    /// </summary>
    public partial class InvoiceZSDetailAccessor : EntityAccessor, IInvoiceZSDetailAccessor
    {
        #region IInvoiceZSDetailAccessor 成员


        public IList<Book.Model.InvoiceZSDetail> Select(Book.Model.InvoiceZS invoice)
        {
            return sqlmapper.QueryForList<Model.InvoiceZSDetail>("InvoiceZSDetail.select_by_invoiceid", invoice.InvoiceId);
        }


        public void Delete(Book.Model.InvoiceZS invoice)
        {
            sqlmapper.Delete("InvoiceZSDetail.delete_by_invoiceid", invoice.InvoiceId);
        }

        #endregion
    }
}
