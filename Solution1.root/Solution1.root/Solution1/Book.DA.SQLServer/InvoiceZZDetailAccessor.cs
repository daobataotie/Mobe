//------------------------------------------------------------------------------
//
// file name:InvoiceZZDetailAccessor.cs
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
    /// Data accessor of InvoiceZZDetail
    /// </summary>
    public partial class InvoiceZZDetailAccessor : EntityAccessor, IInvoiceZZDetailAccessor
    {

        #region IInvoiceZZDetailAccessor 成员


        public IList<Book.Model.InvoiceZZDetail> Select(string productKind, Book.Model.InvoiceZZ invoiceZZ)
        {
            Hashtable args = new Hashtable();
            args.Add("InvoiceId",invoiceZZ.InvoiceId);
            args.Add("InvoiceZZDetailKind",productKind);
            return sqlmapper.QueryForList<Model.InvoiceZZDetail>("InvoiceZZDetail.select_by_InvoiceId_InvoiceZZDetailKind", args);
        }

        public IList<Book.Model.InvoiceZZDetail> Select(Book.Model.InvoiceZZ invoiceZZ)
        {
            return sqlmapper.QueryForList<Model.InvoiceZZDetail>("InvoiceZZDetail.select_by_invoiceid", invoiceZZ.InvoiceId);
        }

        public void Delete(Book.Model.InvoiceZZ invoice)
        {
            sqlmapper.Delete("InvoiceZZDetail.delete_by_invoiceid", invoice.InvoiceId);
        }

        #endregion
    }
}
