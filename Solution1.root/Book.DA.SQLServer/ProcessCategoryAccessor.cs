//------------------------------------------------------------------------------
//
// file name：ProcessCategoryAccessor.cs
// author: peidun
// create date：2009-09-25 下午 05:16:42
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
    /// Data accessor of ProcessCategory
    /// </summary>
    public partial class ProcessCategoryAccessor : EntityAccessor, IProcessCategoryAccessor
    {
        public bool ExistsName(string name,string id)
        {
            Hashtable ht = new Hashtable();
            ht.Add("name", name);
            ht.Add("id",id);
            return sqlmapper.QueryForObject<bool>("ProcessCategory.ExistsName",ht);
        }
    }
}
