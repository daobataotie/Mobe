//------------------------------------------------------------------------------
//
// file name:InvoiceHZDetailAccessor.cs
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
    /// Data accessor of InvoiceHZDetail
    /// </summary>
    public partial class InvoiceHZDetailAccessor : EntityAccessor, IInvoiceHZDetailAccessor
    {
        #region IInvoiceHZAccessor 成员


        public IList<Book.Model.InvoiceHZDetail> Select(Book.Model.InvoiceHZ invoice)
        {
            return sqlmapper.QueryForList<Model.InvoiceHZDetail>("InvoiceHZDetail.select_by_invoiceid", invoice.InvoiceId);
        }

        public void Delete(Book.Model.InvoiceHZ invoice)
        {
            sqlmapper.Delete("InvoiceHZDetail.delete_by_invoiceid", invoice.InvoiceId);
        }

        #endregion
    }
}
