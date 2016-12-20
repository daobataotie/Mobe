//------------------------------------------------------------------------------
//
// file name：InvoiceJCDetailAccessor.cs
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
    /// Data accessor of InvoiceJCDetail
    /// </summary>
    public partial class InvoiceJCDetailAccessor : EntityAccessor, IInvoiceJCDetailAccessor
    {
        #region IInvoiceJCDetailAccessor 成员

        public IList<Book.Model.InvoiceJCDetail> Select(Book.Model.InvoiceJC invoice)
        {
            return sqlmapper.QueryForList<Model.InvoiceJCDetail>("InvoiceJCDetail.select_by_invoiceid", invoice.InvoiceId);
        }

        public void Delete(Book.Model.InvoiceJC invoice)
        {
            sqlmapper.Delete("InvoiceJCDetail.delete_by_invoiceid", invoice.InvoiceId);
        }

        public IList<Book.Model.InvoiceJCDetail> Select(Book.Model.Supplier supper)
        {
            return sqlmapper.QueryForList<Model.InvoiceJCDetail>("InvoiceJCDetail.select_by_companyid_insert", supper.SupplierId);
        }

        #endregion

        #region IInvoiceJCDetailAccessor 成员


        public IList<Book.Model.InvoiceJCDetail> Select(Book.Model.InvoiceHR invoice)
        {
            Hashtable table = new Hashtable();
            table.Add("invoiceid", invoice.InvoiceId);
            table.Add("companyid", invoice.CustomerId);
            return sqlmapper.QueryForList<Model.InvoiceJCDetail>("InvoiceJCDetail.select_by_companyid_update", table);
        }

        #endregion
    }
}
