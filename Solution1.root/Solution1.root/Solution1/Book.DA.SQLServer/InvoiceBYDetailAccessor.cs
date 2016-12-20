//------------------------------------------------------------------------------
//
// file name:InvoiceBYDetailAccessor.cs
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
    /// Data accessor of InvoiceBYDetail
    /// </summary>
    public partial class InvoiceBYDetailAccessor : EntityAccessor, IInvoiceBYDetailAccessor
    {
        #region IInvoiceBYDetailAccessor 成员


        public IList<Book.Model.InvoiceBYDetail> Select(Book.Model.InvoiceBY invoice)
        {
            
            return sqlmapper.QueryForList<Model.InvoiceBYDetail>("InvoiceBYDetail.select_by_invoiceid", invoice.InvoiceId);
        }

        public void Delete(Book.Model.InvoiceBY invoice)
        {
            sqlmapper.Delete("InvoiceBYDetail.delete_by_invoiceid", invoice.InvoiceId);
        }

        #endregion
    }
}
