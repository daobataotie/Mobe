//------------------------------------------------------------------------------
//
// file name:InvoiceXJAccessor.cs
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
    /// Data accessor of InvoiceXJ
    /// </summary>
    public partial class InvoiceXJAccessor : EntityAccessor, IInvoiceXJAccessor
    {
        #region IInvoiceXJAccessor 成员


        public IList<Book.Model.InvoiceXJ> Select(DateTime start, DateTime end)
        {
            Hashtable pars = new Hashtable();
            pars.Add("startTime", start);
            pars.Add("endTime", end);
            return sqlmapper.QueryForList<Model.InvoiceXJ>("InvoiceXJ.select_byTime", pars);
        }

        #endregion

        #region IInvoiceXJAccessor 成员


        public IList<Book.Model.InvoiceXJ> Select(Helper.InvoiceStatus status)
        {
            return sqlmapper.QueryForList<Model.InvoiceXJ>("InvoiceXJ.select_byStatus", (int)status);
        }

        #endregion
    }
}
