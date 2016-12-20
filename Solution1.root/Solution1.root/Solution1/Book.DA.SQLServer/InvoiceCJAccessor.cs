//------------------------------------------------------------------------------
//
// file name:InvoiceCJAccessor.cs
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
    /// Data accessor of InvoiceCJ
    /// </summary>
    public partial class InvoiceCJAccessor : EntityAccessor, IInvoiceCJAccessor
    {
        #region IInvoiceCJAccessor 成员


        public IList<Book.Model.InvoiceCJ> Select(DateTime start, DateTime end)
        {
            Hashtable pars = new Hashtable();
            pars.Add("startTime", start);
            pars.Add("endTime", end);
            return sqlmapper.QueryForList<Model.InvoiceCJ>("InvoiceCJ.select_byTime", pars);
        }

        #endregion

        #region IInvoiceCJAccessor 成员


        public IList<Book.Model.InvoiceCJ> Select(Helper.InvoiceStatus status)
        {
            return sqlmapper.QueryForList<Model.InvoiceCJ>("InvoiceCJ.select_byStatus", (int)status);
        }

        #endregion

    }
}
