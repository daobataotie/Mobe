//------------------------------------------------------------------------------
//
// file name：PronotedetailsAccessor.cs
// author: peidun
// create date：2009-12-29 11:58:39
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
    /// Data accessor of Pronotedetails
    /// </summary>
    public partial class PronotedetailsAccessor : EntityAccessor, IPronotedetailsAccessor
    {
        public  IList<Book.Model.Pronotedetails> Select(Model.PronoteHeader pronoteHeader)
        {
            return sqlmapper.QueryForList<Model.Pronotedetails>("Pronotedetails.select_bypronoteHeaderId", pronoteHeader.PronoteHeaderID);
        }
        public double GetByMPSdetail(string mPSDetailId)
        {
            return sqlmapper.QueryForObject<double>("Pronotedetails.select_byMPSdetail", mPSDetailId);
        }
        public IList<Book.Model.Pronotedetails> Select(string customerStart, string customerEnd, DateTime dateStart, DateTime dateEnd)
        {
            Hashtable ht = new Hashtable();
            ht.Add("startcustomerid", customerStart);
            ht.Add("endcustomerid", customerEnd);
            ht.Add("startdate", dateStart);
            ht.Add("enddate", dateEnd);
            return sqlmapper.QueryForList<Book.Model.Pronotedetails>("Pronotedetails.select_byCustomerANDdate", ht);
        }
    }
}
