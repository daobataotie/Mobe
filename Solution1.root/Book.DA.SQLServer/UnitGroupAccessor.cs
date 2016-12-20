//------------------------------------------------------------------------------
//
// file name：UnitGroupAccessor.cs
// author: peidun
// create date：2009-08-03 9:37:28
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
    /// Data accessor of UnitGroup
    /// </summary>
    public partial class UnitGroupAccessor : EntityAccessor, IUnitGroupAccessor
    {
        public bool existsInsertName(string name)
        {
            return sqlmapper.QueryForObject<bool>("UnitGroup.existsInsertName", name);
        }
        public bool existsUpdateName(string name,string id)
        {
            Hashtable ht = new Hashtable();
            ht.Add("UnitGroupName", name);
            ht.Add("UnitGroupId", id);
            return sqlmapper.QueryForObject<bool>("UnitGroup.existsUpdateName", ht);
        }
            
    }
}
