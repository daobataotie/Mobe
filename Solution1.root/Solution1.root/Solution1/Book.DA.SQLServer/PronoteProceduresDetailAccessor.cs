//------------------------------------------------------------------------------
//
// file name：PronoteProceduresDetailAccessor.cs
// author: mayanjun
// create date：2010-9-16 15:57:12
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
    /// Data accessor of PronoteProceduresDetail
    /// </summary>
    public partial class PronoteProceduresDetailAccessor : EntityAccessor, IPronoteProceduresDetailAccessor
    {
        public IList<Model.PronoteProceduresDetail> GetPronotedetailsMaterialByHeaderId(Model.PronoteHeader pro)
        {
            return sqlmapper.QueryForList<Model.PronoteProceduresDetail>("PronoteProceduresDetail.select_PronoteProceduresDetailByHeaderid", pro.PronoteHeaderID);
        }

        public IList<Model.PronoteProceduresDetail> SelectByProceduresId(string proceduresId)
        {
            return sqlmapper.QueryForList<Model.PronoteProceduresDetail>("PronoteProceduresDetail.selectByProceduresId", proceduresId);
        }
        public IList<Model.PronoteProceduresDetail> SelectByDateRange(DateTime startdate, DateTime enddate)
        {
            Hashtable ht = new Hashtable();
            ht.Add("startdate", startdate);
            ht.Add("enddate", enddate);
            return sqlmapper.QueryForList<Model.PronoteProceduresDetail>("PronoteProceduresDetail.selectByDateRange", ht);
        }

    }
}
