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
    }
}
