//------------------------------------------------------------------------------
//
// file name：OpticsTestAccessor.cs
// author: mayanjun
// create date：2012-4-21 09:55:32
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
    /// Data accessor of OpticsTest
    /// </summary>
    public partial class OpticsTestAccessor : EntityAccessor, IOpticsTestAccessor
    {
        public Book.Model.OpticsTest mGetFirst(string PCPGOnlineCheckDetailId)
        {
            return sqlmapper.QueryForObject<Model.OpticsTest>("OpticsTest.mGetFirst", PCPGOnlineCheckDetailId);
        }

        public Book.Model.OpticsTest mGetLast(string PCPGOnlineCheckDetailId)
        {
            return sqlmapper.QueryForObject<Model.OpticsTest>("OpticsTest.mGetLast", PCPGOnlineCheckDetailId);
        }

        public Book.Model.OpticsTest mGetPrev(DateTime InsertDate, string PCPGOnlineCheckDetailId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("InsertDate", InsertDate);
            ht.Add("PCPGOnlineCheckDetailId", PCPGOnlineCheckDetailId);
            return sqlmapper.QueryForObject<Model.OpticsTest>("OpticsTest.mGetPrev", ht);
        }

        public Book.Model.OpticsTest mGetNext(DateTime InsertDate, string PCPGOnlineCheckDetailId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("InsertDate", InsertDate);
            ht.Add("PCPGOnlineCheckDetailId", PCPGOnlineCheckDetailId);
            return sqlmapper.QueryForObject<Model.OpticsTest>("OpticsTest.mGetNext", ht);
        }

        public bool mHasRows(string PCPGOnlineCheckDetailId)
        {
            return sqlmapper.QueryForObject<bool>("OpticsTest.mHasRows", PCPGOnlineCheckDetailId);
        }

        public bool mHasRowsBefore(Book.Model.OpticsTest e, string PCPGOnlineCheckDetailId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("InsertTime", e.InsertTime.Value);
            ht.Add("PCPGOnlineCheckDetailId", PCPGOnlineCheckDetailId);
            return sqlmapper.QueryForObject<bool>("OpticsTest.mHasRowsBefore", ht);
        }

        public bool mHasRowsAfter(Book.Model.OpticsTest e, string PCPGOnlineCheckDetailId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("InsertTime", e.InsertTime.Value);
            ht.Add("PCPGOnlineCheckDetailId", PCPGOnlineCheckDetailId);
            return sqlmapper.QueryForObject<bool>("OpticsTest.mHasRowsAfter", ht);
        }

        public IList<Book.Model.OpticsTest> mSelect(string PCPGOnlineCheckDetailId)
        {
            return sqlmapper.QueryForList<Model.OpticsTest>("OpticsTest.mSelect", PCPGOnlineCheckDetailId);
        }

        public IList<Book.Model.OpticsTest> SelectByDateRage(DateTime startdate, DateTime enddate, string PCPGOnlineCheckDetailId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("startdate", startdate.ToString("yyyy-MM-dd HH:mm:ss"));
            ht.Add("enddate", enddate.ToString("yyyy-MM-dd HH:mm:ss"));
            ht.Add("PCPGOnlineCheckDetailId", PCPGOnlineCheckDetailId);

            return sqlmapper.QueryForList<Model.OpticsTest>("OpticsTest.SelectByDateRage", ht);
        }

        public void DeleteByPCPGOnlineCheckDetailId(string PCPGOnlineCheckDetailId)
        {
            sqlmapper.Delete("OpticsTest.DeleteByPCPGOnlineCheckDetailId", PCPGOnlineCheckDetailId);
        }

        public bool ExistsManualId(string OpticsTestid, string ManualId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("OpticsTestid", OpticsTestid);
            ht.Add("ManualId", ManualId);
            return sqlmapper.QueryForObject<bool>("OpticsTest.ExistsManualId", ht);
        }


        #region 为组装成品检验单所加
        public Book.Model.OpticsTest FGetFirst(string PCFinishCheckId)
        {
            return sqlmapper.QueryForObject<Model.OpticsTest>("OpticsTest.FGetFirst", PCFinishCheckId);
        }

        public Book.Model.OpticsTest FGetLast(string PCFinishCheckId)
        {
            return sqlmapper.QueryForObject<Model.OpticsTest>("OpticsTest.FGetLast", PCFinishCheckId);
        }

        public Book.Model.OpticsTest FGetPrev(DateTime InsertDate, string PCFinishCheckId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("InsertDate", InsertDate);
            ht.Add("PCFinishCheckId", PCFinishCheckId);
            return sqlmapper.QueryForObject<Model.OpticsTest>("OpticsTest.FGetPrev", ht);
        }

        public Book.Model.OpticsTest FGetNext(DateTime InsertDate, string PCFinishCheckId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("InsertDate", InsertDate);
            ht.Add("PCFinishCheckId", PCFinishCheckId);
            return sqlmapper.QueryForObject<Model.OpticsTest>("OpticsTest.FGetNext", ht);
        }

        public bool FHasRows(string PCFinishCheckId)
        {
            return sqlmapper.QueryForObject<bool>("OpticsTest.FHasRows", PCFinishCheckId);
        }

        public bool FHasRowsBefore(Book.Model.OpticsTest e, string PCFinishCheckId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("InsertTime", e.InsertTime.Value);
            ht.Add("PCFinishCheckId", PCFinishCheckId);
            return sqlmapper.QueryForObject<bool>("OpticsTest.FHasRowsBefore", ht);
        }

        public bool FHasRowsAfter(Book.Model.OpticsTest e, string PCFinishCheckId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("InsertTime", e.InsertTime.Value);
            ht.Add("PCFinishCheckId", PCFinishCheckId);
            return sqlmapper.QueryForObject<bool>("OpticsTest.FHasRowsAfter", ht);
        }

        public IList<Book.Model.OpticsTest> FSelect(string PCFinishCheckId)
        {
            return sqlmapper.QueryForList<Model.OpticsTest>("OpticsTest.FSelect", PCFinishCheckId);
        }

        public IList<Book.Model.OpticsTest> FSelectByDateRage(DateTime startdate, DateTime enddate, string PCFinishCheckId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("startdate", startdate.ToString("yyyy-MM-dd HH:mm:ss"));
            ht.Add("enddate", enddate.ToString("yyyy-MM-dd HH:mm:ss"));
            ht.Add("PCFinishCheckId", PCFinishCheckId);

            return sqlmapper.QueryForList<Model.OpticsTest>("OpticsTest.FSelectByDateRage", ht);
        } 
        #endregion



        #region 适用于首件上线检查表

        public Book.Model.OpticsTest PFCGetFirst(string PCFirstOnlineCheckDetailId)
        {
            return sqlmapper.QueryForObject<Model.OpticsTest>("OpticsTest.PFCGetFirst", PCFirstOnlineCheckDetailId);
        }

        public Book.Model.OpticsTest PFCGetLast(string PCFirstOnlineCheckDetailId)
        {
            return sqlmapper.QueryForObject<Model.OpticsTest>("OpticsTest.PFCGetLast", PCFirstOnlineCheckDetailId);
        }

        public Book.Model.OpticsTest PFCGetPrev(DateTime InsertDate, string PCFirstOnlineCheckDetailId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("InsertDate", InsertDate);
            ht.Add("PCFirstOnlineCheckDetailId", PCFirstOnlineCheckDetailId);
            return sqlmapper.QueryForObject<Model.OpticsTest>("OpticsTest.PFCGetPrev", ht);
        }

        public Book.Model.OpticsTest PFCGetNext(DateTime InsertDate, string PCFirstOnlineCheckDetailId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("InsertDate", InsertDate);
            ht.Add("PCFirstOnlineCheckDetailId", PCFirstOnlineCheckDetailId);
            return sqlmapper.QueryForObject<Model.OpticsTest>("OpticsTest.PFCGetNext", ht);
        }

        public bool PFCHasRows(string PCFirstOnlineCheckDetailId)
        {
            return sqlmapper.QueryForObject<bool>("OpticsTest.PFCHasRows", PCFirstOnlineCheckDetailId);
        }

        public bool PFCHasRowsBefore(Book.Model.OpticsTest e, string PCFirstOnlineCheckDetailId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("InsertTime", e.InsertTime.Value);
            ht.Add("PCFirstOnlineCheckDetailId", PCFirstOnlineCheckDetailId);
            return sqlmapper.QueryForObject<bool>("OpticsTest.PFCHasRowsBefore", ht);
        }

        public bool PFCHasRowsAfter(Book.Model.OpticsTest e, string PCFirstOnlineCheckDetailId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("InsertTime", e.InsertTime.Value);
            ht.Add("PCFirstOnlineCheckDetailId", PCFirstOnlineCheckDetailId);
            return sqlmapper.QueryForObject<bool>("OpticsTest.PFCHasRowsAfter", ht);
        }

        public IList<Book.Model.OpticsTest> PFCSelect(string PCFirstOnlineCheckDetailId)
        {
            return sqlmapper.QueryForList<Model.OpticsTest>("OpticsTest.PFCSelect", PCFirstOnlineCheckDetailId);
        }

        public IList<Book.Model.OpticsTest> PFCSelectByDateRage(DateTime startdate, DateTime enddate, string PCFirstOnlineCheckDetailId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("startdate", startdate.ToString("yyyy-MM-dd HH:mm:ss"));
            ht.Add("enddate", enddate.ToString("yyyy-MM-dd HH:mm:ss"));
            ht.Add("PCFirstOnlineCheckDetailId", PCFirstOnlineCheckDetailId);

            return sqlmapper.QueryForList<Model.OpticsTest>("OpticsTest.PFCSelectByDateRage", ht);
        } 
        #endregion

    }
}
