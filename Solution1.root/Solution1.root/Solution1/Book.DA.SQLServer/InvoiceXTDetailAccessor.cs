//------------------------------------------------------------------------------
//
// file name:InvoiceXTDetailAccessor.cs
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
    /// Data accessor of InvoiceXTDetail
    /// </summary>
    public partial class InvoiceXTDetailAccessor : EntityAccessor, IInvoiceXTDetailAccessor
    {
        #region IInvoiceXTDetailAccessor 成员


        public IList<Book.Model.InvoiceXTDetail> Select(Book.Model.InvoiceXT invoiceXT)
        {
            return sqlmapper.QueryForList<Model.InvoiceXTDetail>("InvoiceXTDetail.select_by_invoiceid", invoiceXT.InvoiceId);
        }

        public void Delete(Book.Model.InvoiceXT invoice)
        {
            sqlmapper.Delete("InvoiceXTDetail.delete_by_invoiceid", invoice.InvoiceId);
        }

        #endregion
    }
}
