//------------------------------------------------------------------------------
//
// file name：ProduceOtherInDepotAccessor.cs
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
    /// Data accessor of ProduceOtherInDepot
    /// </summary>
    public partial class ProduceOtherInDepotAccessor : EntityAccessor, IProduceOtherInDepotAccessor
    {
        public IList<Model.ProduceOtherInDepot> SelectByCondition(DateTime startdate, DateTime enddate, string supperId1, string supperId2, string ProduceOtherCompactId1, string ProduceOtherCompactId2)
        {
            Hashtable ht = new Hashtable();
            ht.Add("startdate", startdate);
            ht.Add("enddate", enddate);
            ht.Add("sid1", supperId1);
            ht.Add("sid2", supperId2);
            ht.Add("cid1", ProduceOtherCompactId1);
            ht.Add("cid2", ProduceOtherCompactId2);
            return sqlmapper.QueryForList<Model.ProduceOtherInDepot>("ProduceOtherInDepot.selectByCondition", ht);
        }

    }
}
