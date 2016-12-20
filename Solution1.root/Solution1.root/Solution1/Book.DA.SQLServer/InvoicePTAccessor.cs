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


        public IList<Book.Model.InvoicePT> Select(DateTime start, DateTime end)
        {
            Hashtable pars = new Hashtable();
            pars.Add("startTime", start);
            pars.Add("endTime", end);
            return sqlmapper.QueryForList<Model.InvoicePT>("InvoicePT.select_byTime", pars);
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
