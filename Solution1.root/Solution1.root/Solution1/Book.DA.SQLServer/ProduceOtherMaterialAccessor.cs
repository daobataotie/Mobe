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
        public IList<Model.ProduceOtherMaterial> SelectByCondition(DateTime startdate, DateTime enddate, string supperId1, string supperId2, string cid1, string cid2)
        {
            Hashtable ht = new Hashtable();
            ht.Add("startdate",startdate);
            ht.Add("enddate",enddate);
            ht.Add("sid1", supperId1);
            ht.Add("sid2", supperId2);
            ht.Add("cid1", cid1);
            ht.Add("cid2", cid2);
            return sqlmapper.QueryForList<Model.ProduceOtherMaterial>("ProduceOtherMaterial.selectByCondition", ht);
        }
    }
}
