//------------------------------------------------------------------------------
//
// file name:InvoiceCODetailAccessor.cs
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
    /// Data accessor of InvoiceCODetail
    /// </summary>
    public partial class InvoiceCODetailAccessor : EntityAccessor, IInvoiceCODetailAccessor
    {
        #region IInvoiceCODetailAccessor 成员


        public IList<Book.Model.InvoiceCODetail> Select(Book.Model.InvoiceCO invoice)
        {
            return sqlmapper.QueryForList<Model.InvoiceCODetail>("InvoiceCODetail.select_by_invoiceid", invoice.InvoiceId);
        }

        public void Delete(Book.Model.InvoiceCO invoice)
        {
            sqlmapper.Delete("InvoiceCODetail.delete_by_invoiceid", invoice.InvoiceId);
        }

        public IList<Model.InvoiceCODetail> SelectByDateRangeAndPid(string pid, DateTime startdate, DateTime enddate)
        {
            Hashtable ht = new Hashtable();
            ht.Add("pid", pid);
            ht.Add("startdate", startdate);
            ht.Add("enddate", enddate);
            return sqlmapper.QueryForList<Model.InvoiceCODetail>("InvoiceCODetail.SelectByDateRangeAndPid", ht);
        }

        #endregion
    }
}
