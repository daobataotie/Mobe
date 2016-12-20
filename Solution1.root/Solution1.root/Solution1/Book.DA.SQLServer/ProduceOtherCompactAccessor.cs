//------------------------------------------------------------------------------
//
// file name：ProduceOtherCompactAccessor.cs
// author: peidun
// create date：2010-1-4 15:32:39
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
    /// Data accessor of ProduceOtherCompact
    /// </summary>
    public partial class ProduceOtherCompactAccessor : EntityAccessor, IProduceOtherCompactAccessor
    {

        public IList<Model.ProduceOtherCompact> SelectIsInDepot()
        {
            return sqlmapper.QueryForList<Model.ProduceOtherCompact>("ProduceOtherCompact.selectIsInDepot", null);
        }

        public IList<Model.ProduceOtherCompact> SelectIsInDepotMaterial()
        {
            return sqlmapper.QueryForList<Model.ProduceOtherCompact>("ProduceOtherCompact.selectIsInDepotMaterial", null);
        }

        public IList<Model.ProduceOtherCompact> SelectByMRSHeaderId(string MrsHeaderId)
        {
            return sqlmapper.QueryForList<Model.ProduceOtherCompact>("ProduceOtherCompact.selectByMRSHeaderId", MrsHeaderId);
        }
        public IList<Book.Model.ProduceOtherCompact>SelectThreeMonth()
        {
            return sqlmapper.QueryForList<Model.ProduceOtherCompact>("ProduceOtherCompact.Select_ThreeMaths", null);
        }
        public IList<Book.Model.ProduceOtherCompact> GetByDate(DateTime startDate, DateTime endDate)
        {
            Hashtable ht = new Hashtable();

            ht.Add("startdate", startDate);
            ht.Add("enddate", endDate);
            return sqlmapper.QueryForList<Model.ProduceOtherCompact>("ProduceOtherCompact.select_GetToDate", ht);
        }
    }
}
