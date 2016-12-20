//------------------------------------------------------------------------------
//
// file name:InvoiceCFAccessor.cs
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
    /// Data accessor of InvoiceCF
    /// </summary>
    public partial class InvoiceCFAccessor : EntityAccessor, IInvoiceCFAccessor
    {
        #region IInvoiceCFAccessor 成员


        public IList<Book.Model.InvoiceCF> Select(DateTime start, DateTime end)
        {
            Hashtable pars = new Hashtable();
            pars.Add("startTime", start);
            pars.Add("endTime", end);
            return sqlmapper.QueryForList<Model.InvoiceCF>("InvoiceCF.select_byTime", pars);
        }

        #endregion

        #region IInvoiceCFAccessor 成员


        public IList<Book.Model.InvoiceCF> Select(Helper.InvoiceStatus status)
        {
            return sqlmapper.QueryForList<Model.InvoiceCF>("InvoiceCF.select_byStatus", (int)status);
        }

        #endregion
    }
}
