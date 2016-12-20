//------------------------------------------------------------------------------
//
// file name:InvoicePTAccessor.cs
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
    /// Data accessor of InvoicePT
    /// </summary>
    public partial class InvoicePTAccessor : EntityAccessor, IInvoicePTAccessor
    {
        #region IInvoicePTAccessor 成员

        public IList<Book.Model.InvoicePT> Select(DateTime startTime, DateTime endTime)
        {
            Hashtable pars = new Hashtable();
            pars.Add("startTime", startTime);
            pars.Add("endTime", endTime);

            return sqlmapper.QueryForList<Model.InvoicePT>("InvoicePT.select_byTime", pars);
        }

        public IList<Book.Model.InvoicePT> Select(DateTime startTime, DateTime endTime, string invoiceId, string employeeId, string depot, string depotIn, string productId)
        {
            Hashtable pars = new Hashtable();
            pars.Add("startTime", startTime);
            pars.Add("endTime", endTime);

            StringBuilder sql = new StringBuilder();
            if (invoiceId != null)
                sql.Append(" and InvoiceId='" + invoiceId + "'");
            if (employeeId != null)
                sql.Append(" and Employee0Id='" + employeeId + "'");
            if (depot != null)
                sql.Append(" and DepotId='" + depot + "'");
            if (depotIn != null)
                sql.Append(" and DepotInId='" + depotIn + "'");
            if (productId != null)
                sql.Append(" and InvoiceId IN (SELECT InvoiceId FROM invoiceptdetail WHERE ProductId='" + productId + "')");
            pars.Add("sql", sql);

            return sqlmapper.QueryForList<Model.InvoicePT>("InvoicePT.selectByTimeAndOther", pars);
        }

        #endregion

        #region IInvoicePTAccessor 成员


        public IList<Book.Model.InvoicePT> Select(Helper.InvoiceStatus status)
        {
            return sqlmapper.QueryForList<Model.InvoicePT>("InvoicePT.select_byStatus", (int)status);
        }

        #endregion
    }
}
