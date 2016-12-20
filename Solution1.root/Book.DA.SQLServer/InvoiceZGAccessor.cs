//------------------------------------------------------------------------------
//
// file name：InvoiceZGAccessor.cs
// author: mayanjun
// create date：2012-11-19 14:13:51
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
    /// Data accessor of InvoiceZG
    /// </summary>
    public partial class InvoiceZGAccessor : EntityAccessor, IInvoiceZGAccessor
    {
        public IList<Model.InvoiceZG> SelectInvoiceZG(DateTime StartDate, DateTime EndDate, Model.Customer XOcustomer, string InvoiceId, string ShippedId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("StartDate", StartDate);
            ht.Add("EndDate", EndDate);

            StringBuilder sql = new StringBuilder();
            if (XOcustomer != null)
                sql.Append(" and XOCustomerId='" + XOcustomer.CustomerId + "'");
            if (InvoiceId != null)
                sql.Append(" and InvoiceZGId='" + InvoiceId + "'");
            if (ShippedId != null)
                sql.Append(" and ShippedBy='" + ShippedId + "'");
            ht.Add("sql", sql);
            return sqlmapper.QueryForList<Model.InvoiceZG>("InvoiceZG.SelectInvoiceZG", ht);
        }
    }
}
