//------------------------------------------------------------------------------
//
// file name：PCClarityCheckAccessor.cs
// author: mayanjun
// create date：2013-08-19 15:44:12
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
    /// Data accessor of PCClarityCheck
    /// </summary>
    public partial class PCClarityCheckAccessor : EntityAccessor, IPCClarityCheckAccessor
    {
        public IList<Model.PCClarityCheck> SelectByDateRage(DateTime StartDate, DateTime EndDate)
        {
            Hashtable ht = new Hashtable();
            ht.Add("StartDate", StartDate);
            ht.Add("EndDate", EndDate);
            return sqlmapper.QueryForList<Model.PCClarityCheck>("PCClarityCheck.SelectByDateRage", ht);
        }
    }
}
