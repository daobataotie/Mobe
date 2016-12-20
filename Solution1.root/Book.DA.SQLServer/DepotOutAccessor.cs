//------------------------------------------------------------------------------
//
// file name：DepotOutAccessor.cs
// author: mayanjun
// create date：2010-10-15 15:41:09
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
    /// Data accessor of DepotOut
    /// </summary>
    public partial class DepotOutAccessor : EntityAccessor, IDepotOutAccessor
    {
        public IList<Model.DepotOut> SelectByDateRange(DateTime startdate, DateTime enddate)
        {
            Hashtable ht = new Hashtable();
            ht.Add("startdate", startdate);
            ht.Add("enddate", enddate);
            return sqlmapper.QueryForList<Model.DepotOut>("DepotOut.SelectByDateRange", ht);
        }
    }
}
