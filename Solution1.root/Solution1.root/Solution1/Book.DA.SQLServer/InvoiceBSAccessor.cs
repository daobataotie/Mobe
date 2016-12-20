//------------------------------------------------------------------------------
//
// file name:InvoiceBSAccessor.cs
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
    /// Data accessor of InvoiceBS
    /// </summary>
    public partial class InvoiceBSAccessor : EntityAccessor, IInvoiceBSAccessor
    {
        #region IInvoiceBSAccessor 成员


        public IList<Book.Model.InvoiceBS> Select(DateTime start, DateTime end)
        {
            Hashtable pars = new Hashtable();
            pars.Add("startTime", start);
            pars.Add("endTime", end);
            return sqlmapper.QueryForList<Model.InvoiceBS>("InvoiceBS.select_byTime", pars);
        }

        #endregion

        #region IInvoiceBSAccessor 成员


        public IList<Book.Model.InvoiceBS> Select(Helper.InvoiceStatus status)
        {
            return sqlmapper.QueryForList<Model.InvoiceBS>("InvoiceBS.select_byStatus", (int)status);
        }

        #endregion
    }
}
