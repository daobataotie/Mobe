//------------------------------------------------------------------------------
//
// file name:InvoiceCOAccessor.cs
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
    /// Data accessor of InvoiceCO
    /// </summary>
    public partial class InvoiceCOAccessor : EntityAccessor, IInvoiceCOAccessor
    {
        #region IInvoiceCOAccessor 成员


        public IList<Book.Model.InvoiceCO> Select(DateTime start, DateTime end)
        {
            Hashtable pars = new Hashtable();
            pars.Add("startTime", start);
            pars.Add("endTime", end);
            return sqlmapper.QueryForList<Model.InvoiceCO>("InvoiceCO.select_byTime", pars); ;
        }

        #endregion

        #region IInvoiceCOAccessor 成员


        public IList<Book.Model.InvoiceCO> Select(Helper.InvoiceStatus status)
        {
            return sqlmapper.QueryForList<Model.InvoiceCO>("InvoiceCO.select_byStatus", (int)status);
        }
        public IList<Book.Model.InvoiceCO> Select(Model.Supplier supplier)
        {
            return sqlmapper.QueryForList<Model.InvoiceCO>("InvoiceCO.select_bySupplier", supplier.SupplierId);
        }
        public void Updates(Model.InvoiceCO invoiceCO)
        {
            this.Update<Model.InvoiceCO>(invoiceCO);
        }

        public IList<Model.InvoiceCO> SelectbySupplierAndinvoiceId(Model.Supplier supplier, string invoiceId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("supperId", supplier == null ? null : supplier.SupplierId);
            ht.Add("invoiceId", invoiceId == "" ? null : invoiceId);
            return sqlmapper.QueryForList<Model.InvoiceCO>("InvoiceCO.selectbySupplierAndinvoiceId", ht);
        }

        public IList<Model.InvoiceCO> SelectByMrsHeaderId(string MrsHeaderId)
        {
            return sqlmapper.QueryForList<Model.InvoiceCO>("InvoiceCO.selectByMrsHeaderId", MrsHeaderId);
        }
        #endregion
    }
}
