//------------------------------------------------------------------------------
//
// file name：IRoleAccessor.cs
// author: peidun
// create date：2008/6/6 10:00:48
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.Role
    /// </summary>
    public partial interface IRoleAccessor : IEntityAccessor
    {
        IList<Book.Model.Role> Select(string pid);
        Book.Model.Role select_byAuditRandTableName(int auditRank, string tableName);
    }
}

