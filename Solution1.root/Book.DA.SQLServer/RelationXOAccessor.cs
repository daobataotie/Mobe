//------------------------------------------------------------------------------
//
// file name：RelationXOAccessor.cs
// author: mayanjun
// create date：2015/4/19 下午 08:06:08
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
    /// Data accessor of RelationXO
    /// </summary>
    public partial class RelationXOAccessor : EntityAccessor, IRelationXOAccessor
    {

        public bool ExistsXO(string CusId,string RelationXOId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("InvoiceCusId",CusId);
            ht.Add("RelationXOId", RelationXOId);
            return sqlmapper.QueryForObject<bool>("RelationXO.ExistsXO", ht);
        }

        public Model.RelationXO SelectByInvoiceXOCusId(string id)
        {
            return sqlmapper.QueryForObject<Model.RelationXO>("RelationXO.SelectByInvoiceXOCusId", id);
        }
    }
}
