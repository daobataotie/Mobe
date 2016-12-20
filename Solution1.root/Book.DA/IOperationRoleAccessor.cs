//------------------------------------------------------------------------------
//
// file name：IOperationRoleAccessor.cs
// author: peidun
// create date：2009-09-17 上午 11:49:40
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.OperationRole
    /// </summary>
    public partial interface IOperationRoleAccessor : IAccessor
    {
        void Delete(Model.Operators operators);
        IList<Model.OperationRole> Select(Model.Operators operators);

        IList<Book.Model.Role> SelectRole(Book.Model.Operators operators);
        void DeleteByRoleId(string RoleId);
    }
}

