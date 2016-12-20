//------------------------------------------------------------------------------
//
// file name:RoleAccessor.cs
// author: peidun
// create date:2008/6/6 10:00:51
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
    /// Data accessor of Role
    /// </summary>
    public partial class RoleAccessor : EntityAccessor, IRoleAccessor
    {
        public IList<Book.Model.Role> Select(string pid)
        {
            return sqlmapper.QueryForList<Model.Role>("Role.select_byRole", pid);
        }
        //表名 和审核级别查询 角色
        public Book.Model.Role select_byAuditRandTableName(int auditRank, string tableName)
        {
            Hashtable ht = new Hashtable();
            ht.Add("auditRank", auditRank);       
            ht.Add("tableName", tableName);
            return sqlmapper.QueryForObject<Model.Role>("Role.select_byAuditRandTableName", ht);
        }
        
    }
}
