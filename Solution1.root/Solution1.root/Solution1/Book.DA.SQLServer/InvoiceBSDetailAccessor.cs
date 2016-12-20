//------------------------------------------------------------------------------
//
// file name:InvoiceBSDetailAccessor.cs
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
    /// Data accessor of InvoiceBSDetail
    /// </summary>
    public partial class InvoiceBSDetailAccessor : EntityAccessor, IInvoiceBSDetailAccessor
    {
        #region IInvoiceBSDetailAccessor 成员

        /// <summary>
        /// select by invoice
        /// </summary>
        /// <param name="invoiceBs"></param>
        /// <returns></returns>
        public IList<Book.Model.InvoiceBSDetail> Select(Book.Model.InvoiceBS invoiceBs)
        {
            return sqlmapper.QueryForList<Model.InvoiceBSDetail>("InvoiceBSDetail.select_by_invoiceid", invoiceBs.InvoiceId);
        }

        public void Delete(Book.Model.InvoiceBS invoice)
        {
            sqlmapper.Delete("InvoiceBSDetail.delete_by_invoiceid", invoice.InvoiceId);
        }

        #endregion
    }
}
