//------------------------------------------------------------------------------
//
// file name：ProduceOtherExitMaterialAccessor.cs
// author: peidun
// create date：2010-1-6 10:20:17
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
    /// Data accessor of ProduceOtherExitMaterial
    /// </summary>
    public partial class ProduceOtherExitMaterialAccessor : EntityAccessor, IProduceOtherExitMaterialAccessor
    {
        public IList<Model.ProduceOtherExitMaterial> SelectByCondition(DateTime startDate, DateTime endDate, string compactId1,string compactId2,string supperId1,string supperId2,string StartpId,string EndpId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("startdate", startDate);
            ht.Add("enddate", endDate);
            ht.Add("compactId1", compactId1);
            ht.Add("compactId2", compactId2);
            ht.Add("sid1", supperId1);
            ht.Add("sid2", supperId2);
            ht.Add("StartpId", StartpId);
            ht.Add("EndpId", EndpId);
            return sqlmapper.QueryForList<Model.ProduceOtherExitMaterial>("ProduceOtherExitMaterial.selectByCondition", ht);
        }
    }
}
