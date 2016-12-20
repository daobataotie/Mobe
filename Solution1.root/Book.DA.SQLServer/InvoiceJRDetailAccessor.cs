//------------------------------------------------------------------------------
//
// file name：InvoiceJRDetailAccessor.cs
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
    /// Data accessor of InvoiceJRDetail
    /// </summary>
    public partial class InvoiceJRDetailAccessor : EntityAccessor, IInvoiceJRDetailAccessor
    {
        #region IInvoiceJRDetailAccessor 成员


        public IList<Book.Model.InvoiceJRDetail> Select(Book.Model.InvoiceJR invoice)
        {
            return sqlmapper.QueryForList<Model.InvoiceJRDetail>("InvoiceJRDetail.select_by_invoiceid", invoice.InvoiceId);
        }

        public void Delete(Book.Model.InvoiceJR invoice)
        {
            sqlmapper.Delete("InvoiceJRDetail.delete_by_invoiceid", invoice.InvoiceId);
        }

        public IList<Book.Model.InvoiceJRDetail> Select(Book.Model.Supplier supper)
        {
            return sqlmapper.QueryForList<Model.InvoiceJRDetail>("InvoiceJRDetail.select_by_supperId_insert", supper.SupplierId);
        }

        public IList<Book.Model.InvoiceJRDetail> Select(Book.Model.InvoiceHC invoice)
        {
            Hashtable table = new Hashtable();
            table.Add("invoiceid", invoice.JrInvoiceId);
            table.Add("SupplierId", invoice.SupplierId);
            return sqlmapper.QueryForList<Model.InvoiceJRDetail>("InvoiceJRDetail.select_by_supperId_update", table);
        }

        #endregion
    }
}
