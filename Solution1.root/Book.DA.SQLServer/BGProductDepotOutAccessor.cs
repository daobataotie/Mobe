//------------------------------------------------------------------------------
//
// file name：BGProductDepotOutAccessor.cs
// author: mayanjun
// create date：2014/3/25 18:18:02
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
    /// Data accessor of BGProductDepotOut
    /// </summary>
    public partial class BGProductDepotOutAccessor : EntityAccessor, IBGProductDepotOutAccessor
    {
        public bool IsExistsDeclareCustomsIdInsert(string DeclareCustomsId)
        {
            return sqlmapper.QueryForObject<bool>("BGProductDepotOut.IsExistsDeclareCustomsIdInsert", DeclareCustomsId);
        }

        public bool IsExistsDeclareCustomsIdUpdate(string DeclareCustomsId, string Id)
        {
            Hashtable ht = new Hashtable();
            ht.Add("DeclareCustomsId", DeclareCustomsId);
            ht.Add("Id", Id);
            return sqlmapper.QueryForObject<bool>("BGProductDepotOut.IsExistsDeclareCustomsIdUpdate", ht);
        }
    }
}
