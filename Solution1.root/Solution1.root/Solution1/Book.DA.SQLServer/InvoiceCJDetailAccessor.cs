//------------------------------------------------------------------------------
//
// file name:InvoiceCJDetailAccessor.cs
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
    /// Data accessor of InvoiceCJDetail
    /// </summary>
    public partial class InvoiceCJDetailAccessor : EntityAccessor, IInvoiceCJDetailAccessor
    {
        #region IInvoiceCJDetailAccessor 成员


        public IList<Book.Model.InvoiceCJDetail> Select(Book.Model.InvoiceCJ invoiceCJ)
        {
            return sqlmapper.QueryForList<Model.InvoiceCJDetail>("InvoiceCJDetail.select_by_invoiceid", invoiceCJ.InvoiceId);
        }

        public void Delete(Book.Model.InvoiceCJ invoice)
        {
            sqlmapper.Delete("InvoiceCJDetail.delete_by_invoiceid", invoice.InvoiceId);
        }
    

        #endregion
    }
}
