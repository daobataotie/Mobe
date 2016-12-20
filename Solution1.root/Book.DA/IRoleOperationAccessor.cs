//------------------------------------------------------------------------------
//
// file name：IRoleOperationAccessor.cs
// author: peidun
// create date：2008/6/6 10:00:48
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.RoleOperation
    /// </summary>
    public partial interface IRoleOperationAccessor : IEntityAccessor
    {
        IList<Model.RoleOperation> Select(Model.Role role);
        DataSet SelectByType(string parenId, string roleId);
        int UpdateTable(DataSet ds, string roleid);
        IList<Book.Model.RoleOperation> SelectIsSearch(Book.Model.Operators operations);
        IList<Book.Model.RoleOperation> SelectbyOperatorsKeyTag(Book.Model.Operators operations, string keytag);
        string GetbyTable(string tableName);
        void DeleteByRoleId(string RoleId);
    }
}

