//------------------------------------------------------------------------------
//
// file name：PCEarPressCheckAccessor.cs
// author: mayanjun
// create date：2013-08-23 16:50:38
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
    /// Data accessor of PCEarPressCheck
    /// </summary>
    public partial class PCEarPressCheckAccessor : EntityAccessor, IPCEarPressCheckAccessor
    {
        public IList<Book.Model.PCEarPressCheck> SelectByDateRage(DateTime StartDate, DateTime EndDate,bool IsReport)
        {
            Hashtable ht = new Hashtable();
            ht.Add("StartDate", StartDate);
            ht.Add("EndDate", EndDate);
            ht.Add("IsReport", IsReport);
            return sqlmapper.QueryForList<Model.PCEarPressCheck>("PCEarPressCheck.SelectByDateRage", ht);
        }

        public bool mHasRows(bool IsReport)
        {
            return sqlmapper.QueryForObject<bool>("PCEarPressCheck.mhas_rows", IsReport);
        }

        public bool mHasRowsBefore(Model.PCEarPressCheck e)
        {
            return sqlmapper.QueryForObject<bool>("PCEarPressCheck.mhas_rows_before", e);
        }

        public bool mHasRowsAfter(Model.PCEarPressCheck e)
        {
            return sqlmapper.QueryForObject<bool>("PCEarPressCheck.mhas_rows_after", e);
        }

        public Model.PCEarPressCheck mGetFirst(bool IsReport)
        {
            return sqlmapper.QueryForObject<Model.PCEarPressCheck>("PCEarPressCheck.mget_first", IsReport);
        }

        public Model.PCEarPressCheck mGetLast(bool IsReport)
        {
            return sqlmapper.QueryForObject<Model.PCEarPressCheck>("PCEarPressCheck.mget_last", IsReport);
        }

        public Model.PCEarPressCheck mGetNext(Model.PCEarPressCheck e)
        {
            return sqlmapper.QueryForObject<Model.PCEarPressCheck>("PCEarPressCheck.mget_next", e);
        }

        public Model.PCEarPressCheck mGetPrev(Model.PCEarPressCheck e)
        {
            return sqlmapper.QueryForObject<Model.PCEarPressCheck>("PCEarPressCheck.mget_prev", e);
        }
    }
}
