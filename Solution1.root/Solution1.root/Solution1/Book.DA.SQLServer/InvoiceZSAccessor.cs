//------------------------------------------------------------------------------
//
// file name:InvoiceZSAccessor.cs
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
    /// Data accessor of InvoiceZS
    /// </summary>
    public partial class InvoiceZSAccessor : EntityAccessor, IInvoiceZSAccessor
    {
        #region IInvoiceZSAccessor 成员


        public IList<Book.Model.InvoiceZS> Select(DateTime start, DateTime end)
        {
            Hashtable pars = new Hashtable();
            pars.Add("startTime", start);
            pars.Add("endTime", end);
            return sqlmapper.QueryForList<Model.InvoiceZS>("InvoiceZS.select_byTime", pars);
        }

        #endregion

        #region IInvoiceZSAccessor 成员


        public IList<Book.Model.InvoiceZS> Select(Helper.InvoiceStatus status)
        {
            return sqlmapper.QueryForList<Model.InvoiceZS>("InvoiceZS.select_byStatus", (int)status);
        }

        #endregion
    }
}
