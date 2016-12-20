//------------------------------------------------------------------------------
//
// file name：InvoicePODetailAccessor.cs
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
    /// Data accessor of InvoicePODetail
    /// </summary>
    public partial class InvoicePODetailAccessor : EntityAccessor, IInvoicePODetailAccessor
    {
        #region IInvoicePODetailAccessor 成员

        public IList<Book.Model.InvoicePODetail> Select(Book.Model.InvoicePO invoice)
        {
            return sqlmapper.QueryForList<Model.InvoicePODetail>("InvoicePODetail.select_by_invoiceid", invoice.InvoiceId);
        }

        public void Delete(Book.Model.InvoicePO invoice)
        {
            sqlmapper.Delete("InvoicePODetail.delete_by_invoiceid", invoice.InvoiceId);
        }

        public IList<Book.Model.InvoicePODetail> Select(Book.Model.Department depart)
        {
            return sqlmapper.QueryForList<Model.InvoicePODetail>("InvoicePODetail.select_by_departmentid_insert", depart.DepartmentId);
        }

        public IList<Book.Model.InvoicePODetail> Select(Book.Model.InvoicePI invoice)
        {
            Hashtable table = new Hashtable();
            table.Add("invoiceid", invoice.InvoiceId);
            table.Add("departmentid", invoice.DepartmentId);
            return sqlmapper.QueryForList<Model.InvoicePODetail>("InvoicePODetail.select_by_departmentid_update",table);
        }

        #endregion
    }
}
