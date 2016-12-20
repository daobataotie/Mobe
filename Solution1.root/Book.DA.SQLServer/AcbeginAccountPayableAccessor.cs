//------------------------------------------------------------------------------
//
// file name：AcbeginAccountPayableAccessor.cs
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
    /// Data accessor of AcbeginAccountPayable
    /// </summary>
    public partial class AcbeginAccountPayableAccessor : EntityAccessor, IAcbeginAccountPayableAccessor
    {
        public IList<Model.AcbeginAccountPayable> SelectByDateRange(DateTime startdate, DateTime enddate)
        {
            Hashtable ht = new Hashtable();
            ht.Add("startdate", startdate);
            ht.Add("enddate", enddate);
            return sqlmapper.QueryForList<Model.AcbeginAccountPayable>("AcbeginAccountPayable.selectByDateRange", ht);
        }
    }
}
