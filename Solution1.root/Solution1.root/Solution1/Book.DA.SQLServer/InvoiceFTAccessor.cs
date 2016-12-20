//------------------------------------------------------------------------------
//
// file name:InvoiceFTAccessor.cs
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
    /// Data accessor of InvoiceFT
    /// </summary>
    public partial class InvoiceFTAccessor : EntityAccessor, IInvoiceFTAccessor
    {
        #region IInvoiceFTAccessor 成员


        public IList<Book.Model.InvoiceFT> Select(DateTime start, DateTime end)
        {
            Hashtable pars = new Hashtable();
            pars.Add("startTime", start);
            pars.Add("endTime", end);
            return sqlmapper.QueryForList<Model.InvoiceFT>("InvoiceFT.select_byTime", pars);
        }

        #endregion

        #region IInvoiceFTAccessor 成员


        public IList<Book.Model.InvoiceFT> Select(Helper.InvoiceStatus status)
        {
            return sqlmapper.QueryForList<Model.InvoiceFT>("InvoiceFT.select_byStatus", (int)status);
        }

        #endregion
    }
}
