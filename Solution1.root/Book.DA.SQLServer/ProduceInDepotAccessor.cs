//------------------------------------------------------------------------------
//
// file name：ProduceInDepotAccessor.cs
// author: peidun
// create date：2010-1-8 13:43:37
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
    /// Data accessor of ProduceInDepot
    /// </summary>
    public partial class ProduceInDepotAccessor : EntityAccessor, IProduceInDepotAccessor
    {
        public IList<Book.Model.ProduceInDepot> SelectByDateRange(DateTime stardate, DateTime enddate)
        {
            Hashtable ht = new Hashtable();
            ht.Add("stardate", stardate);
            ht.Add("enddate", enddate);
            return sqlmapper.QueryForList<Model.ProduceInDepot>("ProduceInDepot.SelectByDateRange", ht);
        }
    }
}
