//------------------------------------------------------------------------------
//
// file name：MouldCategoryAccessor.cs
// author: peidun
// create date：2009-07-24 11:18:43
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
    /// Data accessor of MouldCategory
    /// </summary>
    public partial class MouldCategoryAccessor : EntityAccessor, IMouldCategoryAccessor
    {

        public bool IsExistMouldCategoryName(Model.MouldCategory mould)
        {
            Hashtable ht=new Hashtable();
            ht.Add("mid", mould.MouldCategoryId == null ? "" : mould.MouldCategoryId);
            ht.Add("name",mould.MouldCategoryName);
            return sqlmapper.QueryForObject<bool>("MouldCategory.IsExistMouldCategoryName", ht);
        }

        public bool IsExistId(Model.MouldCategory mould)
        {
            Hashtable ht = new Hashtable();
            ht.Add("mid", mould.MouldCategoryId == null ? "" : mould.MouldCategoryId);
            ht.Add("id", mould.Id);
            return sqlmapper.QueryForObject<bool>("MouldCategory.IsExistId", ht);
        }
    }
}
