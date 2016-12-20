//------------------------------------------------------------------------------
//
// file name：ProduceOtherMaterialAccessor.cs
// author: peidun
// create date：2010-1-5 15:26:22
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
    /// Data accessor of ProduceOtherMaterial
    /// </summary>
    public partial class ProduceOtherMaterialAccessor : EntityAccessor, IProduceOtherMaterialAccessor
    {
        public IList<Model.ProduceOtherMaterial> SelectState()
        {
            return sqlmapper.QueryForList<Model.ProduceOtherMaterial>("ProduceOtherMaterial.select_byState", null);
        }

        public IList<Model.ProduceOtherMaterial> SelectByDateRange(DateTime startdate,DateTime enddate,bool isColse )
        {
            Hashtable ht = new Hashtable();
            ht.Add("startdate", startdate.ToString("yyyy-MM-dd"));
            ht.Add("enddate", enddate.ToString("yyyy-MM-dd HH:mm:ss"));
            StringBuilder str = new StringBuilder();
            if (isColse)
                str.Append(" and (DepotOutState<>2 or DepotOutState is null)");
            str.Append(" order by produceotherMaterialId desc");
            ht.Add("sql", str.ToString());
            return sqlmapper.QueryForList<Model.ProduceOtherMaterial>("ProduceOtherMaterial.SelectByDateRange", ht);
        }

        public IList<Model.ProduceOtherMaterial> SelectByCondition(DateTime startdate, DateTime enddate, string supperId1, string supperId2, string cid1, string cid2, string StartpId, string EndpId, string invoiceCusID)
        {
            Hashtable ht = new Hashtable();
            ht.Add("startdate",startdate);
            ht.Add("enddate",enddate);
            ht.Add("sid1", supperId1);
            ht.Add("sid2", supperId2);
            ht.Add("cid1", cid1);
            ht.Add("cid2", cid2);
            ht.Add("startpId", StartpId);
            ht.Add("endpId", EndpId);
            ht.Add("CustomerInvoiceXOId", (invoiceCusID == "" ? null : invoiceCusID));
            return sqlmapper.QueryForList<Model.ProduceOtherMaterial>("ProduceOtherMaterial.selectByCondition", ht);
        }

        public bool IsDepotOut(string Id)
        {
            return sqlmapper.QueryForObject<bool>("ProduceOtherMaterial.IsDepotOut", Id);
        }
    }
}
