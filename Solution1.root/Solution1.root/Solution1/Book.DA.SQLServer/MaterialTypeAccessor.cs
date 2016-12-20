//------------------------------------------------------------------------------
//
// file name：MaterialTypeAccessor.cs
// author: peidun
// create date：2009-12-2 16:19:27
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
    /// Data accessor of MaterialType
    /// </summary>
    public partial class MaterialTypeAccessor : EntityAccessor, IMaterialTypeAccessor
    {
        public bool ExistsName(string name, string id)
        {
            Hashtable ht = new Hashtable();
            ht.Add("name",name);
            ht.Add("id",id);
            return sqlmapper.QueryForObject<bool>("MaterialType.ExistsName",ht);
        }

    }
}
