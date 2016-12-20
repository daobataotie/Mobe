//------------------------------------------------------------------------------
//
// file name：ConveyanceMethodAccessor.cs
// author: mayanjun
// create date：2010-8-9 15:00:26
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
    /// Data accessor of ConveyanceMethod
    /// </summary>
    public partial class ConveyanceMethodAccessor : EntityAccessor, IConveyanceMethodAccessor
    {
        public bool IsExists(Model.ConveyanceMethod convery)
        {
            Hashtable ht = new Hashtable();
            ht.Add("id", convery.ConveyanceMethodId);
            ht.Add("name", convery.ConveyanceMethodName);
            return sqlmapper.QueryForObject<bool>("ConveyanceMethod.IsExistConveyanceMethod", ht);
        }
    }
}
