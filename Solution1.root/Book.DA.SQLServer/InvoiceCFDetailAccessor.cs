//------------------------------------------------------------------------------
//
// file name:InvoiceCFDetailAccessor.cs
// author: peidun
// create date:2008/6/6 10:00:49
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
    /// Data accessor of InvoiceCFDetail
    /// </summary>
    public partial class InvoiceCFDetailAccessor : EntityAccessor, IInvoiceCFDetailAccessor
    {
        #region IInvoiceCFDetailAccessor 成员


        public IList<Book.Model.InvoiceCFDetail> Select(Book.Model.InvoiceCF invoice)
        {
            return sqlmapper.QueryForList<Model.InvoiceCFDetail>("InvoiceCFDetail.select_by_invoiceid", invoice.InvoiceId);
        }

        public void Delete(Book.Model.InvoiceCF invoice)
        {
            sqlmapper.Delete("InvoiceCFDetail.delete_by_invoiceid", invoice.InvoiceId);
        }

        public IList<Book.Model.InvoiceCFDetail> Select(string kind, Book.Model.InvoiceCF invoice)
        {
            Hashtable args = new Hashtable();
            args.Add("InvoiceId", invoice.InvoiceId);
            args.Add("InvoiceCFDetailKind", kind);
            return sqlmapper.QueryForList<Model.InvoiceCFDetail>("InvoiceCFDetail.select_by_InvoiceId_InvoiceCFDetailKind", args);
        
        }

        #endregion
    }
}
