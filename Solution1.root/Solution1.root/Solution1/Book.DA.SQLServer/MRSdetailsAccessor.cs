//------------------------------------------------------------------------------
//
// file name：MRSdetailsAccessor.cs
// author: peidun
// create date：2009-12-18 11:12:41
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
    /// Data accessor of MRSdetails
    /// </summary>
    public partial class MRSdetailsAccessor : EntityAccessor, IMRSdetailsAccessor
    {
        public IList<Book.Model.MRSdetails> Select(Model.MRSHeader mRSheader)
        {
            return sqlmapper.QueryForList<Model.MRSdetails>("MRSdetails.select_byMRSheaderId", mRSheader.MRSHeaderId);
        }

        //public IList<Book.Model.MRSdetails> GetMrsdetailBySourceType(string sourceType)
        //{
        //    return sqlmapper.QueryForList<Model.MRSdetails>("MRSdetails.GetMrsdetailBySourceType", sourceType);
        //}
        public IList<Book.Model.MRSdetails> Select(string mpsHeaderId, string sourceType)
        {
            Hashtable ht = new Hashtable();
            ht.Add("mpsHeaderId", mpsHeaderId);
            ht.Add("sourceType", sourceType);
            return sqlmapper.QueryForList<Model.MRSdetails>("MRSdetails.selectBySourceTypeAndMPS", ht);
        }
        public IList<Book.Model.MRSdetails> GetDate(DateTime startDate, DateTime endDate, string sourceType)
        {
            Hashtable ht = new Hashtable();

            ht.Add("startdate", startDate);
            ht.Add("enddate", endDate);
            ht.Add("SourceType", sourceType);
            return sqlmapper.QueryForList<Model.MRSdetails>("MRSdetails.select_GetDateSourceType", ht);
        }
    }
}
