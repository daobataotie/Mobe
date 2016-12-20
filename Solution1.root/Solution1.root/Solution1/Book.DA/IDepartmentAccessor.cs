//------------------------------------------------------------------------------
//
// file name：IDepartmentAccessor.cs
// author: peidun
// create date：2008-11-29 12:15:13
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.Department
    /// </summary>
    public partial interface IDepartmentAccessor : IAccessor
    {
        bool ExistsName(string name, string id);
        void SaveInfo(System.Data.DataTable Deport);
        System.Data.DataTable GetAll();
    }
}

