//------------------------------------------------------------------------------
//
// file name：PCEarProtectCheckAccessor.cs
// author: mayanjun
// create date：2013-09-03 15:16:59
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
    /// Data accessor of PCEarProtectCheck
    /// </summary>
    public partial class PCEarProtectCheckAccessor : EntityAccessor, IPCEarProtectCheckAccessor
    {
        public IList<Model.PCEarProtectCheck> SelectByDateRage(DateTime StartDate, DateTime EndDate, bool IsReport)
        {
            Hashtable ht = new Hashtable();
            ht.Add("StartDate", StartDate);
            ht.Add("EndDate", EndDate);
            ht.Add("IsReport", IsReport);
            return sqlmapper.QueryForList<Model.PCEarProtectCheck>("PCEarProtectCheck.SelectByDateRage", ht);
        }

        public bool mHasRows(bool IsReport)
        {
            return sqlmapper.QueryForObject<bool>("PCEarProtectCheck.mhas_rows", IsReport);
        }

        public bool mHasRowsBefore(Model.PCEarProtectCheck e)
        {
            return sqlmapper.QueryForObject<bool>("PCEarProtectCheck.mhas_rows_before", e);
        }

        public bool mHasRowsAfter(Model.PCEarProtectCheck e)
        {
            return sqlmapper.QueryForObject<bool>("PCEarProtectCheck.mhas_rows_after", e);
        }

        public Model.PCEarProtectCheck mGetFirst(bool IsReport)
        {
            return sqlmapper.QueryForObject<Model.PCEarProtectCheck>("PCEarProtectCheck.mget_first", IsReport);
        }

        public Model.PCEarProtectCheck mGetLast(bool IsReport)
        {
            return sqlmapper.QueryForObject<Model.PCEarProtectCheck>("PCEarProtectCheck.mget_last", IsReport);
        }

        public Model.PCEarProtectCheck mGetNext(Model.PCEarProtectCheck e)
        {
            return sqlmapper.QueryForObject<Model.PCEarProtectCheck>("PCEarProtectCheck.mget_next", e);
        }

        public Model.PCEarProtectCheck mGetPrev(Model.PCEarProtectCheck e)
        {
            return sqlmapper.QueryForObject<Model.PCEarProtectCheck>("PCEarProtectCheck.mget_prev", e);
        }
    }
}
