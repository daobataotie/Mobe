//------------------------------------------------------------------------------
//
// file name：AcbeginbillReceivableAccessor.cs
// author: mayanjun
// create date：2011-6-9 14:42:10
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
    /// Data accessor of AcbeginbillReceivable
    /// </summary>
    public partial class AcbeginbillReceivableAccessor : EntityAccessor, IAcbeginbillReceivableAccessor
    {
        public IList<Model.AcbeginbillReceivable> SelectByDateRange(DateTime startdate, DateTime enddate)
        {
            Hashtable ht = new Hashtable();
            ht.Add("startdate", startdate);
            ht.Add("enddate", enddate);
            return sqlmapper.QueryForList<Model.AcbeginbillReceivable>("AcbeginbillReceivable.selectByDateRange", ht);
        }
    }
}
