//------------------------------------------------------------------------------
//
// file name：BGHandbookRangeAccessor.cs
// author: mayanjun
// create date：2013-4-17 15:13:04
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
    /// Data accessor of BGHandbookRange
    /// </summary>
    public partial class BGHandbookRangeAccessor : EntityAccessor, IBGHandbookRangeAccessor
    {
        public IList<Model.BGHandbookRange> SelectByDate(DateTime startDate, DateTime endDate)
        {
            Hashtable ht = new Hashtable();
            ht.Add("StartDate", startDate);
            ht.Add("EndDate", endDate);

            return sqlmapper.QueryForList<Model.BGHandbookRange>("BGHandbookRange.SelectByDate", ht);
        }
    }
}
