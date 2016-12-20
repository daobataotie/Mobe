//------------------------------------------------------------------------------
//
// file name：AtPropertyAccessor.cs
// author: mayanjun
// create date：2010-11-15 10:11:54
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
    /// Data accessor of AtProperty
    /// </summary>
    public partial class AtPropertyAccessor : EntityAccessor, IAtPropertyAccessor
    {
        public IList<Model.AtProperty> Select(string atProperty)
        {
            return sqlmapper.QueryForList<Model.AtProperty>("AtProperty.select_AtPropertyByObject", atProperty);
        }
        public IList<Book.Model.AtProperty> Select(DateTime startDate, DateTime endDate)
        {
            Hashtable ht = new Hashtable();
            ht.Add("startdate", startDate);
            ht.Add("enddate", endDate);
            return sqlmapper.QueryForList<Model.AtProperty>("AtProperty.select_byDdate", ht);
        }
        public IList<Book.Model.AtProperty> SelectByPropertyId(string startPropertyId, string endPropertyId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("startPropertyId", startPropertyId);
            ht.Add("endPropertyId", endPropertyId);
            return sqlmapper.QueryForList<Model.AtProperty>("AtProperty.select_byPropertyId", ht);
        }
    }
}
