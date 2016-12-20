//------------------------------------------------------------------------------
//
// file name：IFamilyMembersAccessor.cs
// author: peidun
// create date：2009-09-02 上午 10:38:13
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.FamilyMembers
    /// </summary>
    public partial interface IFamilyMembersAccessor : IAccessor
    {
        IList<Book.Model.FamilyMembers> Select(Book.Model.Employee emp);
        void DeleteByEmployeeId(string Employeeid);
    }
}

